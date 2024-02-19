using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject levelCompletedPanel;
    [SerializeField] private GameObject gameOverPanel;
    // Start is called before the first frame update

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameStateChangeCallBack;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChangeCallBack;

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GameStateChangeCallBack(GameState state)
    {
        switch (state)
        {
            case GameState.Menu:
                menuPanel.SetActive(true);
                levelCompletedPanel.SetActive(false);
                gameOverPanel.SetActive(false);
                break;
            case GameState.Game:
                menuPanel.SetActive(false);
                break;
            case GameState.LevelComplete:
                levelCompletedPanel.SetActive(true);
                gameOverPanel.SetActive(false);
                break;
            case GameState.GameOver:
                menuPanel.SetActive(false);
                levelCompletedPanel.SetActive(false);
                gameOverPanel.SetActive(true);
                break;
        }
    }

    public void PlayButtonCallback()
    {
        GameManager.instance.SetGameState(GameState.Game);
    }

    public void RetryButtonCallback()
    {
        SceneManager.LoadScene(0);
    }
}
