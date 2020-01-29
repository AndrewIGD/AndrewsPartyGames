using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrderX : MonoBehaviour
{
    public SpriteRenderer[] sprites;
    public List<int> sortingOrders;
    // Start is called before the first frame update
    void Awake()
    {   
        foreach(SpriteRenderer sprite in sprites)
        {
            sortingOrders.Add(sprite.sortingOrder);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.sortingOrder = (int)(transform.position.x * -1000000)+ sortingOrders[i];
            i++;
        }
    }
}
