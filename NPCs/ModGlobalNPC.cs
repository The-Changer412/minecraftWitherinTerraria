﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
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
          if (npc.type == NPCID.MoonLordCore && World.hellMessage == false)
          {
            string message = "By killing the Moon Lord, a new enemy has appeared in the underworld.";

            //check to see if the world is singlePlayer or multiPlayer
            if (Main.netMode != NetmodeID.Server) {
                //send the message in singlePlayer with the color
                Main.NewText(message, (byte)255, (byte)46, (byte)46);
            }
            else {
                //send the message in multiPlayer with the color
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(message), new Color(255, 46, 46));
            }

            World.hellMessage = true;
          }
          return true;
        }
    }
}
