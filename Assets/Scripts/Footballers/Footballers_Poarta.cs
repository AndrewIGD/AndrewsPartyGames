using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footballers_Poarta : MonoBehaviour
{
    public List<GameObject> players;
    public GameObject ballSpawn;
    public AudioClip goal;
    public AudioClip start;
    public int team;
    GameObject ball;
    public AudioClip goalSfx;
    public GameObject goalVfx;
    
    private void Start()
    {
        ball = FindObjectOfType<Footballers_Ball>().gameObject;
        players = FindObjectOfType<SpawnPlayers>().players;
        Invoke("Begin", 5.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Footballers_Ball>() != null && FindObjectOfType<Footballers_Player>().tutorialAbleToMove == true)
        {
            AudioSource.PlayClipAtPoint(goalSfx, Camera.main.transform.position, 1f);
            GameObject goalexpl = Instantiate(goalVfx);
            ball = collision.gameObject;
            goalexpl.transform.position = ball.transform.position;
            ball.GetComponent<Footballers_Ball>().Detach();
            if (team == 1)
                FindObjectOfType<Footballers_goals>().BlueGoal();
            else FindObjectOfType<Footballers_goals>().RedGoal();
            AudioSource.PlayClipAtPoint(goal, Camera.main.transform.position);
            Footballers_Player[] playerss = FindObjectsOfType<Footballers_Player>();
            foreach (Footballers_Player player in playerss)
            {
                if (Vector2.Distance(ball.transform.position, player.transform.position) < 8)
                {
                    var dir = (player.transform.position - ball.transform.position);
                    float wearoff = 4 - (dir.magnitude / 2);
                    player.GetComponent<Rigidbody2D>().velocity = dir.normalized * 3.5f * wearoff;
                }
                else player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                player.tutorialAbleToMove = false;
                if (player.botPlayStyle == "goalkeeper")
                    player.waypoint = null;
            }
            foreach(Footballers_Ball ball in FindObjectsOfType<Footballers_Ball>())
            {
                if (ball.gameObject != collision.gameObject && FindObjectOfType<Footballers_BallMachine>().ball != ball.gameObject)
                    Destroy(ball.gameObject);
            }
            foreach (Footballers_Obstacle obs in FindObjectsOfType<Footballers_Obstacle>())
            {
                if(obs.active)
                    Destroy(obs.gameObject);
            }
            if (FindObjectOfType<Footballers_BallMachine>() != null)
            {
                FindObjectOfType<Footballers_BallMachine>().CancelInvoke("SpawnBall");
                FindObjectOfType<Footballers_BallMachine>().Invoke("SpawnBall", FindObjectOfType<Footballers_BallMachine>().timeBetweenSpawns + 8.5f);
            }
            if (FindObjectOfType<Footballers_ObstacleSpawner>() != null)
            {
                FindObjectOfType<Footballers_ObstacleSpawner>().CancelInvoke("SpawnObstacle");
                FindObjectOfType<Footballers_ObstacleSpawner>().Invoke("SpawnObstacle", FindObjectOfType<Footballers_ObstacleSpawner>().timeBetweenSpawns + 8.5f);
            }
            ball.SetActive(false);
            Invoke("Respawn", 6f);
            Invoke("Begin", 8.5f);
        }

    }
    private void Update()
    {
        if (FindObjectOfType<Footballers_Player>().tutorialAbleToMove == false && ball != null)
        {
            ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            ball.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
    }
    private void Begin()
    {

        Footballers_Player[] players = FindObjectsOfType<Footballers_Player>();

        foreach(Footballers_Player player in players)
        {
            player.tutorialAbleToMove = true;
            if (player.botPlayStyle == "goalkeeper")
                player.LateStart();
        }
        AudioSource.PlayClipAtPoint(start, Camera.main.transform.position);
    }
    private void Respawn()
    {
        ball.SetActive(true);
        ball.transform.position = ballSpawn.transform.position;
        if (ball.GetComponent<Footballers_Ball>().player != null)
            ball.GetComponent<Footballers_Ball>().player.GetComponent<Footballers_Player>().ball = null;
        ball.GetComponent<Footballers_Ball>().player = null;
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        ball.GetComponent<Rigidbody2D>().angularVelocity = 0;
        ball.transform.eulerAngles = new Vector3(0, 0, 0);
        ball.transform.parent = null;
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null)
            {
                players[i].transform.position = players[i].GetComponent<Footballers_Player>().spawnpoint.transform.position;
                players[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                players[i].transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
}
