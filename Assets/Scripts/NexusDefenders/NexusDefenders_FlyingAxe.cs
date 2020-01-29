using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusDefenders_FlyingAxe : MonoBehaviour
{
    public float damage;
    public GameObject stationaryAxe;
    public GameObject player;
    public float face;
    public float team;
    public bool active = false;
    void Awake()
    {
        damage = PlayerPrefs.GetFloat("NexusAxeDamage", damage);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<NexusDefenders_Player>() != null)
        {
            if (collision.GetComponent<NexusDefenders_Player>().team != team)
            {
                collision.GetComponent<NexusDefenders_Player>().StartCoroutine(collision.GetComponent<NexusDefenders_Player>().DecreaseHp(damage));
                GameObject axe = Instantiate(stationaryAxe);
                axe.GetComponent<NexusDefenders_StationaryAxe>().active = true;
                axe.transform.position = transform.position;
                if (face == 0)
                {
                    axe.transform.eulerAngles = new Vector3(0, 0, -90);
                }
                else axe.transform.eulerAngles = new Vector3(0, 0, -180);
                axe.transform.parent = collision.gameObject.transform;
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.GetComponent<NexusDefenders_Wall>() != null)
        {
            if (collision.gameObject.GetComponent<NexusDefenders_Wall>().team != team)
            {
                collision.gameObject.GetComponent<NexusDefenders_Wall>().DecreaseHp(damage);
                GameObject axe = Instantiate(stationaryAxe);
                axe.GetComponent<NexusDefenders_StationaryAxe>().active = true;
                axe.transform.position = transform.position;
                if (face == 0)
                {
                    axe.transform.eulerAngles = new Vector3(0, 0, -90);
                }
                else axe.transform.eulerAngles = new Vector3(0, 0, -180);
                axe.transform.parent = collision.gameObject.transform;
                player.GetComponent<NexusDefenders_Player>().damageToObjectives += damage;
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.tag == "block")
        {
            GameObject axe = Instantiate(stationaryAxe);
            axe.GetComponent<NexusDefenders_StationaryAxe>().active = true;
            axe.transform.position = transform.position;
            if (face == 0)
            {
                axe.transform.eulerAngles = new Vector3(0, 0, -90);
            }
            else axe.transform.eulerAngles = new Vector3(0, 0, -180);
            axe.transform.parent = collision.gameObject.transform;
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        transform.Rotate(0, 0, 250 * Time.deltaTime);
    }
    public float timeBeforeDestroy;
    void Start()
    {
        if (active == false)
            GetComponent<Rigidbody2D>().gravityScale = 0;
        else Invoke("DestroyAxe", timeBeforeDestroy);
    }
    public void DestroyAxe()
    {
        Destroy(gameObject);
    }
}
