using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    private float speed  = 0.75f;
    private float range  = 3f;
    private int damage = 25;
    private Vector2 direction;
    public Vector2 Direction { 
        set { direction = value.normalized; } 
    } 
    private Vector2 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (Vector2.Distance(startPos, transform.position) > range)
        {
            // TODO : play end animation
            Destroy(gameObject);
        }
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<HealthController>().TakeDamage(damage);
            Destroy(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
