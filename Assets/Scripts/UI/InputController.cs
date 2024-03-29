using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class InputController : MonoBehaviour
    {
        private Camera mainCamera;
        [SerializeField] private MainMenuManager menuManager;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (!context.started) return;

            var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

            if (!rayHit.collider) return;

            if (rayHit.collider.gameObject.name == "Start Area") menuManager.StartGame();
        }
    }
}