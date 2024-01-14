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
        DetectStuff();
    }

    private void DetectStuff()
    {
        Collider[] detectedObjects = Physics.OverlapSphere(transform.position, detectionRange);

        foreach(Collider collider in detectedObjects)
        {
            if(collider.CompareTag("Warzone"))
            {
                EnterTriggerCallBack(collider);
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
}
