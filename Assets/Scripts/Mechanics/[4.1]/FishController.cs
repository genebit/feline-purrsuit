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
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            // randomize the fish type. the white fish is the most common with 70% chance,
            // the triangle fish is uncommon with 20% chance, and
            // the gold fish is rare with 10% chance

            int random = Random.Range(0, 100);

            if (random < 70)
                fishType = FishType.WhiteFish;
            else if (random < 90)
                fishType = FishType.TriangleFish;
            else
                fishType = FishType.GoldFish;

            // Set the fish sprite
            SetFishSprite();
        }

        private void SetFishSprite()
        {
            switch (fishType)
            {
                case FishType.WhiteFish:
                    spriteRenderer.sprite = Resources.Load<Sprite>("Characters/Fishes/white-fish");
                    break;
                case FishType.TriangleFish:
                    spriteRenderer.sprite = Resources.Load<Sprite>("Characters/Fishes/triangle-fish");
                    break;
                case FishType.GoldFish:
                    spriteRenderer.sprite = Resources.Load<Sprite>("Characters/Fishes/gold-fish");
                    break;
            }
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