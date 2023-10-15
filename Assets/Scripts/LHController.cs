using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Reflection;

[RequireComponent(typeof(AudioSource))]
public class LHController : MonoBehaviour
{
    enum MoveMode
    {
        Normal,
        Dash
    }

    [SerializeField] private List<Sprite> _NorthSprites;
    [SerializeField] private List<Sprite> _NorWestSprites;
    [SerializeField] private List<Sprite> _WestSprites;
    [SerializeField] private List<Sprite> _SouWestSprites;
    [SerializeField] private List<Sprite> _SouthSprites;

    [SerializeField] private Light2D _light2D;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    private FieldInfo _LightSprite = typeof(Light2D).GetField("m_LightCookieSprite", BindingFlags.NonPublic | BindingFlags.Instance);
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private Sprite currSprite;
    [SerializeField] private float baselineSpeed; // default walking speed
    [SerializeField] private float currSpeed; // walking speed with buffs/debuffs applied 

    private MoveMode moveMode;
    private Vector3 instantDirection;
    [SerializeField] private bool dashReady;
    [SerializeField] private float dashCd;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private GameObject lantern;
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private bool attackReady;
    [SerializeField] private AudioClip attackClip1;
    [SerializeField] private AudioClip attackClip2;
    [SerializeField] private AudioClip attackClip3;

    private float attackCd;
    private float _speedBuff;
    private AudioSource audioSource;
    private AudioClip[] audioClips;

    public float SpeedBuff
	{
		get { return this._speedBuff; }
		set
		{
			this._speedBuff = value;
		}
	}
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClips = new AudioClip[] {attackClip1,attackClip2,attackClip3};
        
        attackReady = true;
        attackCd = 0.1f;


        //player movement settings
        baselineSpeed = 0.7f;
        currSpeed = baselineSpeed;
        _speedBuff = 1.0f;
        moveMode = MoveMode.Normal;
        dashReady = true;
        dashSpeed = baselineSpeed * 10f;
        dashDuration = 0.1f;
        dashCd = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (moveMode != MoveMode.Dash) // no need to update sprites during a Dash
        {
            List<Sprite> currList = GetSprites();
            currSprite = currList[0];
            _spriteRenderer.sprite = currSprite;
            _LightSprite.SetValue(_light2D, currSprite); 
        }
        if (Input.GetKey(KeyCode.LeftShift) && dashReady == true)
        {
            Debug.Log("starting dash");
            StartCoroutine(Dash());
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && attackReady)
        {
            Attack();
        }
    }

    private void Attack()
    {
        audioSource.clip = audioClips[Random.Range(0,3)];
        audioSource.Play();

        
        StartCoroutine(AttackCooldown());
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
            EnemyBehaviour enemyBehaviour = enemy.GetComponent<EnemyBehaviour>(); 
            if (attackCollider.IsTouching(enemyCollider))
            {
                enemyBehaviour.ReduceHealth(10f); 
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        attackReady = false;
        yield return new WaitForSeconds(attackCd);
        attackReady = true;
    }

    IEnumerator Dash()
    {
        moveMode = MoveMode.Dash;
        StartCoroutine(StartDashCd());
        yield return new WaitForSeconds(dashDuration);
        moveMode = MoveMode.Normal;
    }



    IEnumerator StartDashCd()
    {
        dashReady = false;
        yield return new WaitForSeconds(dashCd);
        dashReady = true;
    }

    void Move()
    {
        if (moveMode != MoveMode.Dash) // heading only updated when not dashing
        {
            var dx = Vector3.right * Input.GetAxis("Horizontal");
            var dy = Vector3.up * Input.GetAxis("Vertical");
            instantDirection = dx + dy;
            instantDirection.Normalize();
            _rigidBody.velocity = instantDirection * currSpeed * _speedBuff;
        } else
        {
            Debug.Log("dashing...");
            _rigidBody.velocity = instantDirection * dashSpeed;            
        }

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    List<Sprite> GetSprites()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = transform.position.z;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        Vector3 vecToMouse = mouseWorldPos - transform.position;
        double angle = System.Math.Atan2(vecToMouse[1], vecToMouse[0]) * (180/System.Math.PI);
        if (angle <= 112.5f && angle > 67.5f)
        {
            _spriteRenderer.flipX = false;
            return _NorthSprites;    
        }
        if (angle <= 67.5f && angle > 22.5f) 
        {
            _spriteRenderer.flipX = true;    
            return _NorWestSprites;
        }
        if (angle <= 22.5f && angle > -22.5f)
        {
            _spriteRenderer.flipX = true;
            return _WestSprites;
        }
        if (angle <= -22.5f && angle > -67.5f)
        {
            _spriteRenderer.flipX = true;
            return _SouWestSprites;
        }
        if (angle <= -67.5f && angle > -112.5f)
        {
            _spriteRenderer.flipX = false;
            return _SouthSprites;
        }
        if (angle <= -112.5f && angle > -157.5f)
        {
            _spriteRenderer.flipX = false;
            return _SouWestSprites;
        }
        if (angle <= -157.5f && angle > -180f )
        {
            _spriteRenderer.flipX = false;
            return _WestSprites;
        }
        if (angle <= 180f && angle > 157.5f) {
            _spriteRenderer.flipX = false;
            return _WestSprites;
        }
        if (angle <= 157.5f && angle > 112.5f)
        {
            _spriteRenderer.flipX = false;
            return _NorWestSprites;
        }
        return _NorthSprites;
 
    }
}
