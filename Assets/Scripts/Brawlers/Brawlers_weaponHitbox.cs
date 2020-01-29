using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawlers_weaponHitbox : MonoBehaviour
{
    public float damage;
    public float launchX;
    public float launchY;
    public float yDamageMultiplier;
    public float xDamageMultiplier;
    public float stun;
    public List<GameObject> objects;
    public GameObject parent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Brawlers_playerHitbox>() != null && collision.gameObject != parent  && FindObjectOfType<PlayerData>())
        {
            if ((FindObjectOfType<PlayerData>().teams == true && collision.gameObject.GetComponent<Brawlers_playerHitbox>().parent.GetComponent<Brawlers_player>().team != parent.GetComponent<Brawlers_player>().team) || FindObjectOfType<PlayerData>().teams == false)
            {
                if (parent.GetComponent<Brawlers_player>().dair == true)
                {
                    parent.GetComponent<Brawlers_player>().DairHitPlayer();
                }
                bool ok = true;
                for(int obj=0;obj<objects.Count;obj++)
                {
                    if (collision.gameObject == objects[obj])
                    {
                        ok = false;
                    }
                }
                if (ok == true)
                {
                    collision.gameObject.GetComponent<Brawlers_playerHitbox>().parent.GetComponent<Brawlers_player>().IncreaseDamage(damage, stun);
                    collision.gameObject.GetComponent<Brawlers_playerHitbox>().parent.GetComponent<Brawlers_player>().Launch(launchX, launchY, stun, xDamageMultiplier, yDamageMultiplier, parent.GetComponent<Brawlers_player>().face);
                    objects.Add(collision.gameObject);
                }
            }
        }
        else if(collision.gameObject.GetComponent<Brawlers_playerHitbox>() != null && collision.gameObject != parent)
        {
 
                if (parent.GetComponent<Brawlers_player>().dair == true)
                {
                    parent.GetComponent<Brawlers_player>().DairHitPlayer();
                }
                bool ok = true;
            for (int obj = 0; obj < objects.Count; obj++)
            {
                if (collision.gameObject == objects[obj])
                {
                    ok = false;
                }
            }
            if (ok == true)
                {
                    collision.gameObject.GetComponent<Brawlers_playerHitbox>().parent.GetComponent<Brawlers_player>().IncreaseDamage(damage, stun);
                    collision.gameObject.GetComponent<Brawlers_playerHitbox>().parent.GetComponent<Brawlers_player>().Launch(launchX, launchY, stun, xDamageMultiplier, yDamageMultiplier, parent.GetComponent<Brawlers_player>().face);
                    objects.Add(collision.gameObject);
                }
            }
        
    }
    public void Stop()
    {
        objects.Clear();
        GetComponent<CircleCollider2D>().enabled = false;
        if(GetComponent<BoxCollider2D>() != null)
            GetComponent<BoxCollider2D>().enabled = false;
    }
}
