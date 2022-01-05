using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MinecraftWither.Items
{
    public class WitherSkeletonSkull : ModItem
    {
        public override void SetStaticDefaults()
        {
            //set the text when hovering over the item
            Tooltip.SetDefault("This is the head of the enemy that you defeated.\nI wonder what will happen if you place three of the skulls on soul sand in a T shape.");
        }

        //set the stats of the item
        public override void SetDefaults() {
			item.width = 23;
			item.height = 23;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = ModContent.TileType<Tiles.WitherSkeletonSkull>();
		}
    }
}
