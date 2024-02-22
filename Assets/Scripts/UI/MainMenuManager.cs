using EasyTransition;
using UnityEngine;

namespace UI
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField]
        private TransitionSettings transitionSettings;

        [SerializeField]
        private Animator boatAnimation;

        [Range(0, 3f)]
        public float loadDelay = 0;

        public void StartGame()
        {
            boatAnimation.SetBool("TakeOff", true);
            TransitionManager.Instance().Transition("Game Scene", transitionSettings, loadDelay);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }

}
