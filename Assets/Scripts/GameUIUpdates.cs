using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIUpdates : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI candleCount;
    [SerializeField] private GameObject specialAttack;
    [SerializeField] private LanternController lanternController;
    [SerializeField] private AttackLogic attackLogic;
    private bool coroutineStarted;
    private int litLights;
    
    // Start is called before the first frame update
    void Start()
    {
        lanternController = GameObject.Find("LanternLight").GetComponent<LanternController>();
        attackLogic = GameObject.Find("LanternLight").GetComponent<AttackLogic>();
        StartCoroutine(candleWarning());
    }

    // Update is called once per frame
    void Update()
    {
        litLights = 0;

        foreach (var light in GameObject.FindGameObjectsWithTag("LightSource"))
        {
            if (light.GetComponent<LightInteraction>().isLit) 
            {
                litLights++;
            }
        }
        
        string candleCountStr = string.Format(": {0}", litLights);
        candleCount.text = candleCountStr;

        if (lanternController.EnableAttack && attackLogic.ready)
        {
            specialAttack.SetActive(true);
        }
        else
        {
            specialAttack.SetActive(false);
        }

    }

    IEnumerator candleWarning()
    {
        coroutineStarted = true;
        candleCount.faceColor = new Color32(255,0,0,255);
        while (true)
        {
            if (litLights <= 1)
            {
                while(litLights <= 1)
                {
                    yield return new WaitForSeconds(0.5f);
                    candleCount.faceColor = new Color32(255,0,0,0);
                    yield return new WaitForSeconds(0.5f);
                    candleCount.faceColor = new Color32(255,0,0,255);
                }
            }
            else
            {
                candleCount.faceColor = new Color32(255,255,255,255);
                yield return null;
            }
        }   
    }
}
