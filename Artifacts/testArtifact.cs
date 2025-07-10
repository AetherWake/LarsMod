using System;
using System.Linq;
using System.Reflection;
using AetherWake.LarsMod;
using HarmonyLib;
using Nickel;

public class testArtifact : CustomArtifact, IDemoArtifact
{
	static int overheatDamageOverflow = 0;
	public static void Register(IModHelper helper)
	{
		helper.Content.Artifacts.RegisterArtifact("testArtifact", new()
		{
			ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
			Meta = new()
			{
				owner = ModEntry.Instance.DemoMod_Deck.Deck,
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
		Console.WriteLine("Overheat begins");
		__state = 0;
		if (s.EnumerateAllArtifacts().FirstOrDefault(a => a is testArtifact) is not { } artifact)
			return true;
		if (c.otherShip.Get(Status.heat) > 0)
		{

			artifact.Pulse();
			c.otherShip.overheatDamage = c.otherShip.Get(Status.heat) / 3;
			__state = c.otherShip.Get(Status.heat) % 3;
			//c.QueueImmediate(new AEnchancedOverheat() {
			//    targetPlayer = false
			//});
		}
		return true;
	}

	private static void AOverheat_Begin_Postfix(G g, State s, ref Combat c, int __state)
	{

		if (s.EnumerateAllArtifacts().FirstOrDefault(a => a is testArtifact) is { } artifact)
		{
			if (__state > 0)
			{
				c.otherShip.Set(Status.heat, __state);
			}
		}
	}

}
