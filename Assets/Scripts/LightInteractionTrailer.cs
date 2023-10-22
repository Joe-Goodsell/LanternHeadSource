
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LightInteractionTrailer : MonoBehaviour
{
    [SerializeField] private MonoBehaviour player;
    [SerializeField] private GameObject helpText;
    [SerializeField] private GameObject torchFlame;
    [SerializeField] private GameObject torchLight;
    [SerializeField] private GameObject glow;
    [SerializeField] private SpriteRenderer snuffEffectRend;
    [SerializeField] private float timeToSnuff;
    [SerializeField] private List<Sprite> snuffSprites;
    [SerializeField] private float snuffEffectFs;
    private bool isLit;
    [SerializeField] private bool playerIsInteracting;
    private SpriteRenderer textRenderer;

    // Start is called before the first frame update
    void Start()
    {
        SetTo(false);
        Debug.Log("LightInteraction initialized");
        helpText.SetActive(false);
        playerIsInteracting = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision with " + other.gameObject.name);
        if (other.gameObject.name == "LanternHead" && !isLit)
        {
            playerIsInteracting = true;
            helpText.SetActive(true);
        } else if (other.gameObject.tag == "Enemy" && isLit)
        {
            System.Random rand = new System.Random();
            var randn = rand.NextDouble();
            if (randn > 0.90f) // 10% chance to be snuffed out 
            {
                StartCoroutine(SnuffAnimation());
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "LanternHead")
        {
            playerIsInteracting = false;
            helpText.SetActive(false);
        }
    }

    IEnumerator SnuffAnimation()
    {
        isLit = false;
        Debug.Log("snuffing torch... ");
        StartCoroutine(torchLight.GetComponent<CandleLight>().Fade());
        torchFlame.SetActive(false);
        snuffEffectRend.enabled = true;
        foreach (Sprite sprite in snuffSprites)
        {
            snuffEffectRend.sprite = sprite;
            yield return new WaitForSeconds(1/snuffEffectFs);
        }
        snuffEffectRend.enabled = false;
        yield return new WaitForSeconds(0.5f); // buffer
        SetTo(false);
    }

    IEnumerator TimedSnuff()
    {
        yield return new WaitForSeconds(timeToSnuff);
        StartCoroutine(SnuffAnimation());
    }


    IEnumerator RandSnuff()
    {
        Debug.Log("randomly snuffed out");
        System.Random rand = new System.Random();
        while (isLit)
        {
            if (rand.NextDouble() > 0.99f) 
            {
                StartCoroutine(SnuffAnimation());
            }
            yield return new WaitForSeconds(3f); 
        }
    }


    void SetTo(bool turnOn)
    {
        isLit = turnOn;
        if (turnOn) {
       //     StartCoroutine(RandSnuff());
            StartCoroutine(TimedSnuff());
        } else {
        //    StopCoroutine(RandSnuff());
            StopCoroutine(TimedSnuff());
        }
        torchFlame.SetActive(turnOn);
        torchLight.SetActive(turnOn);
        glow.SetActive(turnOn);
    }
    // Update is called once per frame
    void Update()
    {
        if (playerIsInteracting)
        {
            if (Input.GetKey(KeyCode.E)) {
                SetTo(true);
                helpText.SetActive(false);
            }
        }
    }
}
