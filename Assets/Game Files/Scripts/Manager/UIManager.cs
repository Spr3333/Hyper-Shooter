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
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject settingsPanel;
    //[SerializeField] private GameObject pausePanel;
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
                gamePanel.SetActive(false);
                settingsPanel.SetActive(false);
                break;

            case GameState.Game:
                menuPanel.SetActive(false);
                gamePanel.SetActive(true);
                //pausePanel.SetActive(false);
                break;

            case GameState.LevelComplete:
                levelCompletedPanel.SetActive(true);
                gameOverPanel.SetActive(false);
                gamePanel.SetActive(false);
                break;

            case GameState.GameOver:
                menuPanel.SetActive(false);
                levelCompletedPanel.SetActive(false);
                gameOverPanel.SetActive(true);
                gamePanel.SetActive(false);
                break;

            case GameState.Pause:
                menuPanel.SetActive(false);
                //pausePanel.SetActive(true);
                break;

            case GameState.Settings:
                settingsPanel.SetActive(true);
                gamePanel.SetActive(false);
                menuPanel.SetActive(false);
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

    public void NextLevelCallback()
    {
        SceneManager.LoadScene(0);
    }

    public void SettingsBtnCallback()
    {
        GameManager.instance.SetGameState(GameState.Settings);
    }    

    public void PauseBtncallback()
    {
        GameManager.instance.SetGameState(GameState.Pause);
        //StopTime();
    }

    public void BackBtnCallback()
    {
        GameManager.instance.SetGameState(GameState.Menu);
    }

    public void ResumeCallback()
    {
        GameManager.instance.SetGameState(GameState.Game);
        //StartTime();
    }

    private void StopTime()
    {
        Time.timeScale = 0;
    }

    private void StartTime()
    {
        Time.timeScale = 1;
    }
}
