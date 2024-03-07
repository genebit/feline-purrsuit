using Core;
using EasyTransition;
using Gameplay;
using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using static Core.Simulation;

namespace Mechanics
{
    /// <summary>
    /// Contains the logic for the tamed cats in the house
    /// </summary>
    public class TamedCatsController : MonoBehaviour
    {
        #region Inspector View
        public Slider tameMeter;
        public List<GameObject> cats;
        [Range(0, 10)]
        public int decreaseBySeconds;
        [Range(0, 10)]
        public int releaseDelay;

        public bool hasTamedAll;

        #region WinLose
        [Header("WinLose")]

        [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")]
        public string transitionToWinner;
        [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")]
        public string transitionToGameOver;
        public TransitionSettings transitionSettings;
        #endregion
        #endregion

        private const int SECOND = 60;

        private readonly IsoModel model = Simulation.GetModel<IsoModel>();

        // Use this for initialization
        void Start()
        {
            tameMeter.maxValue = cats.Count * SECOND;
            tameMeter.value = 0;
        }

        // Update is called once per frame
        void Update()
        {
            // if the tame meter has value, then keep decreasing
            // it by decreaseBySecond every second
            if (tameMeter.value > 0)
            {
                tameMeter.value -= Time.deltaTime * decreaseBySeconds;
            }
            else
            {
                ReleaseCats();
            }


            // Player won, schedule an event.
            if (HasCaughtAll())
            {
                var ev = Schedule<TransitionToScene>();
                ev.transitionTo = transitionToWinner;
                ev.transitionSettings = transitionSettings;
                Destroy(this);
            }
            else if (model.timer.GetCurrentTime() <= 0 && !HasCaughtAll())
            {
                // Player lost, schedule an event.
                var ev = Schedule<TransitionToScene>();
                ev.transitionTo = transitionToGameOver;
                ev.transitionSettings = transitionSettings;
                Destroy(this);
            }
        }

        private bool HasCaughtAll()
        {
            // check if all cats in the list are inactive
            foreach (GameObject cat in cats)
            {
                // Target the child (actual cat game object)
                GameObject c = cat.transform.GetChild(0).gameObject;

                // Check if the cat is inactive
                if (c.activeInHierarchy)
                {
                    hasTamedAll = false;
                    break;
                }
                hasTamedAll = true;
            }

            // check if the timer has value and the tame meter has value and has caught all cats
            return (model.timer.GetCurrentTime() <= 0 && (tameMeter.value > 0 && hasTamedAll));
        }

        public void AddCat(GameObject cat)
        {
            cat.SetActive(false);
            tameMeter.value += SECOND;
        }

        private void ReleaseCats()
        {
            StartCoroutine(ReleaseCatsWithDelay());
        }

        IEnumerator ReleaseCatsWithDelay()
        {
            foreach (GameObject cat in cats)
            {
                // Target the child (actual cat game object)
                GameObject c = cat.transform.GetChild(0).gameObject;

                // Check if the cat is inactive
                if (!c.activeInHierarchy)
                {
                    yield return new WaitForSeconds(releaseDelay);
                    StartCoroutine(Release(c));
                }
            }
        }

        IEnumerator Release(GameObject cat)
        {
            cat.SetActive(true);
            yield return null;
        }
    }
}