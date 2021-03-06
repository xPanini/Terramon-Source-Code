using Terraria;
using Terraria.ModLoader;

namespace Terramon.Items.MiscItems
{
    public class Pokedex : ModItem
    {
        public int Timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pokédex");
            Tooltip.SetDefault("It's a digital encyclopedia created by [c/33FF33:Professor Oak] as an invaluable tool to Trainers."
                    + "\nUse it to open the dex user interface and keep track of your Pokémon data."
                            + "\nOh, and you probably shouldn't get rid of it.");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 32;
            item.scale = 0.8f;
            item.maxStack = 1;
            item.useTime = 34;
            item.useAnimation = 34;
            item.useStyle = 4;
            item.knockBack = 0;
            item.value = 0;
            item.rare = -12;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/pokedexon");
            item.autoReuse = false;
        }

        public override bool UseItem(Player player)
        {
            Main.NewText("This feature has been removed, and will be readded and revamped in a future update. Thanks for your patience.");
            return true;
        }
    }
}
