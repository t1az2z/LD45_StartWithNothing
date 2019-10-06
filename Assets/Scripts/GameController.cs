using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public PlayerController playerController;
    public TrashGenerator trashGenerator;
    public event Action<int> OnPointsUpdate = delegate { };
    public event Action<int> OnGameEnd = delegate { };
    //public AudioManager audioManager;

    public GameState gameState;

    private int points = 0;
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
        UpdatePoints(startPoints);
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
        else if (gameState == GameState.Menu)
        {

        }
        if (gameState == GameState.Game)
        {
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

        }
        else if (gameState == GameState.Pause)
        {

        }
    }
    private IEnumerator GenerateBombs()
    {
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

