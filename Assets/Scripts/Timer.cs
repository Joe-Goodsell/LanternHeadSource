using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float startTime = 300.0f;
    private float currentTime;
    [SerializeField] private TextMeshProUGUI countDown;
    [SerializeField] private SceneTransition sceneTransition;

    public float CurrentTime {
        get {
            return currentTime;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        string currentTimeStr;

        int currentSecond = (int) currentTime % 60;
        int currentMinute = (int) Math.Floor(currentTime / 60);
        if (currentMinute >= 1){
            currentTimeStr = string.Format("{0:00}:{1:00}", currentMinute, currentSecond);
        } else if (CurrentTime < 0){
            currentTimeStr = "0.0";
            sceneTransition.GotoMenuScene(1.0f);
        } else {
            currentTimeStr = currentTime.ToString("0.0");
        }

        countDown.text = currentTimeStr;

    }
}