using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourMasters_PlayerCount : MonoBehaviour
{
    public int playerCount = 0;
    public void RemovePlayer()
    {
        playerCount--;
        if (playerCount <= 0)
            Invoke("StopGame", 2f);
    }
    private void StopGame()
    {
        
    }
}
