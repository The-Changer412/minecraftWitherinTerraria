using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.Enums;

namespace MinecraftWither.NPCs
{

    public class WitherSkeleton : ModNPC
    {

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
            Main.npcFrameCount[npc.type] = 10;
        }

        //set the stats of the wither skeleton
        public override void SetDefaults()
        {
            npc.width = 36;
            npc.height = 67;
            npc.lifeMax = 1200;
            npc.life = npc.lifeMax;
            npc.aiStyle = 3;
            npc.damage = 100;
            npc.defense = 40;
            npc.value = 2000f;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.lavaImmune = true;
            npc.knockBackResist = 0.25f;
        }

        public override void AI()
        {
            //make the wither skeleton target the closest player
            npc.TargetClosest(true);

            //make the wither skeleton shine light at him
            Lighting.AddLight(npc.position, 3, 3, 3);
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

            //iter over each frame of the wither skeleton
            npc.frameCounter++;
            npc.frame.Y =(int) (npc.frameCounter * frameHeight);
            if (npc.frameCounter >= Main.npcFrameCount[npc.type])
            {
                npc.frameCounter = 0;
                npc.frame.Y =(int) (npc.frameCounter * frameHeight);
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

        //give the wither skeleton a 50% chance to spawn after moon lord has been defeated
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedMoonlord)
            {
                return SpawnCondition.Underworld.Chance * 0.5f;
            } else
            {
                return 0f;
            }
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
