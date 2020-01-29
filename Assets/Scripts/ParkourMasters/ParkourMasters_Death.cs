using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourMasters_Death : MonoBehaviour
{
    public bool tilted;
    public bool upper;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ParkourMasters_Player>() != null)
        {
            collision.gameObject.GetComponent<ParkourMasters_Player>().TriggerDeath(Camera.main.gameObject, tilted, upper, true);
        }
    }
}
