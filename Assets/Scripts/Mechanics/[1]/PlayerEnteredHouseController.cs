using Core;
using EasyTransition;
using Model;
using UnityEngine;

public class PlayerEnteredHouseController : MonoBehaviour
{
    [SerializeField]
    private TransitionSettings transitionSettings;

    private bool canTransition = false;

    private readonly IsoModel model = Simulation.GetModel<IsoModel>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            model.actionPrompt.Prompt("Press E to unpack");

            canTransition = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            model.actionPrompt.Close();

            canTransition = false;
        }
    }

    private void Update()
    {
        // NOTE(GENE): this is specific only for scene [1] Onboarding
        if (canTransition && Input.GetKeyDown(KeyCode.E))
        {
            TransitionManager.Instance().Transition("[1] Onboarding", transitionSettings, 0);
        }
    }
}
