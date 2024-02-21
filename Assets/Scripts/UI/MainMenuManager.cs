using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public TransitionSettings transitionSettings;

    [Range(0, 3f)]
    public float loadDelay = 0;

    public void StartGame()
    {
        TransitionManager.Instance().Transition("Game Scene", transitionSettings, loadDelay);
    }

    public void QuitGame()
    {
        Application.Quit();
    }  
}
