using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class FireMiddle : MonoBehaviour
{
    public Sprite[] fireSprites;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float stateChangeDelay = 30f; // 30 sec delay because in the beginning all light sources are not lit
    [SerializeField] private bool canChangeState = false;
    [SerializeField] private int currentState = 2; // default second sprite, so that the fire is lit in the beginning
    [SerializeField] private float countdownTime = 15f; // grace period to relight a light if none are lit
    private IEnumerator cd;
    private bool cdActive;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = fireSprites[currentState];
        StartCoroutine(InitialStateDelay());
        cd = Countdown();
        cdActive = false;
    }

    IEnumerator InitialStateDelay()
    {
        yield return new WaitForSeconds(stateChangeDelay);
        canChangeState = true;
    }

    void Update()
    {
        if (canChangeState)
        {
            int totalLights = GameObject.FindGameObjectsWithTag("LightSource").Length; // all Lightsources need that tag
            int litLights = 0;

            foreach (var light in GameObject.FindGameObjectsWithTag("LightSource"))
            {
                if (light.GetComponent<LightInteraction>().isLit) // LightInteraction script needs to be attached to all light sources
                {
                    litLights++;
                }
            }

            float litPercentage = ((float)litLights / totalLights) * 100;

            int newState = DetermineState(litPercentage);
            Debug.Log("Lit Percentage: " + litPercentage);
            Debug.Log("New State: " + newState);

            if (newState != currentState)
            {
                ChangeSpriteForState(newState);
                currentState = newState;
                if (currentState == 0 && !cdActive) {
                   StartCoroutine(cd); 
                   cdActive = true;
                   StartCdVFX();
                }
                if (currentState > 0 && cdActive) {
                    StopCoroutine(cd);
                    cdActive = false;
                    StopCdVFX();
                }
            }
        }
    }

    void StartCdVFX() {

    } 

    void StopCdVFX() {

    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(countdownTime);
        this.onDeath.Invoke();
    }

    int DetermineState(float litPercentage)
    {
        if (litPercentage == 0) return 0;
        if (litPercentage <= 20) return 1;
        if (litPercentage <= 40) return 2;
        if (litPercentage <= 60) return 3;
        if (litPercentage <= 80) return 4;
        return 5;
    }

    void ChangeSpriteForState(int state)
    {
        if (state >= 0 && state < fireSprites.Length)
        {
            spriteRenderer.sprite = fireSprites[state];
        } else
        {
            Debug.LogError("Invalid state or not enough sprites provided.");
        }
    }
}