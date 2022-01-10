using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;
using Terraria.Localization;

namespace minecraftWitherinTerraria.Tiles
{
    public class SoulSand : ModTile
    {
        static void Talk(string message, int r=150, int g=250, int b=150) {

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
            //make the tile solid and not treat the image as a sprite sheet
            Main.tileSolid[Type] = true;
            Main.tileFrameImportant[Type] = true;

            //make the soul sand detectable by splunker and metal detector
            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true;
			Main.tileValue[Type] = 775;
			Main.tileShine2[Type] = true;
			Main.tileShine[Type] = 975;

            //make it drop it item version and color it on the map
            drop = ModContent.ItemType<Items.SoulSand>();
    		AddMapEntry(new Color(73, 55, 44));
        }

        //give the slow debuff to the player when they are on the tile.
        public override void FloorVisuals (Player player)
        {
            player.AddBuff(BuffID.Slow, 2);
        }

        //check if the soul sand is placed in a T shape with wither skeleton skulls on it when a tile is placed down or a tile is near it
        public override bool TileFrame (int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            //soul sand id : 472
            //wither skeleton skull id: 473

            //get all of the tiles in a T shape
            int center = Main.tile[i, j].type;
            int left = Main.tile[i-1, j].type;
            int right = Main.tile[i+1, j].type;
            int bottom = Main.tile[i, j+1].type;
            int topL = Main.tile[i-1, j-1].type;
            int topC = Main.tile[i, j-1].type;
            int topR = Main.tile[i+1, j-1].type;

            //check if the tiles in the t shape is has the right tiles to spawn in the wither
            if ((center==ModContent.TileType<Tiles.SoulSand>()) && (left==ModContent.TileType<Tiles.SoulSand>()) && (right==ModContent.TileType<Tiles.SoulSand>()) && (bottom==ModContent.TileType<Tiles.SoulSand>()) && (topL==ModContent.TileType<Tiles.WitherSkeletonSkull>()) && (topC==ModContent.TileType<Tiles.WitherSkeletonSkull>()) && (topR==ModContent.TileType<Tiles.WitherSkeletonSkull>()))
            {
                Talk("It's ready to spawn in the wither, if I had one to spawn in!");
            }

            return true;
        }
    }
}
