using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_BotTeam : MonoBehaviour
{
    public int team = 0;
    public Lobby_AddBot botObj;
    public void ChangeTeam()
    {
        if (team == 0)
        {
            team = 1;
            GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            botObj.playerScript.teamNumber.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            team = 0;
            GetComponent<Image>().color = new Color32(0, 191, 255, 255);
            botObj.playerScript.teamNumber.color = new Color32(0, 191, 255, 255);
        }
        botObj.playerScript.teamNumber.text = "Team " + (team + 1);
        botObj.playerScript.team = team;
        FindObjectOfType<PlayerData>().playerTeams[botObj.playerScript.playerNumber] = team;
    }
}
