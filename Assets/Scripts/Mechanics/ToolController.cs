using UnityEngine;

namespace Mechanics
{
    public class ToolController : MonoBehaviour
    {
        private Animator animator;
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            // Flip the sprite based on the mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spriteRenderer.flipX = mousePosition.x <= transform.position.x;

            if (Input.GetMouseButtonDown(0))
            {
                Catch();
            }
        }

        void Catch()
        {
            animator.SetTrigger((spriteRenderer.flipX) ? "Catch" : "CatchInvert");
        }
    }
}