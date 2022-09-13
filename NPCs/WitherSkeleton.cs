using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.Enums;
using Terraria.ModLoader.Utilities;
using minecraftWitherinTerraria.Gores;

namespace minecraftWitherinTerraria.NPCs
{

    public class WitherSkeleton : ModNPC
    {
        public override void SetStaticDefaults()
        {
            //set the amount of frames
            Main.npcFrameCount[NPC.type] = 10;
        }

        //set the stats of the wither skeleton
        public override void SetDefaults()
        {
            NPC.width = 36;
            NPC.height = 67;
            NPC.lifeMax = 1200;
            NPC.life = NPC.lifeMax;
            NPC.aiStyle = 3;
            NPC.damage = 100;
            NPC.defense = 40;
            NPC.value = 2000f;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.lavaImmune = true;
            NPC.knockBackResist = 0.25f;
        }

        public override void AI()
        {
            //make the wither skeleton target the closest Player
            NPC.TargetClosest(true);

            //make the wither skeleton shine light at him
            Lighting.AddLight(NPC.position, 3, 3, 3);
        }

        //give the Player poison when hitted
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


        //animate the wither skeleton
        public override void FindFrame(int frameHeight)
        {
          //set the wither skeelton sprite to face the Player
            NPC.spriteDirection = -NPC.direction;

            //iter over each frame of the wither skeleton
            NPC.frameCounter++;
            NPC.frame.Y =(int) (NPC.frameCounter * frameHeight);
            if (NPC.frameCounter >= Main.npcFrameCount[NPC.type])
            {
                NPC.frameCounter = 0;
                NPC.frame.Y =(int) (NPC.frameCounter * frameHeight);
            }
        }

        //create the gore when the wither skeleton die
        public override bool CheckDead()
        {
            Gore.NewGore(Player.GetSource_NaturalSpawn(), NPC.position, NPC.velocity, ModContent.GoreType<Gores.WitherSkeletonGoreHead>());
            Gore.NewGore(Player.GetSource_NaturalSpawn(), NPC.position, NPC.velocity, ModContent.GoreType<Gores.WitherSkeletonGoreBody>());
            Gore.NewGore(Player.GetSource_NaturalSpawn(), NPC.position, NPC.velocity, ModContent.GoreType<Gores.WitherSkeletonGoreArm>());
            Gore.NewGore(Player.GetSource_NaturalSpawn(), NPC.position, NPC.velocity, ModContent.GoreType<Gores.WitherSkeletonGoreArm>());
            Gore.NewGore(Player.GetSource_NaturalSpawn(), NPC.position, NPC.velocity, ModContent.GoreType<Gores.WitherSkeletonGoreLeg>());
            Gore.NewGore(Player.GetSource_NaturalSpawn(), NPC.position, NPC.velocity, ModContent.GoreType<Gores.WitherSkeletonGoreLeg>());
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
        public override void OnKill()
        {
            if (Main.rand.Next(0, 101) <= 10)
            {
                Item.NewItem(Player.GetSource_NaturalSpawn(), NPC.position, Vector2.One, ModContent.ItemType<Items.WitherSkeletonSkull>(), 1);
            }
            Item.NewItem(Player.GetSource_NaturalSpawn(), NPC.position, Vector2.One, ModContent.ItemType<Items.SoulSand>(), 1);
        }
    }
}
