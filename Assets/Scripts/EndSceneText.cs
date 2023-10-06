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

    // Start is called before the first frame update
    void Start()
    {
        var globalTimer = GameObject.FindWithTag("GlobalTimer").GetComponent<GlobalTimer>();
        float timeSurvived = globalTimer.RecordTime;
        float targetTime = globalTimer.StartTime;
        Debug.Log(timeSurvived);
        int currentSecond = (int) timeSurvived % 60;
        int currentMinute = (int) Math.Floor(timeSurvived / 60);
        string victoryText = "Congratulations. You made it.";
        string defeatText = string.Format("You survived for {0:00}:{1:00}", currentMinute, currentSecond); 

        if (timeSurvived >= targetTime){
            endSceneText.text = victoryText;
        } else {
            endSceneText.text = defeatText;
        }
    }

}
