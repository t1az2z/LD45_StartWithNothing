using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public MenuScreen[] menuScreens;


    public TMP_Text hsText;
    public TMP_Text scoreText;

    private Dictionary<GameState, MenuScreen> menus = new Dictionary<GameState, MenuScreen>();
    private void Awake()
    {
        foreach (var menu in menuScreens)
        {
            if (!menus.ContainsKey(menu.state))
                menus.Add(menu.state, menu);
            else
                Debug.LogError("Same key twice!");
        }
    }

    public void Start()
    {
        GameController.Instance.OnGameEnd += EndGame;
    }

    public void SwitchUI(GameState state)
    {
        foreach (var screen in menuScreens)
        {
            screen.gameObject.SetActive(false);
        }
        menus[state].gameObject.SetActive(true);
        menus[state].Open();
    }

    public void Play()
    {
        GameController.Instance.ResetGame();
        GameController.Instance.gameState = GameState.Game;
        SwitchUI(GameState.Game);
    }

    public void Pause()
    {
        GameController.Instance.gameState = GameState.Pause;
        AudioManager.Instance.SetVolume("Theme", .4f);
        SwitchUI(GameState.Pause);
    }
    public void Resume()
    {
        GameController.Instance.gameState = GameState.Game;
        AudioManager.Instance.SetVolume("Theme", .6f);
        SwitchUI(GameState.Game);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void EndGame(int maxPoints)
    {
        SwitchUI(GameState.EndGame);


        scoreText.SetText($"Your Score: {GameController.Instance.maxPoints}");

        if (PlayerPrefs.HasKey("highScore"))
        {
            int hScore = PlayerPrefs.GetInt("highScore");
            hsText.SetText($"Highscore: {hScore.ToString()}");
        }
        else
            hsText.SetText($"Highscore: {GameController.Instance.maxPoints}");


    }
}
