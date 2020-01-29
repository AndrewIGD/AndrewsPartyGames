using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooter_Block : MonoBehaviour
{
    public bool spawning = false;
    float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Spawn()
    {
        spawning = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(spawning)
        {
            t += Time.deltaTime * 2;
            GetComponent<SpriteRenderer>().color = Color32.Lerp(new Color32(0, 0, 0, 0), new Color32(0, 0, 0, 255), t);
            if (t >= 1)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
                Destroy(GetComponent<SpaceShooter_Block>());
            }
        }
    }
}
