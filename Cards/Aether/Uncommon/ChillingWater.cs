using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class ChillingWater : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ChillingWater", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Aether_Deck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ChillingWater", "name"]).Localize
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
                    cost = 1
                };
                break;
            case Upgrade.B:
                data = new CardData(){
                    cost = 2
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
                    new AAttack(){
                        damage = GetDmg(s, 1),
                        moveEnemy = 1,
                        targetPlayer = false
                    },
                    new AStatus(){
                        targetPlayer=false,
                        status = ModEntry.Instance.AquaRing.Status,
                        statusAmount = 1,
                    },
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(aquaCost, new AStatus(){
                        status=Status.heat,
                        statusAmount=-1,
                        targetPlayer=true
                    }).AsCardAction
                    
                };
                break;
            case Upgrade.A:
                aquaCost = ModEntry.Instance.KokoroApiV2.ActionCosts.MakeResourceCost(aquaRing, 1);
                actions = new()
                {
                    new AAttack(){
                        damage = GetDmg(s, 2),
                        moveEnemy = 1,
                        targetPlayer = false
                    },
                    new AStatus(){
                        targetPlayer=false,
                        status = ModEntry.Instance.AquaRing.Status,
                        statusAmount = 2,
                    },
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(aquaCost, new AStatus(){
                        status=Status.heat,
                        statusAmount=-1,
                        targetPlayer=true
                    }).AsCardAction
                };
                break;
            case Upgrade.B:
                aquaCost = ModEntry.Instance.KokoroApiV2.ActionCosts.MakeResourceCost(aquaRing, 1);
                actions = new()
                {
                    new AAttack(){
                        damage = GetDmg(s, 3),
                        moveEnemy = 1,
                        targetPlayer = false
                    },
                    new AStatus(){
                        targetPlayer=false,
                        status = ModEntry.Instance.AquaRing.Status,
                        statusAmount = 2,
                    },
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(aquaCost, new AStatus(){
                        status=Status.heat,
                        statusAmount=-1,
                        targetPlayer=true
                    }).AsCardAction,
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(aquaCost, new AStatus(){
                        status=Status.heat,
                        statusAmount=-1,
                        targetPlayer=true
                    }).AsCardAction
                };
                break;
        }
        return actions;
    }

}
