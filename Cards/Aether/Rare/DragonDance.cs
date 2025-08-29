using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class DragonDance : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("DragonDance", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Aether_Deck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "DragonDance", "name"]).Localize

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
                    cost = 2,
                };
                break;
            case Upgrade.A:
                data = new CardData()
                {
                    cost = 2,
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
                
                var aquaCost = ModEntry.Instance.KokoroApiV2.ActionCosts.MakeResourceCost(aquaRing, 8);
                actions = new()
                {
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(aquaCost, new AStatus(){
                        status=Status.powerdrive,
                        statusAmount=1,
                        targetPlayer=true
                    }).AsCardAction,
                    new AStatus(){
                        status=Status.evade,
                        statusAmount = 1,
                        targetPlayer=true
                    },
                    new AStatus(){
                        status=ModEntry.Instance.AquaRing.Status,
                        statusAmount = 3,
                        targetPlayer=true
                    },
                    new AStatus(){
                        status=ModEntry.Instance.MaxAquaRing.Status,
                        statusAmount = 1,
                        targetPlayer=true
                    }
                    
                };
                break;
            case Upgrade.A:
                aquaCost = ModEntry.Instance.KokoroApiV2.ActionCosts.MakeResourceCost(aquaRing, 8);
                actions = new()
                {
                    new AStatus(){
                        status=ModEntry.Instance.MaxAquaRing.Status,
                        statusAmount = 1,
                        targetPlayer=true
                    },
                    new AStatus(){
                        status=ModEntry.Instance.AquaRing.Status,
                        statusAmount = 3,
                        targetPlayer=true
                    },
                     ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(aquaCost, new AStatus(){
                        status=Status.powerdrive,
                        statusAmount=1,
                        targetPlayer=true
                    }).AsCardAction,
                    new AStatus(){
                        status=Status.evade,
                        statusAmount = 2,
                        targetPlayer=true
                    }
                    
                };
                break;
                
            case Upgrade.B:
                aquaCost = ModEntry.Instance.KokoroApiV2.ActionCosts.MakeResourceCost(aquaRing, 10);
                actions = new()
                {
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(aquaCost, new AStatus(){
                        status=Status.powerdrive,
                        statusAmount=2,
                        targetPlayer=true
                    }).AsCardAction,
                    new AStatus(){
                        status=Status.evade,
                        statusAmount = 2,
                        targetPlayer=true
                    },
                    new AStatus(){
                        status=ModEntry.Instance.AquaRing.Status,
                        statusAmount = 3,
                        targetPlayer=true
                    },
                    new AStatus(){
                        status=ModEntry.Instance.MaxAquaRing.Status,
                        statusAmount = 1,
                        targetPlayer=true
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
