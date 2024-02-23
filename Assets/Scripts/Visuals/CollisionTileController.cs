using UnityEngine;
using UnityEngine.Tilemaps;

namespace Visuals
{
    public class CollisionTileController : MonoBehaviour
    {
        [SerializeField]
        public bool showRenderer = false;

        private TilemapRenderer tilemapRenderer;

        private void Start()
        {
            tilemapRenderer = gameObject.GetComponent<TilemapRenderer>();
            tilemapRenderer.enabled = showRenderer;
        }
    }
}