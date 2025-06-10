using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class Camera_Shake : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public IEnumerator Shake(float duration,float Magnitude)
    {
        Debug.Log("Camera shake started with duration: " + duration + " and magnitude: " + Magnitude);
        Vector3 OriginalPos = transform.localPosition;
        float elapsed = 0.0f;
        while(elapsed< duration)
        {

           float x = Random.Range(-1f, 1f) * Magnitude;
            float y = Random.Range(-1f, 1f) * Magnitude;

            transform.localPosition = new Vector3(x, y, OriginalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

    }
}
