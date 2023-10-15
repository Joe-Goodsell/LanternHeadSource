// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneManager))]
public class GameManager : MonoBehaviour
{
    public const string Tag = "GameManager";
    
    public const string MenuSceneName = "StartScene";
    public const string StartSceneName = "MainScene";
    public const string EndSceneName = "EndScene";
    
    
    private int _score;
    private float _health;
    private float _fuel;
    private float _recordTime;
    private float _startTime;
    private bool _isVictory;
    private bool _isFirst;


    public UnityEvent<int> OnScoreChanged { get; } = new();
    public UnityEvent<float> OnHealthChanged { get; } = new();
    public UnityEvent<float> OnFuelChanged { get; } = new();

    public float RecordTime 
    {
        get => this._recordTime;
        set{
            this._recordTime = value;
        }
    }

    public bool IsVictory 
    {
        get => this._isVictory;
        set{
            this._isVictory = value;
        }
    }

    public bool IsFirst 
    {
        get => this._isFirst;
        set{
            this._isFirst = value;
        }
    }
    

    public int Score
    {
        get => this._score;
        set
        {
            this._score = value;
            OnScoreChanged.Invoke(this._score);
        }
    }

    public float Health
    {
        get => this._health;
        set
        {
            this._health = value;
            OnHealthChanged.Invoke(this._health);
        }
    }

    public float Fuel
    {
        get => this._fuel;
        set
        {
            this._fuel = value;
            OnFuelChanged.Invoke(this._fuel);
        }
    }






    private void Awake()
    {
        // Should not be created if there's already a manager present (at least
        // two total, including ourselves). This allows us to place a game
        // manager in every scene, in case we want to open scenes direct.
        if (GameObject.FindGameObjectsWithTag(Tag).Length > 1)
            Destroy(gameObject);

        // Make this game object persistent even between scene changes.
        DontDestroyOnLoad(gameObject);
        
        // Hook into scene loaded events.
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Init global game state values and/or set defaults.
        Score = 0;
        // Temporary values
        Health = 100.0f;
        Fuel = 100.0f;
        IsVictory = false;
        IsFirst = true;

    }
    
    public IEnumerator GotoScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        var asyncLoadOp = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoadOp.isDone)
        {
            yield return null;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == StartSceneName){
            Score = 0; 
            Health = 100.0f;
            Fuel = 100.0f;
            IsVictory = false;
        }
           
    }
}
