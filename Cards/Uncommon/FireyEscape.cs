using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class FireyEscape : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("FireyEscape", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Lars_Deck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "FireyEscape", "name"]).Localize
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
                    cost = 1,
                    flippable = true
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
                    new AMove(){
                        isRandom = true,
                        targetPlayer = true,
                        isTeleport = true, 
                        dir = 1
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
                    new AMove(){
                        targetPlayer = true,
                        isTeleport = true, 
                        dir = 2
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
                    new AMove(){
                        isRandom = true,
                        targetPlayer = true,
                        isTeleport = true,
                        dir = 2
                    },
                    new AStatus(){
                        targetPlayer=false,
                        status = Status.heat,
                        statusAmount = 2,
                    },
                    new AStatus(){
                        targetPlayer=true,
                        status = Status.heat,
                        statusAmount = 1,
                    }
                };
                break;
        }
        return actions;
    }

}