using System;
using System.Collections.Generic;
using FSPRO;
using Nickel;
public static class AquaRingHelper
{
    private static int HealTriggerDefault = 5;
    public static int HealTrigger = 5;

    public static void resetHealTrigger()
    {
        HealTrigger = HealTriggerDefault;
    }
}

