using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
public class EndSceneText_2 : GameManagerClient
{
    [SerializeField] private TextMeshProUGUI endSceneText;

    // Start is called before the first frame update
    void Start()
    {
        float timeSurvived = GameManager.RecordTime;
        Debug.Log(timeSurvived);
        int currentSecond = (int) timeSurvived % 60;
        int currentMinute = (int) Math.Floor(timeSurvived / 60);
        string victoryText = "Congratulations. You protected the central fire and survived for 5 minutes.";
        string defeatText = string.Format("You survived for {0:00}:{1:00}", currentMinute, currentSecond); 

        Debug.Log(GameManager.IsVictory);
        if (GameManager.IsVictory){
            endSceneText.text = victoryText;
        } else {
            endSceneText.text = defeatText;
        }
    }

}