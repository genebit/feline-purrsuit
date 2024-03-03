using UnityEngine;

namespace Mechanic
{
    public enum CatType
    {
        White,
        WhiteSpots,
        Blue,
        Yellow,
        Black,
        Orange,
    }

    public class CatController : MonoBehaviour
    {
        public CatType catType;

        [SerializeField]
        [Range(0, 10f)]
        private float moveRadius = 5f;

        [SerializeField]
        [Range(0, 2f)]
        private float moveSpeed = 0.5f;

        private float originalMoveSpeed;

        [SerializeField]
        [Range(0, 1f)]
        private float speedIncreasePercentage = 0.25f;

        [SerializeField]
        private GameObject cat;

        private GameObject player;
        private Vector2 initialPosition;
        private Vector2 targetPosition;
        private Vector2 movement;
        private SpriteRenderer catSpriteRenderer;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            catSpriteRenderer = cat.GetComponent<SpriteRenderer>();

            // Variable assignments
            initialPosition = transform.position;
            targetPosition = initialPosition;
            originalMoveSpeed = moveSpeed;

            SetCatType();
        }

        void FixedUpdate()
        {
            FlipSprite();
            Move();
        }

        private void Move()
        {
            // Move the cat randomly only within this radius
            if (Vector2.Distance(cat.transform.position, targetPosition) < 0.1f)
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
            }
            else
            {
                // Reset move speed to original if player is not within the radius
                moveSpeed = originalMoveSpeed;
            }

            // Move the cat towards the target position
            cat.transform.position = Vector3.MoveTowards(cat.transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        }

        private void SetCatType()
        {
            switch (catType)
            {
                case CatType.White:
                    // change the sprite to white cat
                    catSpriteRenderer.sprite = Resources.Load<Sprite>("Characters/Cats/cat-white");
                    break;
                case CatType.WhiteSpots:
                    // change the sprite to white cat with spots
                    catSpriteRenderer.sprite = Resources.Load<Sprite>("Characters/Cats/cat-white-gray");
                    break;
                case CatType.Blue:
                    // change the sprite to blue cat
                    catSpriteRenderer.sprite = Resources.Load<Sprite>("Characters/Cats/cat-blue");
                    break;
                case CatType.Yellow:
                    // change the sprite to yellow cat
                    catSpriteRenderer.sprite = Resources.Load<Sprite>("Characters/Cats/cat-yellow");
                    break;
                case CatType.Black:
                    // change the sprite to black cat
                    catSpriteRenderer.sprite = Resources.Load<Sprite>("Characters/Cats/cat-black");
                    break;
                case CatType.Orange:
                    // change the sprite to orange cat
                    catSpriteRenderer.sprite = Resources.Load<Sprite>("Characters/Cats/cat-orange");
                    break;
            }
        }

        private void FlipSprite()
        {
            if (movement.x > 0)
            {
                // flip sprite to the right
                catSpriteRenderer.flipX = false;
            }
            else if (movement.x < 0)
            {
                // flip sprite to the left
                catSpriteRenderer.flipX = true;
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