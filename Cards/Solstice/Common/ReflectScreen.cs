using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class ReflectScreen : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ReflectScreen", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Solstice_Deck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ReflectScreen", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new();
        switch(upgrade){
            case Upgrade.None:
                data = new CardData()
                {
                    cost = 1
                };
                break;
            case Upgrade.A:
                data = new CardData(){
                    cost = 2,
                    retain = true
                };
                break;
            case Upgrade.B:
                data = new CardData()
                {
                    cost = 3,
                    exhaust = true
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
                    new AStatus
                    {
                        status = Status.libra,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                    
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus
                    {
                        status = Status.energyNextTurn,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AStatus
                    {
                        status = Status.libra,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                    
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus
                    {
                        status = Status.libra,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AStatus(){
                        status=Status.reflexiveCoating,
                        statusAmount=1,
                        targetPlayer=true
                    }
                };
                break;
        }
        return actions;
    }

}
