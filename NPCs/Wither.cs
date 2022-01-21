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

    public class Wither : ModNPC
    {

        public override string Texture => "minecraftWitherinTerraria/NPCs/WitherSpawning";
        public static bool spawning = true;
        public static float frameTimerMax = 60*5;
        public static float frameTimer = frameTimerMax;

        //create the random class
        Random rand = new Random();

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
            Main.npcFrameCount[npc.type] = 9;
        }

        //set the stats of the wither skeleton
        public override void SetDefaults()
        {
            npc.width = 168;
            npc.height = 156;
            npc.lifeMax = 175000;
            npc.life = npc.lifeMax;
            npc.aiStyle = -1;
            npc.damage = 150;
            npc.defense = 30;
            npc.value = 20000f;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[BuffID.Chilled] = true;
            npc.lavaImmune = true;
            npc.knockBackResist = 0f;
            npc.boss = true;

            spawning = true;
        }

        public override void AI()
        {
            //make the wither skeleton shine light at him
            Lighting.AddLight(npc.position, 3, 3, 3);

            if (spawning)
            {
                npc.damage = 0;
                npc.dontTakeDamage = true;
            }
        }

        //give the player poison when hitted
        public override void OnHitPlayer (Player target, int damage, bool crit)
        {
          if (Main.expertMode == true)
          {
            // target.AddBuff(BuffID.Poisoned, 900);
            target.AddBuff(mod.BuffType("WitherDebuff"), 900);
          } else
          {
            // target.AddBuff(BuffID.Poisoned, 600);
            target.AddBuff(mod.BuffType("WitherDebuff"), 600);
          }
        }


        //animate the wither skeleton
        public override void FindFrame(int frameHeight)
        {
          //set the wither skeelton sprite to face the player
            npc.spriteDirection = -npc.direction;

            frameTimer--;
            if (frameTimer <= 0)
            {
                //iter over each frame of the wither skeleton
                npc.frameCounter++;
                npc.frame.Y =(int) (npc.frameCounter * frameHeight);
                if (npc.frameCounter >= Main.npcFrameCount[npc.type])
                {
                    npc.frameCounter = 0;
                    npc.frame.Y =(int) (npc.frameCounter * frameHeight);
                    frameTimer = frameTimerMax;
                }
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
            return true;
        }

        //give the skull of the wither skeleton a 5% chance to drop from the wither skeleton and a 100% chance to drop the soul sand
        public override void NPCLoot()
        {
            if (rand.Next(0, 101) <= 5)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<Items.WitherSkeletonSkull>(), 1);
            }
            Item.NewItem(npc.getRect(), ModContent.ItemType<Items.SoulSand>(), 1);
        }
    }
}
