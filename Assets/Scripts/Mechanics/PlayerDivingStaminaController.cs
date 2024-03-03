using UnityEngine;

public class PlayerDivingStaminaController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerDivingController>();

            player.SetIsBreathing(true);
            player.staminaSlider.value += 20f;
            player.staminaSlider.value = Mathf.Clamp(player.staminaSlider.value, 0f, 100f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerDivingController>();

            player.SetIsBreathing(false);
        }
    }
}
