using EasyTransition;
using Gameplay;
using Model;
using UnityEngine;
using Utils;
using static Core.Simulation;

namespace Mechanics
{
    public class GameplayWinLoseController : MonoBehaviour
    {
        public TransitionSettings transitionSettings;
        [Header("Winner")]
        [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")]
        public string winTransitionScene;

        [Header("Loser")]
        [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")]
        public string loseTransitionScene;

        private readonly IsoModel model = GetModel<IsoModel>();

        private void Update()
        {
            if (HasCaughtAll())
            {
                Win();
            }
            else if (model.timer.GetCurrentTime() <= 0 && !HasCaughtAll())
            {
                Lose();
            }
        }

        public void Win()
        {
            var ev = Schedule<TransitionToScene>();
            ev.transitionTo = winTransitionScene;
            ev.transitionSettings = transitionSettings;
            Destroy(this);
        }

        public void Lose()
        {
            // Player lost, schedule an event.
            var ev = Schedule<TransitionToScene>();
            ev.transitionTo = loseTransitionScene;
            ev.transitionSettings = transitionSettings;
            Destroy(this);
        }

        private bool HasCaughtAll()
        {
            // check if all cats in the list are inactive
            foreach (GameObject cat in model.tamedCats.cats)
            {
                // Target the child (actual cat game object)
                GameObject c = cat.transform.GetChild(0).gameObject;

                // Check if the cat is inactive
                if (c.activeInHierarchy)
                {
                    model.tamedCats.hasTamedAll = false;
                    break;
                }
                model.tamedCats.hasTamedAll = true;
            }

            // check if the timer has value and the tame meter has value and has caught all cats
            return model.timer.GetCurrentTime() <= 0 && model.tameMeter.value > 0 && model.tamedCats.hasTamedAll;
        }
    }
}