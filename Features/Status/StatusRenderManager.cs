using System.Collections.Generic;
using System.Drawing;
using AetherWake.LarsMod;

internal sealed class StatusRenderManager : IKokoroApi.IV2.IStatusRenderingApi.IHook
{
    private static ModEntry Instance => ModEntry.Instance;

    internal StatusRenderManager()
    {
        Instance.KokoroApiV2.StatusRendering.RegisterHook(this, double.MinValue);
    }

    public IEnumerable<(Status Status, double Priority)> GetExtraStatusesToShow(IKokoroApi.IV2.IStatusRenderingApi.IHook.IGetExtraStatusesToShowArgs args)
    {
        if(args.Ship.Get(ModEntry.Instance.AquaRing.Status) > 0)
            yield return (Status: (Status)Instance.AquaRing.Status, Priority: 10);
    }

    public (IReadOnlyList<Color> Colors, int? BarSegmentWidth)? OverrideStatusRenderingAsBars(IKokoroApi.IV2.IStatusRenderingApi.IHook.IOverrideStatusRenderingAsBarsArgs args)
    {
        if (args.Status != (Status)Instance.AquaRing.Status)
            return null;
        var neutralColor = new Color("8AD2DE");

        var barCount = Instance.aetherApi.getMaxAquaRing(args.Ship);
        var colors = new Color[barCount];

        for (var barIndex = 0; barIndex < colors.Length; barIndex++)
        {
            var aquaRing = args.Ship.Get(ModEntry.Instance.AquaRing.Status);
            if (aquaRing > barIndex)
                colors[barIndex] = neutralColor;
            else colors[barIndex] = Instance.KokoroApiV2.StatusRendering.DefaultInactiveStatusBarColor;
        }
        return (colors, null);
    }
}