using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooter_Void : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<SpaceShooter_Player>() != null)
            collision.gameObject.GetComponent<SpaceShooter_Player>().DecreaseHp(999999);
    }
}
