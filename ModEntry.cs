using Nickel;
using HarmonyLib;
using AetherWake.LarsMod.Cards;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using System;
using System.Collections.Generic;
using System.Linq;
using AetherWake.Features;

/* In the Cobalt Core modding community it is common for namespaces to be <Author>.<ModName>
 * This is helpful to know at a glance what mod we're looking at, and who made it */
namespace AetherWake.LarsMod;

/* ModEntry is the base for our mod. Others like to name it Manifest, and some like to name it <ModName>
 * Notice the ': SimpleMod'. This means ModEntry is a subclass (child) of the superclass SimpleMod (parent) from Nickel. This will help us use Nickel's functions more easily! */
public sealed class ModEntry : SimpleMod
{
    internal static ModEntry Instance { get; private set; } = null!;
    internal IKokoroApi KokoroApi { get; }
    internal IKokoroApi.IV2 KokoroApiV2 { get; }
    internal IHarmony Harmony { get; }
    internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
    internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }

    /* Woah! what's with the block of code right out the gate???
     * These are our manually declared stuff, isn't it neat?
     * Let's continue down, and you'll start getting a hang of how we utilize these */
    internal ISpriteEntry Lars_Character_CardBackground { get; }
    internal ISpriteEntry Lars_Character_CardFrame { get; }
    internal ISpriteEntry Lars_Character_Panel { get; }
    internal ISpriteEntry Lars_Character_Neutral_0 { get; }
    internal ISpriteEntry Lars_Character_Neutral_1 { get; }
    internal ISpriteEntry Lars_Character_Neutral_2 { get; }
    internal ISpriteEntry Lars_Character_Neutral_3 { get; }
    internal ISpriteEntry Lars_Character_Neutral_4 { get; }
    internal ISpriteEntry Lars_Character_Mini_0 { get; }
    internal ISpriteEntry Lars_Character_Squint_0 { get; }
    internal ISpriteEntry Lars_Character_Squint_1 { get; }
    internal ISpriteEntry Lars_Character_Squint_2 { get; }
    internal ISpriteEntry Lars_Character_Squint_3 { get; }
    internal ISpriteEntry Lars_Character_Squint_4 { get; }
    
    internal ISpriteEntry Lars_Character_Rapier_0 { get; }
    internal ISpriteEntry Lars_Character_Rapier_1 { get; }
    internal ISpriteEntry Lars_Character_Rapier_2 { get; }
    internal ISpriteEntry Lars_Character_Rapier_3 { get; }
    internal ISpriteEntry Lars_Character_Rapier_4 { get; }
    
    internal ISpriteEntry Lars_Character_Blep_0 { get; }
    internal ISpriteEntry Lars_Character_Death { get; }
    internal IDeckEntry Lars_Deck { get; }
    internal IPlayableCharacterEntryV2 Lars_Character { get; }
    internal static IReadOnlyList<Type> LarsCharacter_StarterCard_Types { get; } = [
        /* Add more starter cards here if you'd like. */
        typeof(Flamethrower),
        typeof(FireSpin),
    ];

    /* You can create many IReadOnlyList<Type> as a way to organize your content.
     * We recommend having a Starter Cards list, a Common Cards list, an Uncommon Cards list, and a Rare Cards list
     * However you can be more detailed, or you can be more loose, if that's your style */
    internal static IReadOnlyList<Type> LarsCharacter_CommonCard_Types { get; } = [
        typeof(Protect),
        typeof(SunnyDay),
        typeof(Rapier),
        typeof(FiredMove),
        typeof(Focus),
        typeof(HeatingUp)

    ];
    internal static IReadOnlyList<Type> LarsCharacter_UncommonCard_Types { get; } = [
        typeof(DragonTail),
        typeof(Magic),
        typeof(HeatAndDodge),
        typeof(HeatedOptions),
        typeof(Overheat),
        typeof(FireyEscape)
    ];

    internal static IReadOnlyList<Type> LarsCharacter_RareCard_Types { get; } = [
        typeof(FireyExcitement),
        typeof(Fireball),
        typeof(HeatExp)
    ];

    /* We can use an IEnumerable to combine the lists we made above, and modify it if needed
     * Maybe you created a new list for Uncommon cards, and want to add it.
     * If so, you can .Concat(TheUncommonListYouMade) */
    internal static IEnumerable<Type> Lars_AllCard_Types
        => LarsCharacter_StarterCard_Types
        .Concat(LarsCharacter_CommonCard_Types)
        .Concat(LarsCharacter_UncommonCard_Types)
        .Concat(LarsCharacter_RareCard_Types);


    internal static IReadOnlyList<Type> CommonArtifacts { get; } = [
        typeof(overheatArtifact),
    ];

    /* We'll organize our artifacts the same way: making lists and then feed those to an IEnumerable */

    // Init of Aether 
    internal ISpriteEntry Aether_Character_CardBackground { get; }
    internal ISpriteEntry Aether_Character_CardFrame { get; }
    internal ISpriteEntry Aether_Character_Panel { get; }
    internal ISpriteEntry Aether_Character_Neutral_0 { get; }
    internal ISpriteEntry Aether_Character_Neutral_1 { get; }
    internal ISpriteEntry Aether_Character_Neutral_2 { get; }
    internal ISpriteEntry Aether_Character_Neutral_3 { get; }
    internal ISpriteEntry Aether_Character_Neutral_4 { get; }
    internal ISpriteEntry Aether_Character_Mini_0 { get; }
    internal ISpriteEntry Aether_Character_Squint_0 { get; }
    internal ISpriteEntry Aether_Character_Squint_1 { get; }
    internal ISpriteEntry Aether_Character_Squint_2 { get; }
    internal ISpriteEntry Aether_Character_Squint_3 { get; }
    internal ISpriteEntry Aether_Character_Squint_4 { get; }
    internal ISpriteEntry Aether_Character_Blep_0 { get; }
    internal ISpriteEntry Aether_Character_Death { get; }
    internal IStatusEntry AquaRing { get; }
    internal IStatusEntry RainDance { get; }

    internal IDeckEntry Aether_Deck { get; }
    internal IPlayableCharacterEntryV2 Aether_Character { get; }
    internal static IReadOnlyList<Type> AetherCharacter_StarterCard_Types { get; } = [
        /* Add more starter cards here if you'd like. */
        typeof(Bubble),
        typeof(AquaRing)
    ];

    /* You can create many IReadOnlyList<Type> as a way to organize your content.
     * We recommend having a Starter Cards list, a Common Cards list, an Uncommon Cards list, and a Rare Cards list
     * However you can be more detailed, or you can be more loose, if that's your style */
    internal static IReadOnlyList<Type> AetherCharacter_CommonCard_Types { get; } = [
        typeof(Bubble),
        typeof(AquaRing),
        typeof(WaterGun),
        typeof(AquaTail),
        typeof(AcidArmor),
        typeof(Soak),
        typeof(MagicalExpansion)
    ];
    internal static IReadOnlyList<Type> AetherCharacter_UncommonCard_Types { get; } = [
        typeof(Hydropump),
        typeof(ADragonTail),
        typeof(WaterAbsorb),
        typeof(Preparation)
    ];

    internal static IReadOnlyList<Type> AetherCharacter_RareCard_Types { get; } = [
        typeof(RainDance),
        typeof(DragonDance)
    ];

    /* We can use an IEnumerable to combine the lists we made above, and modify it if needed
     * Maybe you created a new list for Uncommon cards, and want to add it.
     * If so, you can .Concat(TheUncommonListYouMade) */
    internal static IEnumerable<Type> Aether_AllCard_Types
        => AetherCharacter_StarterCard_Types
        .Concat(AetherCharacter_CommonCard_Types)
        .Concat(AetherCharacter_UncommonCard_Types)
        .Concat(AetherCharacter_RareCard_Types);

    internal static IReadOnlyList<Type> Aether_CommonArtifacts { get; } = [
        typeof(Moisturizer),
    ];

    // Init of Solstice 
    internal ISpriteEntry Solstice_Character_CardBackground { get; }
    internal ISpriteEntry Solstice_Character_CardFrame { get; }
    internal ISpriteEntry Solstice_Character_Panel { get; }
    internal ISpriteEntry Solstice_Character_Neutral_0 { get; }
    internal ISpriteEntry Solstice_Character_Neutral_1 { get; }
    internal ISpriteEntry Solstice_Character_Neutral_2 { get; }
    internal ISpriteEntry Solstice_Character_Neutral_3 { get; }
    internal ISpriteEntry Solstice_Character_Neutral_4 { get; }
    internal ISpriteEntry Solstice_Character_Mini_0 { get; }
    internal ISpriteEntry Solstice_Character_Squint_0 { get; }
    internal ISpriteEntry Solstice_Character_Squint_1 { get; }
    internal ISpriteEntry Solstice_Character_Squint_2 { get; }
    internal ISpriteEntry Solstice_Character_Squint_3 { get; }
    internal ISpriteEntry Solstice_Character_Squint_4 { get; }
    internal ISpriteEntry Solstice_Character_Blep_0 { get; }
    internal ISpriteEntry Solstice_Character_Death { get; }
    internal IDeckEntry Solstice_Deck { get; }
    internal IPlayableCharacterEntryV2 Solstice_Character { get; }
    internal static IReadOnlyList<Type> SolsticeCharacter_StarterCard_Types { get; } = [
    /* Add more starter cards here if you'd like. */
        typeof(SAttackDroneCard),
        typeof(ParallelShift)
    ];

    /* You can create many IReadOnlyList<Type> as a way to organize your content.
     * We recommend having a Starter Cards list, a Common Cards list, an Uncommon Cards list, and a Rare Cards list
     * However you can be more detailed, or you can be more loose, if that's your style */
    internal static IReadOnlyList<Type> SolsticeCharacter_CommonCard_Types { get; } = [
        typeof(SFlexMove),
        typeof(MissileShot),
        typeof(SPebble),
        typeof(SShieldDroneCard),
        typeof(SShiftShot),
        typeof(SSmallBoulder),
        typeof(SolidBreeze),
        typeof(SSpaceMineCard),
        typeof(LeechShot)
    ];
    internal static IReadOnlyList<Type> SolsticeCharacter_UncommonCard_Types { get; } = [
        typeof(SBattalion),
        typeof(BoulderBundle),
        typeof(SBubbleField),
        typeof(SLargeBoulders),
        typeof(SRadioControl),
        typeof(SRepairKit),
        typeof(SStrikerSquadron),
    ];

    internal static IReadOnlyList<Type> SolsticeCharacter_RareCard_Types { get; } = [
        typeof(SBayOverload),
        typeof(SEnergyDrone),
        typeof(SJupiterDrone),
        typeof(SRockFactory),
        typeof(SScatterShot)
    ];

    /* We can use an IEnumerable to combine the lists we made above, and modify it if needed
     * Maybe you created a new list for Uncommon cards, and want to add it.
     * If so, you can .Concat(TheUncommonListYouMade) */
    internal static IEnumerable<Type> Solstice_AllCard_Types
        => SolsticeCharacter_StarterCard_Types
        .Concat(SolsticeCharacter_CommonCard_Types)
        .Concat(SolsticeCharacter_UncommonCard_Types)
        .Concat(SolsticeCharacter_RareCard_Types);

    internal static IReadOnlyList<Type> Solstice_CommonArtifacts { get; } = [
        typeof(Moisturizer),
    ];

    public ModEntry(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
    {
        Instance = this;


        /* We use Kokoro to handle our statuses. This means Kokoro is a Dependency, and our mod will fail to work without it.
         * We take from Kokoro what we need and put in our own project. Head to ExternalAPI/StatusLogicHook.cs if you're interested in what, exactly, we use.
         * If you're interested in more fancy stuff, make sure to peek at the Kokoro repository found online. */
        KokoroApi = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!;
        KokoroApiV2 = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!.V2;
        Harmony = helper.Utilities.Harmony;
        /* These localizations lists help us organize our mod's text and messages by language.
         * For general use, prefer AnyLocalizations, as that will provide an easier time to potential localization submods that are made for your mod 
         * IMPORTANT: These localizations are found in the i18n folder (short for internationalization). The Demo Mod comes with a barebones en.json localization file that you might want to check out before continuing 
         * Whenever you add a card, artifact, character, ship, pretty much whatever, you will want to update your locale file in i18n with the necessary information
         * Example: You added your own character, you will want to create an appropiate entry in the i18n file. 
         * If you would rather use simple strings whenever possible, that's also an option -you do you. */
        AnyLocalizations = new JsonLocalizationProvider(
            tokenExtractor: new SimpleLocalizationTokenExtractor(),
            localeStreamFunction: locale => package.PackageRoot.GetRelativeFile($"i18n/{locale}.json").OpenRead()
        );
        Localizations = new MissingPlaceholderLocalizationProvider<IReadOnlyList<string>>(
            new CurrentLocaleOrEnglishLocalizationProvider<IReadOnlyList<string>>(AnyLocalizations)
        );
        initArtifacts();
        /* Assigning our ISpriteEntry objects manually. This is the easiest way to do it when starting out!
         * Of note: GetRelativeFile is case sensitive. Double check you've written the file names correctly */
        Lars_Character_CardBackground = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Backgrounds/Lars_cardbackground.png"));
        Lars_Character_CardFrame = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Backgrounds/Lars_cardframe.png"));
        Lars_Character_Panel = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Backgrounds/Background.png"));
        Lars_Character_Neutral_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Neutral/Neutral_0.png"));
        Lars_Character_Neutral_1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Neutral/Neutral_1.png"));
        Lars_Character_Neutral_2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Neutral/Neutral_2.png"));
        Lars_Character_Neutral_3 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Neutral/Neutral_3.png"));
        Lars_Character_Neutral_4 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Neutral/Neutral_4.png"));
        Lars_Character_Mini_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Mini/MiniPortrait.png"));
        Lars_Character_Squint_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Squint/Squint_0.png"));
        Lars_Character_Squint_1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Squint/Squint_1.png"));
        Lars_Character_Squint_2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Squint/Squint_2.png"));
        Lars_Character_Squint_3 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Squint/Squint_3.png"));
        Lars_Character_Squint_4 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Squint/Squint_4.png"));

        Lars_Character_Rapier_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Rapier/Rapier_0.png"));
        Lars_Character_Rapier_1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Rapier/Rapier_1.png"));
        Lars_Character_Rapier_2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Rapier/Rapier_2.png"));
        Lars_Character_Rapier_3 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Rapier/Rapier_3.png"));
        Lars_Character_Rapier_4 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Rapier/Rapier_4.png"));

        Lars_Character_Blep_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/Blep/Blep.png"));
        Lars_Character_Death = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Lars/GameOver/GameOver.png"));

        /* Decks are assigned separate of the character. This is because the game has decks like Trash which is not related to a playable character
         * Do note that Color accepts a HEX string format (like Color("a1b2c3")) or a Float RGB format (like Color(0.63, 0.7, 0.76). It does NOT allow a traditional RGB format (Meaning Color(161, 178, 195) will NOT work) */
        Lars_Deck = helper.Content.Decks.RegisterDeck("LarsDeck", new DeckConfiguration()
        {
            Definition = new DeckDef()
            {
                /* This color is used in various situations. 
                 * It is used as the deck's rarity 'shine'
                 * If a playable character uses this deck, the character Name will use this color
                 * If a playable character uses this deck, the character mini panel will use this color */
                color = new Color("3d79f2"),

                /* This color is for the card name in-game
                 * Make sure it has a good contrast against the CardFrame, and take rarity 'shine' into account as well */
                titleColor = new Color("000000")
            },
            /* We give it a default art and border some Sprite types by adding '.Sprite' at the end of the ISpriteEntry definitions we made above. */
            DefaultCardArt = Lars_Character_CardBackground.Sprite,
            BorderSprite = Lars_Character_CardFrame.Sprite,

            /* Since this deck will be used by our Demo Character, we'll use their name. */
            Name = AnyLocalizations.Bind(["character", "Lars", "name"]).Localize,
        });

        /* Let's create some animations, because if you were to boot up this mod from what you have above,
         * DemoCharacter would be a blank void inside a box, we haven't added their sprites yet! 
         * We first begin by registering the animations. I know, weird. 'Why are we making animations when we still haven't made the character itself', stick with me, okay? 
         * Animations are actually assigned to Deck types, not Characters! */

        /*Of Note: You may notice we aren't assigning these ICharacterAnimationEntry and ICharacterEntry to any object, unlike stuff above,
        * It's totally fine to assign them if you'd like, but we don't have a reason to so in this mod */
        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = Lars_Deck.UniqueName,
            /* What we registered above was an IDeckEntry object, but when you register a character animation the Helper will ask for you to provide its Deck 'id'
             * This is simple enough, as you can get it from DemoMod_Deck */

            /* The Looptag is the 'name' of the animation. When making shouts and events, and you want your character to show emotions, the LoopTag is what you want
             * In vanilla Cobalt Core, there are 4 'animations' looptags that any character should have: "neutral", "mini", "squint" and "gameover",
             * as these are used in: Neutral is used as default, mini is used in character select and out-of-combat UI, Squink is hardcoded used in certain events, and Gameover is used when your run ends */
            LoopTag = "neutral",

            /* The game doesn't use frames properly when there are only 2 or 3 frames. If you want a proper animation, avoid only adding 2 or 3 frames to it */
            Frames =
            [
                Lars_Character_Neutral_0.Sprite,
                Lars_Character_Neutral_1.Sprite,
                Lars_Character_Neutral_2.Sprite,
                Lars_Character_Neutral_3.Sprite,
                Lars_Character_Neutral_4.Sprite,
            ]
        });
        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = Lars_Deck.UniqueName,
            LoopTag = "mini",
            Frames = new[]
            {
                /* Mini only needs one sprite. We call it animation just because we add it the same way as other expressions. */
                Lars_Character_Mini_0.Sprite
            }
        });
        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = Lars_Deck.UniqueName,
            LoopTag = "squint",
            Frames = new[]
            {
                Lars_Character_Squint_0.Sprite,
                Lars_Character_Squint_1.Sprite,
                Lars_Character_Squint_2.Sprite,
                Lars_Character_Squint_3.Sprite,
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = Lars_Deck.UniqueName,
            LoopTag = "rapier",
            Frames = new[]
            {
                Lars_Character_Rapier_0.Sprite,
                Lars_Character_Rapier_1.Sprite,
                Lars_Character_Rapier_2.Sprite,
                Lars_Character_Rapier_3.Sprite,
                Lars_Character_Rapier_4.Sprite,
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = Lars_Deck.UniqueName,
            LoopTag = "blep",
            Frames = new[]
            {
                Lars_Character_Blep_0.Sprite
            }
        });

        /* Wait, so if we want 'gameover', why doesn't this demo come with the registration for it?
         * Answer: You should be able to use the knowledge you have earned so far to register your own animations! If you'd like, try making the 'gameover' registration code here. You can use whatever sprite you want */

        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = Lars_Deck.UniqueName,
            LoopTag = "gameover",
            Frames = new[]
            {
                Lars_Character_Death.Sprite,
            }
        });

        /* Let's continue with the character creation and finally, actually, register the character! */
        Lars_Character = helper.Content.Characters.V2.RegisterPlayableCharacter("Lars", new PlayableCharacterConfigurationV2()
        {
            /* Same as animations, we want to provide the appropiate Deck type */
            Deck = Lars_Deck.Deck,

            /* The Starter Card Types are, as the name implies, the cards you will start a DemoCharacter run with. 
             * You could provide vanilla cards if you want, but it's way more fun to create your own cards! */
            Starters = new StarterDeck
            {
                cards = [
                    new Flamethrower(),
                    new FireSpin(),
                ],
                artifacts = [
                    new overheatArtifact()
                ]
            },

            /* This is the little blurb that appears when you hover over the character in-game.
             * You can make it fluff, use it as a way to tell players about the character's playstyle, or a little bit of both! */
            Description = AnyLocalizations.Bind(["character", "Lars", "description"]).Localize,

            /* This is the fancy panel that encapsulates your character while in active combat.
             * It's recommended that it follows the same color scheme as the character and deck, for cohesion */
            BorderSprite = Lars_Character_Panel.Sprite
        });


        // Init Aether again
        Aether_Character_CardBackground = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Aether/Backgrounds/Aether_cardbackground.png"));
        Aether_Character_CardFrame = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Aether/Backgrounds/Aether_cardframe.png"));
        Aether_Character_Panel = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Aether/Backgrounds/Background.png"));
        Aether_Character_Neutral_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Aether/Neutral/Neutral_0.png"));
        Aether_Character_Neutral_1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Aether/Neutral/Neutral_1.png"));
        Aether_Character_Neutral_2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Aether/Neutral/Neutral_2.png"));
        Aether_Character_Neutral_3 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Aether/Neutral/Neutral_3.png"));
        Aether_Character_Neutral_4 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Aether/Neutral/Neutral_4.png"));
        Aether_Character_Mini_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Aether/Mini/MiniPortrait.png"));
        Aether_Character_Squint_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Aether/Squint/Squint_0.png"));
        Aether_Character_Squint_1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Aether/Squint/Squint_1.png"));
        Aether_Character_Squint_2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Aether/Squint/Squint_2.png"));
        Aether_Character_Squint_3 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Aether/Squint/Squint_3.png"));
        Aether_Character_Squint_4 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Aether/Squint/Squint_4.png"));
        Aether_Character_Blep_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Aether/Blep/Blep.png"));
        Aether_Character_Death = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Aether/GameOver/GameOver.png"));

        /* Decks are assigned separate of the character. This is because the game has decks like Trash which is not related to a playable character
         * Do note that Color accepts a HEX string format (like Color("a1b2c3")) or a Float RGB format (like Color(0.63, 0.7, 0.76). It does NOT allow a traditional RGB format (Meaning Color(161, 178, 195) will NOT work) */
        Aether_Deck = helper.Content.Decks.RegisterDeck("AetherDeck", new DeckConfiguration()
        {
            Definition = new DeckDef()
            {
                /* This color is used in various situations. 
                 * It is used as the deck's rarity 'shine'
                 * If a playable character uses this deck, the character Name will use this color
                 * If a playable character uses this deck, the character mini panel will use this color */
                color = new Color("f65c76"),

                /* This color is for the card name in-game
                 * Make sure it has a good contrast against the CardFrame, and take rarity 'shine' into account as well */
                titleColor = new Color("000000")
            },
            /* We give it a default art and border some Sprite types by adding '.Sprite' at the end of the ISpriteEntry definitions we made above. */
            DefaultCardArt = Aether_Character_CardBackground.Sprite,
            BorderSprite = Aether_Character_CardFrame.Sprite,

            /* Since this deck will be used by our Demo Character, we'll use their name. */
            Name = AnyLocalizations.Bind(["character", "Aether", "name"]).Localize,
        });

        /* Let's create some animations, because if you were to boot up this mod from what you have above,
         * DemoCharacter would be a blank void inside a box, we haven't added their sprites yet! 
         * We first begin by registering the animations. I know, weird. 'Why are we making animations when we still haven't made the character itself', stick with me, okay? 
         * Animations are actually assigned to Deck types, not Characters! */

        /*Of Note: You may notice we aren't assigning these ICharacterAnimationEntry and ICharacterEntry to any object, unlike stuff above,
        * It's totally fine to assign them if you'd like, but we don't have a reason to so in this mod */
        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = Aether_Deck.UniqueName,
            /* What we registered above was an IDeckEntry object, but when you register a character animation the Helper will ask for you to provide its Deck 'id'
             * This is simple enough, as you can get it from DemoMod_Deck */

            /* The Looptag is the 'name' of the animation. When making shouts and events, and you want your character to show emotions, the LoopTag is what you want
             * In vanilla Cobalt Core, there are 4 'animations' looptags that any character should have: "neutral", "mini", "squint" and "gameover",
             * as these are used in: Neutral is used as default, mini is used in character select and out-of-combat UI, Squink is hardcoded used in certain events, and Gameover is used when your run ends */
            LoopTag = "neutral",

            /* The game doesn't use frames properly when there are only 2 or 3 frames. If you want a proper animation, avoid only adding 2 or 3 frames to it */
            Frames =
            [
                Aether_Character_Neutral_0.Sprite,
                Aether_Character_Neutral_1.Sprite,
                Aether_Character_Neutral_2.Sprite,
                Aether_Character_Neutral_3.Sprite,
                Aether_Character_Neutral_4.Sprite,
            ]
        });
        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = Aether_Deck.UniqueName,
            LoopTag = "mini",
            Frames = new[]
            {
                /* Mini only needs one sprite. We call it animation just because we add it the same way as other expressions. */
                Aether_Character_Mini_0.Sprite
            }
        });
        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = Aether_Deck.UniqueName,
            LoopTag = "squint",
            Frames = new[]
            {
                Aether_Character_Squint_0.Sprite,
                Aether_Character_Squint_1.Sprite,
                Aether_Character_Squint_2.Sprite,
                Aether_Character_Squint_3.Sprite,
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = Aether_Deck.UniqueName,
            LoopTag = "blep",
            Frames = new[]
            {
                Aether_Character_Blep_0.Sprite
            }
        });

        /* Wait, so if we want 'gameover', why doesn't this demo come with the registration for it?
         * Answer: You should be able to use the knowledge you have earned so far to register your own animations! If you'd like, try making the 'gameover' registration code here. You can use whatever sprite you want */

        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = Aether_Deck.UniqueName,
            LoopTag = "gameover",
            Frames = new[]
            {
                Aether_Character_Death.Sprite,
            }
        });

        /* Let's continue with the character creation and finally, actually, register the character! */
        Aether_Character = helper.Content.Characters.V2.RegisterPlayableCharacter("Aether", new PlayableCharacterConfigurationV2()
        {
            /* Same as animations, we want to provide the appropiate Deck type */
            Deck = Aether_Deck.Deck,

            /* The Starter Card Types are, as the name implies, the cards you will start a DemoCharacter run with. 
             * You could provide vanilla cards if you want, but it's way more fun to create your own cards! */
            Starters = new StarterDeck
            {
                cards = [
                    new Bubble(),
                    new AquaRing()
                ]
            },

            /* This is the little blurb that appears when you hover over the character in-game.
             * You can make it fluff, use it as a way to tell players about the character's playstyle, or a little bit of both! */
            Description = AnyLocalizations.Bind(["character", "Aether", "description"]).Localize,

            /* This is the fancy panel that encapsulates your character while in active combat.
             * It's recommended that it follows the same color scheme as the character and deck, for cohesion */
            BorderSprite = Aether_Character_Panel.Sprite
        });



        // Init Solstice again
        Solstice_Character_CardBackground = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Solstice/Backgrounds/Solstice_cardbackground.png"));
        Solstice_Character_CardFrame = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Solstice/Backgrounds/Solstice_cardframe.png"));
        Solstice_Character_Panel = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Solstice/Backgrounds/Background.png"));
        Solstice_Character_Neutral_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Solstice/Neutral/Neutral_0.png"));
        Solstice_Character_Neutral_1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Solstice/Neutral/Neutral_1.png"));
        Solstice_Character_Neutral_2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Solstice/Neutral/Neutral_2.png"));
        Solstice_Character_Neutral_3 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Solstice/Neutral/Neutral_3.png"));
        Solstice_Character_Neutral_4 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Solstice/Neutral/Neutral_4.png"));
        Solstice_Character_Mini_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Solstice/Mini/MiniPortrait.png"));
        Solstice_Character_Squint_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Solstice/Squint/Squint_0.png"));
        Solstice_Character_Squint_1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Solstice/Squint/Squint_1.png"));
        Solstice_Character_Squint_2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Solstice/Squint/Squint_2.png"));
        Solstice_Character_Squint_3 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Solstice/Squint/Squint_3.png"));
        Solstice_Character_Squint_4 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Solstice/Squint/Squint_4.png"));
        Solstice_Character_Blep_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Solstice/Blep/Blep.png"));
        Solstice_Character_Death = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/characters/Solstice/GameOver/GameOver.png"));

        /* Decks are assigned separate of the character. This is because the game has decks like Trash which is not related to a playable character
         * Do note that Color accepts a HEX string format (like Color("a1b2c3")) or a Float RGB format (like Color(0.63, 0.7, 0.76). It does NOT allow a traditional RGB format (Meaning Color(161, 178, 195) will NOT work) */
        Solstice_Deck = helper.Content.Decks.RegisterDeck("SolsticeDeck", new DeckConfiguration()
        {
            Definition = new DeckDef()
            {
                /* This color is used in various situations. 
                 * It is used as the deck's rarity 'shine'
                 * If a playable character uses this deck, the character Name will use this color
                 * If a playable character uses this deck, the character mini panel will use this color */
                color = new Color("78c192"),

                /* This color is for the card name in-game
                 * Make sure it has a good contrast against the CardFrame, and take rarity 'shine' into account as well */
                titleColor = new Color("000000")
            },
            /* We give it a default art and border some Sprite types by adding '.Sprite' at the end of the ISpriteEntry definitions we made above. */
            DefaultCardArt = Solstice_Character_CardBackground.Sprite,
            BorderSprite = Solstice_Character_CardFrame.Sprite,

            /* Since this deck will be used by our Demo Character, we'll use their name. */
            Name = AnyLocalizations.Bind(["character", "Solstice", "name"]).Localize,
        });

        /* Let's create some animations, because if you were to boot up this mod from what you have above,
         * DemoCharacter would be a blank void inside a box, we haven't added their sprites yet! 
         * We first begin by registering the animations. I know, weird. 'Why are we making animations when we still haven't made the character itself', stick with me, okay? 
         * Animations are actually assigned to Deck types, not Characters! */

        /*Of Note: You may notice we aren't assigning these ICharacterAnimationEntry and ICharacterEntry to any object, unlike stuff above,
        * It's totally fine to assign them if you'd like, but we don't have a reason to so in this mod */
        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = Solstice_Deck.UniqueName,
            /* What we registered above was an IDeckEntry object, but when you register a character animation the Helper will ask for you to provide its Deck 'id'
             * This is simple enough, as you can get it from DemoMod_Deck */

            /* The Looptag is the 'name' of the animation. When making shouts and events, and you want your character to show emotions, the LoopTag is what you want
             * In vanilla Cobalt Core, there are 4 'animations' looptags that any character should have: "neutral", "mini", "squint" and "gameover",
             * as these are used in: Neutral is used as default, mini is used in character select and out-of-combat UI, Squink is hardcoded used in certain events, and Gameover is used when your run ends */
            LoopTag = "neutral",

            /* The game doesn't use frames properly when there are only 2 or 3 frames. If you want a proper animation, avoid only adding 2 or 3 frames to it */
            Frames =
            [
                Solstice_Character_Neutral_0.Sprite,
                Solstice_Character_Neutral_1.Sprite,
                Solstice_Character_Neutral_2.Sprite,
                Solstice_Character_Neutral_3.Sprite,
                Solstice_Character_Neutral_4.Sprite,
            ]
        });
        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = Solstice_Deck.UniqueName,
            LoopTag = "mini",
            Frames = new[]
            {
                /* Mini only needs one sprite. We call it animation just because we add it the same way as other expressions. */
                Solstice_Character_Mini_0.Sprite
            }
        });
        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = Solstice_Deck.UniqueName,
            LoopTag = "squint",
            Frames = new[]
            {
                Solstice_Character_Squint_0.Sprite,
                Solstice_Character_Squint_1.Sprite,
                Solstice_Character_Squint_2.Sprite,
                Solstice_Character_Squint_3.Sprite,
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = Solstice_Deck.UniqueName,
            LoopTag = "blep",
            Frames = new[]
            {
                Solstice_Character_Blep_0.Sprite
            }
        });

        /* Wait, so if we want 'gameover', why doesn't this demo come with the registration for it?
         * Answer: You should be able to use the knowledge you have earned so far to register your own animations! If you'd like, try making the 'gameover' registration code here. You can use whatever sprite you want */

        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = Solstice_Deck.UniqueName,
            LoopTag = "gameover",
            Frames = new[]
            {
                Solstice_Character_Death.Sprite,
            }
        });


        Solstice_Character = helper.Content.Characters.V2.RegisterPlayableCharacter("Solstice", new PlayableCharacterConfigurationV2()
        {
            Deck = Solstice_Deck.Deck,

            Starters = new StarterDeck
            {
                cards = [
                    new AttackDroneCard(),
                    new ParallelShift()
                ]
            },

            Description = AnyLocalizations.Bind(["character", "Solstice", "description"]).Localize,

            BorderSprite = Solstice_Character_Panel.Sprite
        });


        /* The basics for a Character mod are done!
         * But you may still have mechanics you want to tackle, such as,
         * 1. How to make cards
         * 2. How to make artifacts
         * 3. How to make ships
         * 4. How to make statuses */

        /* 1. CARDS
         * DemoMod comes with a neat folder called Cards where all the .cs files for our cards are stored. Take a look.
         * You can decide to not use the folder, or to add more folders to further organize your cards. That is up to you.
         * We do recommend keeping files organized, however. It's way easier to traverse a project when the paths are clear and meaningful */

        /* Here we register our cards so we can find them in game.
         * Notice the IDemoCard interface, you can find it in InternalInterfaces.cs
         * Each card in the IEnumerable 'DemoMod_AllCard_Types' will be asked to run their 'Register' method. Open a card's .cs file, and see what it does 
         * We *can* instead register characts one by one, like what we did with the sprites. If you'd like an example of what that looks like, check out the Randall mod by Arin! */

        foreach (var cardType in Lars_AllCard_Types)
            AccessTools.DeclaredMethod(cardType, nameof(IDemoCard.Register))?.Invoke(null, [helper]);
        foreach (var artifactType in CommonArtifacts)
        {
            AccessTools.DeclaredMethod(artifactType, nameof(IDemoArtifact.Register))?.Invoke(null, [helper]);
        }

        foreach (var cardType in Aether_AllCard_Types)
            AccessTools.DeclaredMethod(cardType, nameof(IDemoCard.Register))?.Invoke(null, [helper]);

        foreach (var artifactType in Aether_CommonArtifacts)
        {
            AccessTools.DeclaredMethod(artifactType, nameof(IDemoArtifact.Register))?.Invoke(null, [helper]);
        }

        foreach (var cardType in Solstice_AllCard_Types)
            AccessTools.DeclaredMethod(cardType, nameof(IDemoCard.Register))?.Invoke(null, [helper]);

        foreach (var artifactType in Solstice_CommonArtifacts)
        {
            AccessTools.DeclaredMethod(artifactType, nameof(IDemoArtifact.Register))?.Invoke(null, [helper]);
        }


        /* With the parts and sprites done, we can now create our Ship a bit more easily */

        /* 4. STATUSES
         * You might, now, with all this code behind our backs, start recognizing patterns in the way we can register stuff. */

        AquaRing = helper.Content.Statuses.RegisterStatus("AquaRing", new()
        {
            Definition = new StatusDef
            {
                icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Status/Bubble.png")).Sprite,
                color = new("f65c76"),
                isGood = true
            },
            Name = AnyLocalizations.Bind(["status", "AquaRing", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "AquaRing", "description"]).Localize
        });

        RainDance = helper.Content.Statuses.RegisterStatus("RainDance", new()
        {
            Definition = new StatusDef
            {
                icon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/Status/RainDance.png")).Sprite,
                color = new("f65c76"),
                isGood = true,
            },
            Name = AnyLocalizations.Bind(["status", "RainDance", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "RainDance", "description"]).Localize,

        });



        KokoroApi.V2.ActionCosts.RegisterStatusResourceCostIcon(AquaRing.Status, RegisterSprite(package, "assets/status/Bubble.png").Sprite, RegisterSprite(package, "assets/status/Bubble_cost.png").Sprite);

        _ = new StatusManager();
        _ = new DialogueExtensions();
        _ = new CombatDialogue();
        _ = new CardDialogue();
        _ = new AetherCombatDialogue();
        _ = new AetherCardDialogue();
        _ = new SolsticeCardDialogue();
    }

    public static ISpriteEntry RegisterSprite(IPluginPackage<IModManifest> package, string dir)
    {
        return Instance.Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile(dir));
    }

    internal void initArtifacts()
    {
        new overheatArtifact().ApplyPatches(Harmony);
        new Moisturizer().ApplyPatches(Harmony);
    }

    internal static ArtifactPool[] GetArtifactPools(Type type)
    {
        if (CommonArtifacts.Contains(type) || Aether_CommonArtifacts.Contains(type))
            return [ArtifactPool.Common];
        return [];
    }
    
}
