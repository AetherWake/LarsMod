using Nickel;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace AetherWake.LarsMod.Cards;

internal sealed class Fireball : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Fireball", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Lars_Deck.Deck,

                rarity = Rarity.rare,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Fireball", "name"]).Localize
        });
    }

    public override CardData GetData(State s)
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
                data = new CardData()
                {
                    cost = 2,
                    infinite=true
                };
                break;
            case Upgrade.B:
                data = new CardData(){
                    cost = 3,
                };
                break;
        }
        return data;
    }

    private int GetX(Combat c, State s)
	{
        var x = GetDmg(s, 0) + c.hand.Count;
		return x;
	}

    private int GetX(Combat c)
	{
        var x = c.hand.Count;
		return x;
	}

    public override List<CardAction> GetActions(State s, Combat c)
    {
        
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new AStatus(){ status=Status.drawLessNextTurn, statusAmount = 2, targetPlayer= true },
                    new AVariableHint
                    {
                        hand=true,
                    },
                    new AAttack(){
                        damage = GetX(c, s),
                        xHint = 1
                    },
                    new AStatus(){
                        xHint = 1,
                        status=Status.heat, statusAmount = GetX(c), targetPlayer=false
                    }
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new AStatus(){ status=Status.drawLessNextTurn, statusAmount = 2, targetPlayer= true },
                    new AVariableHint
                    {
                        hand=true,
                    },
                    new AAttack(){
                        damage = GetX(c, s),
                        xHint = 1
                    },
                    new AStatus(){
                        xHint = 1,
                        status=Status.heat, statusAmount = GetX(c), targetPlayer=false
                    }
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new AStatus(){ status=Status.drawLessNextTurn, statusAmount = 2, targetPlayer= true },
                    new AVariableHint
                    {
                        hand=true,
                    },
                    new AAttack(){
                        damage = GetX(c, s),
                        xHint = 2
                    },
                    new AStatus(){
                        xHint = 1,
                        status=Status.heat, statusAmount = GetX(c), targetPlayer=true
                    },
                    new AStatus(){
                        xHint = 1,
                        status=Status.heat, statusAmount = GetX(c), targetPlayer = false
                    }
                };
                break;
        }
        return actions;
    }

}