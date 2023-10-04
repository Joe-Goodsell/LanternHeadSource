using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class CandleFlicker : MonoBehaviour
{
    [SerializeField] private Light candleLight;
    [SerializeField] public float minIntensity = 0.5f;
    [SerializeField] public float maxIntensity = 1.5f;
    [SerializeField] public float flickerSpeed = 0.07f;
    [SerializeField] private float randomizer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        candleLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        // randomness to the flicker interval
        randomizer = Random.Range(0.0f, 1.0f);

        // Adjust light's intensity over time
        if (randomizer > 0.9)
        {
            candleLight.intensity = Mathf.Lerp(candleLight.intensity, Random.Range(minIntensity, maxIntensity), flickerSpeed);
        }
    }
}