using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using minecraftWitherinTerraria;

namespace minecraftWitherinTerraria
{
    public class World : ModWorld
    {
        //create variables
        public static bool DownedWither = false;
        public static bool hellMessage = false;

        //save the hellMessage and downedWither to the world
        public override TagCompound Save()
        {
            return new TagCompound
            {
                [nameof(hellMessage)] = hellMessage,
                [nameof(DownedWither)] = DownedWither
            };
        }

        //load the hellMessage to the world
        public override void Load(TagCompound tag)
        {
            hellMessage = tag.GetBool(nameof(hellMessage));
            DownedWither = tag.GetBool(nameof(DownedWither));
        }
    }
}
