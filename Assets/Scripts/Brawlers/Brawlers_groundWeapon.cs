using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawlers_groundWeapon : MonoBehaviour
{
    public GameObject weapon;
    public bool death = false;
    bool gave = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (death == false && gave == false)
        {
            if (collision.gameObject.GetComponent<Brawlers_playerHitbox>() != null)
            {
                if (collision.gameObject.GetComponent<Brawlers_playerHitbox>().parent.GetComponent<Brawlers_player>().weapon == null && collision.gameObject.GetComponent<Brawlers_playerHitbox>().parent.GetComponent<Brawlers_player>().attacking == false)
                {
                    gave = true;
                    collision.gameObject.GetComponent<Brawlers_playerHitbox>().parent.GetComponent<Brawlers_player>().GiveWeapon(weapon);
                    Destroy(gameObject);
                }
            }
        }
    }
    private void Awake()
    {
        foreach (Brawlers_player player in FindObjectsOfType<Brawlers_player>())
            Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), player.GetComponent<BoxCollider2D>());
        foreach (Brawlers_groundWeapon weapon in FindObjectsOfType<Brawlers_groundWeapon>())
            Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), weapon.GetComponent<PolygonCollider2D>());
    }
    float t = 0;
    private void Update()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = true;
        if (death)
        {
            t += Time.deltaTime / 3;
            GetComponent<SpriteRenderer>().color = Color32.Lerp(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 0), t);
            if (t >= 1)
                Destroy(gameObject);
        }
    }
}
