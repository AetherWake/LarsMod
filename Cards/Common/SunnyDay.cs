using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class SunnyDay : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("SunnyDay", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.DemoMod_Deck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "SunnyDay", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new();
        switch(upgrade){
            case Upgrade.None:
                data = new CardData()
                {
                    cost = 1,
                };
                break;
            case Upgrade.A:
                data = new CardData(){
                    cost = 0
                };
                break;
            case Upgrade.B:
                data = new CardData(){
                    cost = 1
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
                    
                    new AStatus()
                    {
                        status = Status.heat,
                        statusAmount = 2,
                        targetPlayer = false
                    },
                    new AStatus()
                    {
                        status = Status.heat,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.heat,
                        statusAmount = 2,
                        targetPlayer = false
                    },
                    new AStatus()
                    {
                        status = Status.heat,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.heat,
                        statusAmount = 3,
                        targetPlayer = false
                    },
                    new AStatus()
                    {
                        status = Status.heat,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                };
                break;
        }
        return actions;
    }

}
