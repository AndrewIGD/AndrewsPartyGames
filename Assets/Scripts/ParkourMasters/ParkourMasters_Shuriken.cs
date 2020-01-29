using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourMasters_Shuriken : MonoBehaviour
{
    public Vector2 force;
    public float timeTillChangeDirection;
    float forceAmplifier = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<ParkourMasters_Player>() != null)
        {
            collision.gameObject.GetComponent<ParkourMasters_Player>().TriggerDeath(Camera.main.gameObject, false, false,false);
        }
    }
    float time;
    private void Start()
    {
        forceAmplifier = PlayerPrefs.GetFloat("ShurikenSpeedAmplifier", forceAmplifier);
        if(GetComponent<Rigidbody2D>() != null)
            GetComponent<Rigidbody2D>().velocity = force*forceAmplifier;
    }
    private void Update()
    {
        time += Time.deltaTime;
        if(time>= timeTillChangeDirection && GetComponent<Rigidbody2D>() != null)
        {
            time = 0;
            GetComponent<Rigidbody2D>().velocity = -GetComponent<Rigidbody2D>().velocity;
        }
    }
}
