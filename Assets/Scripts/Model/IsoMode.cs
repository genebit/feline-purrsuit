using Mechanic;
using Mechanics;
using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class IsoModel
    {
        public Cinemachine.CinemachineVirtualCamera virtualCamera;

        public PlayerController player;

        public InventoryController inventory;

        public ActionPromptController playerActionPrompt;

        public GameObject hudCanvas;

    }
}