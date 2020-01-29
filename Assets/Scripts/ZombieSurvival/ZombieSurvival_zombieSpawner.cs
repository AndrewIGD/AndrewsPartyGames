using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ZombieSurvival_zombieSpawner : MonoBehaviour
{
    public int zombieNum;
    public float zombieSpeed;
    public float zombieHealth;
    public int zombieIncrease;
    public float speedIncrease;
    public float healthIncrease;
    public bool doneSpawning;
    public List<GameObject> zombies;
    int zombieIndex = 0;
    public GameObject zombie;
    public bool spawn = false;
    // Update is called once per frame
    private void Start()
    {
        zombieNum = (int)PlayerPrefs.GetFloat("ZombieSurvivalStartingZombieNumber", zombieNum);
        zombieSpeed = PlayerPrefs.GetFloat("ZombieSurvivalStartingZombieSpeed", zombieSpeed);
        zombieHealth = PlayerPrefs.GetFloat("ZombieSurvivalStartingZombieHealth", zombieHealth);
        zombieIncrease = (int)PlayerPrefs.GetFloat("ZombieSurvivalZombieIncrement", zombieIncrease);
        speedIncrease = PlayerPrefs.GetFloat("ZombieSurvivalSpeedIncrease", speedIncrease);
        healthIncrease = PlayerPrefs.GetFloat("ZombieSurvivalHealthIncrease", healthIncrease);
        Invoke("StartSpawning", 6f);
    }
    void StartSpawning()
    {
        spawn = true;
    }
    void Update()
    {
        if (spawn == true)
        {
            if (zombies.Count == 0 && doneSpawning == true)
            {
                doneSpawning = false;
                zombieNum += zombieIncrease;
                zombieSpeed += speedIncrease;
                zombieHealth += healthIncrease;
                SpawnZombies();
            }
        }
    }
    public void SpawnZombies()
    {
        if(zombieIndex == zombieNum)
        {
            doneSpawning = true;
            zombieIndex = 0;
            CancelInvoke("SpawnZombies");
        }
        else
        {
            zombieIndex++;
            GameObject zom = Instantiate(zombie);
            zom.transform.position = transform.position;
            zom.GetComponent<ZombieSurvival_zombie>().enabled = true;
            zom.GetComponent<ZombieSurvival_zombie>().health = zombieHealth;
            zom.GetComponent<ZombieSurvival_zombie>().speed = zombieSpeed;
            zom.GetComponent<ZombieSurvival_zombie>().parent = gameObject;
            zom.GetComponent<ZombieSurvival_zombie>().team.gameObject.SetActive(false);
            zom.GetComponent<AIPath>().enabled = true;
            zombies.Add(zom);
            Invoke("SpawnZombies", 0.25f);
        }
        
    }
}
