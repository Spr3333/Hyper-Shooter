using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Elements")]
    private Vector3 velocity;
    [SerializeField] private LayerMask EnemiesMask;
    [SerializeField] private float detectionRadius;
    // Start is called before the first frame update
    void Start()
    {
        
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

    private void CheckForEnemies()
    {
        Collider[] detectedEnemies = Physics.OverlapSphere(transform.position, detectionRadius, EnemiesMask);

        foreach(Collider enemiesCollider in detectedEnemies)
        {
            enemiesCollider.GetComponent<Enemy>().TakeDamage();
        }
    }
}
