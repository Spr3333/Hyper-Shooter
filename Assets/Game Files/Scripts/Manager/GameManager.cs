using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Menu, Game, LevelComplete, GameOver}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Settings")]
    private GameState gameState;


    [Header("Action")]
    public static Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetGameState(GameState.Menu);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        OnGameStateChanged?.Invoke(gameState);
    }

    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
}
