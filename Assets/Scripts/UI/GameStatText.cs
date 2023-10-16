using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatText : GameManagerClient
{
    [SerializeField] private TextMeshProUGUI gameStatText;
    // Start is called before the first frame update
    void Start()
    {        
        GameManager.TotalGame += 1;
        if (GameManager.IsVictory){
            GameManager.TotalVictory += 1;
        }
        
        int totalGame = GameManager.TotalGame;
        int totalVictory = GameManager.TotalVictory;

        string statText = string.Format("Number of time(s) played: {0}\nNumber of time(s) survived: {1}", totalGame, totalVictory);
        gameStatText.text = statText;
    }
}
