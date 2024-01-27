using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private GameObject shootingLine;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform bulletParent;


    [Header("Settings")]
    private bool canShoot;

    private void Awake()
    {
        PlayerMovement.OnEnterdWarzone += EnteredWarzoneCallBack;
        PlayerMovement.OnExitWarzone += ExitWarzoneCallBack;
    }

    private void OnDestroy()
    {
        PlayerMovement.OnEnterdWarzone -= EnteredWarzoneCallBack;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetShootingline(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
            ManageShooting();
    }

    private void ManageShooting()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    private void Shoot()
    {
        Vector3 direction = bulletSpawnPosition.right;
        direction.z = 0;
        Bullet bulletInstance = Instantiate(bulletPrefab, bulletSpawnPosition.position, Quaternion.identity, bulletParent);
        bulletInstance.GetVelocity(direction * bulletSpeed);
    }

    private void EnteredWarzoneCallBack()
    {
        SetShootingline(true);
        canShoot = true;
    }

    private void ExitWarzoneCallBack()
    {
        SetShootingline(false);
        canShoot = false;
    }


    private void SetShootingline(bool shooting)
    {
        shootingLine.SetActive(shooting);
    }
}
