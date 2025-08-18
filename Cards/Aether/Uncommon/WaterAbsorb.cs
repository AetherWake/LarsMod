using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class WaterAbsorb : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("WaterAbsorb", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Aether_Deck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },

            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "WaterAbsorb", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new();
        switch (upgrade)
        {
            case Upgrade.None:
                data = new CardData()
                {
                    cost = 2,
                    description = ModEntry.Instance.Localizations.Localize(["card", "WaterAbsorb", "description"])
                };
                break;
            case Upgrade.A:
                data = new CardData()
                {
                    cost = 2,
                    description = ModEntry.Instance.Localizations.Localize(["card", "WaterAbsorb", "description_a"])
                };
                break;
            case Upgrade.B:
                data = new CardData()
                {
                    cost = 3,
                    description = ModEntry.Instance.Localizations.Localize(["card", "WaterAbsorb", "description_b"])
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
                    new AStatus(){
                        targetPlayer = true,
                        status = ModEntry.Instance.AquaRing.Status,
                        statusAmount = GetX(c)
                    },

                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus(){
                        targetPlayer = true,
                        status = ModEntry.Instance.AquaRing.Status,
                        statusAmount = GetX(c) + 3
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus(){
                        targetPlayer = true,
                        status = ModEntry.Instance.AquaRing.Status,
                        statusAmount = GetX(c) * 2
                    },
                };
                break;
        }
        return actions;
    }
    
        private int GetX(Combat c)
	{
		var x = c.otherShip.Get(ModEntry.Instance.AquaRing.Status);
		return x;
	}

}
