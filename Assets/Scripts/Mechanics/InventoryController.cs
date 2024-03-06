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

        private void Start()
        {
            // Load the inventory from the player prefs
            whiteFish = PlayerPrefs.GetInt("WhiteFish", 0);
            triangleFish = PlayerPrefs.GetInt("TriangleFish", 0);
            goldFish = PlayerPrefs.GetInt("GoldFish", 0);
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
        }
    }
}