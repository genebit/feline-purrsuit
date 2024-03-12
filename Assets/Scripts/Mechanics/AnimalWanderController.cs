using UnityEngine;

namespace Mechanics
{
    public class AnimalWanderController : MonoBehaviour
    {
        [Range(0, 10f)]
        [SerializeField] private float moveRadius = 5f;
        [Range(0, 10f)]
        [SerializeField] private float moveSpeed = 0.5f;
        private float originalMoveSpeed;
        [Range(0, 1f)]
        [SerializeField] private float speedIncreasePercentage = 0.25f;
        [SerializeField] private GameObject animal;
        [SerializeField] private AudioSource animalSound;

        private GameObject player;
        private Vector2 initialPosition;
        private Vector2 targetPosition;
        private Vector2 movement;
        private SpriteRenderer animalSpriteRenderer;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            animalSpriteRenderer = animal.GetComponent<SpriteRenderer>();

            // Variable assignments
            initialPosition = transform.position;
            targetPosition = initialPosition;
            originalMoveSpeed = moveSpeed;
        }

        private void FixedUpdate()
        {
            FlipSprite();
            Move();
        }

        private void Move()
        {
            // Move the animal randomly only within this radius
            if (Vector2.Distance(animal.transform.position, targetPosition) < 0.1f)
            {
                movement = Random.insideUnitCircle.normalized;
                targetPosition = initialPosition + movement * moveRadius;
            }

            // Check if player is within the radius
            if (Vector2.Distance(transform.position, player.transform.position) <= moveRadius)
            {
                // Calculate direction away from the player
                Vector2 directionAwayFromPlayer = (transform.position - player.transform.position).normalized;

                // Set target position away from the player within the move radius
                targetPosition = initialPosition + directionAwayFromPlayer * moveRadius;

                moveSpeed = originalMoveSpeed * (1 + speedIncreasePercentage);

                if (transform.GetChild(0).CompareTag("Cat"))
                {
                    // Play sound
                    animalSound.Play();
                }
            }
            else
            {
                // Reset move speed to original if player is not within the radius
                moveSpeed = originalMoveSpeed;
            }

            // Move the animal towards the target position
            animal.transform.position = Vector3.MoveTowards(animal.transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        }

        private void FlipSprite()
        {
            if (movement.x > 0)
            {
                // flip sprite to the right
                animalSpriteRenderer.flipX = false;
            }
            else if (movement.x < 0)
            {
                // flip sprite to the left
                animalSpriteRenderer.flipX = true;
            }
        }

        void OnDrawGizmosSelected()
        {
            // Visualize the radius in the Unity editor
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, moveRadius);
        }
    }
}