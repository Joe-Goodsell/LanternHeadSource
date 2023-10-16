using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;
public class GlobalTimer : MonoBehaviour
{
    [SerializeField] private float _startTime = 300.0f;
    [SerializeField] private TextMeshProUGUI countDown;
    [SerializeField] private UnityEvent onTimesUp;
    [SerializeField] private float _recordTime = 300.0f;
    [SerializeField] private float _currentTime;

    public float RecordTime 
    {
        get => this._recordTime;
    }

    public float StartTime 
    {
        get => this._startTime;
    }

    public float CurrentTime 
    {
        get => this._currentTime;
    }
    
    void Awake()
    {
         if (GameObject.FindGameObjectsWithTag("GlobalTimer").Length > 1)
            Destroy(gameObject);

        // Make this game object persistent even between scene changes.
        _currentTime = _startTime;
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "StartScene" || scene.name == "EndScene")
        {
            _currentTime = _startTime;
        } 
        else
        {
            _currentTime -= Time.deltaTime;
            string currentTimeStr;

            int currentSecond = (int) _currentTime % 60;
            int currentMinute = (int) Math.Floor(_currentTime / 60);
            if (currentMinute >= 1){
                currentTimeStr = string.Format("{0:00}:{1:00}", currentMinute, currentSecond);
            } else if (_currentTime < 0){
                currentTimeStr = "0.0";
                var st = GameObject.Find("SceneTransition").GetComponent<SceneTransition>();
                st.GotoEndScene();
            } else {
                currentTimeStr = _currentTime.ToString("0.0");
            }

            countDown.text = currentTimeStr;
        }
        
        
        
    }

    public void saveRecord()
    {
        _recordTime = _startTime - _currentTime;
    }
    
}
