using Core;
using Mechanics;
using Model;
using UnityEngine;

namespace Gameplay
{
    public class PlayerCatchCat : Simulation.Event<PlayerCatchCat>
    {
        private readonly IsoModel model = Simulation.GetModel<IsoModel>();
        public GameObject cat;

        public override void Execute()
        {
            var catController = cat.transform.parent.GetComponent<CatController>();
            var playerInventory = model.player.GetComponent<InventoryController>();

            switch (catController.catType)
            {
                case CatType.White:
                    if (playerInventory.whiteFish > 0)
                    {
                        model.tamedCats.CatCaught(cat);
                        model.inventory.RemoveFishFromInventory(FishType.WhiteFish);
                    }
                    break;
                case CatType.WhiteSpots:
                    if (playerInventory.whiteFish > 0)
                    {
                        model.tamedCats.CatCaught(cat);
                        model.inventory.RemoveFishFromInventory(FishType.WhiteFish);
                    }
                    break;
                case CatType.Blue:
                    if (playerInventory.whiteFish > 0)
                    {
                        model.tamedCats.CatCaught(cat);
                        model.inventory.RemoveFishFromInventory(FishType.WhiteFish);
                    }
                    break;
                case CatType.Yellow:
                    if (playerInventory.triangleFish > 0)
                    {
                        model.tamedCats.CatCaught(cat);
                        model.inventory.RemoveFishFromInventory(FishType.TriangleFish);
                    }
                    break;
                case CatType.Black:
                    if (playerInventory.goldFish > 0)
                    {
                        model.tamedCats.CatCaught(cat);
                        model.inventory.RemoveFishFromInventory(FishType.GoldFish);
                    }
                    break;
                case CatType.Orange:
                    if (playerInventory.goldFish > 0)
                    {
                        model.tamedCats.CatCaught(cat);
                        model.inventory.RemoveFishFromInventory(FishType.GoldFish);
                    }
                    break;
            }

        }
    }
}