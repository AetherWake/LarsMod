using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class Hydropump : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Hydropump", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Aether_Deck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Hydropump", "name"]).Localize

        });

    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData();

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
                    cost = 0,
                };
                break;
            case Upgrade.B:
                data = new CardData()
                {
                    cost = 2,
                };
                break;
        }
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        var aquaRing = ModEntry.Instance.KokoroApiV2.ActionCosts.MakeStatusResource(ModEntry.Instance.AquaRing.Status);
        switch (upgrade)
        {
            case Upgrade.None:
                
                var aquaCost = ModEntry.Instance.KokoroApiV2.ActionCosts.MakeResourceCost(aquaRing, 1);
                actions = new()
                {
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(aquaCost, new AAttack(){
                        damage=GetDmg(s, 3)
                    }).AsCardAction
                    
                };
                break;
            case Upgrade.A:
                aquaCost = ModEntry.Instance.KokoroApiV2.ActionCosts.MakeResourceCost(aquaRing, 1);
                actions = new()
                {
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(aquaCost, new AAttack(){
                        damage=GetDmg(s, 3)
                    }).AsCardAction
                };
                break;
                
            case Upgrade.B:
                aquaCost = ModEntry.Instance.KokoroApiV2.ActionCosts.MakeResourceCost(aquaRing, 1);
                actions = new()
                {
                    new AVariableHint
				    {
					    status = ModEntry.Instance.AquaRing.Status,
				    },
                    new AAttack(){
                        xHint=GetX(s),
                        damage=GetDmg(s, GetX(s))
                    }
                };
                break;
        }
        return actions;
    }
    
    private int GetX(State state)
	{
		var x = state.ship.Get(ModEntry.Instance.AquaRing.Status);
		return x;
	}

}
