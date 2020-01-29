using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSurvival_wallDetect : MonoBehaviour
{
    public GameObject wall;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
            wall = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == wall)
        {
            wall = null;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
