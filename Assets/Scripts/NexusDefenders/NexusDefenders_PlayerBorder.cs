using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusDefenders_PlayerBorder : MonoBehaviour
{
    public GameObject player;
    GameObject wall;
    GameObject block;
    public int type;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "block" && collision.gameObject.name != "notAWall")
        {
            if (type == 0 || type == 1)
            {
                if (type == 0)
                    player.GetComponent<NexusDefenders_Player>().AllowLeft(false);
                else if (type == 1)
                    player.GetComponent<NexusDefenders_Player>().AllowRight(false);
                wall = collision.gameObject;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == wall)
        {
            wall = null;
            if (type == 0)
                player.GetComponent<NexusDefenders_Player>().AllowLeft(true);
            else if (type == 1)
                player.GetComponent<NexusDefenders_Player>().AllowRight(true);
        }
    }
}
