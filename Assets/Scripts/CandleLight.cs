using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class CandleLight : MonoBehaviour
{
    [SerializeField] private Light2D candleLight;
    [SerializeField] private float baseline = 1.0f;
    [SerializeField] private float maxIntensity = 2.0f;
    [SerializeField] private float minIntensity = 0.5f;
    private float currMaxIntensity;
    private float currMinIntensity;
    [SerializeField] private float flickerStep = 0.1f;
    [SerializeField] public float flickerSpeed = 0.005f;
    private System.Random randomizer;
    private float intensity;
    private bool coroutineActive;


    // Start is called before the first frame update
    void Start()
    {
        currMaxIntensity = maxIntensity;
        currMinIntensity = minIntensity;
        candleLight = GetComponent<Light2D>();
        coroutineActive = false;
        randomizer = new System.Random();
        intensity = baseline;
        candleLight.intensity = intensity;
    }

    IEnumerator Flicker()
    {
        while (true) {
            coroutineActive = true;
            // step is smaller when light is brighter
            var randomStep = randomizer.NextDouble() * (2 * flickerStep) - flickerStep; 
            var step = (1.01f - ((intensity-currMinIntensity) / (currMaxIntensity-currMinIntensity))) * randomStep;
            intensity += (float) step;
            if (intensity >= currMaxIntensity)
            {
                intensity = currMaxIntensity - 0.01f;
            }
            if (intensity <= currMinIntensity)
            {
                intensity = currMinIntensity - 0.01f;
            }
            candleLight.intensity = intensity; 
            yield return new WaitForSeconds(flickerSpeed);
        }
    }

    public IEnumerator Fade()
    {
        while (currMaxIntensity > 0f)
        {
            currMaxIntensity -= 0.075f;
            currMinIntensity -= 0.075f;
            yield return new WaitForSeconds(0.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineActive == false)
        {
            StartCoroutine(Flicker());
        }
    }
}
