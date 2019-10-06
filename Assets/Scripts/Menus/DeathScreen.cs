using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MenuScreen
{
    public TMPro.TMP_Text scoreText;
   public void ShowScore(int score)
    {
        scoreText.SetText($"Your Score: {score}");
    }
}
