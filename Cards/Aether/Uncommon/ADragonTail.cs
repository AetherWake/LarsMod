using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class ADragonTail : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ADragonTail", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Aether_Deck.Deck,

                rarity = Rarity.uncommon,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ADragonTail", "name"]).Localize
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
                    cost = 2,
                    flippable = true
                };
                break;
            case Upgrade.B:
                data = new CardData(){
                    cost = 3
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
                    new AAttack(){
                        damage = GetDmg(s, 1),
                        moveEnemy = 1,
                        targetPlayer = false,
                        dialogueSelector = $".Played::{Key()}"
                    },
                    new AStatus(){
                        targetPlayer=true,
                        status = ModEntry.Instance.AquaRing.Status,
                        statusAmount = 1,
                    }
                    
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AAttack(){
                        damage = GetDmg(s, 2),
                        targetPlayer = false,
                        moveEnemy = 2,
                    },
                    
                    new AStatus(){
                        targetPlayer=true,
                        status = ModEntry.Instance.AquaRing.Status,
                        statusAmount = 1,
                    },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAttack(){
                        damage = GetDmg(s, 3),
                        moveEnemy = 3,
                        targetPlayer = false,
                        dialogueSelector = $".Played::{Key()}"
                    },
                    new AStatus(){
                        targetPlayer=true,
                        status = ModEntry.Instance.AquaRing.Status,
                        statusAmount = 1,
                    },
                };
                break;
        }
        return actions;
    }

}
