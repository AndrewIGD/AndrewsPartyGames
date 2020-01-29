using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrderY : MonoBehaviour
{
    public SpriteRenderer[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(SpriteRenderer sprite in sprites)
            sprite.sortingOrder = (int)(transform.position.y * -100);
    }
}
