using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSurvival_countdown : MonoBehaviour
{
    public AudioClip countdown;
    public int seconds = 11;
    public int zombies;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Countdown",1f);
    }
    public void Countdown()
    {

            AudioSource.PlayClipAtPoint(countdown, Camera.main.transform.position);
            Invoke("Infect", 10f);
        
    }
    private void Infect()
    {
        ZombieSurvival_player[] players = FindObjectsOfType<ZombieSurvival_player>();
        players[Random.Range(0, 1000000) % players.Length].GetComponent<ZombieSurvival_player>().Infect();
    }
    public void RemoveZom()
    {
        zombies--;
        if (zombies == 0)
        {
            if (FindObjectOfType<PlayerData>().teams == true)
            {
                if (FindObjectOfType<ZombieSurvival_damageCounter>().redDmg > FindObjectOfType<ZombieSurvival_damageCounter>().blueDmg)
                    FindObjectOfType<PlayerData>().AddRed();
                else FindObjectOfType<PlayerData>().AddBlue();
            }
            else
            {
                foreach(ZombieSurvival_player player in FindObjectsOfType<ZombieSurvival_player>())
                {
                    FindObjectOfType<PlayerData>().playerObjectiveScores[player.playerNumber] = player.damageDealt;
                }
            }
            FindObjectOfType<PlayerData>().InvokeScores();
        }
    }
}
