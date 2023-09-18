using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    // Update is called once per frame
    void Update()
    {
        
        var dx = Vector3.right * Input.GetAxis("Horizontal");
        var dy = Vector3.up * Input.GetAxis("Vertical");
        var movement = dx + dy;

        transform.position += movement * (this.speed * Time.deltaTime);
    }
}
