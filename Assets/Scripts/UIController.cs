using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public MenuScreen[] menuScreens;

    private Dictionary<GameState, MenuScreen> menus = new Dictionary<GameState, MenuScreen>();
    private void Awake()
    {
        foreach(var menu in menuScreens)
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
        SwitchUI(GameState.Pause);
    }
    public void Resume()
    {
        GameController.Instance.gameState = GameState.Game;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void EndGame(int maxPoints)
    {
        SwitchUI(GameState.EndGame);
        menus[GameState.EndGame].GetComponent<DeathScreen>().ShowScore(maxPoints);
    }
}
