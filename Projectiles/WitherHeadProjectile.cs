using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace minecraftWitherinTerraria.Projectiles
{
	public class WitherHeadProjectile : ModProjectile
	{

        //a temporary function that send a message to the chat for debugging purpose.
        static void Talk(string message, int r=150, int g=250, int b=150) {

            //check to see if the world is singleplayer or multiplayer
            if (Main.netMode != NetmodeID.Server) {
                Main.NewText(message, (byte)r, (byte)g, (byte)b);
            }
            else {
                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(message), new Color(r, g, b));
            }
        }

		//set the static stats for the projectile
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wither's Head Projectile");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		//set the stats for the projectile
		public override void SetDefaults()
		{
			projectile.width = 24;
            projectile.height = 24;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.noDropItem = true;

			Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/wither/shoot"), 1, 1f);
		}

		public override void AI()
		{
			projectile.position += projectile.velocity;
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
              target.AddBuff(mod.BuffType("WitherDebuff"), 900);
            } else
            {
              target.AddBuff(mod.BuffType("WitherDebuff"), 600);
            }
        }
	}
}
