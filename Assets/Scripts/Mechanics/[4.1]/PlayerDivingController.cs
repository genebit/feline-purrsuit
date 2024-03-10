using Core;
using EasyTransition;
using Gameplay;
using Model;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using static Core.Simulation;

namespace Mechanics
{
    public class PlayerDivingController : MonoBehaviour
    {
        #region Inspector View
        public GameObject player;
        [Range(0f, 20f)]
        public float moveSpeed;
        [Header("Stamina")]
        public Slider staminaSlider;
        [Range(0f, 20f)] public float floatForce;
        [Range(0f, 20f)] public float staminaDecrease;
        public bool isUnderwater;
        public bool isDead;

        /// <summary>
        /// This is for when the player stamina reaches 0. Game Over.
        /// </summary>
        [Header("Game Over")]
        [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")]
        public string transitionTo;
        public TransitionSettings transitionSettings;
        #endregion

        private SpriteRenderer playerSpriteRenderer;
        private Rigidbody2D rb;
        private bool warningPromptShown = false;

        private readonly IsoModel model = Simulation.GetModel<IsoModel>();

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                // Horizontal movement
                float horizontalInput = Input.GetAxis("Horizontal");
                Vector2 moveDirection = new Vector2(horizontalInput, 0f).normalized;
                rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
            }

            FlipSprite(rb.velocity);

            if (!isUnderwater && !isDead)
            {
                // Floating control
                if (Input.GetKey(KeyCode.Space))
                {
                    rb.AddForce(Vector2.up * floatForce * Time.deltaTime, ForceMode2D.Impulse);
                }

                staminaSlider.value -= Time.deltaTime * staminaDecrease;
                staminaSlider.value = Mathf.Clamp(staminaSlider.value, 0f, 100f);

                OnPlayerDeath();
            }
        }

        private void FlipSprite(Vector2 velocity)
        {
            if (velocity.x > 0)
            {
                // flip sprite to the right
                playerSpriteRenderer.flipX = false;
                player.transform.rotation = Quaternion.Euler(0, 0, -40);
            }
            else if (velocity.x < 0)
            {
                // flip sprite to the left
                playerSpriteRenderer.flipX = true;
                player.transform.rotation = Quaternion.Euler(0, 0, 40);
            }
            else
            {
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        private void OnPlayerDeath()
        {
            if (staminaSlider.value == 0)
            {
                isDead = true;

                // Game Over, schedule an event.
                var ev = Schedule<TransitionToScene>();
                ev.transitionTo = transitionTo;
                ev.transitionSettings = transitionSettings;
            }
            else if (staminaSlider.value < 30f)
            {
                if (!warningPromptShown)
                {
                    model.playerActionPrompt.Prompt("I'm not feline good...");
                    warningPromptShown = true;
                }
            }
            else
            {
                warningPromptShown = false;
            }
        }

        public void SetIsBreathing(bool value)
        {
            isUnderwater = value;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                rb.gravityScale = 0.1f;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                rb.gravityScale = 0.75f;
            }
        }
    }
}