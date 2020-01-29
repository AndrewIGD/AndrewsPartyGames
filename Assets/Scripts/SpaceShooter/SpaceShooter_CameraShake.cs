using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooter_CameraShake : MonoBehaviour
{
    public bool shake = false;
    Vector3 initPos;
    public float shakePower;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
    }

    public void Shake(float t)
    {
        CancelInvoke("StopShake");
        shake = true;
        Invoke("StopShake", t);
    }
    public void StopShake()
    {
        shake = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(shake)
        {
            transform.localPosition = initPos + (Vector3)Random.insideUnitSphere * shakePower;
        }
        else
        {
            transform.localPosition = initPos;
        }
    }
}
