using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Warzone : MonoBehaviour
{


    [Header("Elements")]
    [SerializeField] private SplineContainer playerSpline;
    [SerializeField] private float WarzoneDuration;
    [SerializeField] private string animationName;
    [SerializeField] private float animationSpeed;
    [SerializeField] private Transform ikTarget;
    [SerializeField] private SplineAnimate ikSpline;
    // Start is called before the first frame update
    void Start()
    {
        ikSpline.Duration = WarzoneDuration;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayIKSplineAnimate()
    {
        ikSpline.Play();
    }

    public Spline GetSpline()
    {
        return playerSpline.Spline;
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
