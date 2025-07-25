using System;
using System.Linq;
using System.Reflection;
using AetherWake.LarsMod;
using HarmonyLib;
using Nickel;

public class overheatArtifact : CustomArtifact, IDemoArtifact
{
	public static void Register(IModHelper helper)
	{
		helper.Content.Artifacts.RegisterArtifact("testArtifact", new()
		{
			ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
			Meta = new()
			{
				owner = ModEntry.Instance.Lars_Deck.Deck,
				pools = [ArtifactPool.Common]
			},
			Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/Artifacts/Briefcase.png")).Sprite,
			Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "testArtifact", "name"]).Localize,
			Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "testArtifact", "description"]).Localize
		});
	}

	protected internal override void ApplyPatches(IHarmony harmony)
	{
		harmony.Patch(
			original: AccessTools.DeclaredMethod(typeof(AOverheat), nameof(AOverheat.Begin)),
			prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(AOverheat_Begin_Prefix)),
			postfix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(AOverheat_Begin_Postfix))
		);
	}

	private static bool AOverheat_Begin_Prefix(G g, State s, ref Combat c, out int __state)
	{
		__state = 0;
		if (s.EnumerateAllArtifacts().FirstOrDefault(a => a is overheatArtifact) is not { } artifact)
			return true;
		if (c.otherShip.Get(Status.heat) > 0)
		{
			artifact.Pulse();
			c.otherShip.overheatDamage += (c.otherShip.Get(Status.heat) / c.otherShip.heatTrigger) -1;
			__state = c.otherShip.Get(Status.heat) % c.otherShip.heatTrigger;
			//c.QueueImmediate(new AEnchancedOverheat() {
			//    targetPlayer = false
			//});
		}
		return true;
	}

	private static void AOverheat_Begin_Postfix(G g, State s, ref Combat c, int __state)
	{

		if (s.EnumerateAllArtifacts().FirstOrDefault(a => a is overheatArtifact) is { } artifact)
		{
			if (__state > 0)
			{
				c.otherShip.Set(Status.heat, __state);
			}
		}
	}

}
