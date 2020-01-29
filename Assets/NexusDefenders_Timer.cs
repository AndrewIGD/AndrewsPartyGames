using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NexusDefenders_Timer : MonoBehaviour
{
    public Text timeText;
    public int time;
    private void Start()
    {
        timeText = GetComponent<Text>();
        time = PlayerPrefs.GetInt("NexusDefendersTime", 180);
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
            bool nexus1 = false;
            bool nexus2 = false;
            foreach(NexusDefenders_Wall wall in FindObjectsOfType<NexusDefenders_Wall>())
            {
                if(wall.nexus)
                {
                    if (wall.team == 0)
                        nexus1 = true;
                    else nexus2 = true;
                }
            }
            if (FindObjectOfType<PlayerData>().teams&&nexus1&&nexus2)
            {
                float damage1 = 0;
                float damage2 = 0;
                foreach(GameObject player in FindObjectOfType<SpawnPlayers>().players)
                {
                    if (player.GetComponent<NexusDefenders_Player>().team == 0)
                        damage1 += player.GetComponent<NexusDefenders_Player>().damageToObjectives;
                    else damage2 += player.GetComponent<NexusDefenders_Player>().damageToObjectives;
                }
                if(damage1!=damage2)
                {
                    if (damage1 > damage2)
                        FindObjectOfType<PlayerData>().AddBlue();
                    else FindObjectOfType<PlayerData>().AddRed();
                    
                }
            }
            FindObjectOfType<PlayerData>().InvokeScores();
        }
        else Invoke("Time", 1f);
    }
}
