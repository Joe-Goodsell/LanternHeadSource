using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlay : GameManagerClient
{
    [SerializeField] SceneTransition sceneTransition;
    [SerializeField] UISwitchPanel uISwitchPanel;
    // Start is called before the first frame update
    void Start()
    {
        uISwitchPanel = GameObject.Find("StartPage").GetComponent<UISwitchPanel>();
    }

    public void skipIntro()
    {
        if (GameManager.IsFirst)
        {
            uISwitchPanel.SwitchTo(uISwitchPanel.Panel[2]);
        } 
        else 
        {
            sceneTransition.GotoGameScene();
        }
    }
}
