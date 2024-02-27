using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Dreamteck.Splines;

public class Warzone : MonoBehaviour
{


    [Header("Elements")]
    [SerializeField] private Transform ikTarget;
    [SerializeField] private SplineComputer newPlayerSpline;
    [SerializeField] private SplineFollower ikSplineFollower;

    [Header("Settings")]
    [SerializeField] private float WarzoneDuration;
    [SerializeField] private string animationName;
    [SerializeField] private float animationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        ikSplineFollower.followDuration = WarzoneDuration;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayIKSplineAnimate()
    {
        ikSplineFollower.follow = true;
    }

    public SplineComputer GetSpline()
    {
        return newPlayerSpline;
    }

    public float GetDuration()
    {
        return WarzoneDuration;
    }

    public string GetAnimationName()
    {
        return animationName;
    }

    public float GetAnimationSpeed()
    {
        return animationSpeed;
    }

    public Transform GetTarget()
    {
        return ikTarget;
    }
}
