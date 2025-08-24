// C:\Users\Administrator\Desktop\PROJECT\LarsMod\obj\Debug\net8.0\CobaltCore.dll
// Decompiled with ICSharpCode.Decompiler 9.1.0.7988

using System;
using System.Collections.Generic;
using AetherWake.LarsMod;
using Nanoray.PluginManager;
using Nickel;

public class BoostBall : StuffBase, IRegisterable
{
    public double particlesToEmit;

    private static ISpriteEntry DroneSprite = null!;
	private static ISpriteEntry DroneIcon = null!;

    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
	{
		DroneSprite = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/SmartShieldDrone.png"));
		DroneIcon = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/SmartShieldDroneIcon.png"));
	}
    public override List<Tooltip> GetTooltips()
     => [
         new GlossaryTooltip($"midrow.{ModEntry.Instance.Package.Manifest.UniqueName}::BoostBall")
            {
                Icon = DroneIcon.Sprite,
                TitleColor = Colors.midrow,
                Title = ModEntry.Instance.Localizations.Localize(["midrow", "BoostBall", "name"]),
                Description = ModEntry.Instance.Localizations.Localize(["midrow", "BoostBall", "description"]),
            },
            .. bubbleShield ? [new TTGlossary("midrow.bubbleShield")] : Array.Empty<Tooltip>()
     ];

    public override Spr? GetIcon()
    {
        return DroneIcon.Sprite;
    }

    public override string GetDialogueTag()
    {
        return "BoostBall";
    }

    public override double GetWiggleAmount()
    {
        return 1.0;
    }

    public override double GetWiggleRate()
    {
        return 1.0;
    }

    public override void Render(G g, Vec v)
    {
        Vec offset = GetOffset(g);
        DrawWithHilight(g, DroneSprite.Sprite, v + offset, Mutil.Rand((double)x + 0.1) > 0.5);
        particlesToEmit += g.dt * 20.0;
        while (particlesToEmit >= 1.0)
        {
            PFX.combatAdd.Add(new Particle
            {
                color = new Color(0.1, 0.3, 1.0),
                pos = new Vec(x * 16 + 1, v.y - 24.0) + offset + new Vec(7.5, 7.5) + Mutil.RandVel().normalized() * 6.0,
                vel = Mutil.RandVel() * 20.0,
                lifetime = 1.0,
                size = 2.0 + Mutil.NextRand() * 3.0,
                dragCoef = 1.0
            });
            particlesToEmit -= 1.0;
        }
    }

    public override List<CardAction>? GetActionsOnDestroyed(State s, Combat c, bool wasPlayer, int worldX)
    {
        return new List<CardAction>
        {
            new AStatus
            {
                status = Status.boost, statusAmount = 2,
                targetPlayer = wasPlayer
            }
        };
    }
}
