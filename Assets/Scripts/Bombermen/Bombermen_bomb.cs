using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombermen_bomb : MonoBehaviour
{
    public GameObject explosion;
    public AudioClip explosionSfx;
    public GameObject parent;
    public int team;
    private void Start()
    {
        Invoke("Explode", 2);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "block")
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<CircleCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
    private void Explode()
    {
        GameObject expl = Instantiate(explosion);
        expl.transform.position = transform.position;
        expl.GetComponentInChildren<Bombermen_explosion>().team = team;
        expl.GetComponentInChildren<Bombermen_explosion>().parent = parent;
        AudioSource.PlayClipAtPoint(explosionSfx, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
