using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSurvival_BotZombieCheck : MonoBehaviour
{
    public List<GameObject> zombies;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ZombieSurvival_zombie>() != null)
        {
            zombies.Add(collision.gameObject);
            Debug.Log("1");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ZombieSurvival_zombie>() != null)
        {
            zombies.Remove(collision.gameObject);
            Debug.Log("2");
        }
    }
}
