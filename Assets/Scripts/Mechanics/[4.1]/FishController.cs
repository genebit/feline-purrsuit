using UnityEngine;

namespace Mechanics
{
    public enum FishType
    {
        WhiteFish,
        TriangleFish,
        GoldFish
    }

    public class FishController : MonoBehaviour
    {
        public FishType fishType;

        private void Start()
        {
            // randomize the fish type. the white fish is the most common with 50% chance,
            // the triangle fish is uncommon with 30% chance, and
            // the gold fish is rare with 20% chance

            int random = Random.Range(0, 100);

            if (random < 50)
                fishType = FishType.WhiteFish;
            else if (random < 80)
                fishType = FishType.TriangleFish;
            else
                fishType = FishType.GoldFish;
        }

        // When the fish is caught by the hook
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Hook"))
            {
                // Destroy the hook
                Destroy(collision.gameObject);

                // Destroy the fish container
                Destroy(transform.parent.gameObject);
            }
        }
    }
}