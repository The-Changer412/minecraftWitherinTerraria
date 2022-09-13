using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace minecraftWitherinTerraria.Items
{
    public class NetherStar : ModItem
    {
        public override void SetStaticDefaults()
        {
            //set the text when hovering over the item
            Tooltip.SetDefault("The star that powers the wither. There is probably some way to harvest this power for yourself.");
        }

        //set the stats of the item
        public override void SetDefaults() {
			Item.width = 16;
			Item.height = 16;
			Item.maxStack = 999;
            Item.value = 40462;
            Item.rare = ItemRarityID.Purple;
		}
    }
}
