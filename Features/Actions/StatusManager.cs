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
        if (timing == StatusTurnTriggerTiming.TurnStart && status == ModEntry.Instance.AquaRing.Status && ship.Get(ModEntry.Instance.AquaRing.Status) > 0)
        {
            if(ship.isPlayerShip == false){
                if(ship.Get(ModEntry.Instance.KokoroApiV2.OxidationStatus.Status)> 0 || ship.Get(Status.corrode) > 0){
                    ship.Add(ModEntry.Instance.KokoroApiV2.OxidationStatus.Status, 1);
                    ship.Add(ModEntry.Instance.AquaRing.Status, -1);
                }
            }
        }
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
        
    }

    private static Ship GetShip(AStatus instance, State s)
    {
        return instance.targetPlayer ? s.ship : ((Combat)s.route).otherShip;
    }
    
}