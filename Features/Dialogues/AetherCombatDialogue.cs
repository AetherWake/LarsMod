using Nickel;
using System.Collections.Generic;

namespace AetherWake.LarsMod;

internal sealed class AetherCombatDialogue : BaseDialogue
{
	public AetherCombatDialogue() : base(locale => ModEntry.Instance.Package.PackageRoot.GetRelativeFile($"i18n/combat-dialogue-aether-{locale}.json").OpenRead())
	{
		var aetherDeck = ModEntry.Instance.Aether_Deck.Deck;
		var aetherType = ModEntry.Instance.Aether_Character.CharacterType;
		var larsType = ModEntry.Instance.Lars_Character.CharacterType;

		var newNodes = new Dictionary<IReadOnlyList<string>, StoryNode>();
		var saySwitchNodes = new Dictionary<IReadOnlyList<string>, Say>();

		ModEntry.Instance.Helper.Events.OnModLoadPhaseFinished += (_, phase) =>
		{
			if (phase != ModLoadPhase.AfterDbInit)
				return;
			InjectStory(newNodes, [], saySwitchNodes, NodeType.combat, aetherType);
		};
		ModEntry.Instance.Helper.Events.OnLoadStringsForLocale += (_, e) => InjectLocalizations(newNodes, [], saySwitchNodes, aetherType, e);

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
		
		newNodes[["Aether", "TookDamage", "Basic", "0"]] = new()
		{
			
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
			],
		};
		newNodes[["Aether", "TookDamage", "Basic", "1"]] = new()
		{

			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			lines = [
				new Say { who = aetherType, loopTag = "squint" },
			],
		};
		newNodes[["Aether", "TookDamage", "Basic", "2"]] = new()
		{

			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
			],
		};

		newNodes[["Aether", "TookDamage", "Dizzy"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [aetherType, Deck.dizzy.Key()],
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
				new Say { who = Deck.dizzy.Key(), loopTag = "neutral" },
			],
		};
		newNodes[["Aether", "TookDamage", "Riggs"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [aetherType, Deck.riggs.Key()],
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
				new Say { who = Deck.riggs.Key(), loopTag = "neutral" },
			],
		};
		newNodes[["Aether", "TookDamage", "Peri"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [aetherType, Deck.peri.Key()],
			lines = [
				new Say { who = aetherType, loopTag = "squint" },
				new Say { who = Deck.peri.Key(), loopTag = "neutral" },
			],
		};
		newNodes[["Aether", "TookDamage", "Isaac"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [aetherType, Deck.goat.Key()],
			lines = [
				new Say { who = Deck.goat.Key(), loopTag = "squint" },
				new Say { who = aetherType, loopTag = "neutral" },
			],
		};
		newNodes[["Aether", "TookDamage", "Drake"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [aetherType, Deck.eunice.Key()],
			lines = [
				new Say { who = Deck.eunice.Key(), loopTag = "squint" },
				new Say { who = aetherType, loopTag = "neutral" },
			],
		};
		newNodes[["Aether", "TookDamage", "Max"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [aetherType, Deck.hacker.Key()],
			lines = [
				new Say { who = Deck.hacker.Key(), loopTag = "mad" },
				new Say { who = aetherType, loopTag = "neutral" },
			],
		};
		newNodes[["Aether", "TookDamage", "Books"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [aetherType, Deck.shard.Key()],
			lines = [
				new Say { who = aetherType, loopTag = "normal" },
				new Say { who = Deck.shard.Key(), loopTag = "intense" },
			],
		};
		newNodes[["Aether", "TookDamage", "CAT"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [aetherType, "comp"],
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
				new Say { who = "comp", loopTag = "grumpy" },
			],
		};
        
        newNodes[["Aether", "TookDamage", "Lars"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [aetherType, larsType],
			lines = [
				new Say { who = larsType, loopTag = "neutral" },
				new Say { who = aetherType, loopTag = "neutral" },
			],
		};

		#endregion

        for (var i = 0; i < 3; i++)
            newNodes[["Aether", "TookNonHullDamage", "Basic", i.ToString()]] = new StoryNode()
            {
                enemyShotJustHit = true,
                maxDamageDealtToPlayerThisTurn = 0,
                allPresent = [aetherType],
                lines = [
                    new Say { who = aetherType, loopTag = "neutral" },
                ],
            };


		#region DealtDamage
		for (var i = 0; i < 3; i++)
			newNodes[["Aether", "DealtDamage", "Basic", i.ToString()]] = new()
			{
				playerShotJustHit = true,
				minDamageDealtToEnemyThisTurn = 1,
				allPresent = [aetherType],
				lines = [
					new Say { who = aetherType, loopTag = "neutral" },
				],
			};

		newNodes[["Aether", "DealtDamage", "Dizzy"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = aetherDeck,
			allPresent = [aetherType, Deck.dizzy.Key()],
			lines = [
				new Say { who = Deck.dizzy.Key(), loopTag = "neutral" },
				new Say { who = aetherType, loopTag = "neutral" },
			],
		};
		newNodes[["Aether", "DealtDamage", "Riggs"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = aetherDeck,
			allPresent = [aetherType, Deck.riggs.Key()],
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
				new Say { who = Deck.riggs.Key(), loopTag = "neutral" },
			],
		};
		newNodes[["Aether", "DealtDamage", "Peri"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = aetherDeck,
			allPresent = [aetherType, Deck.peri.Key()],
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
				new Say { who = Deck.peri.Key(), loopTag = "squint" },
			],
		};
		newNodes[["Aether", "DealtDamage", "Isaac"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = aetherDeck,
			allPresent = [aetherType, Deck.goat.Key()],
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
				new Say { who = Deck.goat.Key(), loopTag = "neutral" },
			],
		};
		newNodes[["Aether", "DealtDamage", "Drake", "0"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = Deck.eunice,
			allPresent = [aetherType, Deck.eunice.Key()],
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
				new Say { who = Deck.eunice.Key(), loopTag = "sly" }, // Might not be the real name of the loopTag, uh... I expect things to break. Neutral works, too. -LP
			],
		};
		newNodes[["Aether", "DealtDamage", "Max"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = aetherDeck,
			allPresent = [aetherType, Deck.hacker.Key()],
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
				new Say { who = Deck.hacker.Key(), loopTag = "squint" },
			],
		};
		newNodes[["Aether", "DealtDamage", "Books"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = aetherDeck,
			allPresent = [aetherType, Deck.shard.Key()],
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
				new Say { who = Deck.shard.Key(), loopTag = "stoked" },
			],
		};
		newNodes[["Aether", "DealtDamage", "CAT"]] = new()
		{
			playerShotJustHit = true,
			minDamageDealtToEnemyThisTurn = 1,
			whoDidThat = aetherDeck,
			allPresent = [aetherType, "comp"],
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
				new Say { who = "comp", loopTag = "squint" },
			],
		};
		#endregion

		for (var i = 0; i < 4; i++)
			newNodes[["Aether", "DealtBigDamage", "Basic", i.ToString()]] = new()
			{
				playerShotJustHit = true,
				minDamageDealtToEnemyThisTurn = 6,
				allPresent = [aetherType],
				lines = [
					new Say { who = aetherType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 3; i++)
			newNodes[["Aether", "ShieldedDamage", "Basic", i.ToString()]] = new StoryNode()
			{
				enemyShotJustHit = true,
				maxDamageDealtToPlayerThisTurn = 0,
				allPresent = [aetherType],
				lines = [
					new Say { who = aetherType, loopTag = "neutral" },
				],
			}.SetMinShieldLostThisTurn(1);

		newNodes[["Aether", "Missed", "Basic", "0"]] = new()
		{
			playerShotJustMissed = true,
			allPresent = [aetherType],
			lines = [
				new Say { who = aetherType, loopTag = "squint" },
			],
		};
		newNodes[["Aether", "Missed", "Basic", "1"]] = new()
		{
			playerShotJustMissed = true,
			allPresent = [aetherType],
			lines = [
				new Say { who = aetherType, loopTag = "squint" },
			],
		};
		newNodes[["Aether", "Missed", "Basic", "2"]] = new()
		{
			playerShotJustMissed = true,
			allPresent = [aetherType],
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
			],
		};
		newNodes[["Aether", "Missed", "Basic", "3"]] = new()
		{
			playerShotJustMissed = true,
			allPresent = [aetherType],
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
			],
		};

		#region AboutToDie
		newNodes[["Aether", "AboutToDie", "Basic", "0"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [aetherType],
			lines = [
				new Say { who = aetherType, loopTag = "squint" },
			],
		};
		newNodes[["Aether", "AboutToDie", "Basic", "1"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [aetherType],
			lines = [
				new Say { who = aetherType, loopTag = "squint" },
			],
		};
		newNodes[["Aether", "AboutToDie", "Basic", "2"]] = new()
		{
			maxHull = 1,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [aetherType],
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
			],
		};

		newNodes[["Aether", "AboutToDie", "Dizzy"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [aetherType, Deck.dizzy.Key()],
			lines = [
				new Say { who = Deck.dizzy.Key(), loopTag = "squint" },
				new Say { who = aetherType, loopTag = "squint" },
			],
		};
		newNodes[["Aether", "AboutToDie", "Riggs"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [aetherType, Deck.riggs.Key()],
			lines = [
				new Say { who = Deck.riggs.Key(), loopTag = "nervous" },
				new Say { who = aetherType, loopTag = "squint" },
			],
		};
		newNodes[["Aether", "AboutToDie", "Peri"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [aetherType, Deck.peri.Key()],
			lines = [
				new Say { who = Deck.peri.Key(), loopTag = "mad" },
				new Say { who = aetherType, loopTag = "squint" },
			],
		};
		newNodes[["Aether", "AboutToDie", "Isaac"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [aetherType, Deck.goat.Key()],
			lines = [
				new Say { who = Deck.goat.Key(), loopTag = "panic" },
				new Say { who = aetherType, loopTag = "neutral" },
			],
		};
		newNodes[["Aether", "AboutToDie", "Drake"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [aetherType, Deck.eunice.Key()],
			lines = [
				new Say { who = Deck.eunice.Key(), loopTag = "mad" },
				new Say { who = aetherType, loopTag = "neutral" },
			],
		};
		newNodes[["Aether", "AboutToDie", "Max"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [aetherType, Deck.hacker.Key()],
			lines = [
				new Say { who = Deck.hacker.Key(), loopTag = "mad" },
				new Say { who = aetherType, loopTag = "neutral" },
			],
		};
		newNodes[["Aether", "AboutToDie", "Books"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [aetherType, Deck.shard.Key()],
			lines = [
				new Say { who = Deck.shard.Key(), loopTag = "intense" },
				new Say { who = aetherType, loopTag = "squint" },
			],
		};
		newNodes[["Aether", "AboutToDie", "CAT"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [aetherType, "comp"],
			lines = [
				new Say { who = aetherType, loopTag = "squint" },
				new Say { who = "comp", loopTag = "squint" },
			],
		};
		#endregion

		for (var i = 0; i < 4; i++)
			newNodes[["Aether", "HitArmor", "Basic", i.ToString()]] = new()
			{
				playerShotJustHit = true,
				minDamageBlockedByEnemyArmorThisTurn = 1,
				oncePerCombat = true,
				oncePerRun = true,
				allPresent = [aetherType],
				lines = [
					new Say { who = aetherType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 2; i++)
			newNodes[["Aether", "ExcessEnergy", "Basic", i.ToString()]] = new()
			{
				handEmpty = true,
				minEnergy = 1,
				allPresent = [aetherType],
				lines = [
					new Say { who = aetherType, loopTag = "squint" },
				],
			};

		for (var i = 0; i < 2; i++)
			newNodes[["Aether", "EmptyHand", "Basic", i.ToString()]] = new()
			{
				handEmpty = true,
				allPresent = [aetherType],
				lines = [
					new Say { who = aetherType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 2; i++)
			newNodes[["Aether", "TrashHand", "Basic", i.ToString()]] = new()
			{
				handFullOfTrash = true,
				allPresent = [aetherType],
				lines = [
					new Say { who = aetherType, loopTag = "squint" },
				],
			};

		for (var i = 0; i < 1; i++)
			newNodes[["Aether", "PlayedRecycle", "Basic", i.ToString()]] = new()
			{
				lookup = [$"{ModEntry.Instance.Package.Manifest.UniqueName}::PlayedRecycle"],
				allPresent = [aetherType],
				oncePerCombat=true,
				lines = [
					new Say { who = aetherType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 1; i++)
			newNodes[["Aether", "NewNonLarsNonTrashTempCard", "Basic", i.ToString()]] = new()
			{
				lookup = [$"{ModEntry.Instance.Package.Manifest.UniqueName}::NewNonLarsNonTrashTempCard"],
				oncePerCombat = true,
				oncePerCombatTags = [$"{ModEntry.Instance.Package.Manifest.UniqueName}::NewNonLarsNonTrashTempCard"],
				allPresent = [aetherType],
				lines = [
					new Say { who = aetherType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 4; i++)
			newNodes[["Aether", "StartedBattle", "Basic", i.ToString()]] = new()
			{
				turnStart = true,
				maxTurnsThisCombat = 1,
				oncePerCombat = true,
				oncePerRun = true,
				allPresent = [aetherType],
				lines = [
					new Say { who = aetherType, loopTag = "neutral" },
				],
			};
			newNodes[["Aether", "StartedBattle", "Peri"]] = new()
			{
				turnStart = true,
				maxTurnsThisCombat = 1,
				oncePerCombat = true,
				oncePerRun = true,
				allPresent = [aetherType, Deck.peri.Key()],
				lines = [
					new Say { who = aetherType, loopTag = "neutral" },
					new Say { who = Deck.peri.Key(), loopTag = "squint" },
				],
			};

		for (var i = 0; i < 4; i++)
			newNodes[["Aether", "NoOverlap", "Basic", i.ToString()]] = new()
			{
				priority = true,
				shipsDontOverlapAtAll = true,
				oncePerCombatTags = ["NoOverlapBetweenShips"],
				oncePerRun = true,
				nonePresent = ["crab", "scrap"],
				allPresent = [aetherType],
				lines = [
					new Say { who = aetherType, loopTag = "neutral" },
				],
			};

		newNodes[["Aether", "NoOverlapButSeeker", "Riggs"]] = new()
		{
			priority = true,
			shipsDontOverlapAtAll = true,
			oncePerCombatTags = ["NoOverlapBetweenShipsSeeker"],
			oncePerRun = true,
			anyDronesHostile = ["missile_seeker"],
			nonePresent = ["crab"],
			allPresent = [aetherType, Deck.riggs.Key()],
			lines = [
				new Say { who = aetherType, loopTag = "squint" },
				new Say { who = Deck.riggs.Key(), loopTag = "neutral" },
			],
		};

		for (var i = 0; i < 4; i++)
			newNodes[["Aether", "LongFight", "Basic", i.ToString()]] = new()
			{
				minTurnsThisCombat = 20,
				oncePerCombatTags = ["manyTurns"],
				oncePerRun = true,
				turnStart = true,
				allPresent = [aetherType],
				lines = [
					new Say { who = aetherType, loopTag = "neutral" },
				],
			};

		newNodes[["Aether", "GoingMissing", "Basic"]] = new()
		{
			priority = true,
			lastTurnPlayerStatuses = [ModEntry.Instance.Aether_Character.MissingStatus.Status],
			oncePerCombatTags = ["AetherWentMissing"],
			oncePerRun = true,
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
			],
		};

		for (var i = 0; i < 4; i++)
			newNodes[["Aether", "ReturningFromMissing", "Basic", i.ToString()]] = new()
			{
				priority = true,
				lookup = [$"{ModEntry.Instance.Package.Manifest.UniqueName}::ReturningFromMissing"],
				oncePerRun = true,
				lines = [
					new Say { who = aetherType, loopTag = "neutral" },
				],
			};

		#region DealtDamage
		for (var i = 0; i < 4; i++)
			newNodes[["Aether", "GoingToOverheat", "Basic", i.ToString()]] = new()
			{
				goingToOverheat = true,
				oncePerCombatTags = ["OverheatGeneric"],
				allPresent = [aetherType],
				lines = [
					new Say { who = aetherType, loopTag = "neutral" },
				],
			};

		newNodes[["Aether", "GoingToOverheat", "Drake"]] = new()
		{
			goingToOverheat = true,
			oncePerCombatTags = ["OverheatGeneric"],
			allPresent = [aetherType, Deck.eunice.Key()],
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
				new Say { who = Deck.eunice.Key(), loopTag = "neutral" },
			],
		};
		#endregion

		newNodes[["Aether", "Recalibrator", "Basic"]] = new()
		{
			playerShotJustMissed = true,
			hasArtifacts = ["Recalibrator"],
			allPresent = [aetherType],
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
			],
		};

		newNodes[["Aether", "StartedBattleAgainstBigCrystal", "Basic"]] = new()
		{
			priority = true,
			turnStart = true,
			oncePerRun = true,
			requiredScenes = ["Crystal_1", "Crystal_1_1"],
			excludedScenes = ["Crystal_2"],
			allPresent = [aetherType, "crystal"],
			lines = [
				new Say { who = aetherType, loopTag = "neutral" },
			],
		};
		// TODO Check crab strings not working in game 
		saySwitchNodes[["Aether", "CrabFacts1_Multi_0"]] = new() 
		{
			who = aetherType,
			loopTag = "neutral"
		};
		saySwitchNodes[["Aether", "CrabFacts2_Multi_0"]] = new()
		{
			who = aetherType,
			loopTag = "neutral"
		};
		saySwitchNodes[["Aether", "CrabFactsAreOverNow_Multi_0"]] = new()
		{
			who = aetherType,
			loopTag = "neutral"
		};
	}
}
