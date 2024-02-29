using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
    public AudioSource explosionClip;
    public float explosionForce = 10f;
    public float cameraShakeIntensity = 0.5f;
    public GameObject particleSystemPrefab; // Assign your particle prefab in the Unity editor

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the left mouse button is clicked
            HandleExplosion();
        }
    }

    private void HandleExplosion()
    {
        // Play explosion audio
        if (explosionClip != null)
        {
            explosionClip.PlayOneShot(explosionClip.clip);
        }

        // Instantiate particle system prefab at the barrel's position
        if (particleSystemPrefab != null)
        {
            Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
        }

        // Get all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionForce);

        // Apply force to nearby objects with Rigidbody
        foreach (var collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionForce);
                Debug.Log("Applied force to: " + collider.name);
            }
        }

        // Trigger camera shake (replace "MainCamera" with your actual camera tag or name)
        Camera.main.GetComponent<CameraShake>().ShakeCamera(cameraShakeIntensity);

        // Disable the barrel GameObject that triggered the explosion
        gameObject.SetActive(false);
    }
}