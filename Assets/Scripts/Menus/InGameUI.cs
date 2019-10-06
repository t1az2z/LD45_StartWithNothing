using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InGameUI : MenuScreen
{
    public TMPro.TMP_Text points;
    public Slider slider;

    private void Start()
    {
        GameController.Instance.OnPointsUpdate += UpdatePoints;
    }
    private void UpdatePoints(int points)
    {
        this.points.SetText($"{points.ToString()}/{GameController.Instance.maxPoints}");
        slider.maxValue = GameController.Instance.maxPoints;
        slider.value = points;
        if (points > 0)
        {
            //Debug.Log("Happy animation");
        }
        else
        {
            //Debug.Log("Sad animation");
        }
    }
}
