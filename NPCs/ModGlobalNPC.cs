using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ModLoader.IO;
using Terraria.Enums;
using minecraftWitherinTerraria;

namespace minecraftWitherinTerraria.NPCs
{

    class ModGlobalNPC : GlobalNPC
    {
        public override bool CheckDead(NPC npc)
        {
          if (npc.type == NPCID.MoonLordCore && minecraftWitherinTerraria.MinecraftWither.hellMessage == false)
          {
            string message = "By killing the Moon Lord, a new enemy has appeared in the underworld.";

            //check to see if the world is singleplayer or multiplayer
            if (Main.netMode != NetmodeID.Server) {
                //send the message in singleplayer with the color
                Main.NewText(message, (byte)255, (byte)46, (byte)46);
            }
            else {
                //send the message in multiplayer with the color
                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(message), new Color(255, 46, 46));
            }

            minecraftWitherinTerraria.MinecraftWither.hellMessage = true;
          }
          return true;
        }
    }
}
