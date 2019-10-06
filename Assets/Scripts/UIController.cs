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

    public void SwitchUI(GameState state)
    {
        foreach (var screen in menuScreens)
        {
            screen.gameObject.SetActive(false);
        }
        menus[state].gameObject.SetActive(true);
        menus[state].Open();
    }
}
