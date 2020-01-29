using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooter_PlayerCounter : MonoBehaviour
{
    public int teamOnePlayers = 0;
    public int teamTwoPlayers = 0;
    public void AddPlayerToTeamOne()
    {
        teamOnePlayers++;
    }
    public void AddPlayerToTeamTwo()
    {
        teamTwoPlayers++;
    }
    public void RemovePlayerFromTeamOne()
    {
        teamOnePlayers--;
        if (teamOnePlayers <= 0 && FindObjectOfType<PlayerData>().teams == false)
        {
            SpaceShooter_Player[] players = FindObjectsOfType<SpaceShooter_Player>();
            int blues = 0;
            for(int i=0;i<players.Length;i++)
            {
                if (players[i].team == 1)
                    blues++;
            }
            if(blues<=1)
                Invoke("StopGame", 1.5f);
        }
    }
    public void RemovePlayerFromTeamTwo()
    {
        teamTwoPlayers--;
        if (teamTwoPlayers <= 0 && FindObjectOfType<PlayerData>().teams == false)
        {
            SpaceShooter_Player[] players = FindObjectsOfType<SpaceShooter_Player>();
            int red = 0;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].team == 0)
                    red++;
            }
            if (red <= 1)
                Invoke("StopGame", 1.5f);
        }
    }
    bool alreadyInvoked = false;
    private void StopGame()
    {

    }
    private void Update()
    {

    }
}
