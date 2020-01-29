using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawlers_portal : MonoBehaviour
{
    public GameObject targetPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Brawlers_player>()!=null)
        {
            collision.transform.position = targetPos.transform.position;
        }
    }
}
