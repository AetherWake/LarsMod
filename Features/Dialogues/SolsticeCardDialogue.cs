using AetherWake.LarsMod.Cards;
using Nickel;
using System.Collections.Generic;

namespace AetherWake.LarsMod;

internal sealed class SolsticeCardDialogue : BaseDialogue
{
	public SolsticeCardDialogue() : base(locale => ModEntry.Instance.Package.PackageRoot.GetRelativeFile($"i18n/card-dialogue-solstice-{locale}.json").OpenRead())
	{
		var solsticeDeck = ModEntry.Instance.Solstice_Deck.Deck;
        var solsticeType = ModEntry.Instance.Solstice_Character.CharacterType;
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

        newNodes[["ParallelShift", "UpgradeNone", "LarsAether"]] = new()
		{
            
			lookup = [$"Played::{new Cards.DefogShift().Key()}"],
			allPresent = [solsticeType, aetherType, larsType],
			oncePerCombat=false,
            priority=true,
			lines = [
                new Say {who = larsType, loopTag= "blep"},
				new Say { who = aetherType, loopTag = "neutral" },
                new Say { who = solsticeType, loopTag = "neutral"}
			],
		};
	}

	
	
}
