using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum State
    {
        Normal,
        Attack
    }
	[SerializeField] private float speed = 0.5f;
    [SerializeField] private float dashSpeed = 2.5f;
	[SerializeField] public GameObject target;
	[SerializeField] private int damage = 30;
	[SerializeField] private float visionRange = 2.5f;
	[SerializeField] private float wanderRadius = 2.5f;
	[SerializeField] private float despawnRadius = 7f;
    [SerializeField] private float attackDistance;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private float attackAnimFs;
    [SerializeField] private float attackCd;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private bool attackReady;
    private State state;
	private Transform _transform;
	private Transform targetTransform;
	private float immunityTime = 2f;
	private bool isImmune = false;
	private Vector3 destination;

    // Start is called before the first frame update
	void Start()
	{
        state = State.Normal;
        attackReady = true;
		_transform = GetComponent<Transform>();
		targetTransform = target.GetComponent<Transform>();

		destination = RandomLocation();

		// Set z to zero
		_transform.position = new Vector3(_transform.position.x, _transform.position.y, 0.1f);
	}

	// Update is called once per frame
	// Move to last known player position then wander randomly
	void Update()
	{
        if (state == State.Attack)
        {
            _transform.position += (destination - _transform.position).normalized * dashSpeed * Time.deltaTime;
            return;
        }

		if (target == null)
		{
			return;
		}

		// If reached destination, choose another random nearby one
		var dist = Vector2.Distance(_transform.position, destination);
		if (dist < 0.1f)
		{
			destination = RandomLocation();
		}

		Vector3 vecToTarget = targetTransform.position - _transform.position;
		float distToTarget = vecToTarget.magnitude;
		vecToTarget.Normalize();

		// Despawn if too far from player
		if (distToTarget > despawnRadius)
		{
			Debug.Log("Despawning enemy...");
			GetComponent<EnemyBehaviour>().Despawn();
		}

		RaycastHit2D hit = Physics2D.Linecast(_transform.position, targetTransform.position, (1<<6));

		if (hit.collider == null && (Vector2.Distance(_transform.position, targetTransform.position) < visionRange)) {
			// If has line of sight, move to player
			destination = targetTransform.position;
		}

		// Move to destination
        if (distToTarget < attackDistance && attackReady)
        {
            StartCoroutine(Attack());
        } else 
        {
            _transform.position += (destination - _transform.position).normalized * speed * Time.deltaTime;
        }
	}

    IEnumerator Attack()
    {
        state = State.Attack;
        StartCoroutine(AttackCooldown());
        Debug.Log("attacking player");
        foreach (Sprite sprite in sprites)
        {
            _spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(1/attackAnimFs);
        }
        state = State.Normal;
    }

    IEnumerator AttackCooldown()
    {
        attackReady = false;
        yield return new WaitForSeconds(attackCd);
        attackReady = true;
    }

	IEnumerator Immunity()
	{
		isImmune = true;
		yield return new WaitForSeconds(immunityTime);
		isImmune = false;
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" && !isImmune && state == State.Attack)
		{
			HealthController healthController = collision.gameObject.GetComponent<HealthController>();
			healthController.TakeDamage(damage);
			StartCoroutine(Immunity());
		}
	}

	private Vector3 RandomLocation() {
		float x = Random.Range(-wanderRadius, wanderRadius);
		float y = Random.Range(-wanderRadius, wanderRadius);
		return new Vector3(x + _transform.position.x, y + _transform.position.y, 0.1f);
	}
}
