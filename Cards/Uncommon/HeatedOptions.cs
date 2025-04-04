using Nickel;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace AetherWake.LarsMod.Cards;

internal sealed class HeatedOptions : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("HeatedOptions", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.DemoMod_Deck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "HeatedOptions", "name"]).Localize
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
                };
                break;
            case Upgrade.B:
                data = new CardData(){
                    cost = 1,
                    exhaust=true
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
                    new ADrawCard(){
                        count=GetX(s),
                        xHint=1,
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {

                    new ADrawCard(){
                        count=GetX(s),
                        xHint=1,
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
                    new ADrawCard(){
                        xHint=1,
                        count=GetX(s)
                    },
                    new AStatus(){
                        status = Status.drawNextTurn,
                        xHint=1,
                        targetPlayer=true,
                        statusAmount = GetX(s)
                    }
                };
                break;
        }
        return actions;
    }

}