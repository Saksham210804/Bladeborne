using UnityEngine;
using Unity.Cinemachine; // Keep this line as it is

// Remove the line below if it causes issues
// using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private CinemachineImpulseSource impulseSource;

    void Awake()
    {
        Instance = this;
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeCamera(float intensity = 1f)
    {
        impulseSource.GenerateImpulse(intensity);
    }
}
