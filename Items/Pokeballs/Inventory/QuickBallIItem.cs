﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terramon.Items.Pokeballs.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terramon.Items.Pokeballs.Inventory
{
    public sealed class QuickBallItem : BaseThrowablePokeballItem<QuickBallProjectile>
    {
        public QuickBallItem() : base(Constants.Pokeballs.UnlocalizedNames.QUICK_BALL,
            new Dictionary<GameCulture, string>()
            {
                { GameCulture.English, "Quick Ball" },
                { GameCulture.French, "Rapide Ball" }
            },
            new Dictionary<GameCulture, string>()
            {
                { GameCulture.English, "A somewhat different Poké Ball." + "\nProvides a higher catch rate when used at the start of a wild encounter." }
            },
            Item.sellPrice(gold: 6, silver: 25), ItemRarityID.White, Constants.Pokeballs.CatchRates.QUICK_BALL)
        {
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("QuickBallCap"));
            recipe.AddIngredient(mod.ItemType("Button"));
            recipe.AddIngredient(mod.ItemType("QuickBallBase"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.value = 60000;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine nameLine = tooltips.FirstOrDefault(t => t.Name == "ItemName" && t.mod == "Terraria");

            base.ModifyTooltips(tooltips);

            tooltips.RemoveAll(l => l.Name == "Damage");
            tooltips.RemoveAll(l => l.Name == "CritChance");
            tooltips.RemoveAll(l => l.Name == "Speed");
            tooltips.RemoveAll(l => l.Name == "Knockback");

            if (NameColorOverride != null)
                tooltips.Find(t => t.Name == TooltipLines.ITEM_NAME).overrideColor = NameColorOverride;

            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(100, 161, 237);
                }
            }
        }
    }
}
