using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup2 : MonoBehaviour
{
    [SerializeField] private MonoBehaviour player;
    [SerializeField] private GameObject helpText;
    [SerializeField] private GameObject pickup;
    [SerializeField] private bool playerIsInteracting;
    [SerializeField] private int health = 30;
    private SpriteRenderer textRenderer;
    private HealthController healthController;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PickupInteraction initialized");
        textRenderer = helpText.GetComponent<SpriteRenderer>();
        textRenderer.enabled = false;
        playerIsInteracting = false;
        healthController = player.GetComponent<HealthController>();
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision with " + other.gameObject.name);
        if (other.gameObject.name == "LanternHead")
        {
            playerIsInteracting = true;
            textRenderer.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "LanternHead")
        {
            playerIsInteracting = false;
            textRenderer.enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (playerIsInteracting)
        {
            if (Input.GetKey(KeyCode.E))
            {
                textRenderer.enabled = false;
                healthController.Heal(health);
                Destroy(pickup);
            }
        }
    }
}
