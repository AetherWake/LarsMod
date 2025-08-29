using System;
using AetherWake.LarsMod;
using HarmonyLib;
using Microsoft.Xna.Framework.Graphics;

namespace AetherWake.Features;

[HarmonyPatch]
public class StatusManager : IStatusLogicHook
{
    public StatusManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
    }

    public void OnStatusTurnTrigger(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status,
        int oldAmount, int newAmount)
    {
        
        if (timing == StatusTurnTriggerTiming.TurnStart && status == ModEntry.Instance.RainDance.Status && ship.Get(ModEntry.Instance.RainDance.Status) >= 1)
        {
            var RainDance = ship.Get(ModEntry.Instance.RainDance.Status);
            combat.Queue(new AStatus()
            {
                status = ModEntry.Instance.AquaRing.Status,
                statusAmount = RainDance,
                targetPlayer = ship.isPlayerShip
            });
        }
        
        if (timing == StatusTurnTriggerTiming.TurnEnd && status == ModEntry.Instance.AquaRing.Status && ship.Get(ModEntry.Instance.AquaRing.Status) >= AquaRingHelper.HealTriggerDefault)
        {
            if (!(ship.hull == ship.hullMax))
            {
                var AcidArmor = ship.Get(ModEntry.Instance.AquaRing.Status);
                if (!ship.isPlayerShip)
                {
                    combat.Queue(new AHeal
                    {
                        healAmount = 1,
                        targetPlayer = ship.isPlayerShip
                    });
                }
                ship.Set(ModEntry.Instance.AquaRing.Status, AcidArmor - AquaRingHelper.HealTriggerDefault);
            }
        }
        
    }

    private static Ship GetShip(AStatus instance, State s)
    {
        return instance.targetPlayer ? s.ship : ((Combat)s.route).otherShip;
    }
    
}