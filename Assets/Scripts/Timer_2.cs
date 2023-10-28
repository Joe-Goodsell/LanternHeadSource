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
    [SerializeField] private Animator dawnAnimator;
    
   
    public float CurrentTime 
    {
        get => this._currentTime;
    }
    
    
    void Start()
    {
        _currentTime = _startTime;
        // Place here so that when player press pause and return to menu, they don't have to read instruction again
        GameManager.IsFirst = false;
        dawnAnimator = GameObject.Find("Dawn").GetComponent<Animator>();
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
            Debug.Log("Set victory");
            GameManager.IsFirst = false;
            Destroy(gameObject);
            onTimesUp.Invoke();
        } else {
            dawnAnimator.SetBool("StartCountDown", true);
            currentTimeStr = _currentTime.ToString("0.0");
        }

        countDown.text = currentTimeStr;
    }

    public void saveRecord()
    {
        GameManager.RecordTime = _startTime - _currentTime;
    }

}
