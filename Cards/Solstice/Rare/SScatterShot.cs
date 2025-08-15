using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class SScatterShot : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("SScatterShot", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Solstice_Deck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "SScatterShot", "name"]).Localize
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
                };
                break;
            case Upgrade.A:
                data = new CardData()
                {
                    cost = 2,
                    flippable=true
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
                    new AAttack(){
                        damage=1,
                        moveEnemy=2
                    },
                    new AMove(){
                        dir=-2
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AAttack(){
                        damage=1,
                        moveEnemy=2
                    },
                    new AMove(){
                        dir=-2
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAttack(){
                        damage=3,
                        moveEnemy=2
                    },
                    new AMove(){
                        dir=-2
                    }
                };
                break;
        }
        return actions;
    }

}
