using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDirectionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    Vector2 newPos;
    Vector2 oldPos;
    // Update is called once per frame
    void Update()
    {
        oldPos = newPos;
        newPos = transform.position;
        Vector3 diff = newPos - oldPos;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
    }
}
