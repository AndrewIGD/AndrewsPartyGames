using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencingMate_CornerDetect : MonoBehaviour
{
    public GameObject target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<FencingMate_Corner>() != null)
        {
                target = collision.gameObject;
        }
    }
}
