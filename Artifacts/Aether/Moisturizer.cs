using System;
using System.Linq;
using System.Reflection;
using AetherWake.LarsMod;
using HarmonyLib;
using Nickel;

public class Moisturizer : CustomArtifact, IDemoArtifact
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("Moisturizer", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.Aether_Deck.Deck,
                pools = [ArtifactPool.Common]
            },
            Sprite = helper.Content.Sprites.RegisterSprite(ModEntry.Instance.Package.PackageRoot.GetRelativeFile("assets/Artifacts/Briefcase.png")).Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Moisturizer", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "Moisturizer", "description"]).Localize
        });
    }

    protected internal override void ApplyPatches(IHarmony harmony)
    {
        AquaRingHelper.resetHealTrigger();
        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Combat), nameof(Combat.OnEnter)),
            prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(Combat_OnEntry_Prefix))
        );

        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Combat), nameof(Combat.PlayerWon)),
            prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(Combat_PlayerWon_Prefix))
        );

    }

    private static bool Combat_OnEntry_Prefix(State s)
    {
        if (s.EnumerateAllArtifacts().FirstOrDefault(a => a is Moisturizer) is not { } artifact)
            return true;
        AquaRingHelper.HealTrigger -= 1;
        return true;
    }
    
    private static bool Combat_PlayerWon_Prefix(G g)
	{
        AquaRingHelper.resetHealTrigger();
		return true;
	}

}
