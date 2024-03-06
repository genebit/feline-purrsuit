using Core;
using Gameplay;
using Mechanics;
using Model;
using UnityEngine;
using static Core.Simulation;

public class HookController : MonoBehaviour
{
    [Range(0f, 20f)]
    public float speed = 8f;
    private Vector2 initialBulletDirection;
    private readonly IsoModel model = Simulation.GetModel<IsoModel>();

    public void SetInitialBulletDirection(Vector2 direction)
    {
        initialBulletDirection = direction.normalized;
    }

    void Update()
    {
        // Shoot the bullet towards the bullet direction
        transform.Translate(speed * Time.deltaTime * initialBulletDirection);

        // Destroy the bullet if it goes off-screen
        if (!IsVisible()) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            var ev = Schedule<CaughtFish>();
            ev.fish = collision.gameObject.GetComponent<FishController>();
        }
    }

    private bool IsVisible()
    {
        // Check if any renderer of the object is visible from any camera
        return GetComponent<Renderer>().isVisible;
    }
}
