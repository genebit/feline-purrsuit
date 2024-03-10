using Core;
using EasyTransition;

namespace Gameplay
{
    public class TransitionToScene : Simulation.Event<TransitionToScene>
    {
        public string transitionTo;
        public TransitionSettings transitionSettings;

        public override void Execute()
        {
            TransitionManager.Instance().Transition(transitionTo, transitionSettings, 0);
        }
    }
}