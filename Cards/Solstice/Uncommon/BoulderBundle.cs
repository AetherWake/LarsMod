using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class BoulderBundle : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("BoulderBundle", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Solstice_Deck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "BoulderBundle", "name"]).Localize
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
                    exhaust = true,
                    description= ModEntry.Instance.Localizations.Localize(["card", "BoulderBundle", "description"])
                };
                break;
            case Upgrade.A:
                data = new CardData()
                {
                    cost = 2,
                    exhaust = true,
                    description= ModEntry.Instance.Localizations.Localize(["card", "BoulderBundle", "description_a"])
                };
                break;
            case Upgrade.B:
                data = new CardData()
                {
                    cost = 2,
                    exhaust = true,
                    description= ModEntry.Instance.Localizations.Localize(["card", "BoulderBundle", "description_b"])
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
                        status=Status.droneShift,
                        statusAmount=1
                    },
                    new AAddCard(){
                        card=new SPebble(),
                        amount=3
                    }
                    
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus(){
                        targetPlayer=true,
                        status=Status.droneShift,
                        statusAmount=3
                    },
                    new AAddCard(){
                        card=new SPebble(),
                        amount=3
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAddCard(){
                        card=new SPebble(){upgrade=Upgrade.A},
                        amount=3
                    }
                };
                break;
        }
        return actions;
    }

}
