﻿using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Terramon.UI;
using Terraria;
// using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terramon.UI.Starter;
using Terramon.UI.SidebarParty;
using Terraria.ID;
using System.IO;
using System.Reflection;
using Terramon.Network.Catching;
using Terramon.Network.Starter;
using Terramon.Pokemon;
using Terramon.Pokemon.FirstGeneration.Normal._caughtForms;
using Terramon.UI.Moveset;

namespace Terramon
{
    public class TerramonMod : Mod
    {
        internal ChooseStarter ChooseStarter;
        internal ChooseStarterBulbasaur ChooseStarterBulbasaur;
        internal ChooseStarterCharmander ChooseStarterCharmander;
        internal ChooseStarterSquirtle ChooseStarterSquirtle;

        public static bool PartyUITheme = true;
        public static bool PartyUIAutoMode = false;
        public static bool PartyUIReverseAutoMode = false;
        public static bool ShowHelpButton = true;
        public static bool HelpButtonInitialize = true;
        public int PartyUIThemeChanged = 0;

        // UI SIDEBAR //
        internal UISidebar UISidebar;
        internal Moves Moves;
        public PartySlots PartySlots { get; private set; }

        // UI SIDEBAR //

        internal PokegearUI PokegearUI;
        internal PokegearUIEvents PokegearUIEvents;
        internal EvolveUI evolveUI;
        public UserInterface _exampleUserInterface; // Choose Starter
        private UserInterface _exampleUserInterfaceNew; // Pokegear Main Menu
        private UserInterface PokegearUserInterfaceNew;
        private UserInterface evolveUserInterfaceNew;// Pokegear Events Menu
        private UserInterface _uiSidebar;
        private UserInterface _moves;
        public UserInterface _partySlots;

        public static ModHotKey PartyCycle;

        //evolution


        public TerramonMod()
        {
            Instance = this;
            // By default, all Autoload properties are True. You only need to change this if you know what you are doing.
            //Properties = new ModProperties()
            //{
            //	Autoload = true,
            //	AutoloadGores = true,
            //	AutoloadSounds = true,
            //	AutoloadBackgrounds = true
            //};
        }

        private readonly static string[] balls = { "Pokeball",
                                                   "GreatBall",
                                                   "UltraBall",
                                                   "DuskBall",
                                                    "PremierBall",
                                                   "QuickBall",
                                                   "MasterBall"};

        // catch chance of the ball refers to the same index as the ball
        private readonly static float[][] catchChances = {
            new float[] { .1190f }, //Pokeball
            new float[] { .1785f }, //Great Ball
            new float[] { .2380f }, //Ultra Ball
            new float[] { .2380f,   //Dusk Ball
                          .1190f },
            new float[] { .1190f }, //Premier Ball
            new float[] { .2380f }, //Quick Ball
            new float[] { 1f } //Master Ball
        };

        public static string[] GetBallProjectiles()
        {
            string[] ballProjectiles = new string[balls.Length];
            for (int i = 0; i < balls.Length; i++)
            {
                ballProjectiles[i] = balls[i] + "Projectile";
            }

            return ballProjectiles;
        }

