using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusDefenders_JumpPadDetect : MonoBehaviour
{
    public GameObject jumpPad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<NexusDefenders_JumpPad>()!=null)
        {
            jumpPad = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<NexusDefenders_JumpPad>() != null)
        {
            jumpPad = null;
        }
    }
}
