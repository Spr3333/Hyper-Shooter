using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRunAnimation()
    {
        //anim.Play("Run");
        Play("Run",1);
    }

    public void Play(string animationName)
    {
        anim.Play(animationName);
    }

    public void Play(string animationName, float animationSpeed)
    {
        anim.speed = animationSpeed;
        Play(animationName);
    }
}
