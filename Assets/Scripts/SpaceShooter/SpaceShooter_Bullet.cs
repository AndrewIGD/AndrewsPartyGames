using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooter_Bullet : MonoBehaviour
{
    float damage;
    int team;
    public GameObject player;
    public GameObject bulletVfx;

    //Functions
    public void GetDamageInfo(float damageInfo)
    {
        damage = damageInfo;
    }

    public void GetTeamInfo(int teamInfo)
    {
        team = teamInfo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<SpaceShooter_Player>() != null)
        {
            if (collision.gameObject.GetComponent<SpaceShooter_Player>().team != team && player != null)
            {
                if(collision.gameObject.GetComponent<SpaceShooter_Player>().health <= damage && player.GetComponent<SpaceShooter_Player>().training)
                    player.GetComponent<SpaceShooter_Player>().Reward(1f);
                collision.gameObject.GetComponent<SpaceShooter_Player>().DecreaseHp(damage);
                player.GetComponent<SpaceShooter_Player>().damageDealt += damage;
                player.GetComponent<SpaceShooter_Player>().Reward(damage / 100 * 2.5f);
                if(collision.gameObject.GetComponent<SpaceShooter_Player>().health > 0)
                {   
                    GameObject vfx = Instantiate(bulletVfx);
                    if(GetComponent<BoxCollider2D>() != null)
                        vfx.transform.position = Physics2D.OverlapBox(transform.position, GetComponent<BoxCollider2D>().size, 0f).ClosestPoint(transform.position);
                    else vfx.transform.position = Physics2D.OverlapCircle(transform.position, GetComponent<CircleCollider2D>().radius).ClosestPoint(transform.position);
                    Destroy(vfx, 1f);
                }
                Destroy(gameObject);
            }
        }
    }
}
