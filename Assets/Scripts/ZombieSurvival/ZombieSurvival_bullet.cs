using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSurvival_bullet : MonoBehaviour
{
    public float damage;
    public GameObject parent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
            Destroy(gameObject);
        if(collision.gameObject.GetComponent<ZombieSurvival_zombie>() != null)
        {
            if (parent != null)
            {
                parent.GetComponent<ZombieSurvival_player>().damageDealt += damage;
                if (FindObjectOfType<PlayerData>().teams == true)
                {
                    if (parent.GetComponent<ZombieSurvival_player>().team == 0)
                    {
                        FindObjectOfType<ZombieSurvival_damageCounter>().blueDmg += damage;
                    }
                    else FindObjectOfType<ZombieSurvival_damageCounter>().redDmg += damage;
                }
            }
            collision.gameObject.GetComponent<ZombieSurvival_zombie>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
