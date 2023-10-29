using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShader : MonoBehaviour
{
    [SerializeField] private float _spread = 1f;
    private Coroutine _spreadCoroutine;
    private Material _material;

    private int _spreadID = Shader.PropertyToID("_spread");

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            changeSpread();
        }
    }

    public void changeSpread()
    {
        _spreadCoroutine = StartCoroutine(spreadAction(0.01f,1.0f));
    }

    private IEnumerator spreadAction(float start, float end)
    {
        _material.SetFloat(_spreadID, start);

        float lerpedAmount = 0f;

        float elapsedTime = 0f;

        while(elapsedTime < _spread)
        {
            elapsedTime += Time.deltaTime;

            lerpedAmount = Mathf.Sin(elapsedTime) / 5;
            _material.SetFloat(_spreadID, lerpedAmount);

            yield return null;
        } 
        _material.SetFloat(_spreadID, start);
    }
}
