using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencingBot_PlayerDetect : MonoBehaviour
{
    public GameObject target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<FencingMates_Player>() != null)
        {
            if(collision.gameObject.GetComponent<FencingMates_Player>().team != transform.parent.GetComponent<FencingMates_Player>().team)
                if (target == null)
                    target = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == target)
        {
            target = null;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
