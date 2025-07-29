using Nickel;
using System.Collections.Generic;

namespace AetherWake.LarsMod;

internal sealed class AetherCardDialogue : BaseDialogue
{
	public AetherCardDialogue() : base(locale => ModEntry.Instance.Package.PackageRoot.GetRelativeFile($"i18n/card-dialogue-aether-{locale}.json").OpenRead())
	{
		var aetherDeck = ModEntry.Instance.Aether_Deck.Deck;
		var aetherType = ModEntry.Instance.Aether_Character.CharacterType;
        var larsType = ModEntry.Instance.Lars_Character.CharacterType;
		var newNodes = new Dictionary<IReadOnlyList<string>, StoryNode>();

		ModEntry.Instance.Helper.Events.OnModLoadPhaseFinished += (_, phase) =>
		{
			if (phase != ModLoadPhase.AfterDbInit)
				return;
			InjectStory(newNodes, [], [], NodeType.combat, aetherType);
		};
		ModEntry.Instance.Helper.Events.OnLoadStringsForLocale += (_, e) => InjectLocalizations(newNodes, [], [], aetherType, e);

        newNodes[["DragonTail", "UpgradeNone", "Lars"]] = new()
		{
            
			lookup = [$"Played::{new Cards.ADragonTail().Key()}"],
			allPresent = [aetherType, larsType],

			lines = [
				new Say { who = larsType, loopTag = "squint" },
                new Say { who = aetherType, loopTag = "neutral"}
			],
		};
	}

	
	
}
