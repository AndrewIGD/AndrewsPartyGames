using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgMove : MonoBehaviour
{
    public float speed;
    bool ableToMove = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("AbleToMove", 5.5f);
    }
    void AbleToMove()
    {
        ableToMove = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(ableToMove)
            transform.localPosition = new Vector3(transform.localPosition.x - (speed *  Time.deltaTime), transform.localPosition.y, 10);
    }
}
