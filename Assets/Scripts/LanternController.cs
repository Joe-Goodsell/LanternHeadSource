using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Reflection;
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
    [SerializeField] private int maxFuel = 100;
    [SerializeField] private int _fuel;
    [SerializeField] private AttackLogic attackLogic;
    [SerializeField] private SpriteRenderer fuelBar;
    [SerializeField] private float _maxIntensity = 3.0f; 

    public int Fuel 
	{
		get { return this._fuel; }
		set
		{
			this._fuel = value;
			if (_fuel <= 0)
			{
				this._fuel = 0;
			} else if (this._fuel > 100) {
				this._fuel = 100;
			}
			fuelBar.transform.localScale = new Vector3(((float) this._fuel / maxFuel),1,1); 
		}
	}
    // Start is called before the first frame update
    void Start()
    {
        Fuel = maxFuel;
        attackLogic = GetComponent<AttackLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attackLogic.isAttacking)
        {
            bool isFocussed = Focus();
            Aim();
            ChangeIntensity();
            Debug.Log(isFocussed + " " + attackLogic.ready);

            if (isFocussed && attackLogic.ready && Input.GetKey(KeyCode.Mouse0))
            {
                Debug.Log("special attack");
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

    public void Refuel(int fuel)
    {
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
        currIntensity += Input.mouseScrollDelta.y;
        currIntensity = Mathf.Min(_maxIntensity, currIntensity);
        _light2D.intensity = currIntensity;
    }
    
}
