using UnityEngine;
using UnityEngine.Tilemaps;

namespace Visuals
{
    public class CollisionTileController : MonoBehaviour
    {
        [SerializeField]
        private TilemapRenderer tilemapRenderer;

        [SerializeField]
        public bool showRenderer = false;

        private void Start()
        {
            tilemapRenderer.GetComponent<TilemapRenderer>();
            tilemapRenderer.enabled = showRenderer;
        }
    }
}