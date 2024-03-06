using Core;
using EasyTransition;
using Mechanics;

namespace Gameplay
{
    public class PlayerDied : Simulation.Event<PlayerDied>
    {
        public PlayerDivingController player;

        public override void Execute()
        {
            TransitionManager.Instance().Transition(player.transitionTo, player.transitionSettings, 0);
        }
    }
}