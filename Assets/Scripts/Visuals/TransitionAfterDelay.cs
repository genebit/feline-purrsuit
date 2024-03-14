using EasyTransition;
using System.Collections;
using UnityEngine;
using Utils;

public class TransitionAfterDelay : MonoBehaviour
{
    [SerializeField] private TransitionSettings transitionSettings;
    [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")]
    [SerializeField] private string transitionTo;
    [Range(0, 20f)]
    [SerializeField] private float transitionDelay;

    void Start()
    {
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        yield return new WaitForSeconds(transitionDelay);
        TransitionManager.Instance().Transition(transitionTo, transitionSettings, 0f);
    }
}
