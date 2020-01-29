using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieInfection_trap : MonoBehaviour
{
    public Sprite bearTrapClosed;
    bool used = false;
    public AudioClip snap;
    float trapDuration = 2f;
    private void Awake()
    {
        trapDuration = PlayerPrefs.GetFloat("ZombieInfectionTrapDuration", trapDuration);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<ZombieSurvival_zombie>() != null)
        {
            if(used == false)
            {
                used = true;
                GetComponent<SpriteRenderer>().sprite = bearTrapClosed;
                collision.gameObject.GetComponent<ZombieSurvival_zombie>().Trap();
                Destroy(gameObject, trapDuration);
                AudioSource.PlayClipAtPoint(snap, Camera.main.transform.position);
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                collision.gameObject.transform.position = transform.position;
            }
        }
    }
}
