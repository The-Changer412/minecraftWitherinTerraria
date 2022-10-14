using System;
using Terraria.ModLoader;

namespace minecraftWitherinTerraria
{
	public class MinecraftWither : Mod
	{
		public override void PostSetupContent()
		{
			try
			{
                Mod bossChecklist = ModLoader.GetMod("BossChecklist");
                if (bossChecklist != null)
                {
                    bossChecklist.Call("AddBoss", 15f, ModContent.NPCType<NPCs.Wither>(), this, "Wither", (Func<bool>)(() => World.DownedWither), null, null, ModContent.ItemType<Items.NetherStar>(), "Spawn in the Wither by placing soul sand in a T-pose shape with three Wither skulls on it.", "The Wither has left this world.");
                }
            } catch {}
		}
	}
}
