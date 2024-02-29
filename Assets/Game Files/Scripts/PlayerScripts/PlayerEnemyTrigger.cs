using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerEnemyTrigger : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private LineRenderer shootingLine;
    private PlayerMovement playerMovement;

    [Header("Settings")]
    [SerializeField] private LayerMask enemyMask;
    private List<Enemy> currentEnemies = new List<Enemy>();
    private bool OnWarzone;

    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();

        PlayerMovement.OnEnterdWarzone += EnteredWarzoneCallBack;
        PlayerMovement.OnExitWarzone += ExitWArzoneCallBack;
    }

    private void OnDestroy()
    {
        PlayerMovement.OnEnterdWarzone -= EnteredWarzoneCallBack;
        PlayerMovement.OnExitWarzone -= ExitWArzoneCallBack;
    }

    // Update is called once per frame
    void Update()
    {
        if (OnWarzone)
            CheckForShootingEnemies();
    }

    private void EnteredWarzoneCallBack()
    {
        OnWarzone = true;
    }

    private void ExitWArzoneCallBack()
    {
        OnWarzone = false;
    }

    private void CheckForShootingEnemies()
    {
        //World Space Ray Origin
        Vector3 rayOrigin = shootingLine.transform.TransformPoint(shootingLine.GetPosition(0));
        Vector3 lineSecondPoint = shootingLine.transform.TransformPoint(shootingLine.GetPosition(1));
        Vector3 rayDirection = (lineSecondPoint - rayOrigin).normalized;
        float maxDistance = Vector3.Distance(rayOrigin, lineSecondPoint);
        RaycastHit[] hits = Physics.RaycastAll(rayOrigin, rayDirection, maxDistance, enemyMask);

        for (int i = 0; i < hits.Length; i++)
        {
            Enemy currentEnemy = hits[i].collider.GetComponent<Enemy>();
            if (!currentEnemies.Contains(currentEnemy))
                currentEnemies.Add(currentEnemy);
        }

        List<Enemy> enemyRemoved = new List<Enemy>();

        foreach (Enemy enemy in currentEnemies)
        {
            bool enemyFound = false;

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.GetComponent<Enemy>() == enemy)
                {
                    enemyFound = true;
                    break;
                }
            }

            if (!enemyFound)
            {
                if (enemy.transform.parent == playerMovement.GetCurrentWarzone().transform)
                    enemy.ShootAtPlayer();
                enemyRemoved.Add(enemy);
            }
        }

        foreach (Enemy enemy in enemyRemoved)
        {
            currentEnemies.Remove(enemy);
        }
    }
}