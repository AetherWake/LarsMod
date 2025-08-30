using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class Eruption : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Eruption", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Lars_Deck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Eruption", "name"]).Localize
        });
    }
    
    private int GetX(State s)
	{
        var x = s.ship.Get(Status.heat);
		return x;
	}

    public override CardData GetData(State state)
    {
        CardData data = new();
        switch (upgrade)
        {
            case Upgrade.None:
                data = new CardData()
                {
                    cost = 1,
                };
                break;
            case Upgrade.A:
                data = new CardData()
                {
                    cost = 0
                };
                break;
            case Upgrade.B:
                data = new CardData()
                {
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
                    new AVariableHint
                    {
                        status=  Status.heat,
                    },
                    new AAttack(){
                        damage = GetX(s),
                        xHint = 1
                    },
                    new AStatus(){
                        status=Status.heat, statusAmount=-1
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus(){
                        status=Status.heat, statusAmount=1
                    },
                    new AVariableHint
                    {
                        status=  Status.heat,
                    },
                    new AAttack(){
                        damage = GetX(s),
                        xHint = 1
                    },
                    new AStatus(){
                        status=Status.heat, statusAmount=-2
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus(){
                        status=Status.heat, statusAmount=2
                    },
                    new AVariableHint
                    {
                        status=  Status.heat,
                    },
                    new AAttack(){
                        damage = GetX(s),
                        xHint = 1
                    },
                    new AStatus(){
                        status=Status.heat, statusAmount=-1
                    }
                };
                break;
        }
        return actions;
    }

}
