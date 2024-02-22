using TMPro;
using UnityEngine;

public class FormController : MonoBehaviour
{
    [SerializeField]
    private GameObject formCanvas;

    [SerializeField]
    private GameObject dialogueCanvas;

    [SerializeField]
    private TMP_InputField inputfield;

    public void Submit()
    {
        // Form validation
        if (inputfield.text.Trim().Length != 0)
        {
            // Store it into player prefs
            PlayerPrefs.SetString("PlayerName", inputfield.text);

            formCanvas.SetActive(false);
            dialogueCanvas.SetActive(true);
        }
    }
}
