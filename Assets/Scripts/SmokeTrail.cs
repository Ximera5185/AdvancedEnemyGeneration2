using System.Collections;
using UnityEngine;

public class SmokeTrail : MonoBehaviour
{
    public void DestroyWith()
    {
        transform.SetParent(null);

        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        float delay = 5f;

        while (delay > 0)
        {
            delay -= Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }
}