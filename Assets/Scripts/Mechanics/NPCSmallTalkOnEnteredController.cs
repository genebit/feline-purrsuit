using Core;
using Model;
using UnityEngine;

public class NPCSmallTalkOnEnteredController : MonoBehaviour
{
    [SerializeField]
    private string dialogue;

    private readonly IsoModel model = Simulation.GetModel<IsoModel>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInChildren<ActionPromptController>().Prompt(dialogue);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInChildren<ActionPromptController>().Close();
        }
    }
}
