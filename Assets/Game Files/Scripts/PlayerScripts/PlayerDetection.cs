using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]

public class PlayerDetection : MonoBehaviour
{

    [Header("Settings")]
    private PlayerMovement playerMovement;

    [Header("Elements")]
    [SerializeField] private float detectionRange = 1;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsGameState())
            DetectStuff();
    }

    private void DetectStuff()
    {
        Collider[] detectedObjects = Physics.OverlapSphere(transform.position, detectionRange);

        foreach (Collider collider in detectedObjects)
        {
            if (collider.CompareTag("Warzone"))
            {
                EnterTriggerCallBack(collider);
            }
            if (collider.CompareTag("FinishLine"))
            {
                FinishLineHitCallback();
            }
        }
    }

    private void EnterTriggerCallBack(Collider triggerCollider)
    {
        Warzone warzone = triggerCollider.GetComponentInParent<Warzone>();
        playerMovement.EnteredWarzoneCallback(warzone);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    private void FinishLineHitCallback()
    {
        Debug.Log("Finish Line");
        playerMovement.FinishLevelCallBack();
    }
}
