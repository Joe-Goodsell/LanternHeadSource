using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;
public class Timer_2 : GameManagerClient
{
    [SerializeField] private float _startTime = 300.0f;
    [SerializeField] private TextMeshProUGUI countDown;
    [SerializeField] private UnityEvent onTimesUp;
    [SerializeField] private float _currentTime;
    
   
    public float CurrentTime 
    {
        get => this._currentTime;
    }
    
    
    void Start()
    {
        _currentTime = _startTime;
    }

    // Update is called once per frame
    void Update()
    {

        _currentTime -= Time.deltaTime;
        string currentTimeStr;

        int currentSecond = (int) _currentTime % 60;
        int currentMinute = (int) Math.Floor(_currentTime / 60);
        if (currentMinute >= 1){
            currentTimeStr = string.Format("{0:00}:{1:00}", currentMinute, currentSecond);
        } else if (_currentTime < 0){
            currentTimeStr = "0.0";
            GameManager.IsVictory = true;
            GameManager.IsFirst = false;
            onTimesUp.Invoke();
        } else {
            currentTimeStr = _currentTime.ToString("0.0");
        }

        countDown.text = currentTimeStr;
    }

    public void saveRecord()
    {
        GameManager.RecordTime = _startTime - _currentTime;
        GameManager.IsFirst = false;
    }
}
