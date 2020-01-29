using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathrunners_BotRadius : MonoBehaviour
{
    public GameObject block;
    public bool ignoreCollision = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Deathrunners_Death>() != null && ignoreCollision == false)
            block = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (block == collision.gameObject)
            block = null;
    }
}
