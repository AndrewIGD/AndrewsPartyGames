using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusDefenders_SwordCollider : MonoBehaviour
{
    public GameObject originalPlayer;
    public float damage;
    public List<GameObject> players;
    public float team;
    public bool active = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            if (collision.gameObject.GetComponent<NexusDefenders_Player>() != null)
            {
                bool ok = true;
                foreach (GameObject player in players)
                {
                    if (collision.gameObject == player)
                    {
                        ok = false;
                    }
                }
                if (ok == true && collision.gameObject.GetComponent<NexusDefenders_Player>().team != team)
                {
                    players.Add(collision.gameObject);
                    collision.gameObject.GetComponent<NexusDefenders_Player>().StartCoroutine(collision.gameObject.GetComponent<NexusDefenders_Player>().DecreaseHp(damage));
                }
            }
            else if (collision.gameObject.GetComponent<NexusDefenders_Wall>() != null)
            {
                if (collision.gameObject.GetComponent<NexusDefenders_Wall>().team != team)
                {
                    collision.gameObject.GetComponent<NexusDefenders_Wall>().DecreaseHp(damage);
                    originalPlayer.GetComponent<NexusDefenders_Player>().damageToObjectives += damage;
                }
            }
        }
    }
    void Start()
    {
        damage = PlayerPrefs.GetFloat("NexusSwordDamage", damage);
        team = originalPlayer.GetComponent<NexusDefenders_Player>().team;
    }
    public IEnumerator ResetPlayers()
    {
        while (players.Count != 0)
        {
            players.Remove(players[0]);
        }
        yield break;
    }
}
