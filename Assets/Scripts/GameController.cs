using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public PlayerController playerController;
    public UIController uiController;
    public TrashGenerator trashGenerator;
    public event Action<int> OnPointsUpdate = delegate { };
    public event Action<int> OnGameEnd = delegate { };

    public GameState gameState;

    public int points = 0;
    public int startPoints = 100;
    public int maxPoints = 0;
    private float timeInGame = 0;

    int playerPointsCount = 0;
    private bool generating;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        trashGenerator = FindObjectOfType<TrashGenerator>();
        uiController = FindObjectOfType<UIController>();
        gameState = GameState.Menu;
        uiController.SwitchUI(GameState.Menu);
        AudioManager.Instance.Play("Theme");
        playerController.controllsEnabled = false;
    }
    private void Update()
    {
        if (!playerController)
        {
            playerController = FindObjectOfType<PlayerController>();
        }
        if(!trashGenerator)
        {
            trashGenerator = FindObjectOfType<TrashGenerator>();
        }
        if (gameState == GameState.Game)
        {
            Time.timeScale = 1;
            playerController.controllsEnabled = true;
            if (!generating)
            {
                StartCoroutine(GenerateBombs());
                generating = true;
            }
            timeInGame += Time.deltaTime;
        }
        else if (gameState == GameState.EndGame)
        {
            playerController.controllsEnabled = false;
            playerController.rb.velocity = Vector3.zero;
        }
        else if (gameState == GameState.Pause)
        {
            Time.timeScale = .0001f;
        }
    }

    public void ResetGame()
    {
        playerController.gameObject.transform.position = playerController.transform.position.Where(y:.61f);
        StopCoroutine(GenerateBombs());
        generating = false;
        gameState = GameState.Game;
        startPoints = 100;
        maxPoints = startPoints;
        UpdatePoints(startPoints);
        trashGenerator.transform.ClearChildren();
        playerController.controllsEnabled = true;
    }

    private IEnumerator GenerateBombs()
    {
        yield return new WaitForSeconds(2);
        while (gameState == GameState.Game)
        {
            if(trashGenerator.timeBetweenGenerations >= trashGenerator.timeBetweenGenerationsMin)
                trashGenerator.timeBetweenGenerations -= timeInGame / trashGenerator.timeBetweenGenerationsRate;
            trashGenerator.CreateItem(timeInGame);
            yield return new WaitForSeconds(trashGenerator.timeBetweenGenerations);
        }

    }
    public void UpdatePoints(int points)
    {
        if (gameState == GameState.Game)
        {
            this.points += points;
            if (this.points < 0)
                this.points = 0;
            if (this.points >= maxPoints)
            {
                maxPoints = this.points;
            }
            OnPointsUpdate(this.points);
            if (this.points <=0)
            {
                if (PlayerPrefs.HasKey("highScore"))
                {
                    if (maxPoints > PlayerPrefs.GetInt("highScore"))
                        PlayerPrefs.SetInt("highScore", maxPoints);
                }
                else
                {
                    PlayerPrefs.SetInt("highScore", maxPoints);
                }
                AudioManager.Instance.Play("Lose");
                gameState = GameState.EndGame;
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        OnGameEnd(maxPoints);
    }
}

public enum GameState
{
    Menu,
    Game,
    Pause,
    EndGame
}

