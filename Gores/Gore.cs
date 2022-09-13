using Terraria.ID;
using Terraria.ModLoader;

namespace minecraftWitherinTerraria.Gores
{
    public class WitherSkeletonGoreArm : ModGore
    {
        public override string Texture => "minecraftWitherinTerraria/Gores/WitherSkeletonGoreArm";

        public override void SetStaticDefaults()
        {

            GoreID.Sets.SpecialAI[Type] = 3;
        }
    }

    public class WitherSkeletonGoreLeg : ModGore
    {
        public override string Texture => "minecraftWitherinTerraria/Gores/WitherSkeletonGoreLeg";

        public override void SetStaticDefaults()
        {

            GoreID.Sets.SpecialAI[Type] = 3;
        }
    }

    public class WitherSkeletonGoreHead : ModGore
    {
        public override string Texture => "minecraftWitherinTerraria/Gores/WitherSkeletonGoreHead";

        public override void SetStaticDefaults()
        {

            GoreID.Sets.SpecialAI[Type] = 3;
        }
    }

    public class WitherSkeletonGoreBody : ModGore
    {
        public override string Texture => "minecraftWitherinTerraria/Gores/WitherSkeletonGoreBody";

        public override void SetStaticDefaults()
        {

            GoreID.Sets.SpecialAI[Type] = 3;
        }
    }

    public class WitherGoreHead : ModGore
    {
        public override string Texture => "minecraftWitherinTerraria/Gores/WitherGoreHead";

        public override void SetStaticDefaults()
        {

            GoreID.Sets.SpecialAI[Type] = 3;
        }
    }

    public class WitherGoreBody : ModGore
    {
        public override string Texture => "minecraftWitherinTerraria/Gores/WitherGoreBody";

        public override void SetStaticDefaults()
        {

            GoreID.Sets.SpecialAI[Type] = 3;
        }
    }

    public class WitherGoreNeck : ModGore
    {
        public override string Texture => "minecraftWitherinTerraria/Gores/WitherGoreNeck";

        public override void SetStaticDefaults()
        {

            GoreID.Sets.SpecialAI[Type] = 3;
        }
    }

    public class WitherGoreTail : ModGore
    {
        public override string Texture => "minecraftWitherinTerraria/Gores/WitherGoreTail";

        public override void SetStaticDefaults()
        {

            GoreID.Sets.SpecialAI[Type] = 3;
        }
    }
}
