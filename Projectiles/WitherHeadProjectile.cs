using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.Localization;    
using static Terraria.ModLoader.ModContent;

namespace minecraftWitherinTerraria.Projectiles
{
	public class WitherHeadProjectile : ModProjectile
	{
		//set the static stats for the projectile
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wither's Head Projectile");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		//set the stats for the projectile
		public override void SetDefaults()
		{
			Projectile.width = 24;
            Projectile.height = 24;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.noDropItem = true;

            SoundEngine.PlaySound(new SoundStyle("minecraftWitherinTerraria/Sounds/wither/shoot"));
		}

		public override void AI()
		{
			Projectile.position += Projectile.velocity;
		}

        //make it where the wither can't be hurt by it's own head
        public override bool? CanHitNPC(NPC target)
        {
			if (target.type == ModContent.NPCType<NPCs.Wither>())
            {
                return false;
            } else
            {
                return null;
            }
        }

        //make it that the player will get the wither debuff on hit
        public override void OnHitPlayer (Player target, int damage, bool crit)
        {
            if (Main.expertMode == true)
            {
              target.AddBuff(Mod.Find<ModBuff>("WitherDebuff").Type, 900);
            } else
            {
              target.AddBuff(Mod.Find<ModBuff>("WitherDebuff").Type, 600);
            }
        }
	}
}
