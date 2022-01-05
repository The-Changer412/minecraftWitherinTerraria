using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ModLoader.IO;
using Terraria.Enums;

using MinecraftWither;

namespace ExpertModePlusPlus.NPCs
{

    class ModGlobalNPC : GlobalNPC
    {
        public override bool CheckDead(NPC npc)
        {
          if (npc.type == NPCID.MoonLordCore && MinecraftWither.MinecraftWither.hellMessage == false)
          {
            string message = "The curse, that the moon lord has put in the underworld, has been removed.";

            //check to see if the world is singleplayer or multiplayer
            if (Main.netMode != NetmodeID.Server) {
                //send the message in singleplayer with the color
                Main.NewText(message, (byte)255, (byte)46, (byte)46);
            }
            else {
                //send the message in multiplayer with the color
                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(message), new Color(255, 46, 46));
            }

            MinecraftWither.MinecraftWither.hellMessage = true;
          }
          return true;
        }
    }
}
