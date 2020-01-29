using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourMasters_PlayerBorder : MonoBehaviour
{
    public GameObject player;
    GameObject wall;
    GameObject block;
    public int type;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "block")
        {
            if (type == 0 || type == 1)
            {
                if (type == 0)
                    player.GetComponent<ParkourMasters_Player>().AllowLeft(false);
                else if (type == 1)
                    player.GetComponent<ParkourMasters_Player>().AllowRight(false);
                wall = collision.gameObject;
            }
            else if (type == 2)
            {
                player.GetComponent<ParkourMasters_Player>().IsGrounded(true);
                block = collision.gameObject;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == wall)
        {
            wall = null;
            if (type == 0)
                player.GetComponent<ParkourMasters_Player>().AllowLeft(true);
            else if (type == 1)
                player.GetComponent<ParkourMasters_Player>().AllowRight(true);
        }
        else if (type == 2 && block == collision.gameObject)
        {
            player.GetComponent<ParkourMasters_Player>().IsGrounded(false);
            block = null;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
