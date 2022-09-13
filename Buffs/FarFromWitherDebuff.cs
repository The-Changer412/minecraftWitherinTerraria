using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace minecraftWitherinTerraria.Buffs
{
  public class FarFromWitherDebuff : ModBuff
  {
    //set the properties of the wither debuff
    public override void SetStaticDefaults()
    {
		DisplayName.SetDefault("Far From Wither Debuff");
		Description.SetDefault("You are too far away from the Wither to do damage to him.");
		Main.debuff[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

    //slowly remove health from the player
    public override void Update(Player player, ref int buffIndex)
    {
		//
	}
  }
}
