using Nickel;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace AetherWake.LarsMod.Cards;

internal sealed class HeatExp : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("HeatExp", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.DemoMod_Deck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "HeatExp", "name"]).Localize
        });
    }

    public override CardData GetData(State s)
    {
        CardData data = new();
        switch(upgrade){
            case Upgrade.None:
                data = new CardData()
                {
                    cost = 2,
                    exhaust=true,
                    description = ModEntry.Instance.Localizations.Localize(["card", "HeatExp", "description"])
                };
                break;
            case Upgrade.A:
                data = new CardData(){
                    cost = 1,
                    exhaust=true,
                    description = ModEntry.Instance.Localizations.Localize(["card", "HeatExp", "description"])
                };
                break;
            case Upgrade.B:
                data = new CardData(){
                    cost = 2,
                    exhaust=false,
                    description = ModEntry.Instance.Localizations.Localize(["card", "HeatExp", "description"])
                };
                break;
        }
        return data;
    }

    private int GetX(Combat c)
	{
		var x = c.otherShip.Get(Status.heat);
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
                    new AStatus(){
                        status=Status.heat, 
                        statusAmount = GetX(c)
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus(){
                        status=Status.heat, 
                        statusAmount = GetX(c)
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus(){
                        status=Status.heat, 
                        statusAmount = GetX(c)
                    }
                };
                break;
        }
        return actions;
    }

}