using Core;
using Model;
using UnityEngine;

public class ActionPromptController : MonoBehaviour
{
    [SerializeField]
    private TextTypewriter actionPromptText;
    private GameObject actionPromptTextBackground;

    private readonly IsoModel model = Simulation.GetModel<IsoModel>();

    private void Start()
    {
        actionPromptTextBackground = transform.GetChild(0).gameObject;
    }

    public void Prompt(string message)
    {
        actionPromptTextBackground.SetActive(true);
        actionPromptText.fullText = message;
        actionPromptText.PlayText();
    }

    public void Close()
    {
        actionPromptTextBackground.SetActive(false);
    }
}
