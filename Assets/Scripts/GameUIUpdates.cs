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
    
    // Start is called before the first frame update
    void Start()
    {
        lanternController = GameObject.Find("LanternLight").GetComponent<LanternController>();
        attackLogic = GameObject.Find("LanternLight").GetComponent<AttackLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        int litLights = 0;

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
}
