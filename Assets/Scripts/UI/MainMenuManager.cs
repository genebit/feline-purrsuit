using EasyTransition;
using UnityEngine;

namespace UI
{
    public class MainMenuManager : MonoBehaviour
    {
        public TransitionSettings transitionSettings;

        [Range(0, 3f)]
        public float loadDelay = 0;

        public void StartGame()
        {
            TransitionManager.Instance().Transition("Game Scene", transitionSettings, loadDelay);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }

}
