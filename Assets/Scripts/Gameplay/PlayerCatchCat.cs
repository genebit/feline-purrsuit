using Core;
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
            model.tamedCats.AddCat(cat);
        }
    }
}