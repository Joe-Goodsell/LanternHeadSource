using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLight : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

        Vector3 aim = Input.mousePosition;
        aim.z = transform.position.z;
        Vector3 target = Camera.main.ScreenToWorldPoint(aim);
        transform.LookAt(target, Vector3.forward);
        transform.eulerAngles = new Vector3(0, 0, -transform.eulerAngles.z);
    }
}
