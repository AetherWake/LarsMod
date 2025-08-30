using Nickel;
using OneOf.Types;
using System.Collections.Generic;
using System.Reflection;

namespace AetherWake.LarsMod.Cards;

internal sealed class LeechShot : Card, IDemoCard
{
    private static ModEntry Instance => ModEntry.Instance;
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("LeechShot", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.Solstice_Deck.Deck,

                rarity = Rarity.common,

                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "LeechShot", "name"]).Localize
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
                    cost = 2,
                    exhaust = true
                };
                break;
            case Upgrade.B:
                data = new CardData()
                {
                    cost = 2,
                    exhaust = true
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
                        thing=new ShieldDrone(){targetPlayer = true},
                    },
                    new AAttack(){ damage=GetDmg(s, 1), status = ModEntry.Instance.KokoroApiV2.OxidationStatus.Status , statusAmount = 1 },
                    
                };
                break;
            case Upgrade.A:
                actions = new()
                {
                    new ASpawn(){
                        thing=new AttackDrone(){bubbleShield = true, upgraded = true},
                    },
                    new AAttack(){ damage=GetDmg(s, 0), status = ModEntry.Instance.KokoroApiV2.OxidationStatus.Status , statusAmount = 1 },
                    new AAttack(){ damage=GetDmg(s, 0), status = ModEntry.Instance.KokoroApiV2.OxidationStatus.Status , statusAmount = 1 },
                };
                break;
            case Upgrade.B:
                actions = new()
                {
                    new ASpawn(){
                        thing=new ShieldDrone(){targetPlayer = true, bubbleShield = true},
                    },
                    new AAttack(){ damage=GetDmg(s, 1), status = ModEntry.Instance.KokoroApiV2.OxidationStatus.Status , statusAmount = 1 },
                };
                break;
        }
        return actions;
    }

}
