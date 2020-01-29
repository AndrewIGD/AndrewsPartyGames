using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_Teams : MonoBehaviour
{
    public bool teams = false;
    public Text buttonText;
    public void Teams()
    {
        if(teams == false)
        {
            buttonText.text = "Teams : ON";
            teams = true;
            FindObjectOfType<PlayerData>().teams = true;
            Lobby_Player[] players = FindObjectsOfType<Lobby_Player>();
            foreach(Lobby_Player player in players)
            {
                player.teams = true;
                player.teamNumber.gameObject.SetActive(true);
                player.botTeam.SetActive(true);
            }
        }
        else
        {
            buttonText.text = "Teams : OFF";
            teams = false;
            FindObjectOfType<PlayerData>().teams = false;
            Lobby_Player[] players = FindObjectsOfType<Lobby_Player>();
            foreach (Lobby_Player player in players)
            {
                player.teams = false;
                if (player.selected == 2)
                    player.SelectStanga();
                player.teamNumber.gameObject.SetActive(false);
                player.botTeam.SetActive(false);
            }
        }
    }
}
