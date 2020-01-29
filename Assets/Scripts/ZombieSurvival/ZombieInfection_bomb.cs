using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieInfection_bomb : MonoBehaviour
{
    public GameObject vfx;
    public AudioClip explode;
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        timeTillInfection = PlayerPrefs.GetFloat("ZombieInfectionBombTimeTillInfection", timeTillInfection);
        foreach (ZombieSurvival_player player in FindObjectsOfType<ZombieSurvival_player>())
        {
            if (player.bot)
            {
                    Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), player.GetComponent<CircleCollider2D>());
            }
            else
            {
               Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), player.GetComponent<BoxCollider2D>());
            }
        }
        foreach (ZombieSurvival_zombie player in FindObjectsOfType<ZombieSurvival_zombie>())
        {
            Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), player.GetComponent<BoxCollider2D>());
        }
        if(active)
            Invoke("Infect", timeTillInfection); 
    }
    float timeTillInfection = 1f;
    private void Infect()
    {
        GameObject explosion = Instantiate(vfx);
        explosion.transform.position = transform.position;
        foreach(ZombieSurvival_player player in FindObjectsOfType<ZombieSurvival_player>())
        {
            if(Vector2.Distance(gameObject.transform.position, player.gameObject.transform.position) <= 1.28f)
            {
                player.Infect();
            }
        }
        AudioSource.PlayClipAtPoint(explode, Camera.main.transform.position);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
