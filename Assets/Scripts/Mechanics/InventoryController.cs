using Core;
using Model;
using TMPro;
using UnityEngine;

namespace Mechanics
{
    public class InventoryController : MonoBehaviour
    {
        // 3 types of fishes to store in the inventory
        [Header("White Fish (Common)")]
        // common
        public int whiteFish = 0;
        public TextMeshProUGUI whiteFishTotalText;
        [Header("Triangle Fish (Uncommon)")]
        // uncommon
        public int triangleFish = 0;
        public TextMeshProUGUI triangleFishTotalText;
        [Header("Gold Fish (Rare)")]
        // rare
        public int goldFish = 0;
        public TextMeshProUGUI goldFishTotalText;

        private readonly IsoModel model = Simulation.GetModel<IsoModel>();

        private void Start()
        {
            // Load the inventory from the player prefs
            whiteFish = PlayerPrefs.GetInt(SaveKeys.WHITE_FISH, 0);
            whiteFishTotalText.text = whiteFish.ToString();

            triangleFish = PlayerPrefs.GetInt(SaveKeys.TRIANGLE_FISH, 0);
            triangleFishTotalText.text = triangleFish.ToString();

            goldFish = PlayerPrefs.GetInt(SaveKeys.GOLD_FISH, 0);
            goldFishTotalText.text = goldFish.ToString();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
            {
                Reset();
            }
        }

        public void AddFishToInventory(FishType fishType)
        {
            switch (fishType)
            {
                case FishType.WhiteFish:
                    whiteFish++;
                    PlayerPrefs.SetInt(SaveKeys.WHITE_FISH, whiteFish);
                    whiteFishTotalText.text = whiteFish.ToString();
                    break;
                case FishType.TriangleFish:
                    triangleFish++;
                    PlayerPrefs.SetInt(SaveKeys.TRIANGLE_FISH, triangleFish);
                    triangleFishTotalText.text = triangleFish.ToString();
                    break;
                case FishType.GoldFish:
                    goldFish++;
                    PlayerPrefs.SetInt(SaveKeys.GOLD_FISH, goldFish);
                    goldFishTotalText.text = goldFish.ToString();
                    break;
            }
        }

        public void RemoveFishFromInventory(FishType fishType)
        {
            switch (fishType)
            {
                case FishType.WhiteFish:
                    // decrement until 0
                    if (whiteFish > 0) whiteFish--;
                    PlayerPrefs.SetInt(SaveKeys.WHITE_FISH, whiteFish);
                    whiteFishTotalText.text = whiteFish.ToString();
                    break;
                case FishType.TriangleFish:
                    if (triangleFish > 0) triangleFish--;
                    PlayerPrefs.SetInt(SaveKeys.TRIANGLE_FISH, triangleFish);
                    triangleFishTotalText.text = triangleFish.ToString();
                    break;
                case FishType.GoldFish:
                    if (goldFish > 0) goldFish--;
                    PlayerPrefs.SetInt(SaveKeys.GOLD_FISH, goldFish);
                    goldFishTotalText.text = goldFish.ToString();
                    break;
            }
        }

        private void Reset()
        {
            whiteFish = 0;
            PlayerPrefs.DeleteKey(SaveKeys.WHITE_FISH);
            whiteFishTotalText.text = whiteFish.ToString();
            triangleFish = 0;
            PlayerPrefs.DeleteKey(SaveKeys.TRIANGLE_FISH);
            triangleFishTotalText.text = whiteFish.ToString();
            goldFish = 0;
            PlayerPrefs.DeleteKey(SaveKeys.GOLD_FISH);
            goldFishTotalText.text = whiteFish.ToString();
        }
    }
}