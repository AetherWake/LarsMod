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
			allPresent = [larsType],
            nonePresent = [Deck.dizzy.Key()],

			lines = [
				new Say { who = larsType, loopTag = "idle" },
                new Say { who = "Crew", loopTag = "squint"},
                new Say { who = "Crew", loopTag = "squint"}
			],
		};

        newNodes[["Rapier", "UpgradeNone", "Dizzy"]] = new()
		{
            
			lookup = [$"Played::{new Cards.Rapier().Key()}"],
			priority = true,
			oncePerRun = true,
			allPresent = [larsType, Deck.dizzy.Key()],

			lines = [
				new Say { who = larsType, loopTag = "idle" },
                new Say { who = "Crew", loopTag = "squint"},
                new Say { who = Deck.dizzy.Key(), loopTag = "squint"}
			],
		};
	}
	
}