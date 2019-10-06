using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScreen : MenuScreen
{
    public TMP_Text hsText;
    public TMP_Text scoreText;

    public void ShowScore(int score)
    {
        scoreText.SetText($"Your Score: {score}");

        if (PlayerPrefs.HasKey("highScore"))
        {
            int hScore = PlayerPrefs.GetInt("highScore");
            hsText.SetText($"Highscore: {hScore.ToString()}");
        }
        else
            hsText.SetText($"Highscore: {GameController.Instance.maxPoints}");

    }
}
