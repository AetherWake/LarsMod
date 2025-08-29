using System;
using System.Collections.Generic;
using AetherWake.LarsMod;
using FSPRO;
using Nickel;


public static class AquaRingHelper
{
    public static int HealTriggerDefault { get; } = 5;
    public static int maxAquaRing = 7;



}
public class AquaRingStatusManager
{
    private static ModEntry Instance => ModEntry.Instance;
    internal AquaRingStatusManager()
    {
        Instance.KokoroApiV2.StatusLogic.RegisterHook(new AquaRingStatusLogicHook(), double.MinValue);
    }
    private sealed class AquaRingStatusLogicHook : IKokoroApi.IV2.IStatusLogicApi.IHook
    {
        
        public int ModifyStatusChange(IKokoroApi.IV2.IStatusLogicApi.IHook.IModifyStatusChangeArgs args)
        {
            if (args.Status != (Status)Instance.AquaRing.Status)
                return args.NewAmount;
            int xx = Math.Clamp(args.NewAmount, 0, Instance.aetherApi.getMaxAquaRing(args.Ship));
            return xx;
        }
    }
}

