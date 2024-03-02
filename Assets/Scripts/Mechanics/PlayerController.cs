using Gameplay;
using UnityEngine;
using UnityEngine.UI;
using static Core.Simulation;

namespace Mechanic
{
    public class PlayerController : MonoBehaviour
    {
        #region Inspector View
        [Range(0, 5f)]
        public float moveSpeed = 2.5f;

        [HideInInspector]
        public float originalSpeed;

        #region Sprinting
        [Range(0, 5f)]
        [Header("Sprinting")]
        public float sprintSpeed = 3.5f;

        public Slider staminaSlider;
        [Range(0, 50f)]
        public float decreaseSpeed = 25f;
        [Range(0, 50f)]
        public float recoverySpeed = 25f;

        public UIOpacity background;
        public UIOpacity fill;
        #endregion

        #region Attack
        [Header("Attack")]
        [Range(0, 1f)]
        public float attackRadius;

        public bool controlEnabled;

        public ParticleSystem dustParticle;
        public GameObject actionPrompt;
        #endregion
        #endregion

        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            originalSpeed = moveSpeed;
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
                Schedule<PlayerSprint>();

                // Catch attack
                if (Input.GetMouseButtonDown(0))
                {
                    var ev = Schedule<PlayerCatchRadius>();
                    ev.player = this;
                }
            }
        }

        private void Move(float moveHorizontal, float moveVertical)
        {
            Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized * moveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);

            // handle dust effect when walking
            var ev = Schedule<PlayerWalkDustEffect>();
            ev.movement = movement;
        }

        public void ToggleControl(bool value)
        {
            controlEnabled = value;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}
