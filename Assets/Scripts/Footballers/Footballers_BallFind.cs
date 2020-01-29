using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footballers_BallFind : MonoBehaviour
{
    public GameObject ball;
    public GameObject parent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Footballers_Ball>() != null)
        {
            if(collision.gameObject.GetComponent<Footballers_Ball>().player == null || collision.gameObject.GetComponent<Footballers_Ball>().player.GetComponent<Footballers_Player>().team != parent.GetComponent<Footballers_Player>().team)
                ball = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == ball)
            ball = null;
    }
}
