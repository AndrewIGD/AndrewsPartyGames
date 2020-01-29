using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathrunners_ChangeCameraMovement : MonoBehaviour
{
    public bool moveX;
    public bool moveY;
    public bool activateKilling;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<FencingMates_Player>() != null)
        {
            if (collision.gameObject.GetComponent<FencingMates_Player>().bot == false && collision.gameObject.GetComponent<FencingMates_Player>().team != 2)
            {
                Camera.main.GetComponent<Deathrunners_Camera>().moveX = moveX;
                Camera.main.GetComponent<Deathrunners_Camera>().moveY = moveY;
            }
            if(activateKilling)
                collision.gameObject.GetComponent<FencingMates_Player>().killPlayers = true;
        }
    }
}
