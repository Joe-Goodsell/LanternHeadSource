using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Reflection;
[RequireComponent(typeof(AudioSource))]
public class LanternController : MonoBehaviour
{
    [SerializeField] private Light2D _light2D;
    private FieldInfo _Falloff = typeof(Light2D).GetField("m_FalloffIntensity", BindingFlags.NonPublic | BindingFlags.Instance);

    [SerializeField] private float defaultFalloff = 0.7f;
    [SerializeField] private float currIntensity = 0.7f;
    [SerializeField] private float defaultInnerSpotAngle = 50f;
    [SerializeField] private float defaultOuterSpotAngle = 100f;
    [SerializeField] private float focussedFalloff = 0.4f;
    [SerializeField] private float focussedIntensity = 1.5f;
    [SerializeField] private float focussedInnerSpotAngle = 20f;
    [SerializeField] private float focussedOuterSpotAngle = 40f;
    [SerializeField] private float maxFuel = 100.0f;
    [SerializeField] private float _fuel;
    [SerializeField] private AttackLogic attackLogic;
    [SerializeField] private LHController lanternHead;
    [SerializeField] private SpriteRenderer fuelBar;
    [SerializeField] private float _maxIntensity = 3.0f;
    [SerializeField] private float _minIntensity = 0.2f;
    [SerializeField] private float _intensityPerScroll = 1.0f;
    [SerializeField] private bool _enableAttack = true;
    [SerializeField] private float _fuelDecreaseRate = 0.001f;
    [SerializeField] private float _currIntensityDecreaseWeight = 0.001f;
    [SerializeField] private AudioClip refuelSound;
    [SerializeField] private AudioClip specialAttackSound;
    private AudioSource audioSource;

    public float Fuel 
	{
		get { return this._fuel; }
		set
		{
			this._fuel = value;
			if (_fuel <= 0)
			{
				this._fuel = 0;
			} else if (this._fuel > maxFuel) {
				this._fuel = maxFuel;
			}
			fuelBar.transform.localScale = new Vector3((this._fuel / maxFuel),1,1); 
		}
	}

    public bool EnableAttack
    {
        get {return this._enableAttack;}
    }

    // Start is called before the first frame update
    void Start()
    {
        Fuel = maxFuel;
        attackLogic = GetComponent<AttackLogic>();
        lanternHead = GameObject.Find("LanternHead").GetComponent<LHController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Fuel -= _fuelDecreaseRate + (currIntensity * _currIntensityDecreaseWeight);

        if (Fuel == 0){
            _light2D.intensity = 0;
            return;
        }

        // Debug.Log(Fuel);
        if (Fuel > 90){
            lanternHead.SpeedBuff = 1.1f;
            _enableAttack = true;
        } else if (Fuel > 70){
            lanternHead.SpeedBuff = 1.05f;
            _enableAttack = true;
        } else if (Fuel > 50){
             lanternHead.SpeedBuff = 1.0f;
            _enableAttack = true;
        } else if (Fuel > 30){
             lanternHead.SpeedBuff = 0.95f;
            _enableAttack = true;
        } else if (Fuel > 10){
             lanternHead.SpeedBuff = 0.85f;
            _enableAttack = false;
        } else {
             lanternHead.SpeedBuff = 0.70f;
            _enableAttack = false;
        } 

        if (!attackLogic.isAttacking)
        {
            bool isFocussed = Focus();
            Aim();
            ChangeIntensity();

            if (isFocussed && attackLogic.ready && Input.GetKey(KeyCode.Mouse0) && _enableAttack)
            {
                audioSource.clip = specialAttackSound;
                audioSource.Play();
                
                attackLogic.SpecialAttack();
            }
        }
    }

    void Aim()
    {
        Vector3 aim = Input.mousePosition;
        aim.z = transform.position.z;
        Vector3 target = Camera.main.ScreenToWorldPoint(aim);
        transform.LookAt(target, Vector3.forward);
        transform.eulerAngles = new Vector3(0, 0, -transform.eulerAngles.z);
    }

    public void Refuel(float fuel)
    {
        audioSource.clip = refuelSound;
        audioSource.Play();
        Fuel += fuel;
    }

    bool Focus()
    {
       if (Input.GetKey(KeyCode.Mouse1))
       {
           _light2D.intensity = currIntensity * focussedIntensity;
           _light2D.pointLightInnerAngle = focussedInnerSpotAngle;
           _light2D.pointLightOuterAngle = focussedOuterSpotAngle;
           _Falloff.SetValue(_light2D, focussedFalloff);
           return true;
       } 
       else
       {
           _light2D.intensity = currIntensity; 
           _light2D.pointLightInnerAngle = defaultInnerSpotAngle;
           _light2D.pointLightOuterAngle = defaultOuterSpotAngle;
           _Falloff.SetValue(_light2D, defaultFalloff);
           return false;
       }
    }

    void ChangeIntensity(){
        currIntensity += Input.mouseScrollDelta.y * _intensityPerScroll;
        currIntensity = Mathf.Max(_minIntensity, currIntensity);
        currIntensity = Mathf.Min(_maxIntensity, currIntensity);
        _light2D.intensity = currIntensity;
    }
    
}