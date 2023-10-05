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
    [SerializeField] private float flickerStep = 0.1f;
    [SerializeField] public float flickerSpeed = 0.005f;
    private System.Random randomizer;
    private float intensity;
    private bool coroutineActive;




    // Start is called before the first frame update
    void Start()
    {
        candleLight = GetComponent<Light2D>();
        coroutineActive = false;
        randomizer = new System.Random();
        intensity = baseline;
        candleLight.intensity = intensity;
    }

    IEnumerator Flicker()
    {
        while (true) {
            Debug.Log("flickering");
            coroutineActive = true;
            // step is smaller when light is brighter
            var randomStep = randomizer.NextDouble() * (2 * flickerStep) - flickerStep; 
            var step = (1.01f - ((intensity-minIntensity) / (maxIntensity-minIntensity))) * randomStep;
            intensity += (float) step;
            if (intensity >= maxIntensity)
            {
                intensity = maxIntensity - 0.01f;
            }
            if (intensity <= minIntensity)
            {
                intensity = minIntensity - 0.01f;
            }
            candleLight.intensity = intensity; 
            yield return new WaitForSeconds(flickerSpeed);
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
