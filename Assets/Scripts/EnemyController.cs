using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField] private float speed = 0.5f;
	[SerializeField] private GameObject target;
	[SerializeField] private int damage = 10;
	private Transform _transform;
	private Transform targetTransform;
	private float immunityTime = 2f;
	private bool isImmune = false;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
		targetTransform = target.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
		if (target == null)
		{
			return;
		}
		Vector3 vecToTarget = targetTransform.position - _transform.position;
		vecToTarget.Normalize();
		_transform.position += vecToTarget * (this.speed * Time.deltaTime);
    }

	IEnumerator Immunity()
	{
		isImmune = true;
		yield return new WaitForSeconds(immunityTime);
		isImmune = false;
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		Debug.Log("detected collision with " + collision.gameObject.tag);
		if (collision.gameObject.tag == "Player" && !isImmune)
		{
			Debug.Log("collision with player");
			HealthController healthController = collision.gameObject.GetComponent<HealthController>();
			healthController.TakeDamage(damage);
			StartCoroutine(Immunity());
		}
	}
}
