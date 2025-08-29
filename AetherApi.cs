using AetherWake.LarsMod;

public class AetherApi
{
    public int getMaxAquaRing(Ship ship)
    {
        int extraWater = ship.Get(ModEntry.Instance.MaxAquaRing.Status);
        return AquaRingHelper.maxAquaRing + extraWater;
    }
}