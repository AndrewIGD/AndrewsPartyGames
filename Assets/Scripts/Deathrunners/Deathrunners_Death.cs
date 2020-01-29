using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathrunners_Death : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<FencingMates_Player>() != null)
        {
            if (collision.gameObject.GetComponent<FencingMates_Player>().team != 2 || !gameObject.name.Contains("arrow"))
                StartCoroutine(collision.gameObject.GetComponent<FencingMates_Player>().DecreaseHp(3));
        }
    }
}
