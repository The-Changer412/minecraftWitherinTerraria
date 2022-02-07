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
        public static int WitherWhoAmI = 0;
        public static string state = "null";
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

        //make the player will get the far from wither debuff if they are more then 500 pixels away
        public override void PostUpdate()
        {
            if (WitherWhoAmI != 0)
            {
                if (state == "2nd phase")
                {
                    //get the distance between the wither and the player, and also convert it to double
                    float xDis = (Main.npc[WitherWhoAmI].position.X - player.position.X);
                    float yDis = (Main.npc[WitherWhoAmI].position.Y - player.position.Y);
                    decimal xDisDec = new decimal(xDis);
                    double xDisDou = (double)xDisDec;
                    decimal yDisDec = new decimal(yDis);
                    double yDisDou = (double)yDisDec;
                    double dis = Math.Sqrt(Math.Pow(xDisDou, 2) + Math.Pow(yDisDou, 2));

                    //if the wither distance is to far from the player, then give them the debuff
                    if (dis > 500.0)
                    {
                        player.AddBuff(ModContent.BuffType<Buffs.FarFromWitherDebuff>(), 1);
                    }

                }
            }
        }

        public override bool? CanHitNPC(Item item, NPC target)
        {
            if (WitherWhoAmI != 0)
            {
                if (target.whoAmI == WitherWhoAmI)
                {
                    for (int i=0; i<player.buffType.Length; i++)
                    {
                        //check if the player has the honey buff
                        if (player.buffType[i] == ModContent.BuffType<Buffs.FarFromWitherDebuff>())
                        {
                            Talk("I can't hit it.");
                            return false;
                        }
                    }
                }
            }
            Talk("Maybe I can hit it?");
            return null;
        }

        public override bool? CanHitNPCWithProj(Projectile proj, NPC target)
        {
            if (WitherWhoAmI != 0)
            {
                if (target.whoAmI == WitherWhoAmI)
                {
                    for (int i=0; i<player.buffType.Length; i++)
                    {
                        //check if the player has the honey buff
                        if (player.buffType[i] == ModContent.BuffType<Buffs.FarFromWitherDebuff>())
                        {
                            Talk("I can't hit it.");
                            return false;
                        }
                    }
                }
            }
            Talk("Maybe I can hit it?");
            return null;
        }
    }
}
