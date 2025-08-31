using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class SolarBeam : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("SolarBeam", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Solstice_Deck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "SolarBeam", "name"]).Localize
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
                    infinite = true
                };
                break;
            case Upgrade.A:
                data = new CardData()
                {
                    cost = 1,
                    infinite = true,
                    retain = true
                };
                break;
            case Upgrade.B:
                data = new CardData()
                {
                    cost = 1,
                    infinite = true
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
                    new AAttack(){ damage=GetDmg(s, 0)},
                    
                    new AStatus
                    {
                        status = Status.engineStall,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AStatus
                    {
                        status = Status.overdrive,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus
                    {
                        status = Status.overdrive,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AStatus
                    {
                        status = Status.stunCharge,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AStatus
                    {
                        status = Status.engineStall,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAttack(){ damage=GetDmg(s, 0), stunEnemy = true},
                    
                    new AStatus
                    {
                        status = Status.engineStall,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AStatus
                    {
                        status = Status.overdrive,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    
                };
                break;
        }
        return actions;
    }

}
