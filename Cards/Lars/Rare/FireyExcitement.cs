using Nickel;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace AetherWake.LarsMod.Cards;

internal sealed class FireyExcitement : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("FireyExcitement", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Lars_Deck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "FireyExcitement", "name"]).Localize
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
                    cost = 1,
                    description = ModEntry.Instance.Localizations.Localize(["card", "FireyExcitement", "description"])
                };
                break;
            case Upgrade.A:
                data = new CardData(){
                    cost = 0,
                    description = ModEntry.Instance.Localizations.Localize(["card", "FireyExcitement", "description"])
                };
                break;
            case Upgrade.B:
                data = new CardData(){
                    cost = 1,
                    description = ModEntry.Instance.Localizations.Localize(["card", "FireyExcitement", "description_b"])
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
        int EnergyGain = GetX(s) >= 3 ? GetX(s)/3 : 0;
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new AVariableHint
				    {
					    status = Status.heat,
				    },
                    new AEnergy(){
                        changeAmount = EnergyGain>4 ? 4 : EnergyGain,
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
                    new AEnergy(){
                        changeAmount = EnergyGain>4 ? 4 : EnergyGain,
                        xHint = 1/3
                    }
                };
                break;
            case Upgrade.B:
                int EnergyGainB = GetX(s) >= 2 ? GetX(s)/2 : 0;
                actions = new()
                {
                    new AVariableHint
				    {
					    status = Status.heat,
				    },
                    new AEnergy(){
                        changeAmount = EnergyGainB>4 ? 4 : EnergyGainB,
                        xHint = 1/2
                    }
                };
                break;
        }
        return actions;
    }

}