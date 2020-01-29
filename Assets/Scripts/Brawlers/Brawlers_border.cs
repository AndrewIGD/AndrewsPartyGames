using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawlers_border : MonoBehaviour
{
    public GameObject player;
    public GameObject wall;
    public GameObject block;
    public GameObject type3border;
    public int type;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "block")
        {
            if (type == 0 || type == 1)
            {
                if (type3border.GetComponent<Brawlers_border>().block != collision.gameObject)
                {
                    if (type == 0)
                    {
                        player.GetComponent<Brawlers_player>().AllowLeft(false);
                        player.GetComponent<Brawlers_player>().touchingWall = true;
                    }
                    else if (type == 1)
                    {
                        player.GetComponent<Brawlers_player>().AllowRight(false);
                        player.GetComponent<Brawlers_player>().touchingWall = true;
                    }
                }
                wall = collision.gameObject;
            }
            else if (type == 2 && block == null)
            {
                if (player.GetComponent<Brawlers_player>().dair == true)
                {
                    player.GetComponent<Brawlers_player>().DairHitGround();
                }
                player.GetComponent<Brawlers_player>().IsGrounded(true);
                block = collision.gameObject;
            }
            else if (type == 3)
                block = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == wall)
        {
            wall = null;
            if (type == 0)
            {
                player.GetComponent<Brawlers_player>().AllowLeft(true);
                player.GetComponent<Brawlers_player>().touchingWall = false;
            }
            else if (type == 1)
            {
                player.GetComponent<Brawlers_player>().AllowRight(true);
                player.GetComponent<Brawlers_player>().touchingWall = false;
            }
        }
        else if (type == 2 && player.GetComponent<Rigidbody2D>().gravityScale > 0 && block == collision.gameObject)
        {
            player.GetComponent<Brawlers_player>().IsGrounded(false);
            block = null;
 
        }
        if (type == 3 && collision.gameObject == block)
            block = null;
    }
    private void Update()
    {
        if(wall == null)
        {
            if (type == 0)
            {
                player.GetComponent<Brawlers_player>().AllowLeft(true);
            }
            else if (type == 1)
            {
                player.GetComponent<Brawlers_player>().AllowRight(true);
            }
        }
    }
}
