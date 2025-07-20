using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class Magic : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Magic", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Lars_Deck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Magic", "name"]).Localize
        });
    }

    private int GetX(State state)
	{
		var x = state.ship.Get(Status.heat);
		return x;
	}

    public override CardData GetData(State state)
    {
        CardData data = new();
        switch(upgrade){
            case Upgrade.None:
                data = new CardData()
                {
                    cost = 2,
                    exhaust = true
                };
                break;
            case Upgrade.A:
                data = new CardData(){
                    cost = 3,
                    exhaust = false
                };
                break;
            case Upgrade.B:
                data = new CardData(){
                    cost = 2,
                    exhaust = true
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
                    new AHeal(){
                        healAmount = 1,
                        targetPlayer = true
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AHeal(){
                        healAmount = 1,
                        targetPlayer = true
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
                    new AHeal{
                        healAmount = GetX(s),
                        targetPlayer = true,
                        xHint = 1
                    }
                };
                break;
        }
        return actions;
    }

}
