using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooter_luckyBlock : MonoBehaviour
{
    public int type;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<SpaceShooter_Bullet>() != null)
        {
            if(type == 0)
                collision.gameObject.GetComponent<SpaceShooter_Bullet>().player.GetComponent<SpaceShooter_Player>().TripleShot();
            else if(type == 1)
                collision.gameObject.GetComponent<SpaceShooter_Bullet>().player.GetComponent<SpaceShooter_Player>().FastAttack();
            else if(type == 2)
                collision.gameObject.GetComponent<SpaceShooter_Bullet>().player.GetComponent<SpaceShooter_Player>().FastSpeed();
            else if (type == 3)
                collision.gameObject.GetComponent<SpaceShooter_Bullet>().player.GetComponent<SpaceShooter_Player>().RechargeUlt();
            else if (type == 4)
                collision.gameObject.GetComponent<SpaceShooter_Bullet>().player.GetComponent<SpaceShooter_Player>().ExtraHp();
            else if (type == 5)
                collision.gameObject.GetComponent<SpaceShooter_Bullet>().player.GetComponent<SpaceShooter_Player>().LaserUlt();
            Destroy(gameObject);
        }
    }
}
