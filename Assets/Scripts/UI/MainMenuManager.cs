using Core;
using EasyTransition;
using UnityEngine;

namespace UI
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private TransitionSettings transitionSettings;
        [SerializeField] private Animator boatAnimation;
        [SerializeField] private AudioSource transitionSound;

        [Range(0, 3f)]
        public float loadDelay = 0;

        public void StartGame()
        {
            boatAnimation.SetBool("TakeOff", true);
            transitionSound.Play();
            TransitionManager.Instance().Transition("[1] Onboarding", transitionSettings, loadDelay);
            DeleteAllKeys();
        }

        public void ExitGame()
        {
            TransitionManager.Instance().Transition("[0] Main Menu", transitionSettings, loadDelay);
            transitionSound.Play();
            DeleteAllKeys();
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void DeleteAllKeys()
        {
            PlayerPrefs.DeleteKey(SaveKeys.GAMPLAY_TIMER);
            PlayerPrefs.DeleteKey(SaveKeys.TAMED_CATS);
            PlayerPrefs.DeleteKey(SaveKeys.TAME_METER);
            PlayerPrefs.DeleteKey(SaveKeys.WHITE_FISH);
            PlayerPrefs.DeleteKey(SaveKeys.TRIANGLE_FISH);
            PlayerPrefs.DeleteKey(SaveKeys.GOLD_FISH);
        }
    }

}
