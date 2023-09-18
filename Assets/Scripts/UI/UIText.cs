using System.Collections;
using TMPro;
using UnityEngine;

public class UIText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float defaultValue;
    [SerializeField] private string fieldName;


    private void Awake()
    {
        this.text.SetText(fieldName + " : " +  defaultValue + "%");
    }
}