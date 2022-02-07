using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.Enums;

namespace minecraftWitherinTerraria.NPCs
{

    [AutoloadBossHead]
    public class Wither : ModNPC
    {

        public static int frameStart = 0;
        public static int frameEnd = 0;
        public static float frameTimerMax = 60*5;
        public static float frameTimer = frameTimerMax;
        public static int frameState = 0;
        public static string state = "spawning";
        public static string phase = "move";
        public static float AITimerMax = 0;
        public static float AITimer = 0;
        public static int AICounter = 0;

        //a temporary function that send a message to the chat for debugging purpose.
        static void Talk(string message, int r=150, int g=250, int b=150) {

            //check to see if the world is singleplayer or multiplayer
            if (Main.netMode != NetmodeID.Server) {
                Main.NewText(message, (byte)r, (byte)g, (byte)b);
            }
            else {
                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(message), new Color(r, g, b));
            }
        }

        public override void SetStaticDefaults()
        {
            //set the amount of frames
            Main.npcFrameCount[npc.type] = 20;
        }

        //set the stats of the wither skeleton
        public override void SetDefaults()
        {
            //set the npc stats
            npc.width = 84;
            npc.height = 85;
            npc.lifeMax = 150000;
            npc.life = npc.lifeMax;
            npc.aiStyle = -1;
            npc.damage = 80;
            npc.defense = 10;
            npc.value = 20000f;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[BuffID.Chilled] = true;
            npc.lavaImmune = true;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.boss = true;

            //reset the wither variables
            state = "spawning";
            phase = "move";
            frameStart = 0;
            frameEnd = 1;
            AITimerMax = 50;
            AITimer = AITimerMax;
            AICounter = 0;
        }

        public override void AI()
        {
            //make the wither skeleton shine light at him
            Lighting.AddLight(npc.position, 3, 3, 3);

            //check what state the wither is in
            if (state == "spawning")
            {
                //set the stats for the spawning state
                npc.damage = 0;
                npc.dontTakeDamage = true;
                frameStart = 0;
                frameEnd = 1;
                npc.frameCounter = frameStart;

                if (AICounter == 0)
                {
                    npc.position.Y += 32;
                    AICounter++;
                }

                //slowly grow the wither and tick down the AI timer
                npc.scale += 0.001f;
                AITimer -= 1;

                //switch between the normal sprite and the blue sprite and speed it up
                if (AITimer <= 0)
                {
                    //alter between the frames
                    if (frameState == frameStart)
                    {
                        frameState = frameEnd;
                    }
                    else
                    {
                        frameState = frameStart;
                    }

                    //slowly decrease the timer
                    AITimer = AITimerMax;
                    if (AITimerMax <= 8f)
                    {
                        AITimerMax = 8f;
                    } else
                    {
                        AITimerMax -= 8f;
                    }
                }

                //when the wither hits a certian state, then switch the state to 1st phase
                if (npc.scale >= 1.25f)
                {
                    npc.scale = 1.25f;
                    // state = "1st phase";
                    state = "2nd phase";
                    AICounter = 0;
                }
            }
            else if (state == "1st phase")
            {
                //set the stats for the 1st phase
                npc.dontTakeDamage = false;
                npc.damage = 50;

                frameStart = 2;
                frameEnd = 11;

                //make the wither attack
                AttackAI();
            }
            else if (state == "2nd phase")
            {
                //set the stats for the 2nd phase
                npc.dontTakeDamage = false;
                npc.damage = 80;

                frameStart = 12;
                frameEnd = 20;

                for (int i = 0; i<Main.player.Length; i++)
                {
                    Main.player[i].AddBuff(mod.BuffType("FarFromWitherDebuff"), 1);
                }

                //make the wither attack
                AttackAI();
            }

            //switch to 2nd phase at half hp
            if (npc.life <= npc.lifeMax / 2 && state != "2nd phase")
            {
                state = "2nd phase";
            }
        }

        //the function that will randomly pick the next phase
        public void NextState()
        {
            string[] options = new string[] {"move", "rapid shoot", "circle shoot", "opposite dir"};
            // phase = options[Main.rand.Next(0, options.Length)];
            Talk(phase);
            AICounter = 0;
        }

