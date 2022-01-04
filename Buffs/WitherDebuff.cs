using Terraria;
using Terraria.ModLoader;

namespace MinecraftWither.Buffs
{
  public class WitherDebuff : ModBuff
  {
    //set the properties of the wither debuff
    public override void SetDefaults()
    {
			DisplayName.SetDefault("Wither");
			Description.SetDefault("You are losing your life. If only there was milk in this mod.");
			Main.debuff[Type] = true;
      canBeCleared = false;
		}

    public override void Update(Player player, ref int buffIndex)
    {
			player.lifeRegen -= 10;
		}
  }
}
