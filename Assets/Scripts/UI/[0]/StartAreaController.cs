using UnityEngine;

namespace UI
{
    public class StartAreaController : MonoBehaviour
    {
        [SerializeField] private GameObject optionMenu;
        [SerializeField] private GameObject startArea;

        private void Update()
        {
            startArea.SetActive(!optionMenu.activeSelf);
        }
    }
}