using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace minecraftWitherinTerraria.Items
{
    public class SoulSand : ModItem
    {
        public override void SetStaticDefaults()
        {
            //set the text when hovering over the item
            Tooltip.SetDefault("The tile that holds the soul innocent beings.");
        }

        //set the stats of the item
        public override void SetDefaults() {
			Item.width = 23;
			Item.height = 23;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Tiles.SoulSand>();
		}
    }
}
