using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRagdoll : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Collider mainCollider;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody[] rigidBody;


    [Header("Settings")]
    [SerializeField] private float ragDollForce;
    // Start is called before the first frame update
    void Start()
    {
        foreach(var rb in rigidBody)
        {
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableRagdoll()
    {
        foreach (var rb in rigidBody)
        {
            rb.isKinematic = false;
            rb.AddForce((Vector3.up + Random.insideUnitSphere) * ragDollForce);
        }

        animator.enabled = false;
        mainCollider.enabled = false;
    }
}
