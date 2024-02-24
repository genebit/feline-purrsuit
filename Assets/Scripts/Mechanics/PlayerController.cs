using UnityEngine;

namespace Mechanic
{
    public class PlayerController : MonoBehaviour
    {
        #region Inspector View
        [Range(0, 5f)]
        public float moveSpeed = 2f;

        [Range(0, 5f)]
        public float sprintSpeed = 3f;
        public bool controlEnabled;

        public ParticleSystem dustParticle;
        public GameObject actionPrompt;
        #endregion

        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (controlEnabled)
            {
                // Input handling
                float horizontalInput = Input.GetAxisRaw("Horizontal");
                float verticalInput = Input.GetAxisRaw("Vertical");

                // Movement calculation
                Move(horizontalInput, verticalInput);

                // Sprint handling
                Sprint();
            }
        }

        private void Move(float moveHorizontal, float moveVertical)
        {
            Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized * moveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
            HandleDustParticle(movement);
        }

        private void HandleDustParticle(Vector2 movement)
        {
            if (movement != Vector2.zero)
            {
                dustParticle.Play();
            }
            else
            {
                dustParticle.Stop();
            }
        }

        private void Sprint()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, 1f);
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                moveSpeed = 2f;
            }
        }
    }
}
