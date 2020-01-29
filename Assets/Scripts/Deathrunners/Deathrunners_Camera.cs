using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Deathrunners_Camera : MonoBehaviour
{
    public List<FencingMates_Player> players;
    public bool moveX = false;
    public bool moveY = true;
    public GameObject spawn;
    public bool detected = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Detect", 5.5f);
    }
  
    private void Detect()
    {
        players = new List<FencingMates_Player>(FindObjectsOfType<FencingMates_Player>());
        List<FencingMates_Player> newPlayers = new List<FencingMates_Player>(players);
        bool okk = true;
        int player = Random.Range(0, (players.Count - 1) * 10000) % (players.Count);
        if (detected)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].team == 2)
                {
                    player = i;
                    break;
                }
            }
        }
            if (detected == false)
            {
                players[player].team = 2;
                players[player].teamColor.color = new Color32(0, 70, 0, 255);
                players[player].transform.position = spawn.transform.position;
                if (players[player].bot == true)
                {
                    players[player].botPlayStyle = "trapper";
                    GameObject waypointObj = GameObject.Find("Levers");
                    players[player].waypoints.Clear();
                    foreach (Transform child in waypointObj.transform)
                    {
                        players[player].waypoints.Add(child.gameObject);
                    }
                }
            foreach (FencingMates_Player play in FindObjectsOfType<FencingMates_Player>())
                play.StartMoving();
        }
        
        bool ok = false;
        foreach(FencingMates_Player playerr in players)
        {
            if (playerr.bot == true)
            {
                foreach (Transform child in transform)
                {
                    Physics2D.IgnoreCollision(child.GetComponent<BoxCollider2D>(), playerr.GetComponent<BoxCollider2D>(), true);
                }
                newPlayers.Remove(playerr);
            }
            else if(playerr.team !=2) ok = true;
        }
        if (ok == false)
            newPlayers.Add(players[player]);
        if (ok == false)
        {
            GetComponent<Camera>().orthographicSize = 9;
            moveX = true;
            moveY = true;
        }
        players = newPlayers;
        detected = true;
    }
    // Update is called once per frame
    void Update()
    {

        if (players.Count != 0 && detected)
        {
            float posX = 0;
            float posY = 0;
            foreach (FencingMates_Player player in players)
            {
                if (player == null)
                {
                    Detect();
                    break;
                }
                if (player.transform.position.x < Camera.main.transform.position.x - 8.5f)
                    player.transform.position = new Vector2(Camera.main.transform.position.x - 8.5f, player.transform.position.y);
                if (player.transform.position.x > Camera.main.transform.position.x + 8.5f)
                    player.transform.position = new Vector2(Camera.main.transform.position.x + 8.5f, player.transform.position.y);
                if (player.transform.position.y < Camera.main.transform.position.y - 4.5f)
                    player.transform.position = new Vector2(player.transform.position.x, Camera.main.transform.position.y - 4.5f);
                if (player.transform.position.y > Camera.main.transform.position.y + 4.5f)
                    player.transform.position = new Vector2(player.transform.position.x, Camera.main.transform.position.y + 4.5f);
                if (moveX)
                    posX += player.transform.position.x;
                if (moveY)
                    posY += player.transform.position.y;
            }
            if (moveX)
                posX = posX / players.Count;
            else posX = transform.position.x;
            if (moveY)
                posY = posY / players.Count;
            else posY = transform.position.y;
            transform.position = new Vector3(posX, posY, -10);
        }
        else if (players.Count == 0 && detected)
        {
                Detect();
        }
    }
}
