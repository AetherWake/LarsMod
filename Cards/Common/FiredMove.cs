using Nickel;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class FiredMove : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("FiredMove", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.DemoMod_Deck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "FiredMove", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new();
        switch(upgrade){
            case Upgrade.None:
                data = new CardData()
                {
                    cost = 3,
                };
                break;
            case Upgrade.A:
                data = new CardData(){
                    cost = 2,
                };
                break;
            case Upgrade.B:
                data = new CardData(){
                    cost = 3,
                };
                break;
        }
        return data;
    }

    private int GetX(State state)
	{
		var x = state.ship.Get(Status.heat);
		return x;
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
					    status = Status.heat,
				    },
                    new AStatus(){
                        status = Status.evade,
                        statusAmount = GetX(s),
                        xHint=1,
                        targetPlayer=true
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AVariableHint
				    {
					    status = Status.heat,
				    },
                    new AStatus(){
                        status = Status.evade,
                        statusAmount = GetX(s),
                        xHint=1,
                        targetPlayer=true
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AVariableHint
				    {
					    status = Status.heat,
				    },
                    new AStatus(){
                        status = Status.evade,
                        statusAmount = GetX(s),
                        xHint=1,
                        targetPlayer=true
                    },
                    new AStatus(){
                        status=Status.evade,
                        statusAmount=2,
                        targetPlayer=true
                    }
                };
                break;
        }
        return actions;
    }

}