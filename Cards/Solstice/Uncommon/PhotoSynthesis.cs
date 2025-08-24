using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class PhotoSynthesis : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("PhotoSynthesis", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Solstice_Deck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "PhotoSynthesis", "name"]).Localize
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
                    exhaust = true
                };
                break;
            case Upgrade.A:
                data = new CardData()
                {
                    cost = 2,
                    exhaust = true
                };
                break;
            case Upgrade.B:
                data = new CardData()
                {
                    cost = 3,
                    exhaust = true,
                    retain = true
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
                    new AHeal
                    {
                        healAmount = 1,
                        targetPlayer = true,
                    },
                    new AStatus
                    {
                        status = Status.lockdown,
                        statusAmount = 1,
                        targetPlayer = true,
                    },
                    new AStatus
                    {
                        status = Status.tempShield,
                        statusAmount = 4,
                        targetPlayer = true,
                    },
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AHeal
                    {
                        healAmount = 1,
                        targetPlayer = true,
                    },
                    new AStatus
                    {
                        status = Status.engineStall,
                        statusAmount = 2,
                        targetPlayer = true,
                    },
                    new AStatus
                    {
                        status = Status.tempShield,
                        statusAmount = 4,
                        targetPlayer = true,
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AHeal
                    {
                        healAmount = 1,
                        targetPlayer = true,
                    },
                    new AStatus
                    {
                        status = Status.lockdown,
                        statusAmount = 1,
                        targetPlayer = true,
                    },
                    new AStatus
                    {
                        status = Status.boost,
                        statusAmount = 2,
                        targetPlayer = true,
                    },
                };
                break;
        }
        return actions;
    }

}
