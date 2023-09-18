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
        currHealth -= damage;
        healthBarFill.transform.localScale = new Vector3((currHealth / maxHealth), 
            healthBarFill.transform.localScale.y,
            healthBarFill.transform.localScale.z);
        if (currHealth <= 0)
        {
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
