using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Rattata
{
    public class Rattata : ParentPokemon
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 35;
            projectile.height = 40;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.rattataPet = false;
            }
            if (modPlayer.rattataPet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}