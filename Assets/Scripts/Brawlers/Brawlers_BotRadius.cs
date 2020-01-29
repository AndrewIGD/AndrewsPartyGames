using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawlers_BotRadius : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Brawlers_player>() != null)
            collision.gameObject.GetComponent<Brawlers_player>().outOfMapRadius = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Brawlers_player>() != null)
            collision.gameObject.GetComponent<Brawlers_player>().outOfMapRadius = true;
    }
}
