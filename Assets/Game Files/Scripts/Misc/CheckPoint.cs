using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private SpriteRenderer gradient;

    [Header("Action")]
    public static Action<CheckPoint> OnCheckpointPassed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        gradient.color = Color.green;
        OnCheckpointPassed?.Invoke(this);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
