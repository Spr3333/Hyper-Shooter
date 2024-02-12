using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float detectionRange;
    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckForPlayer();
    }

    private void Move()
    {
        transform.position += velocity * Time.deltaTime;
    }

    public void ConfigureVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    private void CheckForPlayer()
    {
        Collider[] detectedPlayer = Physics.OverlapSphere(transform.position, detectionRange, playerMask);
        foreach(Collider collider in detectedPlayer)
        {
            collider.GetComponent<PlayerMovement>().TakeDamage();
        }
    }


}
