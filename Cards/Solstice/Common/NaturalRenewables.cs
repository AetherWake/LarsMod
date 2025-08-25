using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class NaturalRenewables : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("NaturalRenewables", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Solstice_Deck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "NaturalRenewables", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new();
        switch(upgrade){
            case Upgrade.None:
                data = new CardData()
                {
                    cost = 2,
                    recycle = true
                };
                break;
            case Upgrade.A:
                data = new CardData()
                {
                    cost = 1,
                    infinite = true
                };
                break;
            case Upgrade.B:
                data = new CardData()
                {
                    cost = 1,
                    floppable = true,
                    recycle = true
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
                    new ASpawn(){
                        thing=new ShieldDrone(){targetPlayer=true},
                        offset=-1
                    },
                    new ASpawn(){
                        thing=new AttackDrone(),
                        offset=1
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new ASpawn(){
                        thing=new AttackDrone()
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new ASpawn()
                    {
                        thing=new AttackDrone(),
                        disabled = flipped
                    },
                    new ASpawn(){
                        thing=new ShieldDrone(){targetPlayer=true},
				        disabled = !flipped
                    }
                };
                break;
        }
        return actions;
    }

}
