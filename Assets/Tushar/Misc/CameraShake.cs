using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeIntensity = 0.2f;

    private Vector3 originalPosition;
    private float shakeTimer = 0f;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            // Shake the camera
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeIntensity;

            // Reduce the shake timer
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // Reset the camera position after the shake duration
            shakeTimer = 0f;
            transform.localPosition = originalPosition;
        }
    }

    // Call this method to start the camera shake
    public void ShakeCamera(float intensity)
    {
        shakeIntensity = intensity;
        shakeTimer = shakeDuration;
    }
}
