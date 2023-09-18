using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteraction : MonoBehaviour
{
    [SerializeField] private MonoBehaviour player;
    [SerializeField] private GameObject helpText;
    [SerializeField] private GameObject torchFlame;
    [SerializeField] private GameObject torchLight;
    private bool isLit;
    [SerializeField] private bool playerIsInteracting;
    private SpriteRenderer textRenderer;
    // Start is called before the first frame update
    void Start()
    {
        isLit = false;
        Debug.Log("LightInteraction initialized");
        playerIsInteracting = false;
        helpText.SetActive(false);
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
    // Update is called once per frame
    void Update()
    {
        if (playerIsInteracting)
        {
            if (Input.GetKey(KeyCode.E)) {
                isLit = true;
                torchFlame.SetActive(true);
                torchLight.SetActive(true);
                helpText.SetActive(false);
            }
        }
    }
}
