using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State
    {
       Alive,
       Dead
    }

    private State state;

    [Header("Elements")]
    private CharacterRagdoll characterRagdoll;
    private PlayerMovement playerMovement;
    [SerializeField] private CharacterIK characterIK;
    // Start is called before the first frame update
    void Start()
    {
        characterRagdoll = GetComponent<CharacterRagdoll>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        characterIK.ConfigureIK(playerMovement.GetEnemyTarget());
        state = State.Alive;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        if (state == State.Dead)
            return;

        Die();
    }

    private void Die()
    {
        state = State.Dead;
        characterRagdoll.EnableRagdoll();
    }
}
