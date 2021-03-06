using System;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Haunter
{
    public class Haunter : ParentPokemonGastly
    {
        public override int EvolveCost => 10;

        public override Type EvolveTo => typeof(Gengar.Gengar);

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // projectile.scale = 0.5f;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 38;
            projectile.height = 40;
            drawOriginOffsetY = -36;
            projectile.alpha = 95;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.haunterPet = false;
            }
            if (modPlayer.haunterPet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}