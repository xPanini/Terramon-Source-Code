using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Magikarp
{
    public class Magikarp : ParentPokemon
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 34; //-6
            projectile.height = 24; //-4
            drawOriginOffsetY = -20;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.magikarpPet = false;
            }
            if (modPlayer.magikarpPet)
            {
                projectile.timeLeft = 2;
            }
            if (Main.rand.Next(12) == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 34, 0f, 0f, 100, default, 1f);
            }
        }
    }
}