        //the ai for the wither to attack
        public void AttackAI()
        {
            //set the variables for the 1st phase
            float moveSpd = 0.0175f;
            float hoverDis = 248;

            //make the wither target the closest player
            npc.TargetClosest(true);

            if (phase == "move")
            {
                AITimerMax = 50f;
                //make the wither hover near the player
                npc.position.X = MathHelper.Lerp(npc.position.X, Main.player[npc.target].position.X - (hoverDis*npc.direction), moveSpd);
                npc.position.Y = MathHelper.Lerp(npc.position.Y, Main.player[npc.target].position.Y - hoverDis, moveSpd);

                //make the wither shoot his's head
                if (AITimer < 0)
                {
                    float spd = 2.5f;
                    Projectile.NewProjectile(npc.position.X, npc.position.Y, (Main.player[npc.target].position.X - npc.position.X) * spd, (Main.player[npc.target].position.Y - npc.position.Y) * spd, ModContent.ProjectileType<Projectiles.WitherHeadProjectile>(), (int)(npc.damage*.20f), 0f, Main.myPlayer, npc.whoAmI, Main.rand.Next());
                    AITimer = AITimerMax;
                    AICounter++;
                }

                if (AICounter == 5)
                {
                    NextState();
                }
            } else if (phase == "rapid shoot")
            {
                AITimerMax = 20f;

                //make the wither shoot his's head
                if (AITimer < 0)
                {
                    float spd = 6f;
                    Projectile.NewProjectile(npc.position.X, npc.position.Y, (Main.player[npc.target].position.X - npc.position.X) * spd, (Main.player[npc.target].position.Y - npc.position.Y) * spd, ModContent.ProjectileType<Projectiles.WitherHeadProjectile>(), (int)(npc.damage*.20f), 0f, Main.myPlayer, npc.whoAmI, Main.rand.Next());
                    AITimer = AITimerMax;
                    AICounter++;
                }

                if (AICounter == 3)
                {
                    NextState();
                }
            } else if (phase == "circle shoot")
            {
                AITimerMax = 80f;

                //get the direction to shoot the projectile
                Vector2 dir = new Vector2(0, 0);
                if (AICounter != 0)
                {
                    double rad = (Math.PI / 180) * (AICounter*36);
                    dir = new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad));
                } else
                {
                    dir = new Vector2(1, 0);
                }

                //make the wither shoot his's head
                if (AITimer < 0)
                {
                    AITimerMax = 30;
                    float spd = 12f;
                    dir*=spd;
                    Projectile.NewProjectile(npc.position.X, npc.position.Y, dir.X, dir.Y, ModContent.ProjectileType<Projectiles.WitherHeadProjectile>(), (int)(npc.damage*.20f), 0f, Main.myPlayer, npc.whoAmI, Main.rand.Next());
                    AITimer = AITimerMax;
                    AICounter++;
                }

                if (AICounter == 10)
                {
                    NextState();
                }
            } else if (phase == "opposite dir")
            {
                AITimerMax = 50f;
                //make the wither hover infront of the player
                npc.position.X = MathHelper.Lerp(npc.position.X, Main.player[npc.target].position.X - (hoverDis*-Main.player[npc.target].direction), moveSpd);
                npc.position.Y = MathHelper.Lerp(npc.position.Y, Main.player[npc.target].position.Y - hoverDis, moveSpd);

                //make the wither shoot his's head
                if (AITimer < 0)
                {
                    float spd = 2.5f;
                    Projectile.NewProjectile(npc.position.X, npc.position.Y, (Main.player[npc.target].position.X - npc.position.X) * spd, (Main.player[npc.target].position.Y - npc.position.Y) * spd, ModContent.ProjectileType<Projectiles.WitherHeadProjectile>(), (int)(npc.damage*.20f), 0f, Main.myPlayer, npc.whoAmI, Main.rand.Next());
                    AITimer = AITimerMax;
                    AICounter++;
                }

                if (AICounter == 5)
                {
                    NextState();
                }
            }

            AITimer--;
        }


        //give the player poison when hitted
        public override void OnHitPlayer (Player target, int damage, bool crit)
        {
          if (Main.expertMode == true)
          {
            target.AddBuff(mod.BuffType("WitherDebuff"), 900);
          } else
          {
            target.AddBuff(mod.BuffType("WitherDebuff"), 600);
          }
        }


        //animate the wither
        public override void FindFrame(int frameHeight)
        {
          //set the wither sprite to face the player
            npc.spriteDirection = -npc.direction;


            //animate him normaly if the wither is not in the spawning state
            if (state != "spawning")
            {
                //iter over each frame of the wither
                npc.frameCounter++;
                npc.frame.Y =(int) (npc.frameCounter * npc.height);
                if (npc.frameCounter >= frameEnd)
                {
                    npc.frameCounter = frameStart;
                    npc.frame.Y =(int) (npc.frameCounter * npc.height);
                }
            }
            else
            {
                npc.frame.Y =(int) (frameState * npc.height);
            }
        }

        //create the gore when the wither skeleton die
        public override bool CheckDead()
        {
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WitherSkeletonGoreHead"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WitherSkeletonGoreBody"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WitherSkeletonGoreArm"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WitherSkeletonGoreArm"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WitherSkeletonGoreLeg"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WitherSkeletonGoreLeg"), 1f);
            Talk("The Wither has been defeated!", 143, 61, 209);
            return true;
        }

        //give the skull of the wither skeleton a 5% chance to drop from the wither skeleton and a 100% chance to drop the soul sand
        public override void NPCLoot()
        {
            if (Main.rand.Next(0, 101) <= 5)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<Items.WitherSkeletonSkull>(), 1);
            }
            Item.NewItem(npc.getRect(), ModContent.ItemType<Items.SoulSand>(), 1);
        }
    }
}
