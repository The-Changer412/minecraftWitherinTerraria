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
            Tooltip.SetDefault("The block that gives power to all nearby users. Needs any bars to power it.");
        }

        //set the stats of the item
        public override void SetDefaults() {
			Item.width = 16;
			Item.height = 16;
			Item.maxStack = 999;
            Item.value = 100462;
            Item.rare = ItemRarityID.Purple;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Tiles.Beacon>();
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Glass, 5);
            recipe.AddIngredient(ItemID.Obsidian, 3);
            recipe.AddIngredient(ModContent.ItemType<Items.NetherStar>(), 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
