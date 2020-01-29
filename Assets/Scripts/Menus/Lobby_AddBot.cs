using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby_AddBot : MonoBehaviour
{
    public GameObject botObj;
    public GameObject botText;
    public Lobby_Player playerScript;
    public void AddBot()
    {
        botObj.SetActive(false);
        botText.SetActive(true);
        playerScript.bot = true;
        FindObjectOfType<PlayerData>().bots[playerScript.playerNumber] = true;
        FindObjectOfType<PlayerData>().players.Add(playerScript.playerNumber);
    }
    private void Awake()
    {
        playerScript.botObj = botObj;
        playerScript.botText = botText;
    }
}
