using System;
using System.Collections.Generic;
using FSPRO;
public class AEnchancedOverheat : AOverheat
{
    public override void Begin(G g, State s, Combat c)
    {
        timer = 1.0;
        Ship ship = (targetPlayer ? s.ship : c.otherShip);
        if (ship == null)
        {
            return;
        }
        
        ship.DirectHullDamage(s, c, ship.Get(Status.heat)/3);
        ship.Set(Status.heat, 0);
        Audio.Play(Event.Hits_HitHurt);
        ship.pendingEffects.Add(Ship.MiscEffects.Overheat);
        if (targetPlayer)
        {
            g.state.storyVars.justOverheated = true;
        }

        if (!targetPlayer)
        {
            return;
        }

        foreach (Artifact item in s.EnumerateAllArtifacts())
        {
            item.AfterPlayerOverheat(s, c);
        }
    }
}
