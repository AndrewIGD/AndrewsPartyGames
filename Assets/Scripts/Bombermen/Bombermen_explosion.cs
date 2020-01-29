using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombermen_explosion : MonoBehaviour
{
    public int team;
    public GameObject parent;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "block")
        {
            parent.GetComponent<Bombermen_player>().score += 1;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.GetComponent<Bombermen_player>() != null)
        {
            if (team != collision.gameObject.GetComponent<Bombermen_player>().team)
            {
                collision.gameObject.GetComponent<Bombermen_player>().Death();
                parent.GetComponent<Bombermen_player>().score += 3;
            }
            else
            {
                var dir = (collision.transform.position - transform.position);
                float wearoff = 1 - (dir.magnitude / 2);
                collision.GetComponent<Rigidbody2D>().velocity = dir.normalized * force * wearoff;
                collision.GetComponent<Bombermen_player>().ableToMove = false;
                collision.GetComponent<Bombermen_player>().AbleToMove();
            }
        }
        else if (collision.gameObject.GetComponent<Bombermen_nexus>() != null)
        {
            if (collision.gameObject.GetComponent<Bombermen_nexus>().team != team)
            {
                collision.gameObject.GetComponent<Bombermen_nexus>().Damage();
                parent.GetComponent<Bombermen_player>().score += 5;
            }
        }
    }
}
