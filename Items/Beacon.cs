using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace minecraftWitherinTerraria.Items
{
    public class Beacon : ModItem
    {
        public override void SetStaticDefaults()
        {
            //set the text when hovering over the item
            Tooltip.SetDefault("The block that gives power to all nearby users. Needs prehardmore bars to power it.");
        }

        //set the stats of the item
        public override void SetDefaults() {
			item.width = 16;
			item.height = 16;
			item.maxStack = 999;
            item.value = 100462;
            item.rare = ItemRarityID.Purple;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = ModContent.TileType<Tiles.Beacon>();
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Glass, 5);
            recipe.AddIngredient(ItemID.Obsidian, 3);
            recipe.AddIngredient(ModContent.ItemType<Items.NetherStar>(), 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
