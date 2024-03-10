using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class EnableAfterDelay : MonoBehaviour
    {
        public GameObject target;
        [Range(0, 10f)]
        public float delay;

        private void Start()
        {
            StartCoroutine(Enable());
        }

        IEnumerator Enable()
        {
            yield return new WaitForSeconds(delay);
            target.SetActive(true);
        }
    }
}