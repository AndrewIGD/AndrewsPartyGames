using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusDefenders_JumpPad : MonoBehaviour
{
    public float jumpHeight;
    float jumpHeightAmplifier = 1f;
    private void Start()
    {
        jumpHeightAmplifier = PlayerPrefs.GetFloat("NexusJumpPadJumpAmplifier", jumpHeightAmplifier);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "block" && collision.GetComponent<Rigidbody2D>() != null && collision.GetComponentInChildren<Bombermen_explosion>() == null)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, jumpHeight*jumpHeightAmplifier);
        }
    }
}
