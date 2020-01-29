using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KingOfHill_PointGenerator : MonoBehaviour
{
    public List<Brawlers_player> players;
    public Text timeText;
    public int time;
    private void Start()
    {
        time = PlayerPrefs.GetInt("KOTHTime", 60);
        timeText.text = (time / 60).ToString() + ":0" + (time % 60).ToString();
        Invoke("Time", 5.5f);
    }
    private void Time()
    {
        time--;
        if (time % 60 < 10)
            timeText.text = (time / 60).ToString() + ":0" + (time % 60).ToString();
        else timeText.text = (time / 60).ToString() + ":" + (time % 60).ToString();
        if (time <= 0)
        {
            if (FindObjectOfType<PlayerData>().teams == false)
            {
                Brawlers_player[] players = FindObjectsOfType<Brawlers_player>();
                    foreach (Brawlers_player player in players)
                    {
                        if (player.gameObject != gameObject)
                            FindObjectOfType<PlayerData>().playerObjectiveScores[player.playerNumber] = player.points;
                    }
                    FindObjectOfType<PlayerData>().InvokeScores();
                
            }
            else
            {
                bool ok = false;
                int bluePoints = 0;
                int redPoints = 0;
                Brawlers_player[] players = FindObjectsOfType<Brawlers_player>();
                foreach (Brawlers_player player in players)
                {
                    if (player.team == 0)
                        bluePoints += player.points;
                    else redPoints += player.points;
                }
                    if (redPoints > bluePoints)
                        FindObjectOfType<PlayerData>().AddRed();
                    else FindObjectOfType<PlayerData>().AddBlue();
                    FindObjectOfType<PlayerData>().InvokeScores();
                

            }
        }
        else Invoke("Time", 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Brawlers_player>() != null)
            players.Add(collision.gameObject.GetComponent<Brawlers_player>());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Brawlers_player>() != null)
            players.Remove(collision.gameObject.GetComponent<Brawlers_player>());
    }
    private void Update()
    {
        foreach(Brawlers_player player in players)
        {
            if(player != null)
                player.points++;
        }
    }
}
