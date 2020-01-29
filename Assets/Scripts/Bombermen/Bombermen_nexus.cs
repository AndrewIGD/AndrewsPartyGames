using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombermen_nexus : MonoBehaviour
{
    public GameObject death;
    public GameObject damage;
    public AudioClip damageSfx;
    public AudioClip deathSfx;
    public int team;
    public int health;
    public void Damage()
    {
        health--;
        if(health<=0)
        {
            GameObject deathVfx = Instantiate(death);
            deathVfx.transform.position = transform.position;
            AudioSource.PlayClipAtPoint(deathSfx, Camera.main.transform.position);
            if (FindObjectOfType<PlayerData>().teams == false)
            {
                Bombermen_player[] players = FindObjectsOfType<Bombermen_player>();
                foreach (Bombermen_player player in players)
                {

                    FindObjectOfType<PlayerData>().playerObjectiveScores[player.playerNumber] = player.score;


                }
            }
            else
            {
                if (team == 0)
                    FindObjectOfType<PlayerData>().redPoints++;
                else FindObjectOfType<PlayerData>().bluePoints++;
            }
            FindObjectOfType<PlayerData>().InvokeScores();
            Destroy(gameObject);
        }
        else
        {
            GameObject deathVfx = Instantiate(damage);
            deathVfx.transform.position = transform.position;
            AudioSource.PlayClipAtPoint(damageSfx, Camera.main.transform.position);
        }
    }
}
