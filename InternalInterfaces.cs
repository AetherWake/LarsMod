using Nanoray.PluginManager;
using Nickel;

namespace AetherWake.LarsMod;

/* Much like a namespace, these interfaces can be named whatever you'd like.
 * We recommend using descriptive names for what they're supposed to do.
 * In this case, we use the IDemoCard interface to call for a Card's 'Register' method */
internal interface IDemoCard
{
    static abstract void Register(IModHelper helper);
    
}

internal interface IDemoArtifact
{
    protected static ModEntry Instance => ModEntry.Instance;
    static abstract void Register(IModHelper helper);
    protected internal void ApplyPatches(IHarmony harmony) { }
}

internal interface IRegisterable
{
    static abstract void Register(IPluginPackage<IModManifest> package, IModHelper helper);
}
