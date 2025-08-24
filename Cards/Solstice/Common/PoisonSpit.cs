using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class PoisonSpit : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("PoisonSpit", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Solstice_Deck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "PoisonSpit", "name"]).Localize
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
                    exhaust = true,
                };
                break;
            case Upgrade.A:
                data = new CardData(){
                    cost = 2,
                    exhaust = true,
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
                    new AAttack(){ damage=0, status = Status.corrode, statusAmount = 1 }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AAttack(){ damage=0, status = Status.corrode, statusAmount = 2 }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAttack(){ damage=4, status = Status.corrode, statusAmount = 1 }
                };
                break;
        }
        return actions;
    }

}
