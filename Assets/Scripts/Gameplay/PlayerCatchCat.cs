using Core;
using Mechanics;
using Model;

namespace Gameplay
{
    public class PlayerCatchCat : Simulation.Event<PlayerCatchCat>
    {
        private readonly IsoModel isoModel = Simulation.GetModel<IsoModel>();
        public CatController cat;

        public override void Execute()
        {
            UnityEngine.Debug.Log("Catch cat!");
        }
    }
}