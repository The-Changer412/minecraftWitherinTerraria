using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using minecraftWitherinTerraria;

namespace minecraftWitherinTerraria
{
    public class SaveAndLoadToWorld : ModWorld
    {
        //save the hellMessage to the world
        public override TagCompound Save()
        {
            return new TagCompound
            {
                [nameof(MinecraftWither.hellMessage)] = MinecraftWither.hellMessage
            };
        }

        //load the hellMessage to the world
        public override void Load(TagCompound tag)
        {
            MinecraftWither.hellMessage = tag.GetBool(nameof(MinecraftWither.hellMessage));
        }
    }
}
