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
	public EnemySpawner spawner;
    private SpriteRenderer healthBarBorderSprite;
    private SpriteRenderer healthBarFillSprite;
	private DamageFlash damageFlash;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100f;
        currHealth = maxHealth;
        healthBarBorderSprite = healthBarBorder.GetComponent<SpriteRenderer>();
        healthBarFillSprite = healthBarFill.GetComponent<SpriteRenderer>();
		damageFlash = GetComponent<DamageFlash>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReduceHealth(float damage)
    {
        currHealth -= damage;
		damageFlash.Flash();
        healthBarFill.transform.localScale = new Vector3((currHealth / maxHealth), 
            healthBarFill.transform.localScale.y,
            healthBarFill.transform.localScale.z);
        if (currHealth <= 0)
        {
			// Let spawner know they died (Manually added enemies have no spawner)
			if (spawner != null) {
				spawner.numEnemiesAlive--;
				spawner.SpawnItem(GetComponent<Transform>().position);
			}

            Destroy(enemy);
        } else
        {
            StartCoroutine(DisplayHealth());
        }
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
