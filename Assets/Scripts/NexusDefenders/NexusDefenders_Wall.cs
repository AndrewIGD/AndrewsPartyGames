using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusDefenders_Wall : MonoBehaviour
{
    public float health;
    public int team;
    public GameObject particles;
    public GameObject death;
    public bool nexus;
    public AudioClip hit;
    public AudioClip destroy;
    public SpriteRenderer[] damageLevelz;
    public float maxHp;
    public GameObject nexusDamageLevel;
    public bool changeSprite = true;
    private void Awake()
    {
        if (nexus == false)
            health = PlayerPrefs.GetFloat("NexusWallHealth", health);
        else health = PlayerPrefs.GetFloat("NexusNexusHealth", health);
        maxHp = health;
    }
    public void DecreaseHp(float damage)
    {
        health -= damage;
        if (changeSprite)
        {
            if (nexus == true)
                nexusDamageLevel.GetComponent<SpriteRenderer>().sprite = damageLevelz[3 - (int)(health / (maxHp / 4))].sprite;
            else GetComponent<SpriteRenderer>().sprite = damageLevelz[3 - (int)(health / (maxHp / 4))].sprite;
        }
        if (health > 0)
        {
            GameObject vfx = Instantiate(particles);
            vfx.transform.position = transform.position;
            Destroy(vfx, 1f);
            AudioSource.PlayClipAtPoint(hit, Camera.main.transform.position);
        }
        else
        {
            GameObject vfx = Instantiate(death);
            vfx.transform.position = transform.position;
            Destroy(vfx, 3f);
            if(nexus == true)
            {
                if (FindObjectOfType<PlayerData>().teams == false)
                {
                    NexusDefenders_Player[] players = FindObjectsOfType<NexusDefenders_Player>();
                    foreach (NexusDefenders_Player player in players)
                    {

                        FindObjectOfType<PlayerData>().playerObjectiveScores[player.playerNumber] = player.damageToObjectives;

                        
                    }
                }
                else
                {
                    if (FindObjectOfType<NexusDefenders_Timer>().time != 0)
                    {
                        if (team == 0)
                            FindObjectOfType<PlayerData>().AddRed();
                        else FindObjectOfType<PlayerData>().AddBlue();
                    }
                }
                FindObjectOfType<PlayerData>().InvokeScores();
            }
            AudioSource.PlayClipAtPoint(destroy, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
    private void Scores()
    {
        FindObjectOfType<PlayerData>().InvokeScores();
    }
    public void Start()
    {
        NexusDefenders_Player[] players = FindObjectsOfType<NexusDefenders_Player>();
        foreach(NexusDefenders_Player player in players)
        {
            if (player.team == team)
            {
                if(GetComponent<PolygonCollider2D>() != null)
                {
                    Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), player.GetComponent<BoxCollider2D>());
                }
                else Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
            }
        }
    }
}
