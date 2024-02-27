using UnityEngine;

public class AnimalController : MonoBehaviour
{
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
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition;
        player = GameObject.FindWithTag("Player");
        originalMoveSpeed = moveSpeed;
    }

    void FixedUpdate()
    {
        // Move the cat randomly only within this radius
        if (Vector3.Distance(cat.transform.position, targetPosition) < 0.1f)
        {
            Vector3 randomDirection = Random.insideUnitCircle.normalized;
            targetPosition = initialPosition + randomDirection * moveRadius;
        }

        // Check if player is within the radius
        if (Vector3.Distance(transform.position, player.transform.position) <= moveRadius)
        {
            // Calculate direction away from the player
            Vector3 directionAwayFromPlayer = (transform.position - player.transform.position).normalized;

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

    // Visualize the radius in the Unity editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, moveRadius);
    }
}
