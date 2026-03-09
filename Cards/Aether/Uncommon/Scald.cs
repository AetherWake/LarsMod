using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class Scald : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Scald", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Aether_Deck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Scald", "name"]).Localize

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
                actions = new()
                {
                    new AAttack()
                    {
                        damage=GetDmg(s, 0),
                        status=Status.heat,
                        statusAmount=1
                    },
                    new AStatus()
                    {
                        status=ModEntry.Instance.AquaRing.Status,
                        statusAmount=1,
                        targetPlayer=false
                    }
                    
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AAttack()
                    {
                        damage=GetDmg(s, 0),
                        status=Status.heat,
                        statusAmount=2
                    },
                    new AStatus()
                    {
                        status=ModEntry.Instance.AquaRing.Status,
                        statusAmount=2,
                        targetPlayer=false
                    }
                };
                break;
                
            case Upgrade.B:
                actions = new()
                {
                    new AAttack()
                    {
                        damage=GetDmg(s, 1),
                        status=Status.heat,
                        statusAmount=3
                    },
                    new AStatus()
                    {
                        status=ModEntry.Instance.AquaRing.Status,
                        statusAmount=3,
                        targetPlayer=false
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
