using Mechanic;
using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class IsoModel
    {
        public Cinemachine.CinemachineVirtualCamera virtualCamera;

        public PlayerController player;

        public ActionPromptController playerActionPrompt;

        public GameObject hudCanvas;

    }
}