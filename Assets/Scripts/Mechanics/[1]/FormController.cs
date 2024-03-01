using TMPro;
using UnityEngine;

namespace Mechanic
{
    public class FormController : MonoBehaviour
    {
        [SerializeField]
        private GameObject formCanvas;

        [SerializeField]
        private GameObject dialogueCanvas;

        [SerializeField]
        private TMP_InputField inputfield;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Submit();
            }
        }

        public void RandomizeName()
        {
            string[] randomNames = { "Bubbles", "Whiskers", "Bob", "Boomer", "Dizzy", "Doodle", "Bongo", "Tom", "Darren", "Karen", "Weeboo", "Borgor", "Gene" };
            inputfield.text = randomNames[Random.Range(0, randomNames.Length)];
        }

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
}