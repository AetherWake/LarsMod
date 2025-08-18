using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class ParallelShift : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ParallelShift", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Solstice_Deck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ParallelShift", "name"]).Localize
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
                    cost = 1
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
                    new AStatus(){
                        status=Status.droneShift,
                        statusAmount=1,
                        targetPlayer=true,
                        dialogueSelector = $".Played::{Key()}"
                    },
                    new ADroneMove(){
                        dir=1
                    }
                    
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus(){
                        status=Status.droneShift,
                        statusAmount=2,
                        targetPlayer=true
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus(){
                        status=Status.droneShift,
                        statusAmount=3,
                        targetPlayer=true
                    },
                };
                break;
        }
        return actions;
    }

}
