using Gameplay;
using UI;
using UnityEngine;
using UnityEngine.UI;
using static Core.Simulation;

namespace Mechanics
{
    public class PlayerController : MonoBehaviour
    {
        #region Inspector View
        [Range(0, 5f)]
        public float moveSpeed = 2.5f;
        [SerializeField] private AudioSource walkSound;

        [HideInInspector]
        public float originalSpeed;

        #region Sprinting
        [Range(0, 5f)]
        [Header("Sprinting")]
        public float sprintSpeed = 3.5f;
        [SerializeField] private AudioSource sprintSound;

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
        private Vector2 movement;
        internal SpriteRenderer spriteRenderer;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
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

                FlipSprite();
            }
        }

        private void FlipSprite()
        {
            if (movement.x > 0)
            {
                // flip sprite to the right
                spriteRenderer.flipX = false;
            }
            else if (movement.x < 0)
            {
                // flip sprite to the left
                spriteRenderer.flipX = true;
            }
        }

        private void Move(float moveHorizontal, float moveVertical)
        {
            movement = new Vector2(moveHorizontal, moveVertical).normalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);

            // handle dust effect when walking
            var ev = Schedule<PlayerWalkDustEffect>();
            ev.movement = movement;

            // NOTE(Gene): this code is ugly af. I'm sorry.
            if (movement.x > 0 || movement.x < 0)
            {
                walkSound.Play();
            }
            else if (moveSpeed == 1.25f)
            {
                sprintSound.Play();
            }
            else
            {
                walkSound.Stop();
                sprintSound.Stop();
            }
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