        public override void Load()
        {
            //Load all mons to a store
            LoadPokemons();

            if (Main.netMode != NetmodeID.Server)
            {
                ChooseStarter = new ChooseStarter();
                ChooseStarter.Activate();
                ChooseStarterBulbasaur = new ChooseStarterBulbasaur();
                ChooseStarterBulbasaur.Activate();
                ChooseStarterCharmander = new ChooseStarterCharmander();
                ChooseStarterCharmander.Activate();
                ChooseStarterSquirtle = new ChooseStarterSquirtle();
                ChooseStarterSquirtle.Activate();
                PokegearUI = new PokegearUI();
                PokegearUI.Activate();
                PokegearUIEvents = new PokegearUIEvents();
                PokegearUIEvents.Activate();
                evolveUI = new EvolveUI();
                evolveUI.Activate();
                UISidebar = new UISidebar();
                UISidebar.Activate();
                Moves = new Moves();
                Moves.Activate();
                PartySlots = new PartySlots();
                PartySlots.Activate();
                _exampleUserInterface = new UserInterface();
                _exampleUserInterfaceNew = new UserInterface();
                PokegearUserInterfaceNew = new UserInterface();
                evolveUserInterfaceNew = new UserInterface();
                _uiSidebar = new UserInterface();
                _moves = new UserInterface();
                _partySlots = new UserInterface();
            


                _exampleUserInterface.SetState(ChooseStarter); // Choose Starter
                _exampleUserInterfaceNew.SetState(PokegearUI); // Pokegear Main Menu
                PokegearUserInterfaceNew.SetState(PokegearUIEvents); // Pokegear Events Menu
                evolveUserInterfaceNew.SetState(evolveUI);
                _uiSidebar.SetState(UISidebar);
                _moves.SetState(Moves);
                _partySlots.SetState(PartySlots);
            }


            if (Main.dedServ)
                return;

            FirstPKMAbility = this.RegisterHotKey("First Pokémon Move", Keys.Z.ToString());
            SecondPKMAbility = this.RegisterHotKey("Second Pokémon Move", Keys.X.ToString());
            ThirdPKMAbility = this.RegisterHotKey("Third Pokémon Move", Keys.C.ToString());
            FourthPKMAbility = this.RegisterHotKey("Fourth Pokémon Move", Keys.V.ToString());

            PartyCycle = RegisterHotKey("Quick Spawn First Party Pokémon", Keys.RightAlt.ToString());
        }

        public override void Unload()
        {
            Instance = null;
            _exampleUserInterface.SetState(null); // Choose Starter
            _exampleUserInterfaceNew.SetState(null); // Pokegear Main Menu
            PokegearUserInterfaceNew.SetState(null); // Pokegear Events Menu
            evolveUserInterfaceNew.SetState(null);
            _uiSidebar.SetState(null);
            _partySlots.SetState(null);
            _moves.SetState(null);
            PartySlots = null;
            pokemonStore = null;
            wildPokemonStore = null;
        }

        //ModContent.GetInstance<TerramonMod>(). (grab instance)


        public static float[][] GetCatchChances() => catchChances;

        /* public override void Load()
		{
			Main.music[MusicID.OverworldDay] = GetMusic("Sounds/Music/overworldnew");
			Main.music[MusicID.Night] = GetMusic("Sounds/Music/nighttime");
			Main.music[MusicID.AltOverworldDay] = GetMusic("Sounds/Music/overworldnew");
		} */

        // UI STUFF DOWN HERE

