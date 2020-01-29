using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawlers_death : MonoBehaviour
{
    public int direction;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Brawlers_player>() != null)
            collision.gameObject.GetComponent<Brawlers_player>().TriggerDeath(direction);
        if(direction == 0)
        {
            if (collision.gameObject.GetComponent<Brawlers_groundWeapon>() != null)
                Destroy(collision.gameObject);
        }
    }
}
