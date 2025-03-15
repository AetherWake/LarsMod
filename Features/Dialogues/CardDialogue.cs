using Nickel;
using System.Collections.Generic;

namespace AetherWake.LarsMod;

internal sealed class CardDialogue : BaseDialogue
{
	public CardDialogue() : base(locale => ModEntry.Instance.Package.PackageRoot.GetRelativeFile($"i18n/card-dialogue-lars-{locale}.json").OpenRead())
	{
		var larsDeck = ModEntry.Instance.DemoMod_Deck.Deck;
		var larsType = ModEntry.Instance.Lars_Character.CharacterType;
		var newNodes = new Dictionary<IReadOnlyList<string>, StoryNode>();

		ModEntry.Instance.Helper.Events.OnModLoadPhaseFinished += (_, phase) =>
		{
			if (phase != ModLoadPhase.AfterDbInit)
				return;
			InjectStory(newNodes, [], [], NodeType.combat);
		};
		ModEntry.Instance.Helper.Events.OnLoadStringsForLocale += (_, e) => InjectLocalizations(newNodes, [], [], e);

		newNodes[["Rapier", "UpgradeNone", "Basic"]] = new()
		{
            
			lookup = [$"Played::{new Cards.Rapier().Key()}"],
			priority = true,
			oncePerRun = true,
			allPresent = [larsType, Deck.eunice.Key(), Deck.riggs.Key()],

			lines = [
				new Say { who = larsType, loopTag = "neutral" },
				new SaySwitch(){
					lines = [
						new Say { who = Deck.riggs.Key(), loopTag = "neutral"},
						new Say { who = Deck.eunice.Key(), loopTag = "neutral"},
					]
				},
				new SaySwitch(){
					lines = [
						new Say { who = Deck.riggs.Key(), loopTag = "neutral"},
						new Say { who = Deck.eunice.Key(), loopTag = "neutral"},
					]
				},
			],
		};

        newNodes[["Rapier", "UpgradeNone", "Dizzy"]] = new()
		{
            
			lookup = [$"Played::{new Cards.Rapier().Key()}"],
			priority = true,
			oncePerRun = true,
			allPresent = [larsType, Deck.dizzy.Key()],

			lines = [
				new Say { who = larsType, loopTag = "neutral" },
                
                new Say { who = Deck.dizzy.Key(), loopTag = "squint"}
			],
		};


        newNodes[["Rapier", "UpgradeA", "Basic"]] = new()
		{
            
			lookup = [$"Played::{new Cards.Rapier().Key()}_A"],
			priority = false,
			oncePerCombat=true,
			allPresent = [larsType, Deck.peri.Key()],

			lines = [
				new Say { who = Deck.peri.Key(), loopTag = "squint" },
                new Say { who = larsType, loopTag = "blep"}
			],
		};
	}

	
	
}