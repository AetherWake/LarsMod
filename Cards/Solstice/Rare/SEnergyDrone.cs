using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class SEnergyDrone : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("SEnergyDrone", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Solstice_Deck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "SEnergyDrone", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new();
        switch(upgrade){
            case Upgrade.None:
                data = new CardData()
                {
                    cost = 2
                };
                break;
            case Upgrade.A:
                data = new CardData()
                {
                    cost = 2
                };
                break;
            case Upgrade.B:
                data = new CardData()
                {
                    cost = 0,
                    exhaust=true
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
                    new ASpawn(){thing=new EnergyDrone(){targetPlayer=true}}
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new ASpawn(){thing=new EnergyDrone(){bubbleShield=true, targetPlayer=true}}
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new ASpawn(){thing=new EnergyDrone(){targetPlayer=true}}
                };
                break;
        }
        return actions;
    }

}
