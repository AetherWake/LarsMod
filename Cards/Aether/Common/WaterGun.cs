using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class WaterGun : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("WaterGun", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Aether_Deck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "WaterGun", "name"]).Localize
            
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
                    cost = 1,
                };
                break;
            case Upgrade.B:
                data = new CardData()
                {
                    cost = 1,
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
                    new AStatus(){
                        status=Status.shield,
                        statusAmount=1,
                        targetPlayer=true
                    },
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(aquaCost, new AAttack(){
                        damage=2
                    }).AsCardAction

                };
                break;
            case Upgrade.A:
                aquaCost = ModEntry.Instance.KokoroApiV2.ActionCosts.MakeResourceCost(aquaRing, 1);
                actions = new()
                {
                   new AStatus(){
                        status=Status.shield,
                        statusAmount=1,
                        targetPlayer=true
                    },
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(aquaCost, new AAttack(){
                        damage=3
                    }).AsCardAction
                };
                break;
            case Upgrade.B:
                aquaCost = ModEntry.Instance.KokoroApiV2.ActionCosts.MakeResourceCost(aquaRing, 1);
                actions = new()
                {
                    new AAttack(){
                        damage=2
                    },
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(aquaCost, new AStatus(){
                        status=Status.shield,
                        statusAmount=2,
                        targetPlayer=true
                    }).AsCardAction
                };
                break;
        }
        return actions;
    }

}
