using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControllerDisconnect : MonoBehaviour
{
    public Text text;
    bool canShow = true;
    public bool[] controllers;
    bool disconnect1=true;
    private void Update()
    {
  
    }
    public void Disconnect(int playerNum)
    {
        if (controllers[playerNum])
        {
            Debug.Log("stuff");
            text.text = "Player " + playerNum + " controller disconnected";

            if(disconnect1)
                GetComponent<Animator>().Play("Disconnect");
            else GetComponent<Animator>().Play("Disconnect2");

            disconnect1 = !disconnect1;
            controllers[playerNum] = false;
        }
        
    }
    public void Reconnect(int playerNum)
    {
        if (controllers[playerNum] == false)
        {
            Debug.Log("stuff");
            text.text = "Player " + playerNum + " controller reconnected";

            if (disconnect1)
                GetComponent<Animator>().Play("Disconnect");
            else GetComponent<Animator>().Play("Disconnect2");
            disconnect1 = !disconnect1;
            controllers[playerNum] = true;
        }    
    }
    void CanShow()
    {
        canShow = true;
    }
    private void Start()
    {
        controllers = new bool[19];
        for (int i = 0; i <= 18; i++)
            controllers[i] = true;
    }
}
