using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class LeafBlade : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("LeafBlade", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Solstice_Deck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "LeafBlade", "name"]).Localize
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
                };
                break;
            case Upgrade.A:
                data = new CardData(){
                    cost = 3
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

        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new ASpawn(){
                        thing=new Missile(){missileType=MissileType.normal},
                    },
                    new AAttack(){ damage=GetDmg(s, 1)},
                    new AAttack(){ damage=GetDmg(s, 1)}
                    
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new ASpawn(){
                        thing =new AttackDrone(),
                        omitFromTooltips=true
                    },
                    new AAttack(){ damage=GetDmg(s, 1)},
                    new AAttack(){ damage=GetDmg(s, 1)},
                    new AAttack(){ damage=GetDmg(s, 1)}
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new ASpawn(){
                        thing=new Missile(){missileType=MissileType.corrode},
                    },
                    new AAttack(){damage=GetDmg(s, 0), piercing=true},
                    new AAttack(){damage=GetDmg(s, 0), piercing=true},
                };
                break;
        }
        return actions;
    }

}
