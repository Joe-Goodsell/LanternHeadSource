using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField] private float speed = 0.5f;
	[SerializeField] private GameObject target;
	[SerializeField] private int damage = 10;
	[SerializeField] private float visionRange = 10.0f;
	private Transform _transform;
	private Transform targetTransform;
	private float immunityTime = 2f;
	private bool isImmune = false;
	private Vector3 destination;

    // Start is called before the first frame update
	void Start()
	{
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
		if (target == null)
		{
			return;
		}

		// If reached destination, choose another random nearby one
		var dist = Vector2.Distance(_transform.position, destination);
		if (dist < 1.0f)
		{
			destination = RandomLocation();
		}

		Vector3 vecToTarget = targetTransform.position - _transform.position;
		float distToTarget = vecToTarget.magnitude;
		vecToTarget.Normalize();

		RaycastHit2D hit = Physics2D.Linecast(_transform.position, targetTransform.position, (1<<6));

		if (hit.collider == null && (Vector2.Distance(_transform.position, targetTransform.position) < visionRange)) {
			// If has line of sight, move to player
			destination = targetTransform.position;
		}

		// Move to destination
		_transform.position += (destination - _transform.position).normalized * speed * Time.deltaTime;
	}

	IEnumerator Immunity()
	{
		isImmune = true;
		yield return new WaitForSeconds(immunityTime);
		isImmune = false;
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" && !isImmune)
		{
			HealthController healthController = collision.gameObject.GetComponent<HealthController>();
			healthController.TakeDamage(damage);
			StartCoroutine(Immunity());
		}
	}

	private Vector3 RandomLocation() {
		float x = Random.Range(-10.0f, 10.0f);
		float y = Random.Range(-10.0f, 10.0f);
		return new Vector3(x + _transform.position.x, y + _transform.position.y, 0.1f);
	}
}
