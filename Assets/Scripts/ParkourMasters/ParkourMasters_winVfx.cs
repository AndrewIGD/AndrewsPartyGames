using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourMasters_winVfx : MonoBehaviour
{

    public float increaseModifier;
    public float decreaseModifier;
    bool decrease = false;
    public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.01f, 1f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (active == true)
        {
            if (decrease)
            {
                transform.localScale = new Vector3(transform.localScale.x - decreaseModifier * Time.deltaTime, 1, 1);
                if (transform.localScale.x < 0.01f)
                    Destroy(gameObject);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x + increaseModifier * Time.deltaTime, 1, 1);
                if (transform.localScale.x > 1f)
                    decrease = true;
            }
        }
    }
}
