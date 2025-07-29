using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class MagicalExpansion : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("MagicalExpansion", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Aether_Deck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "MagicalExpansion", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new();
        switch(upgrade){
            case Upgrade.None:
                data = new CardData()
                {
                    cost = 0,
                    exhaust=true
                };
                break;
            case Upgrade.A:
                data = new CardData(){
                    cost = 0,
                };
                break;
            case Upgrade.B:
                data = new CardData()
                {
                    cost = 0,
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
                    
                    new AStatus(){
                        targetPlayer=true,
                        status = Status.maxShield,
                        statusAmount = 1,
                    },
                    
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus(){
                        targetPlayer=true,
                        status = Status.maxShield,
                        statusAmount = 1,
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus(){
                        targetPlayer=true,
                        status = Status.maxShield,
                        statusAmount = 3,
                    },
                };
                break;
        }
        return actions;
    }

}
