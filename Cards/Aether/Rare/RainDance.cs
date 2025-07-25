using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class RainDance : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("RainDance", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Aether_Deck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "RainDance", "name"]).Localize
            
        });
        
    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        
        switch (upgrade)
        {
            case Upgrade.None:
                data = new CardData()
                {
                    cost = 3,
                };
                break;
            case Upgrade.A:
                data = new CardData()
                {
                    cost = 2,
                };
                break;
            case Upgrade.B:
                data = new CardData()
                {
                    cost = 3,
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
                        status=ModEntry.Instance.RainDance.Status,
                        statusAmount=1,
                        targetPlayer=true
                    }

                };
                break;
            case Upgrade.A:
                actions = new()
                {
                   new AStatus(){
                        status=ModEntry.Instance.RainDance.Status,
                        statusAmount=1,
                        targetPlayer=true
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus(){
                        status=ModEntry.Instance.RainDance.Status,
                        statusAmount=2,
                        targetPlayer=true
                    }
                };
                break;
        }
        return actions;
    }

}
