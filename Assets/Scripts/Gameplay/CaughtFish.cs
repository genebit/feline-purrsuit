using Core;
using Mechanics;
using Model;

namespace Gameplay
{
    public class CaughtFish : Simulation.Event<CaughtFish>
    {
        public FishController fish;
        private readonly IsoModel model = Simulation.GetModel<IsoModel>();

        public override void Execute()
        {
            model.inventory.AddFishToInventory(fish.fishType);
        }
    }
}