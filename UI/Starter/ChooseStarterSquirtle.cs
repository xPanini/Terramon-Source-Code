﻿using Microsoft.Xna.Framework.Graphics;
using Terramon.Network.Starter;
using Terramon.Players;
using Terramon.Pokemon.FirstGeneration.Normal._caughtForms;
using Terramon.Pokemon.FirstGeneration.Normal.Squirtle;
using Terramon.UI.SidebarParty;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Terramon.UI.Starter
{
    internal class ChooseStarterSquirtle : UIState
    {
        public NonDragableUIPanel mainPanel;
        public static bool Visible;

        public override void OnInitialize()
        {
            mainPanel = new NonDragableUIPanel();
            mainPanel.SetPadding(0);
            mainPanel.Left.Set(0f, 0f);
            mainPanel.Top.Set(0f, 0f);
            mainPanel.Width.Set(0f, 1f);
            mainPanel.Height.Set(0f, 1f);






            Texture2D starterselect = ModContent.GetTexture("Terramon/UI/PossibleAssets/StarterMenuNew");
            UIImagez starterselectmenu = new UIImagez(starterselect);
            starterselectmenu.Left.Set(0, 0);
            starterselectmenu.Top.Set(0, 0);
            starterselectmenu.Width.Set(1, 0);
            starterselectmenu.Height.Set(1, 0);
            mainPanel.Append(starterselectmenu);

            Texture2D bottomleftcornertexture = ModContent.GetTexture("Terramon/UI/Starter/BottomLeftCorner");
            UIImagez bottomleftcorner = new UIImagez(bottomleftcornertexture);
            bottomleftcorner.HAlign = 0f;
            bottomleftcorner.VAlign = 1f;
            bottomleftcorner.Top.Set(0, 0);
            bottomleftcorner.Width.Set(64, 0);
            bottomleftcorner.Height.Set(64, 0);
            mainPanel.Append(bottomleftcorner);

            Texture2D topleftcornertexture = ModContent.GetTexture("Terramon/UI/Starter/TopLeftCorner");
            UIImagez topleftcorner = new UIImagez(topleftcornertexture);
            topleftcorner.HAlign = 0f;
            topleftcorner.VAlign = 0f;
            topleftcorner.Top.Set(0, 0);
            topleftcorner.Width.Set(64, 0);
            topleftcorner.Height.Set(64, 0);
            mainPanel.Append(topleftcorner);

            Texture2D bottomrightcornertexture = ModContent.GetTexture("Terramon/UI/Starter/BottomRightCorner");
            UIImagez bottomrightcorner = new UIImagez(bottomrightcornertexture);
            bottomrightcorner.HAlign = 1f;
            bottomrightcorner.VAlign = 1f;
            bottomrightcorner.Top.Set(0, 0);
            bottomrightcorner.Width.Set(64, 0);
            bottomrightcorner.Height.Set(64, 0);
            mainPanel.Append(bottomrightcorner);

            Texture2D toprightcornertexture = ModContent.GetTexture("Terramon/UI/Starter/TopRightCorner");
            UIImagez toprightcorner = new UIImagez(toprightcornertexture);
            toprightcorner.HAlign = 1f;
            toprightcorner.VAlign = 0f;
            toprightcorner.Top.Set(0, 0);
            toprightcorner.Width.Set(64, 0);
            toprightcorner.Height.Set(64, 0);
            mainPanel.Append(toprightcorner);

            Texture2D test = ModContent.GetTexture("Terramon/UI/PossibleAssets/Text");
             UIImagez testmenu = new UIImagez(test);
             testmenu.HAlign = 0.5f;  
             testmenu.VAlign = 0.3f;  
              testmenu.Width.Set(391, 0);
              testmenu.Height.Set(99, 0);
             mainPanel.Append(testmenu);

            Texture2D bulbasaurTexture = ModContent.GetTexture("Terramon/UI/PossibleAssets/Bulbasaur");
            UIHoverImageButton bulbasaurTextureButton = new UIHoverImageButton(bulbasaurTexture, "Bulbasaur");     
            bulbasaurTextureButton.HAlign = 0.35f;  
            bulbasaurTextureButton.VAlign = 0.5f;   
            bulbasaurTextureButton.Width.Set(100, 0f);
            bulbasaurTextureButton.Height.Set(92, 0f);
            bulbasaurTextureButton.OnClick += new MouseEvent(bulbasaurTextureButtonClicked);
            mainPanel.Append(bulbasaurTextureButton);

            Texture2D charmanderTexture = ModContent.GetTexture("Terramon/UI/PossibleAssets/Charmander");
            UIHoverImageButton charmanderTextureButton = new UIHoverImageButton(charmanderTexture, "Charmander");     
            charmanderTextureButton.HAlign = 0.5f;  
            charmanderTextureButton.VAlign = 0.5f;   
            charmanderTextureButton.Width.Set(100, 0f);
            charmanderTextureButton.Height.Set(92, 0f);
            charmanderTextureButton.OnClick += new MouseEvent(charmanderTextureButtonClicked);
            mainPanel.Append(charmanderTextureButton);

            Texture2D squirtleTexture = ModContent.GetTexture("Terramon/UI/PossibleAssets/Squirtle");
            UIImagez squirtleTextureButton = new UIImagez(squirtleTexture);     
            squirtleTextureButton.HAlign = 0.65f;  
            squirtleTextureButton.VAlign = 0.5f;   
            squirtleTextureButton.Width.Set(100, 0f);
            squirtleTextureButton.Height.Set(92, 0f);
            mainPanel.Append(squirtleTextureButton);

            Texture2D charmanderTextTexture = ModContent.GetTexture("Terramon/UI/PossibleAssets/SquirtleText");
            UIImagez charmanderText = new UIImagez(charmanderTextTexture);
            charmanderText.HAlign = 0.5f;  
            charmanderText.VAlign = 0.7f;  
            charmanderText.Width.Set(351, 0);
            charmanderText.Height.Set(65, 0);
            mainPanel.Append(charmanderText);

            Texture2D chooseTexture = ModContent.GetTexture("Terramon/UI/PossibleAssets/Choose");
            UIHoverImageButton choose = new UIHoverImageButton(chooseTexture, "Choose Squirtle!");
            choose.HAlign = 0.5f;  
            choose.VAlign = 0.8f;  
            choose.Width.Set(153, 0);
            choose.Height.Set(43, 0);
            choose.OnClick += new MouseEvent(Chosen);
            mainPanel.Append(choose);



            Append(mainPanel);

        }

        
        Player player = Main.LocalPlayer;
       
        private void bulbasaurTextureButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.PlaySound(SoundID.MenuOpen);
            ModContent.GetInstance<TerramonMod>()._exampleUserInterface.SetState(new ChooseStarterBulbasaur());
        }
        private void charmanderTextureButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.PlaySound(SoundID.MenuOpen);
            ModContent.GetInstance<TerramonMod>()._exampleUserInterface.SetState(new ChooseStarterCharmander());
        }
        private void Chosen(UIMouseEvent evt, UIElement listeningElement)
        {
            TerramonPlayer TerramonPlayer = Main.LocalPlayer.GetModPlayer<TerramonPlayer>();
            Mod mod = ModContent.GetInstance<TerramonMod>();
            Player player = Main.LocalPlayer;
            Main.PlaySound(SoundID.MenuOpen);
            ModContent.GetInstance<TerramonMod>()._exampleUserInterface.SetState(null);
            Main.PlaySound(SoundID.Coins);
            TerramonPlayer.StarterChosen = true;
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                SpawnStarterPacket packet = new SpawnStarterPacket();
                packet.Send((TerramonMod) mod, SpawnStarterPacket.SQUIRTLE);
            }
            else
            {
                int index = Item.NewItem(player.getRect(), ModContent.ItemType<PokeballCaught>());
                if (index >= 400)
                    return;
                (Main.item[index].modItem as PokeballCaught).PokemonNPC = ModContent.NPCType<SquirtleNPC>();
                (Main.item[index].modItem as PokeballCaught).PokemonName = "Squirtle";
                (Main.item[index].modItem as PokeballCaught).SmallSpritePath = "Terramon/Minisprites/Regular/miniSquirtle";
            }

            Main.NewText("You chose [c/00FFFF:Squirtle, the Tiny Turtle Pokemon.] Great choice!");
            ChooseStarter.Visible = false;
            UISidebar.Visible = true;
        }


    }
}