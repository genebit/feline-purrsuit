using System.Collections;
using UnityEngine;

public class DisableOnDelay : MonoBehaviour
{
    [Range(0, 3f)]
    public float startAt = 2f;

    [Range(0, 100f)]
    public float endAt = 5f;

    private void Start()
    {
        StartCoroutine(StartText());
    }

    IEnumerator StartText()
    {
        yield return new WaitForSeconds(startAt);

        float timer = 0f;
        while (timer < endAt)
        {
            timer += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        gameObject.SetActive(false);
    }
}
