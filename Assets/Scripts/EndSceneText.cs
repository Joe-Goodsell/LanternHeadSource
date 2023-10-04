using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
public class EndSceneText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI endSceneText;
    [SerializeField] private float timeSurvived;

    // Start is called before the first frame update
    void Start()
    {
        timeSurvived = 300.0f - Time.time;
        Debug.Log(timeSurvived);
        int currentSecond = (int) timeSurvived % 60;
        int currentMinute = (int) Math.Floor(timeSurvived / 60);
        string victoryText = "Congratulations. You made it.";
        string defeatText = string.Format("You survived for {0:00}:{1:00}", currentMinute, currentSecond); 

        if (timeSurvived <= 0){
            endSceneText.text = victoryText;
        } else {
            endSceneText.text = defeatText;
        }
    }

}
