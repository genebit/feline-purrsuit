using Core;
using Model;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    #region Inspector View
    [SerializeField]
    private TextMeshProUGUI dialogueText;

    [SerializeField]
    private TextMeshProUGUI heading;

    [Range(10f, 100f)]
    public float textSpeed;

    [SerializeField]
    private string[] lines;
    #endregion

    private int index;

    private readonly IsoModel model = Simulation.GetModel<IsoModel>();

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
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
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        float waitTime = 1f / textSpeed;

        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(waitTime);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            // get the parent of the game object and set it to inactive
            transform.parent.gameObject.SetActive(false);

            model.player.controlEnabled = true;
        }
    }
}