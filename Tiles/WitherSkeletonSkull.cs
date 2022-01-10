using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace minecraftWitherinTerraria.Tiles
{
    public class WitherSkeletonSkull : ModTile
    {
        public override void SetDefaults()
        {
            //make the tile solid and not treat the image as a sprite sheet
            Main.tileSolid[Type] = true;
            Main.tileFrameImportant[Type] = true;
            //make it drop it item version and color it on the map
            drop = ModContent.ItemType<Items.WitherSkeletonSkull>();
    		AddMapEntry(new Color(0, 0, 0));
        }
    }
}
