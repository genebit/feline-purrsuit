using TMPro;
using UnityEngine;

namespace Mechanics
{
    public class InventoryController : MonoBehaviour
    {
        // 3 types of fishes to store in the inventory
        // common
        public int whiteFish = 0;
        // uncommon
        public int triangleFish = 0;
        // rare
        public int goldFish = 0;

        private int totalFishes;

        public TextMeshProUGUI resourceCounter;

        private void Start()
        {
            // Load the inventory from the player prefs
            whiteFish = PlayerPrefs.GetInt("WhiteFish", 0);
            triangleFish = PlayerPrefs.GetInt("TriangleFish", 0);
            goldFish = PlayerPrefs.GetInt("GoldFish", 0);

            totalFishes = whiteFish + triangleFish + goldFish;

            // Set the resource counter
            SetResourceCounter(totalFishes);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
            {
                Reset();
            }
        }

        public void SetResourceCounter(int value)
        {
            if (resourceCounter != null)
            {
                resourceCounter.text = value.ToString();
            }
        }

        public void AddFishToInventory(FishType fishType)
        {
            switch (fishType)
            {
                case FishType.WhiteFish:
                    whiteFish++;
                    PlayerPrefs.SetInt("WhiteFish", whiteFish);
                    break;
                case FishType.TriangleFish:
                    triangleFish++;
                    PlayerPrefs.SetInt("TriangleFish", triangleFish);
                    break;
                case FishType.GoldFish:
                    goldFish++;
                    PlayerPrefs.SetInt("GoldFish", goldFish);
                    break;
            }

            totalFishes = whiteFish + triangleFish + goldFish;

            SetResourceCounter(totalFishes);
        }

        private void Reset()
        {
            whiteFish = 0;
            PlayerPrefs.DeleteKey("WhiteFish");
            triangleFish = 0;
            PlayerPrefs.DeleteKey("TriangleFish");
            goldFish = 0;
            PlayerPrefs.DeleteKey("GoldFish");

            SetResourceCounter(0);
        }
    }
}