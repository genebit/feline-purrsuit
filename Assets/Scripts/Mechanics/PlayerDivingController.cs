using UnityEngine;

public class PlayerDivingController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float floatForce = 10f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(horizontalInput, 0f).normalized;
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        // Floating control
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * floatForce * Time.deltaTime, ForceMode2D.Impulse);
        }
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
