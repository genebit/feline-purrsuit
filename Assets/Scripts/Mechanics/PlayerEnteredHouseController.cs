using EasyTransition;
using UnityEngine;

public class PlayerEnteredHouseController : MonoBehaviour
{
    [SerializeField]
    private TransitionSettings transitionSettings;

    private bool canTransition = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Press E to continue");
            canTransition = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canTransition = false;
        }
    }

    private void Update()
    {
        if (canTransition && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed");
            TransitionManager.Instance().Transition("[1] Onboarding", transitionSettings, 0);
        }
    }
}
