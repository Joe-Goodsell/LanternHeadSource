using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AttackLogic : MonoBehaviour
{
    [SerializeField] private LanternController lanternController;
    [SerializeField] private Light2D _light2D;
    [SerializeField] private Color normalColor;
    [SerializeField] private float normalIntensity;
    [SerializeField] private float normalInnerAngle;
    [SerializeField] private float fuelCost;
    [SerializeField] private float cdTime;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip specialAttackHit;
    public bool isAttacking { get; private set;}
    public bool ready { get; private set;}


    // Start is called before the first frame update
    void Start()
    {
        fuelCost = 15;
        cdTime = 5.0f;
        ready = true;
        isAttacking = false;
        normalColor = _light2D.color;
        normalIntensity = _light2D.intensity;
        normalInnerAngle = _light2D.pointLightInnerAngle;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Special Lantern Attack
    public void SpecialAttack() {
        float fuel = lanternController.Fuel;

        StartCoroutine(VisualEffects());
        RaycastAttack(fuel); // Hit detection

        lanternController.Fuel -= fuelCost;                          // Decrease fuel
        lanternController.StartCoroutine(Cooldown()); // Trigger Cooldown
    }

    private IEnumerator Cooldown()
    {
        ready = false;
        yield return new WaitForSeconds(cdTime);
        ready = true;
    }

    private IEnumerator VisualEffects()
    {
        isAttacking = true;
        _light2D.color = new Color(1, 0.64f, 0); // orange
        _light2D.intensity = 12.0f;
        _light2D.pointLightInnerAngle = 0f;
        _light2D.pointLightOuterAngle = 5f;
        yield return new WaitForSeconds(0.4f);
        ResetAttack();
        isAttacking = false;
    }

    private void RaycastAttack(float fuel)
    {
        float attackRange = 15.0f; // Maybe adjust again

        Vector2 aimDirection = lanternController.transform.up;  

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, aimDirection, attackRange);

        foreach (RaycastHit2D hit in hits) {
            if (hit.collider.gameObject.tag == "Enemy") {     // Need to tag our normal enemies with "ShadowEnemy" to make it work
                float damage = 45f; // Base damage

                if (fuel > 30 && fuel <= 50) {
                    damage *= 0.5f; // half damage
                }
                else if (fuel > 90 && fuel <= 100) {
                    damage *= 2.0f; // double damage
                }
                else {
                    damage = damage;
                }
                audioSource.PlayOneShot(specialAttackHit);
                hit.collider.gameObject.GetComponent<EnemyBehaviour>().ReduceHealth(damage); // Write ReduceHealth method in Enemy class.
            }
        }
    }

    public void ResetAttack() {
        // Reset to normal conditions
        _light2D.color = normalColor;
        _light2D.intensity = normalIntensity;
        _light2D.pointLightInnerAngle = normalInnerAngle;
    }
}
