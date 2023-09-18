using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBobbing : MonoBehaviour
{
    [SerializeField] private float bobRate;
    [SerializeField] private float bobHeight;
    private float yBaseline;
    private float delta;
    // Start is called before the first frame update
    void Start()
    {
        bobRate = .02f;
        bobHeight = .04f;
        delta = 1f;
        yBaseline = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float currPos = transform.position[1];
        if (currPos >= yBaseline + (bobHeight / 2))
        {
            delta = -1;
        }
        else if (currPos <= yBaseline - (bobHeight / 2))
        {
            delta = 1;
        }
        transform.position += new Vector3(0f, Time.deltaTime * bobRate * delta, 0f); 
    }
}
