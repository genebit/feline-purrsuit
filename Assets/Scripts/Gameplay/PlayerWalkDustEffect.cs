using Core;
using Model;
using UnityEngine;

public class PlayerWalkDustEffect : Simulation.Event<PlayerWalkDustEffect>
{
    private readonly IsoModel model = Simulation.GetModel<IsoModel>();

    public Vector2 movement;

    public override void Execute()
    {
        if (movement != Vector2.zero)
        {
            model.player.dustParticle.Play();
        }
        else
        {
            model.player.dustParticle.Stop();
        }
    }
}
