using Nickel;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class Rapier : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Rapier", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.DemoMod_Deck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Rapier", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new();
        switch(upgrade){
            case Upgrade.None:
                data = new CardData()
                {
                    cost = 0,
                    singleUse = true,
                    description = ModEntry.Instance.Localizations.Localize(["card", "Rapier", "description"])
                };
                break;
            case Upgrade.A:
                data = new CardData(){
                    cost = 0,
                    singleUse = false,
                };
                break;
            case Upgrade.B:
                data = new CardData(){
                    cost = 0,
                    singleUse = false,
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
                    new ADummyAction() { dialogueSelector = $".Played::{Key()}" }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AAttack(){
                        damage = GetDmg(s, 0),
                    },
                    new AAttack(){
                        damage = GetDmg(s, 0)
                    },
                    
                    new ADummyAction() { dialogueSelector = $".Played::{Key()}_A" }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AAttack(){
                        damage = GetDmg(s, 0)
                    },
                    new ADrawCard(){
                        count=1
                    },
                    new ADummyAction() { dialogueSelector = $".Played::{Key()}_B" }
                };
                break;
        }
        return actions;
    }

}
