using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject lantern;
    [SerializeField] private GameObject helpText;
    [SerializeField] private GameObject pickup;
    [SerializeField] private bool playerIsInteracting;
    private SpriteRenderer textRenderer;
    private HealthController healthController;
    private LanternController lanternController;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PickupInteraction initialized");
        textRenderer = helpText.GetComponent<SpriteRenderer>();
        textRenderer.enabled = false;
        playerIsInteracting = false;
        player = GameObject.FindWithTag("Player");
        lantern = GameObject.FindWithTag("Lantern");
        healthController = player.GetComponent<HealthController>();
        lanternController = lantern.GetComponent<LanternController>();
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
                if (pickup.tag == "HealthPotion")
                {
                    healthController.Heal(25);
                }
                else if (pickup.tag == "LanternFuel")
                {
                    lanternController.Refuel(25);
                }
                Destroy(pickup);
            }
        }
    }
}
