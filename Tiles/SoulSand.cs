using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;
using Terraria.Localization;

namespace minecraftWitherinTerraria.Tiles
{
    public class SoulSand : ModTile
    {
        //a function for sending a message to the player in the chat
        static void Talk(string message, int r=150, int g=250, int b=150) {
            //check to see if the world is singleplayer or multiplayer
            if (Main.netMode != NetmodeID.Server) {
                Main.NewText(message, (byte)r, (byte)g, (byte)b);
            }
            else {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(message), new Color(r, g, b));
            }
        }

        public override void SetStaticDefaults()
        {
            //make the tile solid and not treat the image as a sprite sheet
            Main.tileSolid[Type] = true;
            Main.tileFrameImportant[Type] = true;

            //make it drop it item version and color it on the map
            ItemDrop = ModContent.ItemType<Items.SoulSand>();
    		AddMapEntry(new Color(73, 55, 44));
        }

        //give the slow debuff to the player when they are on the tile.
        public override void FloorVisuals (Player player)
        {
            player.AddBuff(BuffID.Slow, 2);
        }

        //play a sound when placing the soul sand
        public override void PlaceInWorld(int i, int j, Item item)
        {
            SoundEngine.PlaySound(new SoundStyle("minecraftWitherinTerraria/Sounds/soul_sand/break1"));
        }
        //play a sound when destroying the soul sand
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            SoundEngine.PlaySound(new SoundStyle("minecraftWitherinTerraria/Sounds/soul_sand/break3"));
        }

        //check if the soul sand is placed in a T shape with wither skeleton skulls on it when a tile is placed down or a tile is near it
        public override bool TileFrame (int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            //get all of the tiles in a T shape
            int center = Main.tile[i, j].TileType;
            int left = Main.tile[i-1, j].TileType;
            int right = Main.tile[i+1, j].TileType;
            int bottom = Main.tile[i, j+1].TileType;
            int topL = Main.tile[i-1, j-1].TileType;
            int topC = Main.tile[i, j-1].TileType;
            int topR = Main.tile[i+1, j-1].TileType;

            //check if the tiles in the t shape is has the right tiles to spawn in the wither
            if ((center==ModContent.TileType<Tiles.SoulSand>()) && (left==ModContent.TileType<Tiles.SoulSand>()) && (right==ModContent.TileType<Tiles.SoulSand>()) && (bottom==ModContent.TileType<Tiles.SoulSand>()) && (topL==ModContent.TileType<Tiles.WitherSkeletonSkull>()) && (topC==ModContent.TileType<Tiles.WitherSkeletonSkull>()) && (topR==ModContent.TileType<Tiles.WitherSkeletonSkull>()))
            {
                //kill the tiles
                WorldGen.KillTile(i, j, noItem: true);
                WorldGen.KillTile(i-1, j, noItem: true);
                WorldGen.KillTile(i+1, j, noItem: true);
                WorldGen.KillTile(i, j+1, noItem: true);
                WorldGen.KillTile(i-1, j-1, noItem: true);
                WorldGen.KillTile(i, j-1, noItem: true);
                WorldGen.KillTile(i+1, j-1, noItem: true);

                //spawn in the wither
                NPC.NewNPC(Player.GetSource_NaturalSpawn(), (int) i * 16, (int) j*16, ModContent.NPCType<NPCs.Wither>());
                Talk("The Wither has awoken!", 143, 61, 209);
            }

            return true;
        }
    }
}
