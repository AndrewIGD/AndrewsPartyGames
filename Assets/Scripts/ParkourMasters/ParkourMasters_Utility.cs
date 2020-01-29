using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourMasters_Utility : MonoBehaviour
{
    public int type;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ParkourMasters_Player>() != null && collision.gameObject.GetComponent<ParkourMasters_Player>().action2Type == 0)
        {
            collision.gameObject.GetComponent<ParkourMasters_Player>().action2Type = type;
            collision.gameObject.GetComponent<ParkourMasters_Player>().pickup.Play();
            Destroy(gameObject);
        }
    }
}
