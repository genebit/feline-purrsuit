using UnityEngine;

namespace Visuals
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;

        private Vector2 mousePosition;

        private void Update()
        {
            // Click left mouse button to turn particles on
            // and place them at mouse position
            if (Input.GetMouseButtonDown(0))
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                particles.Play();
                particles.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
            }
        }
    }
}