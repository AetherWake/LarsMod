using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class DragonTail : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("DragonTail", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Lars_Deck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "DragonTail", "name"]).Localize
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
                    cost = 2,
                    flippable = true
                };
                break;
            case Upgrade.B:
                data = new CardData(){
                    cost = 3
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
                        damage = 1,
                        moveEnemy = -1,
                        targetPlayer = false,
                    },
                    new AStatus(){
                        targetPlayer=false,
                        status = Status.heat,
                        statusAmount = 1,
                    }
                    
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AAttack(){
                        damage = 2,
                        targetPlayer = false,
                        moveEnemy = -2,
                    },
                    
                    new AStatus(){
                        targetPlayer=false,
                        status = Status.heat,
                        statusAmount = 1,
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAttack(){
                        damage = 3,
                        moveEnemy = -3,
                        targetPlayer = false
                    },
                    new AStatus(){
                        targetPlayer=false,
                        status = Status.heat,
                        statusAmount = 1,
                    }
                };
                break;
        }
        return actions;
    }

}
