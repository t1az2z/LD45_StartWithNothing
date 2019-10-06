using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InGameUI : MenuScreen
{
    public TMPro.TMP_Text points;
    private void Start()
    {
        GameController.Instance.OnPointsUpdate += UpdatePoints;
    }
    private void UpdatePoints(int points)
    {
        this.points.SetText($"Trash can productivity: {points.ToString()}");
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
