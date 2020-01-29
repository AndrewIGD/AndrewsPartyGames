using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooter_laser : MonoBehaviour
{
    int times = 0;
    public int team;
    float damage=25f;
    // Start is called before the first frame update
    void Start()
    {
        damage = PlayerPrefs.GetFloat("LaserDamage", damage);
        Invoke("ResetCol", 0.25f);
    }
    void ResetCol()
    {
        if(times < 3)
        {
            times++;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
            Invoke("ResetCol", 0.25f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<SpaceShooter_Player>() != null)
        {
            if(collision.gameObject.GetComponent<SpaceShooter_Player>().team != team)
                collision.gameObject.GetComponent<SpaceShooter_Player>().DecreaseHp(damage);
        }
    }

}
