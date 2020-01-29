using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby_BotCancel : MonoBehaviour
{
    public Lobby_AddBot botObj;
    public void CancelBot()
    {
        botObj.botObj.SetActive(true);
        botObj.botText.SetActive(false);
        botObj.playerScript.bot = false;
        FindObjectOfType<PlayerData>().bots[botObj.playerScript.playerNumber] = false;
        FindObjectOfType<PlayerData>().players.Remove(botObj.playerScript.playerNumber);
    }
}
