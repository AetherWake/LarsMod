using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class Absorb : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Absorb", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Aether_Deck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Absorb", "name"]).Localize
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
                    retain = true,
                    recycle = true
                };
                break;
            case Upgrade.A:
                data = new CardData()
                {
                    cost = 1,
                    retain = true,
                    recycle = true
                };
                break;
            case Upgrade.B:
                data = new CardData()
                {
                    cost = 2,
                    retain = true,
                    recycle = true
                };
                break;
        }
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        var aquaRing = ModEntry.Instance.KokoroApiV2.ActionCosts.MakeStatusResource(ModEntry.Instance.AquaRing.Status);
        List<CardAction> actions = new();

        switch (upgrade)
        {
            case Upgrade.None:
                var aquaCost = ModEntry.Instance.KokoroApiV2.ActionCosts.MakeResourceCost(aquaRing, AquaRingHelper.HealTriggerDefault);
                actions = new()
                {
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(aquaCost, new AHeal(){healAmount=1, targetPlayer=true}).AsCardAction
                };
                break;
            case Upgrade.A:
                aquaCost = ModEntry.Instance.KokoroApiV2.ActionCosts.MakeResourceCost(aquaRing, AquaRingHelper.HealTriggerDefault);
                actions = new()
                {
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(aquaCost, new AHeal(){healAmount=1, targetPlayer=true}).AsCardAction
                };
                break;
            case Upgrade.B:
                aquaCost = ModEntry.Instance.KokoroApiV2.ActionCosts.MakeResourceCost(aquaRing, AquaRingHelper.HealTriggerDefault);
                actions = new()
                {
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(aquaCost, new AHeal(){healAmount=2, targetPlayer=true}).AsCardAction
                };
                break;
        }
        return actions;
    }

}
