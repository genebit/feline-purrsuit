using UnityEngine;

namespace Mechanic
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;

        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // Input handling
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

            // Movement calculation
            Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized * moveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }
}
