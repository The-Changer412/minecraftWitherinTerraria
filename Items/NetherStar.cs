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
			item.width = 16;
			item.height = 16;
			item.maxStack = 999;
            item.value = 40462;
            item.rare = ItemRarityID.Purple;
		}
    }
}
