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
        if (timing == StatusTurnTriggerTiming.TurnEnd && status == ModEntry.Instance.AquaRing.Status && ship.Get(ModEntry.Instance.AquaRing.Status) >= 5)
        {
            if (!(ship.hull == ship.hullMax))
            {
                var AcidArmor = ship.Get(ModEntry.Instance.AquaRing.Status);
                combat.Queue(new AHeal
                {
                    healAmount = 1,
                    targetPlayer = ship.isPlayerShip
                });
                ship.Set(ModEntry.Instance.AquaRing.Status, AcidArmor - 5);
            }
        
        }
    }

    private static Ship GetShip(AStatus instance, State s)
    {
        return instance.targetPlayer ? s.ship : ((Combat)s.route).otherShip;
    }
}