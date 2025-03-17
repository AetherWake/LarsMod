
using Nickel;
using System.Collections.Generic;
using System.Linq;

namespace AetherWake.LarsMod;

public abstract class CustomArtifact : Artifact
{
	protected static ModEntry Instance => ModEntry.Instance;

	protected internal virtual void ApplyPatches(IHarmony harmony)
	{
	}

	protected internal virtual void ApplyLatePatches(IHarmony harmony)
	{
	}

}