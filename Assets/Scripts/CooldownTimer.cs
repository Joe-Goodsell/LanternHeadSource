using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownTimer : MonoBehaviour
{
    private MonoBehaviour caller;
    private float cdTime;
    public bool ready { get; private set; }
    // Start is called before the first frame update
    public CooldownTimer(MonoBehaviour caller, float cdTime)
    {
        this.caller = caller;
        this.cdTime = cdTime;
        this.ready = true;
    }

    public void Activate()
    {
        ready = false;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(cdTime);
        ready = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
