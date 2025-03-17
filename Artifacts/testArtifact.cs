using System;
using System.Linq;
using System.Reflection;
using AetherWake.LarsMod;
using HarmonyLib;
using Nickel;

public class testArtifact : CustomArtifact, IDemoArtifact {
    
    private static bool DuringOverheat;
    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("testArtifact", new()
		{
			ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
			Meta = new()
			{
				owner = ModEntry.Instance.DemoMod_Deck.Deck,
				pools = ModEntry.GetArtifactPools(MethodBase.GetCurrentMethod()!.DeclaringType!)
			},
			Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/Artifacts/Briefcase.png")).Sprite,
			Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "testArtifact", "name"]).Localize,
			Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "testArtifact", "description"]).Localize
		});
    }

    protected internal override void ApplyPatches(IHarmony harmony){
		harmony.Patch(
			original: AccessTools.DeclaredMethod(typeof(AOverheat), nameof(AOverheat.Begin)),
			prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(AOverheat_Begin_Prefix)),
			finalizer: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(AOverheat_Begin_Finalizer))
		);
    }

    private static bool AOverheat_Begin_Prefix(G g, State s, ref Combat c){
        Console.WriteLine("Overheat begins");
		if (s.EnumerateAllArtifacts().FirstOrDefault(a => a is testArtifact) is not { } artifact)
			return true;
		if (c.otherShip.Get(Status.heat) > 0) {
            
            artifact.Pulse();
            c.otherShip.overheatDamage = c.otherShip.Get(Status.heat) / 3;
            
            Console.WriteLine(c.otherShip.Get(Status.heat) / 3);
            //c.QueueImmediate(new AEnchancedOverheat() {
            //    targetPlayer = false
            //});
            DuringOverheat = false;
        }
		return true;
    }
		

	private static void AOverheat_Begin_Finalizer()
		=> DuringOverheat = false;

}
