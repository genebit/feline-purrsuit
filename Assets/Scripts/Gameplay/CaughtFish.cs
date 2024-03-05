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
            // TODO: Debug this why its not adding to the inventory
            model.inventory.AddFishToInventory(fish.fishType);
        }
    }
}