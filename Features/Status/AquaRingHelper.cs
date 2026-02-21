using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using AetherWake.LarsMod;
using FMOD.Studio;
using FSPRO;
using HarmonyLib;
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

    public static void ApplyPatches(IHarmony harmony)
	{
		harmony.Patch(
			original: AccessTools.DeclaredMethod(typeof(AOverheat), nameof(AOverheat.Begin)),
			prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(AOverheat_Begin_Prefix))
		);
	}

	private static bool AOverheat_Begin_Prefix(G g, State s, ref Combat c, out int __state)
	{
		__state = 0;
		
		if (c.otherShip.Get(Status.heat) > 0)
		{
			c.otherShip.overheatDamage += (c.otherShip.Get(Status.heat) / c.otherShip.heatTrigger) -1;
			__state = c.otherShip.Get(Status.heat) % c.otherShip.heatTrigger;
			//c.QueueImmediate(new AEnchancedOverheat() {
			//    targetPlayer = false
			//});
		}
        if (c.otherShip.Get(ModEntry.Instance.AquaRing.Status) > 0)
        {
            if (c.otherShip.Get(Status.heat) >= c.otherShip.heatTrigger)
            {
                c.otherShip.DirectHullDamage(s, c, 1);
                Audio.Play(Event.Hits_HitHurt);
            }
        }
		return true;
	}


    
}

