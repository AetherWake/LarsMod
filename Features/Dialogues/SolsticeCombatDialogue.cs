using Nickel;
using System.Collections.Generic;

namespace AetherWake.LarsMod;

internal sealed class SolsticeCombatDialogue : BaseDialogue
{
	public SolsticeCombatDialogue() : base(locale => ModEntry.Instance.Package.PackageRoot.GetRelativeFile($"i18n/combat-dialogue-solstice-{locale}.json").OpenRead())
	{
		var solsticeDeck = ModEntry.Instance.Solstice_Deck.Deck;
		var solsticeType = ModEntry.Instance.Solstice_Character!.CharacterType;
		var newNodes = new Dictionary<IReadOnlyList<string>, StoryNode>();
		var saySwitchNodes = new Dictionary<IReadOnlyList<string>, Say>();
		var larsType 	= ModEntry.Instance.Lars_Character.CharacterType;
		var aetherType 	= ModEntry.Instance.Aether_Character.CharacterType;

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
		
		newNodes[["Solstice", "TookDamage", "Basic", "0"]] = new()
		{
			
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};
		newNodes[["Solstice", "TookDamage", "Basic", "1"]] = new()
		{

			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			lines = [
				new Say { who = solsticeType, loopTag = "squint" },
			],
		};
		newNodes[["Solstice", "TookDamage", "Basic", "2"]] = new()
		{

			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};

		newNodes[["Solstice", "TookDamage", "Dizzy"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [solsticeType, Deck.dizzy.Key()],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
				new Say { who = Deck.dizzy.Key(), loopTag = "neutral" },
			],
		};
		newNodes[["Solstice", "TookDamage", "Riggs"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [solsticeType, Deck.riggs.Key()],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
				new Say { who = Deck.riggs.Key(), loopTag = "neutral" },
			],
		};
		newNodes[["Solstice", "TookDamage", "Peri"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [solsticeType, Deck.peri.Key()],
			lines = [
				new Say { who = solsticeType, loopTag = "squint" },
				new Say { who = Deck.peri.Key(), loopTag = "neutral" },
			],
		};
		newNodes[["Solstice", "TookDamage", "Isaac"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [solsticeType, Deck.goat.Key()],
			lines = [
				new Say { who = Deck.goat.Key(), loopTag = "squint" },
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};
		newNodes[["Solstice", "TookDamage", "Drake"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [solsticeType, Deck.eunice.Key()],
			lines = [
				new Say { who = Deck.eunice.Key(), loopTag = "squint" },
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};
		newNodes[["Solstice", "TookDamage", "Max"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [solsticeType, Deck.hacker.Key()],
			lines = [
				new Say { who = Deck.hacker.Key(), loopTag = "mad" },
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};
		newNodes[["Solstice", "TookDamage", "Books"]] = new()
		{
			enemyShotJustHit = true,
			minDamageDealtToPlayerThisTurn = 1,
			allPresent = [solsticeType, Deck.shard.Key()],
			lines = [
				new Say { who = solsticeType, loopTag = "normal" },
				new Say { who = Deck.shard.Key(), loopTag = "intense" },
			],
		};
		newNodes[["Solstice", "TookDamage", "CAT"]] = new()
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
			newNodes[["Solstice", "TookNonHullDamage", "Basic", i.ToString()]] = new StoryNode()
			{
				enemyShotJustHit = true,
				maxDamageDealtToPlayerThisTurn = 0,
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			};

		newNodes[["Solstice", "TookNonHullDamage", "Dizzy"]] = new()
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
			newNodes[["Solstice", "DealtDamage", "Basic", i.ToString()]] = new()
			{
				playerShotJustHit = true,
				minDamageDealtToEnemyThisTurn = 1,
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			};

		newNodes[["Solstice", "DealtDamage", "Dizzy"]] = new()
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
		newNodes[["Solstice", "DealtDamage", "Riggs"]] = new()
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
		newNodes[["Solstice", "DealtDamage", "Peri"]] = new()
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
		newNodes[["Solstice", "DealtDamage", "Isaac"]] = new()
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
		newNodes[["Solstice", "DealtDamage", "Drake", "0"]] = new()
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
		newNodes[["Solstice", "DealtDamage", "Drake", "1"]] = new()
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
		newNodes[["Solstice", "DealtDamage", "Max"]] = new()
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
		newNodes[["Solstice", "DealtDamage", "Books"]] = new()
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
		newNodes[["Solstice", "DealtDamage", "CAT"]] = new()
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
			newNodes[["Solstice", "DealtBigDamage", "Basic", i.ToString()]] = new()
			{
				playerShotJustHit = true,
				minDamageDealtToEnemyThisTurn = 6,
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 3; i++)
			newNodes[["Solstice", "ShieldedDamage", "Basic", i.ToString()]] = new StoryNode()
			{
				enemyShotJustHit = true,
				maxDamageDealtToPlayerThisTurn = 0,
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			}.SetMinShieldLostThisTurn(1);

		newNodes[["Solstice", "Missed", "Basic", "0"]] = new()
		{
			playerShotJustMissed = true,
			allPresent = [solsticeType],
			lines = [
				new Say { who = solsticeType, loopTag = "squint" },
			],
		};
		newNodes[["Solstice", "Missed", "Basic", "1"]] = new()
		{
			playerShotJustMissed = true,
			allPresent = [solsticeType],
			lines = [
				new Say { who = solsticeType, loopTag = "squint" },
			],
		};
		newNodes[["Solstice", "Missed", "Basic", "2"]] = new()
		{
			playerShotJustMissed = true,
			allPresent = [solsticeType],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};
		newNodes[["Solstice", "Missed", "Basic", "3"]] = new()
		{
			playerShotJustMissed = true,
			allPresent = [solsticeType],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};

		#region AboutToDie
		newNodes[["Solstice", "AboutToDie", "Basic", "0"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [solsticeType],
			lines = [
				new Say { who = solsticeType, loopTag = "squint" },
			],
		};
		newNodes[["Solstice", "AboutToDie", "Basic", "1"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [solsticeType],
			lines = [
				new Say { who = solsticeType, loopTag = "squint" },
			],
		};
		newNodes[["Solstice", "AboutToDie", "Basic", "2"]] = new()
		{
			maxHull = 1,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [solsticeType],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};

		newNodes[["Solstice", "AboutToDie", "Dizzy"]] = new()
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
		newNodes[["Solstice", "AboutToDie", "Riggs"]] = new()
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
		newNodes[["Solstice", "AboutToDie", "Peri"]] = new()
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
		newNodes[["Solstice", "AboutToDie", "Isaac"]] = new()
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
		newNodes[["Solstice", "AboutToDie", "Drake"]] = new()
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
		newNodes[["Solstice", "AboutToDie", "Max"]] = new()
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
		newNodes[["Solstice", "AboutToDie", "Books"]] = new()
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
		newNodes[["Solstice", "AboutToDie", "CAT"]] = new()
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
		newNodes[["Solstice", "AboutToDie", "LarsAether"]] = new()
		{
			maxHull = 2,
			oncePerCombatTags = ["aboutToDie"],
			oncePerRun = true,
			allPresent = [solsticeType, larsType, aetherType],
			lines = [
				new Say { who = solsticeType, loopTag = "squint" },
				new Say { who = larsType, loopTag = "neutral" },
				new Say { who = aetherType, loopTag = "neutral"}
			],
		};
		#endregion

		for (var i = 0; i < 4; i++)
			newNodes[["Solstice", "HitArmor", "Basic", i.ToString()]] = new()
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
			newNodes[["Solstice", "ExcessEnergy", "Basic", i.ToString()]] = new()
			{
				handEmpty = true,
				minEnergy = 1,
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "squint" },
				],
			};

		for (var i = 0; i < 2; i++)
			newNodes[["Solstice", "EmptyHand", "Basic", i.ToString()]] = new()
			{
				handEmpty = true,
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 2; i++)
			newNodes[["Solstice", "TrashHand", "Basic", i.ToString()]] = new()
			{
				handFullOfTrash = true,
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "squint" },
				],
			};

		for (var i = 0; i < 1; i++)
			newNodes[["Solstice", "PlayedRecycle", "Basic", i.ToString()]] = new()
			{
				lookup = [$"{ModEntry.Instance.Package.Manifest.UniqueName}::PlayedRecycle"],
				allPresent = [solsticeType],
				oncePerRun=true,
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			};

		for (var i = 0; i < 1; i++)
			newNodes[["Solstice", "NewNonLarsNonTrashTempCard", "Basic", i.ToString()]] = new()
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
			newNodes[["Solstice", "StartedBattle", "Basic", i.ToString()]] = new()
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
			newNodes[["Solstice", "StartedBattle", "Peri"]] = new()
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
			newNodes[["Solstice", "NoOverlap", "Basic", i.ToString()]] = new()
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

		newNodes[["Solstice", "NoOverlapButSeeker", "Riggs"]] = new()
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
			newNodes[["Solstice", "LongFight", "Basic", i.ToString()]] = new()
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

		newNodes[["Solstice", "GoingMissing", "Basic"]] = new()
		{
			priority = true,
			lastTurnPlayerStatuses = [ModEntry.Instance.Solstice_Character.MissingStatus.Status],
			oncePerCombatTags = ["SolsticeWentMissing"],
			oncePerRun = true,
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};

		for (var i = 0; i < 4; i++)
			newNodes[["Solstice", "ReturningFromMissing", "Basic", i.ToString()]] = new()
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
			newNodes[["Solstice", "GoingToOverheat", "Basic", i.ToString()]] = new()
			{
				goingToOverheat = true,
				oncePerCombatTags = ["OverheatGeneric"],
				allPresent = [solsticeType],
				lines = [
					new Say { who = solsticeType, loopTag = "neutral" },
				],
			};

		newNodes[["Solstice", "GoingToOverheat", "Drake"]] = new()
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

		newNodes[["Solstice", "Recalibrator", "Basic"]] = new()
		{
			playerShotJustMissed = true,
			hasArtifacts = ["Recalibrator"],
			allPresent = [solsticeType],
			lines = [
				new Say { who = solsticeType, loopTag = "neutral" },
			],
		};

		newNodes[["Solstice", "StartedBattleAgainstBigCrystal", "Basic"]] = new()
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
		saySwitchNodes[["Solstice", "CrabFacts1_Multi_0"]] = new() 
		{
			who = solsticeType,
			loopTag = "neutral"
		};
		saySwitchNodes[["Solstice", "CrabFacts2_Multi_0"]] = new()
		{
			who = solsticeType,
			loopTag = "neutral"
		};
		saySwitchNodes[["Solstice", "CrabFactsAreOverNow_Multi_0"]] = new()
		{
			who = solsticeType,
			loopTag = "neutral"
		};
	}
}
