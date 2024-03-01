using Core;
using EasyTransition;
using Model;
using UnityEngine;

namespace Mechanic
{
    public class PlayerEnterTransitionableController : MonoBehaviour
    {
        [SerializeField]
        private TransitionSettings transitionSettings;

        [SerializeField]
        private string promptMessage;

        [SerializeField]
        [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")]
        private string transitionTo;

        private bool canTransition = false;


        private readonly IsoModel model = Simulation.GetModel<IsoModel>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                model.playerActionPrompt.Prompt(promptMessage);

                canTransition = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                model.playerActionPrompt.Close();

                canTransition = false;
            }
        }

        private void Update()
        {
            if (canTransition && Input.GetKeyDown(KeyCode.E))
            {
                TransitionManager.Instance().Transition(transitionTo, transitionSettings, 0);
            }
        }
    }
}