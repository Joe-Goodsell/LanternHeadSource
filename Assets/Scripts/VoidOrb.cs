using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidOrb : MonoBehaviour
{
    private int frameTotal;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private int currFrame;
    private Sprite currSprite;
    [SerializeField] private float fs;
    [SerializeField] private float bobRate;
    [SerializeField] private float bobHeight;
    private float yBaseline;
    private float delta;
    // Start is called before the first frame update
    void Start()
    {
       delta = 1f;
       yBaseline = 0.525f;
       currFrame = frameTotal - 1;
       StartCoroutine(PlayAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = currSprite; 

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

    IEnumerator PlayAnimation() {
        while (true) {
            if (currFrame < 0)
            {
                currFrame = sprites.Count - 1;
            }
            currSprite = sprites[currFrame];
            currFrame--;
            float waitTime = 1f / fs;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
