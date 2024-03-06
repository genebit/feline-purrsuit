using UnityEngine;

namespace Mechanics
{
    public class BoatsController : MonoBehaviour
    {
        public PatrolPath path;
        private PatrolPath.Mover mover;

        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        [Range(0f, 5f)]
        public float speed;

        void Update()
        {
            if (path != null)
            {
                if (mover == null) mover = path.CreateMover(speed * Time.deltaTime);

                Vector2 move = mover.Position - (Vector2)transform.position;
                move.x = Mathf.Clamp(move.x, -1, 1);

                transform.position += (Vector3)move;

                spriteRenderer.flipX = move.x <= 0;
            }
        }
    }
}