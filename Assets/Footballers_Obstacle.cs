using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footballers_Obstacle : MonoBehaviour
{
    public bool active = false;
    public float grow;
    public float timeTillDestroy;

    private void Awake()
    {
        timeTillDestroy = PlayerPrefs.GetFloat("FootballersObstacleTimeAlive", timeTillDestroy);
    }
    public void Start()
    {
        if (active)
            Destroy(gameObject, timeTillDestroy);
    }
    private void Update()
    {
        if(active && transform.localScale.x < 0.5f)
        {
            transform.localScale = new Vector3(transform.localScale.x + grow * Time.deltaTime, transform.localScale.x + grow * Time.deltaTime, 1);
        }
    }
}
