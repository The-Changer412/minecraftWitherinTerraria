using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;
using Terraria.Localization;

namespace minecraftWitherinTerraria.Tiles
{
    public class Beacon : ModTile
    {
        static void Talk(string message, int r=150, int g=250, int b=150)
        {
            //check to see if the world is singleplayer or multiplayer
            if (Main.netMode != NetmodeID.Server) {
                Main.NewText(message, (byte)r, (byte)g, (byte)b);
            }
            else {
                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(message), new Color(r, g, b));
            }
        }

        public override void SetDefaults()
        {
            //make the tile only solid on the top and not treat the image as a sprite sheet
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            //make it drop it item version and color it on the map
            drop = ModContent.ItemType<Items.Beacon>();
    		AddMapEntry(new Color(43, 203, 192));
        }

        //check if the soul sand is placed in a T shape with wither skeleton skulls on it when a tile is placed down or a tile is near it
        public override bool TileFrame (int i, int j, ref bool resetFrame, ref bool noBreak)
        {

            //count how many bars have been placed.
            int TotalBars = 0;
            for(int yLayer = 1; yLayer<5; yLayer++)
            {
                for (int xLayer = -yLayer; xLayer<yLayer+1; xLayer++)
                {
                    //239 is the tile id for all bars
                    if (Main.tile[i+xLayer, j+yLayer].type == 239)
                    {
                        TotalBars++;
                    }
                }
            }

            //if the
            if (TotalBars == 3 | TotalBars == 8 | TotalBars == 15 | TotalBars == 24)
            {
                Talk("The Beacon is activated");
            }

            return true;
        }
    }
}
