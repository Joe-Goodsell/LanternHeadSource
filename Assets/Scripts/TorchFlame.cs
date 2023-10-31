using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchFlame : MonoBehaviour
{
    
    [SerializeField] private List<Sprite> flameSprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private int currFrame;
    // Start is called before the first frame update
    void Start()
    { 
        System.Random rand = new System.Random(3023);
        int startFrame = rand.Next(0,100); // ensures flames aren't perfectly synchronised
        currFrame = startFrame;
    }

    // Update is called once per frame
    void Update()
    {
       if (currFrame >= flameSprites.Count)
       {
           currFrame = 0;
       }
       spriteRenderer.sprite = flameSprites[currFrame];
       currFrame++;
    }
}
