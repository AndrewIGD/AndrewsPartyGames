using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footballers_Ball : MonoBehaviour
{
    public GameObject player;
    public bool bounce = false;
    float bounciness = 1f;
    private void Start()
    {
        if(bounce)
        {
            bounciness = PlayerPrefs.GetFloat("FootballersBallBounciness", bounciness);
            GetComponent<Rigidbody2D>().sharedMaterial.bounciness = bounciness;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Footballers_Player>() != null)
        {
            if (collision.gameObject.GetComponent<Footballers_Player>().ball == null)
            {
                if (player != null)
                {
                    Detach();
                }
                player = collision.gameObject;
                player.GetComponent<Footballers_Player>().ball = null;
                player.GetComponent<BoxCollider2D>().size = new Vector2(1, 4.261937f);
                player.GetComponent<BoxCollider2D>().offset = new Vector2(0, -1.630968f);
                Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), player.GetComponent<BoxCollider2D>(), true);
                transform.parent = collision.gameObject.transform;
                transform.localPosition = new Vector2(0, 0);
                collision.gameObject.GetComponent<Footballers_Player>().ball = gameObject;
            }
        }
    }

    public void Detach()
    {
        if (player != null)
        {
            player.GetComponent<Footballers_Player>().ball = null;
            player.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
            player.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
            Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), player.GetComponent<BoxCollider2D>(), false);
        }
    }

    private void Update()
    {
        foreach(Footballers_Poarta poarta in FindObjectsOfType<Footballers_Poarta>())
        {
            if(Vector2.Distance(transform.position, poarta.transform.position) < 5f && GetComponent<Rigidbody2D>().velocity.magnitude > 5f)
            {
                FindObjectOfType<Footballers_Crowd>().Play();
                break;
            }
        }
        if(transform.parent != null)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
