using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivePanel : MonoBehaviour
{
    [SerializeField] private RectTransform[] panels;
    
    public void Deactivate()
    {
        foreach (var panel in this.panels)
            panel.gameObject.SetActive(false);
    }
}
