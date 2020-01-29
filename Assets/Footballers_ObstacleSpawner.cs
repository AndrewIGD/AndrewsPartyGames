using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footballers_ObstacleSpawner : MonoBehaviour
{
    public GameObject obstacle;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float timeBetweenSpawns;
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenSpawns = PlayerPrefs.GetFloat("FootballersObstacleTimeBetweenSpawns", timeBetweenSpawns);
        Invoke("SpawnObstacle", 5.5f + timeBetweenSpawns);
    }
    void SpawnObstacle()
    {
        GameObject obs = Instantiate(obstacle);
        obs.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        obs.GetComponent<Footballers_Obstacle>().active = true;
        Invoke("SpawnObstacle", timeBetweenSpawns);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
