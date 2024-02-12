using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private EnemyBullet bulletPrefab;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private Transform bulletSpawnPoint;


    [Header("Settings")]
    [SerializeField] private float bulletSpeed;
    private bool hasShot = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryShooting()
    {
        if (hasShot)
            return;

        hasShot = true;
        Invoke("Shoot", 0.2f);
    }

    private void Shoot()
    {
        Vector3 velocity = bulletSpawnPoint.right * bulletSpeed;
        velocity.z = 0;
        EnemyBullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity, bulletParent);
        bullet.ConfigureVelocity(velocity);
    }
}
