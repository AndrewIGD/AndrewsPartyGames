using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSurvival_IgnoreColl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LateStart", 4f);
    }
    void LateStart()
    {
        foreach(ZombieSurvival_player player in FindObjectsOfType<ZombieSurvival_player>())
        {
            if (player.bot)
                Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
            else Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
