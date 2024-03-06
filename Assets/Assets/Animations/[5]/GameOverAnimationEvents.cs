using Core;
using Model;
using UnityEngine;

public class GameOverAnimationEvents : MonoBehaviour
{
    private readonly IsoModel model = Simulation.GetModel<IsoModel>();
    public GameObject gameOverTitle;

    public void Land()
    {
        model.playerActionPrompt.Prompt("You’ve cat to be kitten me!");
    }

    public void ShowTitleScreen()
    {
        gameOverTitle.SetActive(true);
    }
}
