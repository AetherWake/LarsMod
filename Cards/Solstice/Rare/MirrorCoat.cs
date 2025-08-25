using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class MirrorCoat : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("MirrorCoat", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Solstice_Deck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "MirrorCoat", "name"]).Localize
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
                    new AAttack(){ damage=1, piercing = true},
                    new AStatus
                    {
                        status = Status.backwardsMissiles,
                        statusAmount = 1,
                    },
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AAttack(){ damage=1, piercing = true, status = Status.backwardsMissiles},
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus
                    {
                        status = Status.backwardsMissiles,
                        statusAmount = 1,
                    },
                    new AStatus
                    {
                        status = Status.tempPayback,
                        statusAmount = 1,
                        targetPlayer = true,
                    },
                };
                break;
        }
        return actions;
    }

}
