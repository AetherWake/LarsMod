using Nickel;
using System.Collections.Generic;

namespace AetherWake.LarsMod;

internal sealed class SolsticeCombatDialogue : BaseDialogue
{
	public SolsticeCombatDialogue() : base(locale => ModEntry.Instance.Package.PackageRoot.GetRelativeFile($"i18n/combat-dialogue-lars-{locale}.json").OpenRead())
	{
		var solsticeDeck = ModEntry.Instance.Solstice_Deck.Deck;
		var solsticeType = ModEntry.Instance.Solstice_Character!.CharacterType;
		var newNodes = new Dictionary<IReadOnlyList<string>, StoryNode>();
		var saySwitchNodes = new Dictionary<IReadOnlyList<string>, Say>();

		ModEntry.Instance.Helper.Events.OnModLoadPhaseFinished += (_, phase) =>
		{
			if (phase != ModLoadPhase.AfterDbInit)
				return;
			InjectStory(newNodes, [], saySwitchNodes, NodeType.combat, solsticeType);
		};
		ModEntry.Instance.Helper.Events.OnLoadStringsForLocale += (_, e) => InjectLocalizations(newNodes, [], saySwitchNodes, solsticeType, e);

        /*
        To add a new dialogue, you need to use a piece of code like this 
        newNodes[["TookDamage", "Dizzy"]] = new() // the strings inside the "newNodes" variables is a tree of keys that reference to the file in i18n
		{
			enemyShotJustHit = true,    //The values you add here modify the conditions of when the dialogue can be triggered
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [larsType, Deck.dizzy.Key()],
			lines = [
                // Inside the lines variable, you can create new Say. Each say takes a {who} which refers to the character speaking a line, and a {loopTag} that defines the animation to be played
                // In this example, the file in i18n has:
                //   "Alright. Enough is enough. I'm contacting security.",  
                //   "I'm security!" 
                // The order of says directly refers to the order of the lines in the i18n file
                // In this example

				new Say { who = larsType, loopTag = "fiddling" }, // Lars is saying "Alright. Enough is enough. I'm contacting security."  with the "fiddling" loop tag
				new Say { who = Deck.dizzy.Key(), loopTag = "squint" }, // And now Dizzy is saying "I'm security!" with the "squint" looptag
			],
		};
        */


		#region TookDamage
		
		newNodes[["TookDamage", "Basic", "0"]] = new()
		{
			
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};
		newNodes[["TookDamage", "Basic", "1"]] = new()
		{

			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			lines = [
				new Say { who = solsticeType, loopTag = "squint" },
			],
		};
		newNodes[["TookDamage", "Basic", "2"]] = new()
		{

			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};

		newNodes[["TookDamage", "Dizzy"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [solsticeType, Deck.dizzy.Key()],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
				new Say { who = Deck.dizzy.Key(), loopTag = "neutral" },
			],
		};
		newNodes[["TookDamage", "Riggs"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [solsticeType, Deck.riggs.Key()],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
				new Say { who = Deck.riggs.Key(), loopTag = "neutral" },
			],
		};
		newNodes[["TookDamage", "Peri"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [solsticeType, Deck.peri.Key()],
			lines = [
				new Say { who = solsticeType, loopTag = "squint" },
				new Say { who = Deck.peri.Key(), loopTag = "neutral" },
			],
		};
		newNodes[["TookDamage", "Isaac"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [solsticeType, Deck.goat.Key()],
			lines = [
				new Say { who = Deck.goat.Key(), loopTag = "squint" },
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};
		newNodes[["TookDamage", "Drake"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [solsticeType, Deck.eunice.Key()],
			lines = [
				new Say { who = Deck.eunice.Key(), loopTag = "squint" },
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};
		newNodes[["TookDamage", "Max"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [solsticeType, Deck.hacker.Key()],
			lines = [
				new Say { who = Deck.hacker.Key(), loopTag = "mad" },
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};
		newNodes[["TookDamage", "Books"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [solsticeType, Deck.shard.Key()],
			lines = [
				new Say { who = solsticeType, loopTag = "normal" },
				new Say { who = Deck.shard.Key(), loopTag = "intense" },
			],
		};
		newNodes[["TookDamage", "CAT"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [solsticeType, "comp"],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
				new Say { who = "comp", loopTag = "grumpy" },
			],
		};
		#endregion

		for (var i = 0; i < 5; i++)
			newNodes[["TookNonHullDamage", "Basic", i.ToString()]] = new StoryNode()
			{
				enemyShotJustHit = true,
				maxDamageDealtToPlayerThisTurn = 0,
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			};

		newNodes[["TookNonHullDamage", "Dizzy"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 0,
			allPresent = [solsticeType, Deck.dizzy.Key()],
			oncePerRun = true,
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
				new Say { who = Deck.dizzy.Key(), loopTag = "neutral" },
			],
		};

		#region DealtDamage
		for (var i = 0; i < 5; i++)
			newNodes[["DealtDamage", "Basic", i.ToString()]] = new()
			{
				playerShotJustHit = true,
				minDamageDealtToEnemyThisTurn = 1,
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			};

		newNodes[["DealtDamage", "Dizzy"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = solsticeDeck,
			allPresent = [solsticeType, Deck.dizzy.Key()],
			lines = [
				new Say { who = Deck.dizzy.Key(), loopTag = "neutral" },
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};
		newNodes[["DealtDamage", "Riggs"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = solsticeDeck,
			allPresent = [solsticeType, Deck.riggs.Key()],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
				new Say { who = Deck.riggs.Key(), loopTag = "neutral" },
			],
		};
		newNodes[["DealtDamage", "Peri"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = solsticeDeck,
			allPresent = [solsticeType, Deck.peri.Key()],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
				new Say { who = Deck.peri.Key(), loopTag = "squint" },
			],
		};
		newNodes[["DealtDamage", "Isaac"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = solsticeDeck,
			allPresent = [solsticeType, Deck.goat.Key()],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
				new Say { who = Deck.goat.Key(), loopTag = "neutral" },
			],
		};
		newNodes[["DealtDamage", "Drake", "0"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = solsticeDeck,
			allPresent = [solsticeType, Deck.eunice.Key()],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
				new Say { who = Deck.eunice.Key(), loopTag = "neutral" },
			],
		};
		newNodes[["DealtDamage", "Drake", "1"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = Deck.eunice,
			allPresent = [solsticeType, Deck.eunice.Key()],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
				new Say { who = Deck.eunice.Key(), loopTag = "sly" }, // Might not be the real name of the loopTag, uh... I expect things to break. Neutral works, too. -LP
			],
		};
		newNodes[["DealtDamage", "Max"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = solsticeDeck,
			allPresent = [solsticeType, Deck.hacker.Key()],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
				new Say { who = Deck.hacker.Key(), loopTag = "squint" },
			],
		};
		newNodes[["DealtDamage", "Books"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = solsticeDeck,
			allPresent = [solsticeType, Deck.shard.Key()],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
				new Say { who = Deck.shard.Key(), loopTag = "stoked" },
			],
		};
		newNodes[["DealtDamage", "CAT"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = solsticeDeck,
			allPresent = [solsticeType, "comp"],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
				new Say { who = "comp", loopTag = "squint" },
			],
		};
		#endregion

		for (var i = 0; i < 5; i++)
			newNodes[["DealtBigDamage", "Basic", i.ToString()]] = new()
			{
				playerShotJustHit = true,
				minDamageDealtToEnemyThisTurn = 6,
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 3; i++)
			newNodes[["ShieldedDamage", "Basic", i.ToString()]] = new StoryNode()
			{
				enemyShotJustHit = true,
				maxDamageDealtToPlayerThisTurn = 0,
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			}.SetMinShieldLostThisTurn(1);

		newNodes[["Missed", "Basic", "0"]] = new()
		{
			playerShotJustMissed = true,
			allPresent = [solsticeType],
			lines = [
				new Say { who = solsticeType, loopTag = "squint" },
			],
		};
		newNodes[["Missed", "Basic", "1"]] = new()
		{
			playerShotJustMissed = true,
			allPresent = [solsticeType],
			lines = [
				new Say { who = solsticeType, loopTag = "squint" },
			],
		};
		newNodes[["Missed", "Basic", "2"]] = new()
		{
			playerShotJustMissed = true,
			allPresent = [solsticeType],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};
		newNodes[["Missed", "Basic", "3"]] = new()
		{
			playerShotJustMissed = true,
			allPresent = [solsticeType],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};

		#region AboutToDie
		newNodes[["AboutToDie", "Basic", "0"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [solsticeType],
			lines = [
				new Say { who = solsticeType, loopTag = "squint" },
			],
		};
		newNodes[["AboutToDie", "Basic", "1"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [solsticeType],
			lines = [
				new Say { who = solsticeType, loopTag = "squint" },
			],
		};
		newNodes[["AboutToDie", "Basic", "2"]] = new()
		{
			maxHull = 1,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [solsticeType],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};

		newNodes[["AboutToDie", "Dizzy"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [solsticeType, Deck.dizzy.Key()],
			lines = [
				new Say { who = Deck.dizzy.Key(), loopTag = "squint" },
				new Say { who = solsticeType, loopTag = "squint" },
			],
		};
		newNodes[["AboutToDie", "Riggs"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [solsticeType, Deck.riggs.Key()],
			lines = [
				new Say { who = Deck.riggs.Key(), loopTag = "nervous" },
				new Say { who = solsticeType, loopTag = "squint" },
			],
		};
		newNodes[["AboutToDie", "Peri"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [solsticeType, Deck.peri.Key()],
			lines = [
				new Say { who = Deck.peri.Key(), loopTag = "mad" },
				new Say { who = solsticeType, loopTag = "squint" },
			],
		};
		newNodes[["AboutToDie", "Isaac"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [solsticeType, Deck.goat.Key()],
			lines = [
				new Say { who = Deck.goat.Key(), loopTag = "panic" },
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};
		newNodes[["AboutToDie", "Drake"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [solsticeType, Deck.eunice.Key()],
			lines = [
				new Say { who = Deck.eunice.Key(), loopTag = "mad" },
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};
		newNodes[["AboutToDie", "Max"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [solsticeType, Deck.hacker.Key()],
			lines = [
				new Say { who = Deck.hacker.Key(), loopTag = "mad" },
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};
		newNodes[["AboutToDie", "Books"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [solsticeType, Deck.shard.Key()],
			lines = [
				new Say { who = Deck.shard.Key(), loopTag = "intense" },
				new Say { who = solsticeType, loopTag = "squint" },
			],
		};
		newNodes[["AboutToDie", "CAT"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [solsticeType, "comp"],
			lines = [
				new Say { who = solsticeType, loopTag = "squint" },
				new Say { who = "comp", loopTag = "squint" },
			],
		};
		#endregion

		for (var i = 0; i < 4; i++)
			newNodes[["HitArmor", "Basic", i.ToString()]] = new()
			{
				playerShotJustHit = true,
				minDamageBlockedByEnemyArmorThisTurn = 1,
				oncePerCombat = true,
				oncePerRun = true,
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 2; i++)
			newNodes[["ExcessEnergy", "Basic", i.ToString()]] = new()
			{
				handEmpty = true,
				minEnergy = 1,
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "squint" },
				],
			};

		for (var i = 0; i < 2; i++)
			newNodes[["EmptyHand", "Basic", i.ToString()]] = new()
			{
				handEmpty = true,
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 2; i++)
			newNodes[["TrashHand", "Basic", i.ToString()]] = new()
			{
				handFullOfTrash = true,
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "squint" },
				],
			};

		for (var i = 0; i < 1; i++)
			newNodes[["PlayedRecycle", "Basic", i.ToString()]] = new()
			{
				lookup = [$"{ModEntry.Instance.Package.Manifest.UniqueName}::PlayedRecycle"],
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 1; i++)
			newNodes[["NewNonLarsNonTrashTempCard", "Basic", i.ToString()]] = new()
			{
				lookup = [$"{ModEntry.Instance.Package.Manifest.UniqueName}::NewNonLarsNonTrashTempCard"],
				oncePerCombat = true,
				oncePerCombatTags = [$"{ModEntry.Instance.Package.Manifest.UniqueName}::NewNonLarsNonTrashTempCard"],
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 4; i++)
			newNodes[["StartedBattle", "Basic", i.ToString()]] = new()
			{
				turnStart = true,
				maxTurnsThisCombat = 1,
				oncePerCombat = true,
				oncePerRun = true,
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			};
			newNodes[["StartedBattle", "Peri"]] = new()
			{
				turnStart = true,
				maxTurnsThisCombat = 1,
				oncePerCombat = true,
				oncePerRun = true,
				allPresent = [solsticeType, Deck.peri.Key()],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
					new Say { who = Deck.peri.Key(), loopTag = "squint" },
				],
			};

		for (var i = 0; i < 4; i++)
			newNodes[["NoOverlap", "Basic", i.ToString()]] = new()
			{
				priority = true,
				shipsDontOverlapAtAll = true,
				oncePerCombatTags = ["NoOverlapBetweenShips"],
				oncePerRun = true,
				nonePresent = ["crab", "scrap"],
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			};

		newNodes[["NoOverlapButSeeker", "Riggs"]] = new()
		{
			priority = true,
			shipsDontOverlapAtAll = true,
			oncePerCombatTags = ["NoOverlapBetweenShipsSeeker"],
			oncePerRun = true,
			anyDronesHostile = ["missile_seeker"],
			nonePresent = ["crab"],
			allPresent = [solsticeType, Deck.riggs.Key()],
			lines = [
				new Say { who = solsticeType, loopTag = "squint" },
				new Say { who = Deck.riggs.Key(), loopTag = "neutral" },
			],
		};

		for (var i = 0; i < 4; i++)
			newNodes[["LongFight", "Basic", i.ToString()]] = new()
			{
				minTurnsThisCombat = 20,
				oncePerCombatTags = ["manyTurns"],
				oncePerRun = true,
				turnStart = true,
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			};

		newNodes[["GoingMissing", "Basic"]] = new()
		{
			priority = true,
			lastTurnPlayerStatuses = [ModEntry.Instance.Lars_Character.MissingStatus.Status],
			oncePerCombatTags = ["LarsWentMissing"],
			oncePerRun = true,
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};

		for (var i = 0; i < 4; i++)
			newNodes[["ReturningFromMissing", "Basic", i.ToString()]] = new()
			{
				priority = true,
				lookup = [$"{ModEntry.Instance.Package.Manifest.UniqueName}::ReturningFromMissing"],
				oncePerRun = true,
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			};

		#region DealtDamage
		for (var i = 0; i < 4; i++)
			newNodes[["GoingToOverheat", "Basic", i.ToString()]] = new()
			{
				goingToOverheat = true,
				oncePerCombatTags = ["OverheatGeneric"],
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			};

		newNodes[["GoingToOverheat", "Drake"]] = new()
		{
			goingToOverheat = true,
			oncePerCombatTags = ["OverheatGeneric"],
			allPresent = [solsticeType, Deck.eunice.Key()],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
				new Say { who = Deck.eunice.Key(), loopTag = "neutral" },
			],
		};
		#endregion

		newNodes[["Recalibrator", "Basic"]] = new()
		{
			playerShotJustMissed = true,
			hasArtifacts = ["Recalibrator"],
			allPresent = [solsticeType],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};

		newNodes[["StartedBattleAgainstBigCrystal", "Basic"]] = new()
		{
			priority = true,
			turnStart = true,
			oncePerRun = true,
			requiredScenes = ["Crystal_1", "Crystal_1_1"],
			excludedScenes = ["Crystal_2"],
			allPresent = [solsticeType, "crystal"],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};
		// TODO Check crab strings not working in game 
		saySwitchNodes[["CrabFacts1_Multi_0"]] = new() 
		{
			who = solsticeType,
			loopTag = "neutral"
		};
		saySwitchNodes[["CrabFacts2_Multi_0"]] = new()
		{
			who = solsticeType,
			loopTag = "neutral"
		};
		saySwitchNodes[["CrabFactsAreOverNow_Multi_0"]] = new()
		{
			who = solsticeType,
			loopTag = "neutral"
		};
	}
}
