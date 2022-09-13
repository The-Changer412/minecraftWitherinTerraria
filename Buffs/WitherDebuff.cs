using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace minecraftWitherinTerraria.Buffs
{
  public class WitherDebuff : ModBuff
  {
    //set the properties of the wither debuff
    public override void SetStaticDefaults()
    {
		DisplayName.SetDefault("Wither");
		Description.SetDefault("You are losing your life. If only there was milk in this mod.");
		Main.debuff[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

    //slowly remove health from the player
    public override void Update(Player player, ref int buffIndex)
    {
		player.lifeRegen -= 10;
	}
  }
}
