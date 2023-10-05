using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteraction : MonoBehaviour
{
    [SerializeField] private MonoBehaviour player;
    [SerializeField] private GameObject helpText;
    [SerializeField] private GameObject torchFlame;
    [SerializeField] private GameObject torchLight;
    [SerializeField] private GameObject glow;
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

    void SetTo(bool turnOn)
    {
        isLit = turnOn;
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
