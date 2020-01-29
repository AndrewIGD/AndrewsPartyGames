using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FencingMates_PlayerCounter : MonoBehaviour
{
    public int playerCount = 0;
    public void RemovePlayer()
    {
        playerCount--;

    }
    public void InvokeStop()
    {
        Invoke("StopGame", 1.5f);
    }
    private void StopGame()
    {
   
    }
}
