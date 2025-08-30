using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class GastroAcid : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("GastroAcid", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Solstice_Deck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "GastroAcid", "name"]).Localize
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
                    buoyant = true
                };
                break;
            case Upgrade.A:
                data = new CardData()
                {
                    cost = 3,
                    buoyant = true
                };
                break;
            case Upgrade.B:
                data = new CardData()
                {
                    cost = 3,
                    exhaust=true,
                    buoyant = true
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
                    new AAttack(){ damage=1, piercing = true, status = Status.mitosis, statusAmount = 1 },
                    new AStatus(){
                        status =Status.backwardsMissiles,
                        statusAmount=1
                    },
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AAttack(){ damage=0, piercing = true, status = Status.mitosis, statusAmount = 1 },
                    new AAttack(){ damage=0, piercing = true, status = Status.backwardsMissiles, statusAmount = 1 },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus(){
                        status =Status.corrode,
                        statusAmount=1
                    },
                    new AStatus(){
                        status =Status.mitosis,
                        statusAmount=1
                    }
                };
                break;
        }
        return actions;
    }

}
