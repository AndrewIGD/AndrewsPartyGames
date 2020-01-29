using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusDefenders_TurretPlayerDetect : MonoBehaviour
{
    public GameObject target;
    public int team;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<NexusDefenders_Player>() != null && target == null)
        {
            if(collision.gameObject.GetComponent<NexusDefenders_Player>().team != team)
                target = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(target == collision.gameObject)
        {
            target = null;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    void Update()
    {
        if(target == null || target.activeInHierarchy == false)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
