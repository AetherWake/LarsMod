using System;
using System.Collections.Generic;
using AetherWake.LarsMod;
using FSPRO;
using Nickel;


public static class AquaRingHelper
{
    public static int HealTriggerDefault { get; } = 5;
    public static int HealTrigger = 5;
    public static int maxAquaRing = 7;

    public static void resetHealTrigger()
    {
        HealTrigger = HealTriggerDefault;
    }

   	private sealed class AquaRingStatusLogicHook : IKokoroApi.IV2.IStatusLogicApi.IHook
    {
        private static ModEntry Instance => ModEntry.Instance;
        public int ModifyStatusChange(IKokoroApi.IV2.IStatusLogicApi.IHook.IModifyStatusChangeArgs args)
        {
            if (args.Status != (Status)Instance.AquaRing.Status)
                return args.NewAmount;
            return Math.Clamp(args.NewAmount, 0, Instance.aetherApi.getMaxAquaRing(args.Ship) + 1);
        }
    }
}

