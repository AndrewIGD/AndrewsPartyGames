using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooter_botShoot : MonoBehaviour
{
    public GameObject parent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SpaceShooter_Player>() != null)
        {
            if (collision.GetComponent<SpaceShooter_Player>().team != parent.GetComponent<SpaceShooter_Player>().team)
            {
                parent.GetComponent<SpaceShooter_Player>().botShoot = true;
                parent.GetComponent<SpaceShooter_Player>().target = collision.gameObject;
            }
        }
        else if(collision.gameObject.GetComponent<SpaceShooter_luckyBlock>())
        {
            parent.GetComponent<SpaceShooter_Player>().botShoot = true;
            parent.GetComponent<SpaceShooter_Player>().target = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<SpaceShooter_Player>() != null)
        {
            if (collision.GetComponent<SpaceShooter_Player>().team != parent.GetComponent<SpaceShooter_Player>().team && collision.gameObject == parent.GetComponent<SpaceShooter_Player>().target)
            {
                parent.GetComponent<SpaceShooter_Player>().botShoot = false;
                parent.GetComponent<SpaceShooter_Player>().target = null;
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else if(collision.gameObject == parent.GetComponent<SpaceShooter_Player>().target)
        {
            parent.GetComponent<SpaceShooter_Player>().botShoot = false;
            parent.GetComponent<SpaceShooter_Player>().target = null;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    private void Update()
    {
        if(parent.GetComponent<SpaceShooter_Player>().target == null)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