        public override void UpdateUI(GameTime gameTime)
        {
            if (ChooseStarter.Visible)
            {
                _exampleUserInterface?.Update(gameTime);
            }
            if (PokegearUI.Visible)
            {
                _exampleUserInterfaceNew?.Update(gameTime);
            }
            if (PokegearUIEvents.Visible)
            {
                PokegearUserInterfaceNew?.Update(gameTime);
            }
            if (EvolveUI.Visible)
            {
                evolveUserInterfaceNew?.Update(gameTime);
            }
            //starters
            if (ChooseStarterBulbasaur.Visible)
            {
                _exampleUserInterface?.Update(gameTime);
            }
            if (ChooseStarterCharmander.Visible)
            {
                _exampleUserInterface?.Update(gameTime);
            }
            if (ChooseStarterSquirtle.Visible)
            {
                _exampleUserInterface?.Update(gameTime);
            }
            if (UISidebar.Visible)
            {
                _uiSidebar?.Update(gameTime);
            }
            if (Moves.Visible)
            {
                _moves?.Update(gameTime);
            }
            if (PartySlots.Visible)
            {
                _partySlots?.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            int StarterSelectionLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Interface Logic 1"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Terramon: Pokemon Interfaces",
                    delegate
                    {
                        if (ChooseStarter.Visible)
                        {
                            _exampleUserInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        if (PokegearUI.Visible)
                        {
                            _exampleUserInterfaceNew.Draw(Main.spriteBatch, new GameTime());
                        }
                        if (PokegearUIEvents.Visible)
                        {
                            PokegearUserInterfaceNew.Draw(Main.spriteBatch, new GameTime());
                        }
                        if (EvolveUI.Visible)
                        {
                            evolveUserInterfaceNew.Draw(Main.spriteBatch, new GameTime());
                        }
                        if (ChooseStarterBulbasaur.Visible)
                        {
                            _exampleUserInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        if (ChooseStarterCharmander.Visible)
                        {
                            _exampleUserInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        if (ChooseStarterSquirtle.Visible)
                        {
                            _exampleUserInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        if (UISidebar.Visible)
                        {
                            _uiSidebar.Draw(Main.spriteBatch, new GameTime());
                        }
                        if (Moves.Visible)
                        {
                            _moves.Draw(Main.spriteBatch, new GameTime());
                        }
                        if (PartySlots.Visible)
                        {
                            _partySlots.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );

            }
            
        }
        public static bool MyUIStateActive(Player player)
        {
            return ChooseStarter.Visible;
        }

        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.myPlayer == -1 || Main.gameMenu || !Main.LocalPlayer.active)
            {
                return;
            }

            if (MyUIStateActive(Main.LocalPlayer))
            {
                music = GetSoundSlot(SoundType.Music, null);
            }
        }

        // END UI STUFF


        #region HotKeys

        public ModHotKey FirstPKMAbility { get; private set; }
        public ModHotKey SecondPKMAbility { get; private set; }
        public ModHotKey ThirdPKMAbility { get; private set; }
        public ModHotKey FourthPKMAbility { get; private set; }

        #endregion


        /// <summary>
        /// Class used to save pokeball rarity when manipulating
        /// items data;
        /// </summary>
        public static class PokeballFactory
        {
            public enum Pokebals : byte
            {
                Nothing = 0,
                Pokeball,
                GreatBall,
                UltraBall,
                DuskBall,
                PremierBall,
                QuickBall,
            }

            /// <summary>
            /// Return type id for provided pokeball.
            /// Mostly used for loading from saves
            /// </summary>
            /// <param name="item">Byte enum of save pokeball</param>
            /// <returns>Return item id or 0 if this is not a pokeball</returns>
            public static int GetPokeballType(Pokebals item)
            {
                switch (item)
                {
                    case Pokebals.Pokeball:
                        return ModContent.ItemType<PokeballCaught>();
                    case Pokebals.GreatBall:
                        return ModContent.ItemType<GreatBallCaught>();
                    case Pokebals.UltraBall:
                        return ModContent.ItemType<UltraBallCaught>();
                    case Pokebals.DuskBall:
                        return ModContent.ItemType<DuskBallCaught>();
                    case Pokebals.PremierBall:
                        return ModContent.ItemType<PremierBallCaught>();
                    case Pokebals.QuickBall:
                        return ModContent.ItemType<QuickBallCaught>();
                    default:
                        return 0;
                }
            }

            /// <summary>
            /// Return enum byte for provided item.
            /// Mostly used for saving
            /// </summary>
            /// <param name="item">ModItem of item</param>
            /// <returns>Return byte enum or <see cref="Pokebals.Nothing"/>
            /// if provided item is not a pokeball</returns>
            public static Pokebals GetEnum(ModItem item)
            {
                if (item is PokeballCaught)
                {
                    return Pokebals.Pokeball;
                }
                if (item is GreatBallCaught)
                {
                    return Pokebals.GreatBall;
                }
                if (item is UltraBallCaught)
                {
                    return Pokebals.UltraBall;
                }
                if (item is DuskBallCaught)
                {
                    return Pokebals.DuskBall;
                }
                if (item is PremierBallCaught)
                {
                    return Pokebals.PremierBall;
                }
                if (item is QuickBallCaught)
                {
                    return Pokebals.QuickBall;
                }
                return Pokebals.Nothing;
            }

            /// <summary>
            /// Return item type id from provided pokeball
            /// </summary>
            /// <param name="item">ModItem of item</param>
            /// <returns>Return item id or 0 if this is not a pokeball</returns>
            public static int GetPokeballType(ModItem item)
            {
                if (item is PokeballCaught)
                {
                    return ModContent.ItemType<PokeballCaught>();
                }
                if (item is GreatBallCaught)
                {
                    return ModContent.ItemType<GreatBallCaught>();
                }
                if (item is UltraBallCaught)
                {
                    return ModContent.ItemType<UltraBallCaught>();
                }
                if (item is DuskBallCaught)
                {
                    return ModContent.ItemType<DuskBallCaught>();
                }
                if (item is PremierBallCaught)
                {
                    return ModContent.ItemType<PremierBallCaught>();
                }
                if (item is QuickBallCaught)
                {
                    return ModContent.ItemType<QuickBallCaught>();
                }
                return 0;
            }
        }



        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            //In case i f*ck the code
            try
            {
                string type = reader.ReadString();
                switch (type)
                {
                    case SpawnStarterPacket.NAME:
                    {
                        //Server can't have any UI
                        if (whoAmI == 256)
                            return;
                        SpawnStarterPacket packet = new SpawnStarterPacket();
                        packet.HandleFromClient(reader, whoAmI);
                    }
                        break;
                    case BaseCatchPacket.NAME:
                    {
                        //Server should handle it from client
                        if (whoAmI == 256)
                            return;
                        BaseCatchPacket packet = new BaseCatchPacket();
                        packet.HandleFromClient(reader, whoAmI);
                    }
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.ErrorFormat("Exception appear in HandlePacket. Please, contact mod devs with folowing stacktrace:\n\n{0}\n\n{1}", e.Message, e.StackTrace);
            }

        }

        public static TerramonMod Instance { get; private set; }

        public static ParentPokemon GetPokemon(string monName)
        {
            if (monName == null)
            {
                return null;
            }
            if (Instance.pokemonStore != null && Instance.pokemonStore.ContainsKey(monName))
            {
                return Instance.pokemonStore[monName];
            }
            return null;
        }

        public static ParentPokemonNPC GetWildPokemon(string monName)
        {
            if (monName == null)
            {
                return null;
            }
            if (Instance.pokemonStore != null && Instance.pokemonStore.ContainsKey(monName))
            {
                return Instance.wildPokemonStore[monName];
            }
            return null;
        }

        private Dictionary<string, ParentPokemon> pokemonStore;
        private Dictionary<string, ParentPokemonNPC> wildPokemonStore;
        private void LoadPokemons()
        {
            pokemonStore = new Dictionary<string, ParentPokemon>();
            foreach (TypeInfo it in GetType().Assembly.DefinedTypes)
            {
                if (it.IsAbstract)
                    continue;
                bool valid = false;
                if (it.BaseType == typeof(ParentPokemon))
                    valid = true;
                else
                {
                    //Recurrent seek for our class
                    var baseType = it.BaseType;
                    while (baseType != null && baseType != typeof(object) )
                    {
                        if (baseType == typeof(ParentPokemon))
                        {
                            valid = true;
                            break;
                        }
                        baseType = baseType.BaseType;
                    }
                }

                if(valid)
                    try
                    {
                        pokemonStore.Add(it.Name, (ParentPokemon) Activator.CreateInstance(it));
                    }
                    catch (Exception e)
                    {
                        Logger.Error(
                            "Exception caught in Events register loop. Report mod author with related stacktrace: \n" +
                            $"{e.Message}\n" +
                            $"{e.StackTrace}\n");
                    }
            }

            wildPokemonStore = new Dictionary<string, ParentPokemonNPC>();
            foreach (TypeInfo it in GetType().Assembly.DefinedTypes)
            {
                if (it.IsAbstract)
                    continue;
                bool valid = false;
                if (it.BaseType == typeof(ParentPokemonNPC))
                    valid = true;
                else
                {
                    //Recurrent seek for our class
                    var baseType = it.BaseType;
                    while (baseType != null && baseType != typeof(object))
                    {
                        if (baseType == typeof(ParentPokemonNPC))
                        {
                            valid = true;
                            break;
                        }
                        baseType = baseType.BaseType;
                    }
                }

                if (valid)
                    try
                    {
                        //We register wild mon and captured mon with same name
                        var inst = (ParentPokemonNPC) Activator.CreateInstance(it);
                        wildPokemonStore.Add(inst.HomeClass().Name, inst);
                    }
                    catch (Exception e)
                    {
                        Logger.Error(
                            "Exception caught in Events register loop. Report mod author with related stacktrace: \n" +
                            $"{e.Message}\n" +
                            $"{e.StackTrace}\n");
                    }
            }

        }
    }
}
