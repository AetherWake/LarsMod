using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class ShadowPanic : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ShadowPanic", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Solstice_Deck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ShadowPanic", "name"]).Localize
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
                    new AAttack(){ damage=1, status = Status.lockdown, statusAmount = 1 },
                    new AStatus
                    {
                        status = Status.lockdown,
                        statusAmount = 3,
                        targetPlayer = true,
                    },
                    new AStatus
                    {
                        status = Status.overdrive,
                        statusAmount = 1,
                        targetPlayer = true,
                    },
                    new AStatus
                    {
                        status = Status.powerdrive,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AAttack(){ damage=1, status = Status.lockdown, statusAmount = 1 },
                    new AStatus
                    {
                        status = Status.lockdown,
                        statusAmount = 2,
                        targetPlayer = true,
                    },
                    new AStatus
                    {
                        status = Status.payback,
                        statusAmount = 1,
                        targetPlayer = true,
                    },
                    new AStatus
                    {
                        status = Status.powerdrive,
                        statusAmount = 1,
                        targetPlayer = true,
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AHurt(){hurtAmount = 1, targetPlayer = true},
                    new AAttack(){ damage=1, status = Status.lockdown, statusAmount = 2 },
                    new AStatus
                    {
                        status = Status.lockdown,
                        statusAmount = 3,
                        targetPlayer = true,
                    },
                    new AStatus
                    {
                        status = Status.overdrive,
                        statusAmount = 1,
                        targetPlayer = true,
                    },
                    new AStatus
                    {
                        status = Status.powerdrive,
                        statusAmount = 1,
                        targetPlayer = true,
                    },
                };
                break;
        }
        return actions;
    }

}
