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
        private GameObject cat;
        private SpriteRenderer catSpriteRenderer;

        void Start()
        {
            catSpriteRenderer = cat.GetComponent<SpriteRenderer>();
            SetCatType();
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
    }
}