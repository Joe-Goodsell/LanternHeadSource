using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class HealthController : MonoBehaviour
{
	[SerializeField] private int startingHealth = 100;

	[SerializeField] private int _currentHealth;

	[SerializeField] private SpriteRenderer healthBar;
	[SerializeField] private UnityEvent onDeath;
	private SpriteRenderer spriteRenderer;
	private DamageFlash damageFlash;

	private int CurrentHealth
	{
		get { return this._currentHealth; }
		set
		{
			this._currentHealth = value;
			if (_currentHealth <= 0)
			{
				this._currentHealth = 0;
				// Not needed if death causes instant restart or screen change
				healthBar.enabled = false;
				Destroy(gameObject);
				
				this.onDeath.Invoke();
				
			} else if (this._currentHealth > 100) {
				this._currentHealth = 100;
			}
			healthBar.transform.localScale = new Vector3(((float) this._currentHealth / startingHealth),1,1); 
		}
	}

	void Start()
	{
		ResetHealth();
		spriteRenderer = GetComponent<SpriteRenderer>();
		damageFlash = GetComponent<DamageFlash>();
	}

	public void ResetHealth()
	{
		CurrentHealth = startingHealth;
	}

	public void TakeDamage(int damage)
	{
		Debug.Log("taking " + damage + " damage");
		CurrentHealth = _currentHealth - damage;
		damageFlash.Flash();
	}

	public void Heal(int health)
	{
		CurrentHealth += health;
	}

}
