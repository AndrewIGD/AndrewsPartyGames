using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawlers_weaponSpawn : MonoBehaviour
{
    public float timeBetweenSpawns;
    public float timeTillFirstSpawn;
    public GameObject[] weapons;
    public float mapHeight;
    public float minWidth;
    public float maxWidth;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnWeapon", timeTillFirstSpawn);
    }
    private void SpawnWeapon()
    {
        if (FindObjectsOfType<Brawlers_groundWeapon>().Length <= 2)
        {
            Debug.Log("spawned");
            GameObject weapon = Instantiate(weapons[Random.Range(1, 1000000) % weapons.Length]);
            weapon.transform.position = new Vector3(Random.Range(minWidth, maxWidth), mapHeight);
        }
        Invoke("SpawnWeapon", timeBetweenSpawns);
    }
}
