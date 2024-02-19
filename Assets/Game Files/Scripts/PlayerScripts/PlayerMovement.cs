using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(CharacterIK), typeof(PlayerAnim))]
public class PlayerMovement : MonoBehaviour
{
    public enum State
    {
        Idle,
        Running,
        Warzone,
        Dead
    }

    [Header("Elements")]
    private PlayerAnim anim;
    private State state;
    private Warzone currentWarzone;
    private CharacterIK playerIK;
    [SerializeField] private Transform enemyTarget;
    [SerializeField] private CharacterRagdoll characterRagdoll;


    [Header("Spline Settings")]
    private float warzoneTimer;
    [SerializeField] private float moveSpeed = 10;
    private bool isDead;


    [Header("Events")]
    public static Action OnEnterdWarzone;
    public static Action OnExitWarzone;
    public static Action IsDead;

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChangedCallback;
    }

    void Start()
    {
        playerIK = GetComponent<CharacterIK>();
        Application.targetFrameRate = 60;
        anim = GetComponent<PlayerAnim>();
        state = State.Idle;

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartRunning();
        //}
        ManageState();
    }

    private void GameStateChangedCallback(GameState state)
    {
        switch(state)
        {
            case GameState.Game:
                StartRunning();
                break;
        }
    }

    private void ManageState()
    {
        switch (state)
        {
            case State.Idle:
                break;

            case State.Running:
                ManageMovement();
                break;
            case State.Warzone:
                ManageWarzoneState();
                break;
            case State.Dead:
                isDead = true;
                break;
        }
    }

    private void StartRunning()
    {
        state = State.Running;
        anim.PlayRunAnimation();
    }

    private void ManageMovement()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

    public void EnteredWarzoneCallback(Warzone warzone)
    {
        if (currentWarzone != null)
            return;

        state = State.Warzone;
        currentWarzone = warzone;

        currentWarzone.PlayIKSplineAnimate();
        warzoneTimer = 0;

        anim.Play(currentWarzone.GetAnimationName(), currentWarzone.GetAnimationSpeed());

        Time.timeScale = 0.3f;
        Time.fixedDeltaTime = 0.3f / 50;

        playerIK.ConfigureIK(currentWarzone.GetTarget());

        OnEnterdWarzone?.Invoke();

        //Debug.Log("Entered Warzone");
    }

    private void ManageWarzoneState()
    {
        warzoneTimer += Time.deltaTime;

        float splinePercent = warzoneTimer / currentWarzone.GetDuration();
        transform.position = currentWarzone.GetSpline().EvaluatePosition(splinePercent);
        if(splinePercent >= 1)
        {
            ExitWarzone();
        }
    }

    private void ExitWarzone()
    {
        state = State.Running;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 1f / 50;
        anim.PlayRunAnimation();
        currentWarzone = null;
        playerIK.DisableIK();
        OnExitWarzone?.Invoke();
    }

    public Transform GetEnemyTarget()
    {
        return enemyTarget;
    }

    public void TakeDamage()
    {
        state = State.Dead;
        characterRagdoll.EnableRagdoll();

        Time.timeScale = 1;
        Time.fixedDeltaTime = 1f / 50;

        IsDead?.Invoke();
        GameManager.instance.SetGameState(GameState.GameOver);
    }
}
