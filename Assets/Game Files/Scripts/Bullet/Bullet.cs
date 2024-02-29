using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Elements")]
    private Vector3 velocity;
    [SerializeField] private LayerMask EnemiesMask;
    [SerializeField] private float detectionRadius;
    [SerializeField] private GameObject popUpImage;

    [Header("Audio")]
    [SerializeField] private AudioClip hsClip;
    [SerializeField] private AudioClip hitClip;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = FindAnyObjectByType<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckForEnemies();
    }

    private void Move()
    {
        transform.position += velocity * Time.deltaTime;
    }

    public void GetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    void ShowPopUpImage(Transform HeadPos)
    {
        var popupimage = Instantiate(popUpImage, HeadPos.position + new Vector3(0.3f, 0.1f, 0f), Quaternion.identity);
        // Set the initial position of the pop-up image
        //popupimage.transform.position = new Vector3(0f, 0f, 0f);

        // Use LeanTween to move the pop-up image upward
        LeanTween.moveY(popupimage, 1f, 1f).setEase(LeanTweenType.easeOutQuad);

        // Use LeanTween to fade out the pop-up image after 1 second
        LeanTween.alpha(popupimage, 0f, 1f).setEase(LeanTweenType.easeOutQuad).setDelay(0.5f);

        // Destroy the pop-up image GameObject after the animation is complete
        LeanTween.delayedCall(0.5f, () => Destroy(popupimage));
    }

    private void CheckForEnemies()
    {
        Collider[] detectedEnemies = Physics.OverlapSphere(transform.position, detectionRadius, EnemiesMask);

        foreach (Collider enemiesCollider in detectedEnemies)
        {
            if (enemiesCollider.CompareTag("Explosive"))
            {
                enemiesCollider.GetComponent<ExplosionDestroy>().HandleExplosion();
            }
            else
            {
                enemiesCollider.GetComponent<Enemy>().TakeDamage();
                audioSource.PlayOneShot(hitClip);
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Head"))
        {
            ShowPopUpImage(collision.gameObject.transform);
            audioSource.PlayOneShot(hsClip);
        }
    }
}
