using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseAndResume : MonoBehaviour
{
    private bool pause;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject lanternHead;
    [SerializeField] private UnityEvent onResume;

    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        lanternHead = GameObject.Find("LanternHead");
        Resume();        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
            if (pause)
            {
                Pause();

            }
            else
            {
                Resume();
            }
        }
        
        
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        lanternHead.SetActive(false);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        pause = false;
        onResume.Invoke();
        pauseMenu.SetActive(false);
        lanternHead.SetActive(true);
        Time.timeScale = 1;
    }
}
