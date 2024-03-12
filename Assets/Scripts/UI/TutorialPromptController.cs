using Core;
using UnityEngine;

namespace UI
{
    public class TutorialPromptController : MonoBehaviour
    {
        [SerializeField] private GameObject HUD;
        private void Start()
        {
            // Check if the player has seen the tutorial
            if ((PlayerPrefs.GetInt(SaveKeys.SEEN_GAMPLAY_TUTORIAL, 0)) == 1)
            {
                gameObject.SetActive(false);
                HUD.SetActive(true);
            }
            else
            {
                // This is the first time the player is playing the game
                PlayerPrefs.SetInt(SaveKeys.SEEN_GAMPLAY_TUTORIAL, 1);
                PlayerPrefs.Save();
            }
        }
    }
}