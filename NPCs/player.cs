using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace minecraftWitherinTerraria.NPCs
{
    public class player : ModPlayer
    {
        //set the stats
        public static int WitherWhoAmI = 0;
        public static string state = "null";
        public static bool BeaconActivated = false;
        public static int BeaconX = 0;
        public static int BeaconY = 0;

        //run some code after the update
        public override void PostUpdate()
        {
            //make the Player will get the far from wither debuff if they are more then 500 pixels away
            if (WitherWhoAmI != 0)
            {
                if (state == "2nd phase")
                {
                    //get the distance between the wither and the Player, and also convert it to double
                    float xDis = (Main.npc[WitherWhoAmI].position.X - Player.position.X);
                    float yDis = (Main.npc[WitherWhoAmI].position.Y - Player.position.Y);
                    decimal xDisDec = new decimal(xDis);
                    double xDisDou = (double)xDisDec;
                    decimal yDisDec = new decimal(yDis);
                    double yDisDou = (double)yDisDec;
                    double dis = Math.Sqrt(Math.Pow(xDisDou, 2) + Math.Pow(yDisDou, 2));

                    //if the wither distance is to far from the Player, then give them the debuff
                    if (dis > 500.0)
                    {
                        Player.AddBuff(ModContent.BuffType<Buffs.FarFromWitherDebuff>(), 1);
                    }

                }
            }

            //check if beacon is activated
            if (BeaconActivated)
            {
                //get the distance between the beacon and the Player, and also convert it to double
                float xDis = (BeaconX - Player.position.X/16);
                float yDis = (BeaconY - Player.position.Y/16);
                decimal xDisDec = new decimal(xDis);
                double xDisDou = (double)xDisDec;
                decimal yDisDec = new decimal(yDis);
                double yDisDou = (double)yDisDec;
                double dis = Math.Sqrt(Math.Pow(xDisDou, 2) + Math.Pow(yDisDou, 2));

                //check if the beacon is still there, and if the Player is less then 75 pixels away from it
                if (Main.tile[BeaconX, BeaconY].TileType == ModContent.TileType<Tiles.Beacon>() && dis <= 160)
                {
                    Player.AddBuff(BuffID.Regeneration, 1);
                    Player.AddBuff(BuffID.Swiftness, 1);
                    Player.AddBuff(BuffID.Ironskin, 1);
                    Player.AddBuff(BuffID.Mining, 1);
                    Player.AddBuff(BuffID.Shine, 1);
                    Player.AddBuff(BuffID.NightOwl, 1);
                    Player.AddBuff(BuffID.Calm, 1);
                }
            }
        }

        //make the Player not able to do damage to the wither with the far from wither debuff
        public override bool? CanHitNPC(Item item, NPC target)
        {
            if (WitherWhoAmI != 0)
            {
                if (target.whoAmI == WitherWhoAmI)
                {
                    for (int i=0; i<Player.buffType.Length; i++)
                    {
                        //check if the Player has the far from wither debuff
                        if (Player.buffType[i] == ModContent.BuffType<Buffs.FarFromWitherDebuff>())
                        {
                            return false;
                        }
                    }
                }
            }
            return null;
        }

        //make the Player not able to do damage to the wither with the far from wither debuff
        public override bool? CanHitNPCWithProj(Projectile proj, NPC target)
        {
            if (WitherWhoAmI != 0)
            {
                if (target.whoAmI == WitherWhoAmI)
                {
                    for (int i=0; i<Player.buffType.Length; i++)
                    {
                        //check if the Player has the far from wither debuff
                        if (Player.buffType[i] == ModContent.BuffType<Buffs.FarFromWitherDebuff>())
                        {
                            return false;
                        }
                    }
                }
            }
            return null;
        }
    }
}
