using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusDefenders_bullet : MonoBehaviour
{
    public float damage;
    public bool hitPlayers = false;
    public int team;
    public bool hitWalls = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<NexusDefenders_Wall>() != null && hitWalls)
        {
            collision.GetComponent<NexusDefenders_Wall>().DecreaseHp(damage);
            Destroy(gameObject);
        }
        else if (collision.GetComponent<NexusDefenders_Player>() != null && hitPlayers)
        {
            if(collision.GetComponent<NexusDefenders_Player>().team != team)
            {
                collision.GetComponent<NexusDefenders_Player>().StartCoroutine(collision.GetComponent<NexusDefenders_Player>().DecreaseHp(damage));
                Destroy(gameObject);
            }
        }
    }
}
