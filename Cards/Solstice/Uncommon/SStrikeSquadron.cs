using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class SStrikerSquadron : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("SStrikerSquadron", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Solstice_Deck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "SStrikerSquadron", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new();
        switch(upgrade){
            case Upgrade.None:
                data = new CardData()
                {
                    cost = 2,
                    exhaust=true
                };
                break;
            case Upgrade.A:
                data = new CardData()
                {
                    cost = 2,
                    exhaust=true
                };
                break;
            case Upgrade.B:
                data = new CardData()
                {
                    cost = 2,
                    exhaust=true
                };
                break;
        }
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();

        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new ASpawn(){
                        thing =new AttackDrone(){upgraded=true},
                        offset=1
                    },
                    new ASpawn(){
                        thing =new AttackDrone(){upgraded=true},
                        offset=1,
                        omitFromTooltips=true
                    },
                    new AStatus
                    {
                        targetPlayer = true,
                        status = Status.energyLessNextTurn,
                        statusAmount = 2
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new ASpawn(){
                        thing =new AttackDrone(){upgraded=true},
                        offset=1
                    },
                    new ASpawn(){
                        thing =new AttackDrone(){upgraded=true},
                        offset=1,
                        omitFromTooltips=true
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new ASpawn(){
                        thing =new AttackDrone(){upgraded=true, bubbleShield=true},
                        offset=1
                    },
                    new ASpawn(){
                        thing =new AttackDrone(){upgraded=true, bubbleShield=true},
                        offset=1,
                        omitFromTooltips=true
                    },
                    new AStatus
                    {
                        targetPlayer = true,
                        status = Status.energyLessNextTurn,
                        statusAmount = 2
                    }
                };
                break;
        }
        return actions;
    }

}
