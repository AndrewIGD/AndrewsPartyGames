using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourMasters_Win : MonoBehaviour
{
    public List<GameObject> players;
    public float speed;
    public float decreaseModifier;
    bool decreasePortal = true;
    public AudioClip win;

    int points = 1000;
    int time = 99999;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<ParkourMasters_Player>() != null)
        {
            players.Add(collision.gameObject);
            collision.gameObject.GetComponent<ParkourMasters_Player>().tutorialAbleToMove = false;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
    float t;
    private void Update()
    {
        t += Time.deltaTime * 1.5f;
        if(decreasePortal)
        {
            transform.localScale = new Vector3(Mathf.Lerp(8.5f, 7f, t), Mathf.Lerp(6f, 5f, t), 1);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Lerp(7f, 8.5f, t), Mathf.Lerp(5f, 6f, t), 1);
        }
        if (t >= 1)
        { t = 0; decreasePortal = !decreasePortal; }
        foreach(GameObject player in players)
        {
            if (player != null)
            {
                if (player.transform.localScale.x < 0.05f && Vector2.Distance(transform.position, player.transform.position) < 0.05f)
                {
                    player.GetComponent<ParkourMasters_Player>().TriggerWin(transform.localEulerAngles.z, time--,score);
                    score = false;
                    AudioSource.PlayClipAtPoint(win, Camera.main.transform.position);
                }
                else
                {
                    player.GetComponent<Animator>().Play("fall");
                    player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, speed * Time.deltaTime);
                    player.transform.localScale = new Vector3(player.transform.localScale.x - decreaseModifier * Time.deltaTime, player.transform.localScale.x - decreaseModifier * Time.deltaTime, 1);
                }
            }
        }
    }
    bool score = true;
}
