using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;
using Terraria.Localization;
using minecraftWitherinTerraria.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace minecraftWitherinTerraria.Tiles
{
    public class Beacon : ModTile
    {
        //create a variable for the beacon activation
        public static bool BeaconActivated = false;
        public static bool SoundPlayedActivated = false;
        public static bool SoundPlayedDeactivated = false;

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

            //if the player has made the four layer pyramid then activate the beacon
            if (TotalBars == 24)
            {
                BeaconActivated = true;

                Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/beacon/activate"), 1, 1f);

            } else
            {
                BeaconActivated = false;
                Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/beacon/deactivate"), 1, 1f);
            }

            return true;
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            //every frame, send to the player if any beacon is activated and it's position
            player.BeaconActivated = BeaconActivated;
            if (BeaconActivated)
            {
                player.BeaconX = i;
                player.BeaconY = j;
            }
            else {
                player.BeaconX = 0;
                player.BeaconY = 0;
            }
            return true;
        }
    }
}
