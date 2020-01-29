using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencingMates_slime : MonoBehaviour
{
    float slimeSlow = 4;
    private void Start()
    {
        slimeSlow = PlayerPrefs.GetFloat("FencingSlimeSlow", slimeSlow);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<FencingMates_Player>() != null)
        {
            collision.gameObject.GetComponent<FencingMates_Player>().speed = collision.gameObject.GetComponent<FencingMates_Player>().speed/slimeSlow;
            collision.gameObject.GetComponent<FencingMates_Player>().dashSpeed = collision.gameObject.GetComponent<FencingMates_Player>().dashSpeed/slimeSlow;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<FencingMates_Player>() != null)
        {
            
            collision.gameObject.GetComponent<FencingMates_Player>().speed = collision.gameObject.GetComponent<FencingMates_Player>().speed*slimeSlow;
            collision.gameObject.GetComponent<FencingMates_Player>().dashSpeed = collision.gameObject.GetComponent<FencingMates_Player>().dashSpeed*slimeSlow;
        }
    }
}
