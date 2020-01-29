using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencingMates_Ice : MonoBehaviour
{
    float boost = 2f;
    private void Start()
    {
        boost = PlayerPrefs.GetFloat("FencingIceBoost", boost);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<FencingMates_Player>() != null)
        {
            collision.gameObject.GetComponent<FencingMates_Player>().freezing = true;
            collision.gameObject.GetComponent<FencingMates_Player>().speed = collision.gameObject.GetComponent<FencingMates_Player>().speed * boost;
            collision.gameObject.GetComponent<FencingMates_Player>().dashSpeed = collision.gameObject.GetComponent<FencingMates_Player>().dashSpeed * boost;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<FencingMates_Player>() != null)
        {
            collision.gameObject.GetComponent<FencingMates_Player>().freezing = false;
            collision.gameObject.GetComponent<FencingMates_Player>().speed = collision.gameObject.GetComponent<FencingMates_Player>().speed / boost;
            collision.gameObject.GetComponent<FencingMates_Player>().dashSpeed = collision.gameObject.GetComponent<FencingMates_Player>().dashSpeed / boost;
        }
    }
}
