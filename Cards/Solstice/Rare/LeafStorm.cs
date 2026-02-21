using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class LeafStorm : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("LeafStorm", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Solstice_Deck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "LeafStorm", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new();
        switch(upgrade){
            case Upgrade.None:
                data = new CardData()
                {
                    cost = 3,
                    exhaust = true
                };
                break;
            case Upgrade.A:
                data = new CardData()
                {
                    cost = 3,
                    exhaust = true
                };
                break;
            case Upgrade.B:
                data = new CardData()
                {
                    cost = 3,
                    flippable = true
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
                        thing=new Missile(){missileType=MissileType.breacher},
                    },
                    new ASpawn(){
                        thing=new Missile(){missileType=MissileType.normal}, offset = -1
                    },
                    new ASpawn(){
                        thing=new Missile(){missileType=MissileType.normal}, offset = 1
                    },
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new ASpawn(){
                        thing=new Missile(){missileType=MissileType.breacher}, offset = -1
                    },
                    new ASpawn(){
                        thing=new Missile(){missileType=MissileType.breacher}, offset = 1
                    },
                    new ASpawn(){
                        thing=new DualDrone()
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new ASpawn(){
                        thing=new Missile(){missileType=MissileType.breacher},
                    },
                    new AMove
                    {
                        dir = -3,
                        targetPlayer = true
                    },
                    new ASpawn(){
                        thing=new Missile(){missileType=MissileType.seeker},
                    },
                };
                break;
        }
        return actions;
    }

}
