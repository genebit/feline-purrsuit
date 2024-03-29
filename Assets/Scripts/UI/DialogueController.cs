using Core;
using EasyTransition;
using Model;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class DialogueController : MonoBehaviour
    {
        #region Inspector View
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private TextMeshProUGUI heading;

        [Range(10f, 100f)]
        public float textSpeed;

        [Header("Avatar 1")]
        [SerializeField] private UIOpacity avatar1;
        [SerializeField] private AudioSource avatar1AudioSource;

        [Header("Avatar 2")]
        [SerializeField] private UIOpacity avatar2;
        [SerializeField] private AudioSource avatar2AudioSource;
        [SerializeField] private string[] lines;

        [SerializeField] private TransitionSettings transitionSettings;
        #endregion

        private string[] speakerNames;
        private int index;
        private string playerName;

        private readonly IsoModel model = Simulation.GetModel<IsoModel>();

        private const string PLAYER = "[PLAYER]";
        private const string TOWN_MAYOR = "[TOWN MAYOR]";
        private const string S_TOWN_MAYOR = "TOWN MAYOR";

        // Start is called before the first frame update
        void Start()
        {
            // Initialize
            dialogueText.text = string.Empty;
            speakerNames = new string[lines.Length];
            playerName = PlayerPrefs.GetString(SaveKeys.PLAYER_NAME);

            ProcessLines();
            StartDialogue();
        }

        private void ProcessLines()
        {
            for (int i = 0; i < lines.Length; i++)
            {
                // find a word [PLAYER] and replace it with the
                // player's name through playerprefs PlayerName
                if (lines[i].Contains(PLAYER))
                {
                    lines[i] = lines[i].Replace(PLAYER, playerName).Trim();
                    speakerNames[i] = playerName.ToUpper();
                }
                // find a word [TOWN MAYOR] and remove it from dialogue text
                if (lines[i].Contains(TOWN_MAYOR))
                {
                    speakerNames[i] = S_TOWN_MAYOR;
                    lines[i] = lines[i].Replace(TOWN_MAYOR, string.Empty).Trim();
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {

                if (dialogueText.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    dialogueText.text = lines[index];
                }

                PlaySpeakerVoice();
            }
        }

        void PlaySpeakerVoice()
        {
            if (speakerNames[index].Equals(playerName.ToUpper()))
            {
                avatar1AudioSource.Play();
            }
            else
            {
                avatar2AudioSource.Play();
            }
        }

        void StartDialogue()
        {
            index = 0;
            StartCoroutine(TypeLine());
        }

        IEnumerator TypeLine()
        {
            SetSpeakerName();
            RemovePlayerNameOnDialogue();

            float waitTime = 1f / textSpeed;

            foreach (char c in lines[index].ToCharArray())
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(waitTime);
            }
        }

        void NextLine()
        {
            SetSpeakerName();
            RemovePlayerNameOnDialogue();

            if (index < lines.Length - 1)
            {
                index++;
                dialogueText.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                // specific only to scene [2.2] tutorial
                if (SceneManager.GetActiveScene().buildIndex == 4)
                {
                    TransitionManager.Instance().Transition("[4] Gameplay", transitionSettings, 0);
                }
                else
                {
                    // The dialogue scene is finished...
                    // get the parent of the game object and set it to inactive
                    transform.parent.gameObject.SetActive(false);
                    model.player.controlEnabled = true;
                    model.hudCanvas.SetActive(true);
                }
            }
        }

        void SetSpeakerName()
        {
            heading.text = speakerNames[index];

            if (speakerNames[index].Equals(playerName.ToUpper()))
            {
                avatar1.SetOpacity(1);
                // gradually transition the opacity from 1 to 0.5f of avatar2
                avatar2.SetOpacity(0.5f);
            }
            else
            {
                avatar1.SetOpacity(0.5f);
                avatar2.SetOpacity(1);
            }
        }

        void RemovePlayerNameOnDialogue()
        {
            // remove the player's name from the dialogue if it's the player's turn to speak
            if (speakerNames[index].Equals(playerName.ToUpper()))
            {
                lines[index] = lines[index].Replace(playerName, string.Empty).Trim();
            }
        }
    }
}