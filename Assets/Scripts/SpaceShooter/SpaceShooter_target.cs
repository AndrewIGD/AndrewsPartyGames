using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooter_target : MonoBehaviour
{
    public bool isEnabled = false;
    void Update()
    {
        if (isEnabled)
        {
            if (transform.localScale.x < 0.3f)
                transform.localScale = new Vector3(transform.localScale.x + 0.4f * Time.deltaTime, transform.localScale.x + 0.4f * Time.deltaTime, 1);
        }
        transform.Rotate(0, 0, 75 * Time.deltaTime);
    }
}
