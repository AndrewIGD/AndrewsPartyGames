using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooter_BulletDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SpaceShooter_Bullet>() != null)
            Destroy(collision.gameObject);
    }
    float oldTime;
    float newTime;
    private void Update()
    {
        oldTime = newTime;
        newTime = Time.deltaTime;
        if (oldTime != newTime)
            Debug.Log(oldTime);
    }
}
