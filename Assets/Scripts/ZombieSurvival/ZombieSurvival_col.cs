using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ZombieSurvival_col : MonoBehaviour
{
    public GameObject parent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<ZombieSurvival_player>() != null)
        {
            if (parent.GetComponent<ZombieSurvival_zombie>().bot == true)
            {
                foreach(ZombieSurvival_zombie zom in FindObjectsOfType<ZombieSurvival_zombie>())
                {
                    if(zom.bot == true && zom.target == parent.GetComponent<ZombieSurvival_zombie>().target && zom.gameObject != parent)
                    {
                        zom.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                        ZombieSurvival_player[] players2 = FindObjectsOfType<ZombieSurvival_player>();
                        if (players2.Length != 0)
                            zom.target = players2[Random.Range(0, 10000000) % players2.Length].gameObject;
                        zom.GetComponent<AIDestinationSetter>().target = zom.target.transform;
                    }
                }
                if(parent.GetComponent<ZombieSurvival_zombie>().infection)
                    collision.gameObject.GetComponent<ZombieSurvival_player>().Infect();
                else collision.gameObject.GetComponent<ZombieSurvival_player>().Death();
                parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                ZombieSurvival_player[] players = FindObjectsOfType<ZombieSurvival_player>();
                if (players.Length != 0)
                    parent.GetComponent<ZombieSurvival_zombie>().target = players[Random.Range(0, 10000000) % players.Length].gameObject;
                parent.GetComponent<AIDestinationSetter>().target = parent.GetComponent<ZombieSurvival_zombie>().target.transform;
            }
        }
        else if (collision.gameObject.GetComponent<ZombieSurvival_bullet>() != null)
        {
            parent.GetComponent<ZombieSurvival_zombie>().Damage(collision.gameObject.GetComponent<ZombieSurvival_bullet>().damage);
            Destroy(collision.gameObject);
        }
    }
    private void Start()
    {
        if (parent.GetComponent<ZombieSurvival_zombie>().bot == false)
            Destroy(gameObject);
    }
}
