using System;
using Terramon.Players;
using Terraria;

namespace Terramon.Pokemon.FirstGeneration.Normal.Bulbasaur
{
    public class Bulbasaur : ParentPokemon
    {
        public override int EvolveCost => 11;

        public override Type EvolveTo => typeof(Ivysaur.Ivysaur);

        public override PokemonType[] PokemonTypes => new[] { PokemonType.Grass, PokemonType.Poison };

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.width = 34; //-6
            projectile.height = 24; //-4
            drawOriginOffsetY = -14;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            TerramonPlayer modPlayer = player.GetModPlayer<TerramonPlayer>();
            if (player.dead)
            {
                modPlayer.bulbasaurPet = false;
            }
            if (modPlayer.bulbasaurPet)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}