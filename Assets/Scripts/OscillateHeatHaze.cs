using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OscillateBlendFactor : MonoBehaviour
{

    [SerializeField] private Material _material;

    private void Start()
    {
    }

    private void Update()
    {
        var x = Time.time;
        this._material.SetFloat("Timer", x);
    }
}
