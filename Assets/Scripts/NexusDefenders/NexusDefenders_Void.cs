using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusDefenders_Void : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<NexusDefenders_Player>() != null)
        {
            collision.gameObject.GetComponent<NexusDefenders_Player>().StartCoroutine(collision.gameObject.GetComponent<NexusDefenders_Player>().DecreaseHp(999999));
        }
    }
}
