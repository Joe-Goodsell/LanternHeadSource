using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using TMPro;

[RequireComponent(typeof(Light2D))]
public class FireMiddle : MonoBehaviour
{
    [SerializeField] GameObject[] _flames;
    [SerializeField] Light2D _light;
    [SerializeField] Light2D _glow;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private float stateChangeDelay = 30f; // 30 sec delay because in the beginning all light sources are not lit
    [SerializeField] private bool canChangeState = false;
    [SerializeField] private int currentState = 2; // default second sprite, so that the fire is lit in the beginning
    [SerializeField] private float countdownTime = 15f; // grace period to relight a light if none are lit
    private float maxIntensityLight;
    private float maxIntensityGlow;
    private Vector3 maxFlameScaling;
    private IEnumerator cd;
    private bool cdActive;

    void Start()
    {
        maxIntensityLight = _light.intensity;
        maxIntensityGlow = _glow.intensity;
        maxFlameScaling = _flames[0].transform.localScale;
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
                SetIntensity(newState);
                currentState = newState;
                if (currentState == 0 && !cdActive) {
                   StartCoroutine(cd); 
                   cdActive = true;
                }
                if (currentState > 0 && cdActive) {
                    StopCoroutine(cd);
                    cdActive = false;
                }
            }
        }
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

    void SetIntensity(int state)
    {
        _light.intensity = maxIntensityLight * (state / 5.0f);
        _glow.intensity = maxIntensityGlow * (state / 5.0f);
        
        Vector3 scaling = maxFlameScaling * (System.Math.Min((state + 1) / 5.0f, 1.0f));
        foreach (GameObject flame in _flames)
        {
            flame.transform.localScale = scaling;
        }
    }
}