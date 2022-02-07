using Terraria;
using Terraria.ModLoader;

namespace minecraftWitherinTerraria.Buffs
{
  public class FarFromWitherDebuff : ModBuff
  {
    //set the properties of the wither debuff
    public override void SetDefaults()
    {
			DisplayName.SetDefault("Wither Armor");
			Description.SetDefault("You are too far away from the Wither to do damage to him.");
			Main.debuff[Type] = true;
            canBeCleared = false;
		}
  }
}
