using System;
using System.Collections.Generic;
using FSPRO;
using Nickel;
public static class AquaRingHelper
{
    public static int HealTriggerDefault { get; } = 5;
    public static int HealTrigger = 5;

    public static void resetHealTrigger()
    {
        HealTrigger = HealTriggerDefault;
    }

   
}

