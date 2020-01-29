using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourMasters_Orb : MonoBehaviour
{
    public int type = 0;
    public Vector2 force;
    Color32 startColor;
    bool changeColor = false;
    float t;

    private void Awake()
    {
        force = new Vector2(0,PlayerPrefs.GetFloat("ParkourYellowOrbJump", force.y));
        if (type == 0)
            GetComponent<SpriteRenderer>().color = new Color32(255, 255, 0, 255);
        else if (type == 1)
            GetComponent<SpriteRenderer>().color = new Color32(0, 255, 255, 255);
        else if (type == 2)
            GetComponent<SpriteRenderer>().color = new Color32(0, 255, 0, 255);
        startColor = GetComponent<SpriteRenderer>().color;
        GetComponent<ParticleSystem>().startColor = GetComponent<SpriteRenderer>().color;
    }


    private void Update()
    {
        if (changeColor)
        {
            t += Time.deltaTime * 2.5f;
            if (t > 1)
                t = 1;
            transform.localScale = new Vector3(Mathf.Lerp(3, 2, t), Mathf.Lerp(3, 2, t), 1);
            GetComponent<SpriteRenderer>().color = Color32.Lerp(new Color32(255, 255, 255, 255), startColor, t);
            if(t >= 1)
            {
                t = 0;
                changeColor = false;
            }
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ParkourMasters_Player>() != null)
        {
            if(collision.gameObject.GetComponent<Rigidbody2D>().gravityScale == 0)
                collision.gameObject.GetComponent<ParkourMasters_Player>().StopDashing();
            GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            changeColor = true;
            transform.localScale = new Vector3(3, 3, 1);
            t = 0;
            if (type == 0)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = force;
            }
            if (type == 1)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = -1;
                collision.gameObject.transform.eulerAngles = new Vector3(180, 0, 0);
            }
            if(type == 2)
            {

                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                collision.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
}
