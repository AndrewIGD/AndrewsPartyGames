using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footballers_PlayerRoles : MonoBehaviour
{
    public List<GameObject> playerss;
    public List<GameObject> players;
    public List<GameObject> red;
    public List<GameObject> blue;
    private void Start()
    {
        Invoke("LateStart", 1f);
    }
    void LateStart()
    {
        playerss = FindObjectOfType<SpawnPlayers>().players;
        foreach (GameObject player in playerss)
        {
            if(player.GetComponent<Footballers_Player>().bot == true)
            {
                players.Add(player);
            }
        }
        foreach(GameObject player in players)
        {
            if (player.GetComponent<Footballers_Player>().team == 0)
                blue.Add(player);
            else red.Add(player);
        }
        if (blue.Count == 1)
        {
            blue[0].GetComponent<Footballers_Player>().botPlayStyle = "single";
        }
        else if (blue.Count <= 3)
        {
            for (int i = 0; i < blue.Count; i++)
            {
                if (i == 0)
                    blue[0].GetComponent<Footballers_Player>().botPlayStyle = "goalkeeper";
                else blue[i].GetComponent<Footballers_Player>().botPlayStyle = "single";
            }
        }
        else
        {
            for (int i = 0; i < blue.Count; i++)
            {
                if (blue[i].GetComponent<Footballers_Player>().difficulty != "easy" && blue[i].GetComponent<Footballers_Player>().difficulty != "normal")
                {
                    blue[i].GetComponent<Footballers_Player>().botPlayStyle = "goalkeeper";
                    blue.Remove(blue[i]);
                    break;
                }
            }
            if (blue.Count % 3 == 0)
            {
                for (int i = 0; i < blue.Count; i++)
                {
                    if (i < blue.Count / 3)
                        blue[i].GetComponent<Footballers_Player>().botPlayStyle = "bot";
                    else if (i < blue.Count / 3 * 2)
                        blue[i].GetComponent<Footballers_Player>().botPlayStyle = "middle";
                    else blue[i].GetComponent<Footballers_Player>().botPlayStyle = "top";
                }
            }
            else if (blue.Count % 3 == 1)
            {
                for (int i = 0; i < blue.Count; i++)
                {
                    if (i < blue.Count / 3 + 1)
                        blue[i].GetComponent<Footballers_Player>().botPlayStyle = "bot";
                    else if (i < blue.Count / 3 * 2 + 1)
                        blue[i].GetComponent<Footballers_Player>().botPlayStyle = "middle";
                    else blue[i].GetComponent<Footballers_Player>().botPlayStyle = "top";
                }
            }
            else
            {
                for (int i = 0; i < blue.Count; i++)
                {
                    if (i < blue.Count / 3 + 1)
                        blue[i].GetComponent<Footballers_Player>().botPlayStyle = "bot";
                    else if (i < blue.Count / 3 * 2 + 2)
                        blue[i].GetComponent<Footballers_Player>().botPlayStyle = "middle";
                    else blue[i].GetComponent<Footballers_Player>().botPlayStyle = "top";
                }
            }
        }
        if (red.Count == 1)
        {
            red[0].GetComponent<Footballers_Player>().botPlayStyle = "single";
        }
        else if (red.Count <= 3)
        {
            for (int i = 0; i < red.Count; i++)
            {
                if (i == 0)
                    red[0].GetComponent<Footballers_Player>().botPlayStyle = "goalkeeper";
                else red[i].GetComponent<Footballers_Player>().botPlayStyle = "single";
            }
        }
        else
        {
            for (int i = 0; i < red.Count; i++)
            {
                if (red[i].GetComponent<Footballers_Player>().difficulty != "easy" && red[i].GetComponent<Footballers_Player>().difficulty != "normal")
                {
                    red[i].GetComponent<Footballers_Player>().botPlayStyle = "goalkeeper";
                    red.Remove(red[i]);
                    break;
                }
            }
            if (red.Count%3==0)
            {
                for(int i=0;i<red.Count;i++)
                {
                    if(i<red.Count/3)
                        red[i].GetComponent<Footballers_Player>().botPlayStyle = "bot";
                    else if(i<red.Count/3*2)
                        red[i].GetComponent<Footballers_Player>().botPlayStyle = "middle";
                    else red[i].GetComponent<Footballers_Player>().botPlayStyle = "top";
                }
            }
            else if(red.Count%3 == 1)
            {
                for (int i = 0; i < red.Count; i++)
                {
                    if (i < red.Count / 3+1)
                        red[i].GetComponent<Footballers_Player>().botPlayStyle = "bot";
                    else if (i < red.Count / 3 * 2 + 1)
                        red[i].GetComponent<Footballers_Player>().botPlayStyle = "middle";
                    else red[i].GetComponent<Footballers_Player>().botPlayStyle = "top";
                }
            }
            else
            {
                for (int i = 0; i < red.Count; i++)
                {
                    if (i < red.Count / 3 + 1)
                        red[i].GetComponent<Footballers_Player>().botPlayStyle = "bot";
                    else if (i < red.Count / 3 * 2 + 2)
                        red[i].GetComponent<Footballers_Player>().botPlayStyle = "middle";
                    else red[i].GetComponent<Footballers_Player>().botPlayStyle = "top";
                }
            }
        }

    }
}
