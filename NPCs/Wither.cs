using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
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
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(message), new Color(r, g, b));
            }
        }

        public override void SetStaticDefaults()
        {
            //set the amount of frames
            Main.npcFrameCount[NPC.type] = 21;
        }

        //set the stats of the wither skeleton
        public override void SetDefaults()
        {
            //set the npc stats
            NPC.width = 84;
            NPC.height = 85;
            NPC.lifeMax = 150000;
            NPC.life = NPC.lifeMax;
            NPC.aiStyle = -1;
            NPC.damage = 80;
            NPC.defense = 10;
            NPC.value = 20000f;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.lavaImmune = true;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.boss = true;
            Music = MusicID.Boss2;

            //reset the wither variables
            state = "spawning";
            phase = "move";
            frameStart = 0;
            frameEnd = 1;
            AITimerMax = 50;
            AITimer = AITimerMax;
            AICounter = 0;

            SoundEngine.PlaySound(new SoundStyle("minecraftWitherinTerraria/Sounds/wither/spawn"));
        }

        //set the ai for the wither
        public override void AI()
        {
            //tell the player the wither who am i and the state the wither is
            player.WitherWhoAmI = NPC.whoAmI;
            player.state = state;

            //make the wither skeleton shine light at him
            Lighting.AddLight(NPC.position, 3, 3, 3);

            //check what state the wither is in
            if (state == "spawning")
            {
                //set the stats for the spawning state
                NPC.damage = 0;
                NPC.dontTakeDamage = true;
                frameStart = 0;
                frameEnd = 1;
                NPC.frameCounter = frameStart;

                if (AICounter == 0)
                {
                    NPC.position.Y += 32;
                    AICounter++;
                }

                //slowly grow the wither and tick down the AI timer
                NPC.scale += 0.001f;
                AITimer -= 1;

                //switch between tplayerhe normal sprite and the blue sprite and speed it up
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
                if (NPC.scale >= 1.25f)
                {
                    NPC.scale = 1.25f;
                    state = "1st phase";
                    AICounter = 0;
                }
            }
            else if (state == "1st phase")
            {
                //set the stats for the 1st phase
                NPC.dontTakeDamage = false;
                NPC.damage = 50;
                NPC.defense = 10;

                frameStart = 2;
                frameEnd = 11;

                //make the wither attack
                AttackAI();
            }
            else if (state == "2nd phase")
            {
                //set the stats for the 2nd phase
                NPC.dontTakeDamage = false;
                NPC.damage = 80;
                NPC.defense = 20;

                frameStart = 12;
                frameEnd = Main.npcFrameCount[NPC.type];

                //make the wither attack
                AttackAI();
            }

            //switch to 2nd phase at half hp
            if (NPC.life <= NPC.lifeMax / 2 && state != "2nd phase")
            {
                state = "2nd phase";
            }
        }

        //the function that will randomly pick the next phase
        public void NextState()
        {
            string[] options = new string[] {"move", "rapid shoot", "circle shoot", "opposite dir"};
            phase = options[Main.rand.Next(0, options.Length)];
            AICounter = 0;
        }

        //the ai for the wither to attack
        public void AttackAI()
        {
            //set the variables for the 1st phase
            float moveSpd = 0.0175f;
            float hoverDis = 248;

            //make the wither target the closest player
            NPC.TargetClosest(true);

            if (phase == "move")
            {
                AITimerMax = 50f;
                //make the wither hover near the player
                NPC.position.X = MathHelper.Lerp(NPC.position.X, Main.player[NPC.target].position.X - (hoverDis*NPC.direction), moveSpd);
                NPC.position.Y = MathHelper.Lerp(NPC.position.Y, Main.player[NPC.target].position.Y - hoverDis, moveSpd);

                //make the wither shoot his's head
                if (AITimer < 0)
                {
                    float spd = 30f;
                    Vector2 dir  = new Vector2((Main.player[NPC.target].position.X - NPC.position.X)/spd, (Main.player[NPC.target].position.Y - NPC.position.Y)/spd);
                    Projectile.NewProjectile(Player.GetSource_NaturalSpawn(), NPC.position.X, NPC.position.Y,  dir.X, dir.Y, ModContent.ProjectileType<Projectiles.WitherHeadProjectile>(), (int)(NPC.damage*0.50f), 0f, Main.myPlayer, NPC.whoAmI, Main.rand.Next());
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
                    float spd = 30f;
                    Vector2 dir  = new Vector2((Main.player[NPC.target].position.X - NPC.position.X)/spd, (Main.player[NPC.target].position.Y - NPC.position.Y)/spd);
                    Projectile.NewProjectile(Player.GetSource_NaturalSpawn(), NPC.position.X, NPC.position.Y,  dir.X, dir.Y, ModContent.ProjectileType<Projectiles.WitherHeadProjectile>(), (int)(NPC.damage*0.50f), 0f, Main.myPlayer, NPC.whoAmI, Main.rand.Next());
                    AITimer = AITimerMax;
                    AICounter++;
                }

                if (AICounter == 3)
                {
                    NextState();
                }
            } else if (phase == "circle shoot")
            {
                AITimerMax = 30f;

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
                    float spd = 16f;
                    dir*=spd;
                    Projectile.NewProjectile(Player.GetSource_NaturalSpawn(), NPC.position.X, NPC.position.Y, dir.X, dir.Y, ModContent.ProjectileType<Projectiles.WitherHeadProjectile>(), (int)(NPC.damage*0.50f), 0f, Main.myPlayer, NPC.whoAmI, Main.rand.Next());
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
                NPC.position.X = MathHelper.Lerp(NPC.position.X, Main.player[NPC.target].position.X - (hoverDis*-Main.player[NPC.target].direction), moveSpd);
                NPC.position.Y = MathHelper.Lerp(NPC.position.Y, Main.player[NPC.target].position.Y - hoverDis, moveSpd);

                //make the wither shoot his's head
                if (AITimer < 0)
                {
                    float spd = 30f;
                    Vector2 dir  = new Vector2((Main.player[NPC.target].position.X - NPC.position.X)/spd, (Main.player[NPC.target].position.Y - NPC.position.Y)/spd);
                    Projectile.NewProjectile(Player.GetSource_NaturalSpawn(), NPC.position.X, NPC.position.Y,  dir.X, dir.Y, ModContent.ProjectileType<Projectiles.WitherHeadProjectile>(), (int)(NPC.damage*0.50f), 0f, Main.myPlayer, NPC.whoAmI, Main.rand.Next());
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


        //give the player the wither effect when touch by the wither
        public override void OnHitPlayer (Player target, int damage, bool crit)
        {
          if (Main.expertMode == true)
          {
            target.AddBuff(Mod.Find<ModBuff>("WitherDebuff").Type, 900);
          } else
          {
            target.AddBuff(Mod.Find<ModBuff>("WitherDebuff").Type, 600);
          }
        }


        //animate the wither
        public override void FindFrame(int frameHeight)
        {
          //set the wither sprite to face the player
            NPC.spriteDirection = -NPC.direction;


            //animate him normaly if the wither is not in the spawning state
            if (state != "spawning")
            {
                //iter over each frame of the wither
                NPC.frameCounter++;
                NPC.frame.Y =(int) (NPC.frameCounter * NPC.height);
                if (NPC.frameCounter >= frameEnd)
                {
                    NPC.frameCounter = frameStart;
                    NPC.frame.Y =(int) (NPC.frameCounter * NPC.height);
                }
            }
            else
            {
                NPC.frame.Y =(int) (frameState * NPC.height);
            }
        }

        //create the gore when the wither die
        public override bool CheckDead()
        {
            //make the gore
            Gore.NewGore(Player.GetSource_NaturalSpawn(), NPC.position, NPC.velocity, ModContent.GoreType<Gores.WitherGoreHead>());
            Gore.NewGore(Player.GetSource_NaturalSpawn(), NPC.position, NPC.velocity, ModContent.GoreType<Gores.WitherGoreHead>());
            Gore.NewGore(Player.GetSource_NaturalSpawn(), NPC.position, NPC.velocity, ModContent.GoreType<Gores.WitherGoreHead>());
            Gore.NewGore(Player.GetSource_NaturalSpawn(), NPC.position, NPC.velocity, ModContent.GoreType<Gores.WitherGoreNeck>());
            Gore.NewGore(Player.GetSource_NaturalSpawn(), NPC.position, NPC.velocity, ModContent.GoreType<Gores.WitherGoreBody>());
            Gore.NewGore(Player.GetSource_NaturalSpawn(), NPC.position, NPC.velocity, ModContent.GoreType<Gores.WitherGoreTail>());

            //reset the player info
            player.WitherWhoAmI = 0;
            player.state = "null";

            //tell the player that the wither is dead, and play the death sound
            Talk("The Wither has been defeated!", 143, 61, 209);
            SoundEngine.PlaySound(new SoundStyle("minecraftWitherinTerraria/Sounds/wither/death"));

            //set the wither downed to true
            World.DownedWither = true;
            return true;
        }

        //give the skull of the wither skeleton a 5% chance to drop from the wither skeleton and a 100% chance to drop the soul sand
        public override void OnKill()
        {
            Item.NewItem(Player.GetSource_NaturalSpawn(), NPC.position, Vector2.One, ModContent.ItemType<Items.NetherStar>(), 1);
        }
    }
}
