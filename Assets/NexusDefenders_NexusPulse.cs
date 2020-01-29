using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusDefenders_NexusPulse : MonoBehaviour
{
    bool growing = true;
    public float growCoeficient;
    // Update is called once per frame
    void Update()
    {
        if (growing)
            transform.localScale = new Vector3(transform.localScale.x + growCoeficient * Time.deltaTime, transform.localScale.x + growCoeficient * Time.deltaTime, 1);
        else transform.localScale = new Vector3(transform.localScale.x - growCoeficient * Time.deltaTime, transform.localScale.x - growCoeficient * Time.deltaTime, 1);
        if (transform.localScale.x >= 0.15f && growing)
            growing = false;
        if (transform.localScale.x <= 0.04f && !growing)
            growing = true;
    }
}
