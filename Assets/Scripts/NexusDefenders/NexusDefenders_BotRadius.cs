using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusDefenders_BotRadius : MonoBehaviour
{
    public GameObject wall;
    public GameObject enemy;
    public GameObject nexus;
    public GameObject parent;
    private void Update()
    {
        if (enemy != null)
        {
            if (enemy.activeInHierarchy == false)
                enemy = null;
        }
        if(enemy != null)
            if (Physics2D.IsTouching(GetComponent<BoxCollider2D>(), enemy.GetComponent<BoxCollider2D>()) == false)
                enemy = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<NexusDefenders_Player>() != null)
        {
            if(collision.gameObject.GetComponent<NexusDefenders_Player>().team != parent.GetComponent<NexusDefenders_Player>().team && enemy==null)
            {
                enemy = collision.gameObject;
            }
        }
        else if (collision.gameObject.GetComponent<NexusDefenders_Wall>() != null)
        {
            if(collision.gameObject.GetComponent<NexusDefenders_Wall>().nexus == false && wall == null && collision.gameObject.GetComponent<NexusDefenders_Wall>().team != parent.GetComponent<NexusDefenders_Player>().team)
            {
                wall = collision.gameObject;
            }
            else if (collision.gameObject.GetComponent<NexusDefenders_Wall>().nexus == true && wall == null && collision.gameObject.GetComponent<NexusDefenders_Wall>().team != parent.GetComponent<NexusDefenders_Player>().team)
            {
                nexus = collision.gameObject;
            }
        }
        else if(collision.gameObject.GetComponent<NexusDefenders_Turret>() != null && Random.Range(1,100000)%4==0)
        {
            wall = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == enemy)
        {
            enemy = null;
            wall = null;
            nexus = null;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (collision.gameObject == wall)
        {
            enemy = null;
            wall = null;
            nexus = null;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (collision.gameObject == nexus)
        {
            enemy = null;
            wall = null;
            nexus = null;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
