using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float currHealth;
    [SerializeField] private GameObject healthBarBorder;
    [SerializeField] private GameObject healthBarFill;
	[SerializeField] private GameObject bloodSplatter;
	[SerializeField] private int deathParticles = 30;
	[SerializeField] private int particleDivisor = 7;
	public EnemySpawner spawner;
    private SpriteRenderer healthBarBorderSprite;
    private SpriteRenderer healthBarFillSprite;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100f;
        currHealth = maxHealth;
        healthBarBorderSprite = healthBarBorder.GetComponent<SpriteRenderer>();
        healthBarFillSprite = healthBarFill.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReduceHealth(float damage)
    {
		// Spawn damage particles
		var bloodParticleSystem = Instantiate(bloodSplatter, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().main;
		bloodParticleSystem.startColor = Color.black;
		bloodParticleSystem.gravityModifier = 0f;
		bloodParticleSystem.startSpeed = 0.7f;

        currHealth -= damage;
        healthBarFill.transform.localScale = new Vector3((currHealth / maxHealth), 
            healthBarFill.transform.localScale.y,
            healthBarFill.transform.localScale.z);
        if (currHealth <= 0)
        {
			bloodParticleSystem.maxParticles = deathParticles;

			// Let spawner know they died (Manually added enemies have no spawner)
			if (spawner != null) {
				spawner.numEnemiesAlive--;
				spawner.SpawnItem(GetComponent<Transform>().position);
			}

            Destroy(enemy);
        } else
        {
			bloodParticleSystem.maxParticles = (int) damage / particleDivisor;
            StartCoroutine(DisplayHealth());
        }
    }

	public void Despawn() {
		// Let spawner know they despawned
		if (spawner != null) {
			spawner.numEnemiesAlive--;
		}
		Destroy(enemy);
	}

    IEnumerator DisplayHealth()
    {
        healthBarFillSprite.enabled = true;
        healthBarBorderSprite.enabled = true;
        yield return new WaitForSeconds(1f);
        healthBarFillSprite.enabled = false;
        healthBarBorderSprite.enabled = false;
    }
}
