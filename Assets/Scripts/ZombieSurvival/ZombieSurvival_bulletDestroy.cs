using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSurvival_bulletDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ZombieSurvival_bullet>() != null)
            Destroy(collision.gameObject);
    }
}
