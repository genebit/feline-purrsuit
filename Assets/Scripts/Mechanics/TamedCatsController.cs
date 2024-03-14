using Core;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Mechanics
{
    [System.Serializable]
    public class TamedCatsModel
    {
        public List<string> cats;
    }

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
        public bool hasTamedAll;

        [SerializeField] private AudioSource catSound;
        #endregion

        private const int TOTAL_CATS = 20;
        private const int SECOND = 60;

        private readonly TamedCatsModel tamedCatsObj = new TamedCatsModel();
        private readonly IsoModel model = Simulation.GetModel<IsoModel>();

        // Use this for initialization
        void Start()
        {
            tamedCatsObj.cats = new List<string>();
            // retrieve the tamed cats from the save file
            string tamedCatsJson = PlayerPrefs.GetString(SaveKeys.TAMED_CATS, null);
            if (tamedCatsJson.Length > 0)
            {
                List<string> idsOfTamedCats = JsonUtility.FromJson<TamedCatsModel>(tamedCatsJson).cats;
                tamedCatsObj.cats = idsOfTamedCats;
                HideAllTamedCats(idsOfTamedCats);
            }

            tameMeter.maxValue = TOTAL_CATS * SECOND;
            tameMeter.value = PlayerPrefs.GetFloat(SaveKeys.TAME_METER, 0);

        }

        void Update()
        {
            // if the tame meter has value, then keep decreasing
            // it by decreaseBySecond every second
            if (tameMeter.value > 0)
            {
                tameMeter.value -= Time.deltaTime * decreaseBySeconds;

                // if the tame meter reaches 0, then prompt the player
                if (tameMeter.value == 0)
                    StartCoroutine(LostCatsPrompt());
            }
            else
            {
                ReleaseCats();
            }
        }

        public void CatCaught(GameObject cat)
        {
            catSound.Play();
            tamedCatsObj.cats.Add(cat.name);
            cat.SetActive(false);

            tameMeter.value += SECOND;
            UpdateKey();
        }

        IEnumerator LostCatsPrompt()
        {
            model.playerActionPrompt.Prompt("Darn! the cats escaped!");
            yield return new WaitForSeconds(4);
            model.playerActionPrompt.Close();
        }

        private void HideAllTamedCats(List<string> tamedCats)
        {
            if (tamedCats.Any())
                foreach (string catId in tamedCats)
                    foreach (GameObject cat in cats)
                    {
                        GameObject c = cat.transform.GetChild(0).gameObject;
                        if (c.name == catId)
                            c.SetActive(false);
                    }
        }

        private void ReleaseCats()
        {
            if (cats.Any())
            {
                foreach (GameObject cat in cats)
                {
                    // Target the child (actual cat game object)
                    GameObject c = cat.transform.GetChild(0).gameObject;

                    // Check if the cat is inactive
                    if (!c.activeInHierarchy)
                    {
                        c.SetActive(true);

                        // remove the cat from the tamed cats list
                        tamedCatsObj.cats.Remove(c.name);
                        UpdateKey();
                    }
                }
            }

            if (SceneManager.GetActiveScene().name.Equals("[6] Underwater"))
                PlayerPrefs.DeleteKey(SaveKeys.TAMED_CATS);
        }

        private void UpdateKey()
        {
            string data = JsonUtility.ToJson(tamedCatsObj);
            PlayerPrefs.SetString(SaveKeys.TAMED_CATS, data);
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetFloat(SaveKeys.TAME_METER, tameMeter.value);
            PlayerPrefs.Save();
        }
    }
}