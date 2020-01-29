using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombermen_death : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bombermen_player>() != null)
            collision.gameObject.GetComponent<Bombermen_player>().Death();
    }
}
