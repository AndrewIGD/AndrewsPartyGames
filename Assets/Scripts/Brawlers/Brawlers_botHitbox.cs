using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawlers_botHitbox : MonoBehaviour
{
    public GameObject parent;
    public string attackName;
    public bool isHalebard = false;
    public bool isSword = false;
    public bool isFlying = false;
    public GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.gameObject.GetComponent<Brawlers_player>() != null && Random.Range(1, 100000) % parent.GetComponent<Brawlers_player>().chanceToAttack == 0 && parent.GetComponent<Brawlers_player>().attacking == false)
        {
            if (collision.gameObject.GetComponent<Brawlers_player>().team != parent.GetComponent<Brawlers_player>().team && parent.GetComponent<Brawlers_player>().tutorialAbleToMove && parent.GetComponent<Brawlers_player>().touchingWall == false && collision.gameObject != parent && parent.GetComponent<Brawlers_player>().flying == false)
            {

                player = collision.gameObject;
                if ((isFlying && parent.GetComponent<Brawlers_player>().grounded == false && parent.GetComponent<Brawlers_player>().difficulty == "expert") || (isFlying == false && parent.GetComponent<Brawlers_player>().grounded == true))
                {
                    if (parent.GetComponent<Brawlers_player>().weapon != null)
                    {
                        if ((parent.GetComponent<Brawlers_player>().weapon.tag == "halberd" && isHalebard) || (parent.GetComponent<Brawlers_player>().weapon.tag == "sword" && isSword))
                            if (!parent.GetComponent<Brawlers_player>().attacking && Random.Range(1, 100000) % parent.GetComponent<Brawlers_player>().chanceToAttack == 0)
                            {
                                parent.GetComponent<Animator>().Play(attackName);
                            }
                    }
                    else if (isHalebard == false && isSword == false)
                        if (!parent.GetComponent<Brawlers_player>().attacking)
                        {
                            parent.GetComponent<Animator>().Play(attackName);
                        }
                }


            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            player = null;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    private void Update()
    {
        if (player != null && Random.Range(1, 100000) % parent.GetComponent<Brawlers_player>().chanceToAttack == 0 && parent.GetComponent<Brawlers_player>().attacking == false)
        {
            if (parent.GetComponent<Brawlers_player>().touchingWall == false && parent.GetComponent<Brawlers_player>().tutorialAbleToMove && parent.GetComponent<Brawlers_player>().attacking == false && parent.GetComponent<Brawlers_player>().flying == false)
            {
                if ((isFlying && parent.GetComponent<Brawlers_player>().grounded == false && parent.GetComponent<Brawlers_player>().difficulty == "expert") || (isFlying == false && parent.GetComponent<Brawlers_player>().grounded == true))
                {
                    if (parent.GetComponent<Brawlers_player>().weapon != null)
                    {
                        if ((parent.GetComponent<Brawlers_player>().weapon.tag == "halberd" && isHalebard) || (parent.GetComponent<Brawlers_player>().weapon.tag == "sword" && isSword))
                            if (!parent.GetComponent<Brawlers_player>().attacking && Random.Range(1, 100000) % parent.GetComponent<Brawlers_player>().chanceToAttack == 0)
                            {
                                parent.GetComponent<Animator>().Play(attackName);
                            }
                    }
                    else if (isHalebard == false && isSword == false)
                        if (!parent.GetComponent<Brawlers_player>().attacking && Random.Range(1, 100000) % parent.GetComponent<Brawlers_player>().chanceToAttack == 0)
                        {
                            parent.GetComponent<Animator>().Play(attackName);
                        }
                }
            }
        }
    }
}
