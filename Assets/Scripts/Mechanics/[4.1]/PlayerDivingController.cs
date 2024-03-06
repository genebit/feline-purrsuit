using UnityEngine;
using UnityEngine.UI;

public class PlayerDivingController : MonoBehaviour
{
    [Range(0f, 20f)]
    public float moveSpeed;
    [Range(0f, 20f)]
    public float floatForce;
    public GameObject player;
    [Range(0f, 20f)]
    public float staminaDecrease;

    public Slider staminaSlider;

    public bool isBreathing;

    private SpriteRenderer playerSpriteRenderer;
    private Rigidbody2D rb;

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

        if (!isBreathing)
        {
            // Floating control
            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * floatForce * Time.deltaTime, ForceMode2D.Impulse);
            }

            staminaSlider.value -= Time.deltaTime * staminaDecrease;
            staminaSlider.value = Mathf.Clamp(staminaSlider.value, 0f, 100f);
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

    public void SetIsBreathing(bool value)
    {
        isBreathing = value;
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
