using UnityEngine;

namespace UI
{
    public class EnableOnPlay : MonoBehaviour
    {
        private Canvas canvas;

        private void Start()
        {
            canvas = gameObject.GetComponent<Canvas>();
            canvas.enabled = true;
        }
    }
}