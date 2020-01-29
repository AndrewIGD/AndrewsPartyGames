using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourMasters_Mine : MonoBehaviour
{
    public GameObject player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ParkourMasters_Player>() != null && collision.gameObject != player)
        {
            collision.gameObject.GetComponent<ParkourMasters_Player>().TriggerDeath(Camera.main.gameObject, false, false, false);
            Destroy(gameObject);
        }
    }
}
