using Nickel;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace AetherWake.LarsMod.Cards;

internal sealed class Fireball : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Fireball", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.DemoMod_Deck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Fireball", "name"]).Localize
        });
    }

    public override CardData GetData(State s)
    {
        CardData data = new();
        int EnergyGain = GetX(s) <= 4 ? GetX(s) : 4;
        string EnergyString = EnergyGain.ToString();
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
                    new AAttack(){
                        damage = GetX(s),
                        xHint = 1
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
                    new AAttack(){
                        damage = GetX(s),
                        xHint = 1
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
                    new AAttack(){
                        damage = GetX(s)*2,
                        xHint = 2
                    }
                };
                break;
        }
        return actions;
    }

}