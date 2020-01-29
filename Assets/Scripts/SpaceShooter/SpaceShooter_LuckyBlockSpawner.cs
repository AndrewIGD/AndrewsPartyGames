using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooter_LuckyBlockSpawner : MonoBehaviour
{
    public GameObject[] luckyBlocks;
    public float timeBetweenSpawns;

    private void Start()
    {
        timeBetweenSpawns = PlayerPrefs.GetFloat("PowerupTimeBetweenSpawns", timeBetweenSpawns);
        Invoke("Spawn", 1+timeBetweenSpawns);
    }

    void Spawn()
    {
        GameObject luckyBlock = Instantiate(luckyBlocks[Random.Range(1, 100000) % luckyBlocks.Length]);
        luckyBlock.transform.position = transform.position;
        luckyBlock.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);
        Destroy(luckyBlock, 10);
        Invoke("Spawn", timeBetweenSpawns);
    }
}
