using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    [Header("Elements")]
    private Vector3 lastCheckpointPos;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
        CheckPoint.OnCheckpointPassed += CheckpointPassed;
        GameManager.OnGameStateChanged += GameStateChangeCallback;
    }

    private void OnDestroy()
    {
        CheckPoint.OnCheckpointPassed -= CheckpointPassed;
        GameManager.OnGameStateChanged -= GameStateChangeCallback;

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckpointPassed(CheckPoint checkPoint)
    {
        lastCheckpointPos = checkPoint.GetPosition();
    }

    public Vector3 GetLastCheckpointPos()
    {
        return lastCheckpointPos;
    }

    private void GameStateChangeCallback(GameState state)
    {
        switch(state)
        {
            case GameState.LevelComplete:
                lastCheckpointPos = Vector3.zero;
                break;
        }
    }
}
