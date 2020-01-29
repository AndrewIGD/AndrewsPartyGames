using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencingMates_SwordCollider : MonoBehaviour
{
    public GameObject originalPlayer;
    public int damage;
    public List<GameObject> players;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<FencingMates_Player>() != null)
        {
            if (FindObjectOfType<Deathrunners_Camera>() != null)
            {
                if (originalPlayer.GetComponent<FencingMates_Player>().team != 2)
                {
                    if (collision.gameObject.GetComponent<FencingMates_Player>().team == 2)
                    {
                        bool ok = true;
                        foreach (GameObject player in players)
                        {
                            if (collision.gameObject == player)
                            {
                                ok = false;
                            }
                        }
                        if (ok == true)
                        {
                            players.Add(collision.gameObject);
                            if(collision.gameObject.GetComponent<FencingMates_Player>().lives-damage <= 0)
                            {
                                if (FindObjectOfType<PlayerData>().teams)
                                {
                                    if (originalPlayer.GetComponent<FencingMates_Player>().originalTeam == 0)
                                        FindObjectOfType<PlayerData>().AddBlue();
                                    else FindObjectOfType<PlayerData>().AddRed();
                                }
                                else
                                {
                                    originalPlayer.GetComponent<FencingMates_Player>().time += 999;
                                    foreach(FencingMates_Player player in FindObjectsOfType<FencingMates_Player>())
                                    {
                                        FindObjectOfType<PlayerData>().playerObjectiveScores[player.playerNumber] = player.time;
                                    }
                                }
                                FindObjectOfType<PlayerData>().InvokeScores();
                            }
                            collision.gameObject.GetComponent<FencingMates_Player>().StartCoroutine(collision.gameObject.GetComponent<FencingMates_Player>().DecreaseHp(damage));
                        }
                    }
                }
                else
                {
                        bool ok = true;
                        foreach (GameObject player in players)
                        {
                            if (collision.gameObject == player)
                            {
                                ok = false;
                            }
                        }
                        if (ok == true)
                        {
                            players.Add(collision.gameObject);
                            collision.gameObject.GetComponent<FencingMates_Player>().StartCoroutine(collision.gameObject.GetComponent<FencingMates_Player>().DecreaseHp(damage));
                    }
                    
                }
            }
            else
            {
                bool ok = true;
                foreach (GameObject player in players)
                {
                    if (collision.gameObject == player)
                    {
                        ok = false;
                    }
                }
                if (ok == true)
                {
                    players.Add(collision.gameObject);
                    collision.gameObject.GetComponent<FencingMates_Player>().StartCoroutine(collision.gameObject.GetComponent<FencingMates_Player>().DecreaseHp(damage));

                }
            }
        }
        else if (collision.gameObject.GetComponent<Deathrunners_Trap>() != null)
        {
            collision.gameObject.GetComponent<Deathrunners_Trap>().ExecuteTrap();
        }
            
    }
    private void Start()
    {
        if(originalPlayer.GetComponent<FencingMates_Player>().unlistedDeaths.Length == 0)
            damage = (int)PlayerPrefs.GetFloat("FencingPlayerDamage", damage);
        else damage = (int)PlayerPrefs.GetFloat("DeathrunnersPlayerDamage", damage);
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
