using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombermen_tgrounded : MonoBehaviour
{
    public GameObject player;
    public GameObject block;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "block")
        {
            
                player.GetComponent<Bombermen_player>().IsGrounded(true);
                block = collision.gameObject;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (block == collision.gameObject)
        {
            player.GetComponent<Bombermen_player>().IsGrounded(false);
            block = null;
        }
    }
    private void Update()
    {
        if(block == null)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
