using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public List<GameObject> blueSpawns;
    public List<GameObject> redSpawns;
    public List<GameObject> players;
    public List<GameObject> Spawns;
    public int type = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<PlayerData>().teams == false)
        {
            bool blue = true;
            int redIndex = 0;
            int blueIndex = 0;
            if (players[0].GetComponent<SpaceShooter_Player>() != null)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    for (int j = i + 1; j < players.Count - 1; j++)
                    {
                        if (players[i].GetComponent<SpaceShooter_Player>().playerNumber < players[j].GetComponent<SpaceShooter_Player>().playerNumber)
                        {
                            GameObject aux = players[i];
                            players[i] = players[j];
                            players[j] = aux;
                        }
                    }
                }
            }
            else if (players[0].GetComponent<ParkourMasters_Player>() != null)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    for (int j = i + 1; j < players.Count - 1; j++)
                    {
                        if (players[i].GetComponent<ParkourMasters_Player>().playerNumber < players[j].GetComponent<ParkourMasters_Player>().playerNumber)
                        {
                            GameObject aux = players[i];
                            players[i] = players[j];
                            players[j] = aux;
                        }
                    }
                }
            }
            else if (players[0].GetComponent<NexusDefenders_Player>() != null)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    for (int j = i + 1; j < players.Count - 1; j++)
                    {
                        if (players[i].GetComponent<NexusDefenders_Player>().playerNumber < players[j].GetComponent<NexusDefenders_Player>().playerNumber)
                        {
                            GameObject aux = players[i];
                            players[i] = players[j];
                            players[j] = aux;
                        }
                    }
                }
            }
            else if (players[0].GetComponent<FencingMates_Player>() != null)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    for (int j = i + 1; j < players.Count - 1; j++)
                    {
                        if (players[i].GetComponent<FencingMates_Player>().playerNumber < players[j].GetComponent<FencingMates_Player>().playerNumber)
                        {
                            GameObject aux = players[i];
                            players[i] = players[j];
                            players[j] = aux;
                        }
                    }
                }
            }
            else if (players[0].GetComponent<Footballers_Player>() != null)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    for (int j = i + 1; j < players.Count - 1; j++)
                    {
                        if (players[i].GetComponent<Footballers_Player>().playerNumber < players[j].GetComponent<Footballers_Player>().playerNumber)
                        {
                            GameObject aux = players[i];
                            players[i] = players[j];
                            players[j] = aux;
                        }
                    }
                }
            }
            else if (players[0].GetComponent<ZombieSurvival_player>() != null)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    for (int j = i + 1; j < players.Count - 1; j++)
                    {
                        if (players[i].GetComponent<ZombieSurvival_player>().playerNumber < players[j].GetComponent<ZombieSurvival_player>().playerNumber)
                        {
                            GameObject aux = players[i];
                            players[i] = players[j];
                            players[j] = aux;
                        }
                    }
                }
            }
            else if (players[0].GetComponent<Brawlers_player>() != null)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    for (int j = i + 1; j < players.Count - 1; j++)
                    {
                        if (players[i].GetComponent<Brawlers_player>().playerNumber < players[j].GetComponent<Brawlers_player>().playerNumber)
                        {
                            GameObject aux = players[i];
                            players[i] = players[j];
                            players[j] = aux;
                        }
                    }
                }
            }
            foreach (GameObject player in players)
            {
                if (player.GetComponent<SpaceShooter_Player>() != null)
                {
                    if (blue)
                    {
                        player.transform.position = blueSpawns[blueIndex].transform.position;
                        blueIndex++;
                        player.GetComponent<SpaceShooter_Player>().ChangeTeam(0);
                    }
                    else
                    {
                        player.transform.position = redSpawns[redIndex].transform.position;
                        redIndex++;
                        player.GetComponent<SpaceShooter_Player>().ChangeTeam(1);
                    }
                    blue = !blue;
                    if (FindObjectOfType<PlayerData>().teams == true)
                        player.GetComponent<SpaceShooter_Player>().Withteams();
                    else player.GetComponent<SpaceShooter_Player>().Noteams();
                }
                else if (player.GetComponent<ParkourMasters_Player>() != null)
                {
                    if (FindObjectOfType<PlayerData>().teams == true)
                        player.GetComponent<ParkourMasters_Player>().Withteams();
                    else player.GetComponent<ParkourMasters_Player>().Noteams();
                }
                else if (player.GetComponent<NexusDefenders_Player>() != null)
                {
                    if (blue)
                    {
                        player.transform.position = blueSpawns[blueIndex].transform.position;
                        if(blueIndex != 0)
                        {
                            player.GetComponent<NexusDefenders_Player>().DeactivateParts();
                            player.GetComponent<NexusDefenders_Player>().Invoke("RespawnParts", 5.5f);
                        }
                        blueIndex++;
                        player.GetComponent<NexusDefenders_Player>().ChangeTeam(0);
                    }
                    else
                    {
                        player.transform.position = redSpawns[redIndex].transform.position;
                        if (redIndex != 0)
                        {
                            player.GetComponent<NexusDefenders_Player>().DeactivateParts();
                            player.GetComponent<NexusDefenders_Player>().Invoke("RespawnParts", 5.5f);
                        }
                        redIndex++;
                        player.GetComponent<NexusDefenders_Player>().ChangeTeam(1);
                    }
                    blue = !blue;
                    if (FindObjectOfType<PlayerData>().teams == true)
                        player.GetComponent<NexusDefenders_Player>().Withteams();
                    else player.GetComponent<NexusDefenders_Player>().Noteams();
                }
                else if (player.GetComponent<FencingMates_Player>() != null)
                {
                    if (FindObjectOfType<PlayerData>().teams == true)
                    {
                        if (player.GetComponent<FencingMates_Player>().team == 0)
                        {
                            player.transform.position = blueSpawns[blueIndex].transform.position;
                            blueIndex++;
                        }
                        else
                        {
                            player.transform.position = redSpawns[redIndex].transform.position;
                            redIndex++;
                        }
                    }
                    if (FindObjectOfType<PlayerData>().teams == true)
                    {
                        player.GetComponent<FencingMates_Player>().Withteams();
                    }
                    else player.GetComponent<FencingMates_Player>().Noteams();
                }
                else if (player.GetComponent<Footballers_Player>() != null)
                {
                        if (blue)
                        {
                            player.transform.position = blueSpawns[blueIndex].transform.position;
                            blueIndex++;
                        player.GetComponent<Footballers_Player>().ChangeTeam(0);
                    }
                        else
                        {
                            player.transform.position = redSpawns[redIndex].transform.position;
                            redIndex++;
                        player.GetComponent<Footballers_Player>().ChangeTeam(1);
                    }

                    blue = !blue;
                    if (FindObjectOfType<PlayerData>().teams == true)
                        player.GetComponent<Footballers_Player>().Withteams();
                    else player.GetComponent<Footballers_Player>().Noteams();
                }
                else if (player.GetComponent<ZombieSurvival_player>() != null)
                {
                    if (player.GetComponent<ZombieSurvival_player>().playerNumber % 2 == 1)
                    {
                        player.transform.position = blueSpawns[blueIndex].transform.position;
                        blueIndex++;
                    }
                    else
                    {
                        player.transform.position = redSpawns[redIndex].transform.position;
                        redIndex++;
                    }
                    if (FindObjectOfType<PlayerData>().teams == true)
                        player.GetComponent<ZombieSurvival_player>().Withteams();
                    else player.GetComponent<ZombieSurvival_player>().Noteams();
                }
                else if (player.GetComponent<Brawlers_player>() != null)
                {
                    if (player.GetComponent<Brawlers_player>().playerNumber % 2 == 1)
                    {
                        player.transform.position = blueSpawns[blueIndex].transform.position;
                        blueIndex++;
                    }
                    else
                    {
                        player.transform.position = redSpawns[redIndex].transform.position;
                        redIndex++;
                    }
                    if (FindObjectOfType<PlayerData>().teams == true)
                        player.GetComponent<Brawlers_player>().Withteams();
                    else player.GetComponent<Brawlers_player>().Noteams();
                }
                else if (player.GetComponent<Bombermen_player>() != null)
                {
                    if (player.GetComponent<Bombermen_player>().playerNumber % 2 == 1)
                    {
                        player.transform.position = blueSpawns[blueIndex].transform.position;
                        blueIndex++;
                    }
                    else
                    {
                        player.transform.position = redSpawns[redIndex].transform.position;
                        redIndex++;
                    }
                    if (FindObjectOfType<PlayerData>().teams == true)
                        player.GetComponent<Bombermen_player>().Withteams();
                    else player.GetComponent<Bombermen_player>().Noteams();
                }
            }
        }
        else
        {
            int redIndex = 0;
            int blueIndex = 0;
            foreach (GameObject player in players)
            {
                if (player.GetComponent<SpaceShooter_Player>() != null)
                {
                    if (player.GetComponent<SpaceShooter_Player>().team == 0)
                    {
                        player.transform.position = blueSpawns[blueIndex].transform.position;
                        blueIndex++;
                    }
                    else
                    {
                        player.transform.position = redSpawns[redIndex].transform.position;
                        redIndex++;
                    }
                    if (FindObjectOfType<PlayerData>().teams == true)
                        player.GetComponent<SpaceShooter_Player>().Withteams();
                    else player.GetComponent<SpaceShooter_Player>().Noteams();
                }
                else if (player.GetComponent<ParkourMasters_Player>() != null)
                {
                    if (player.GetComponent<ParkourMasters_Player>().team == 0)
                    {
                        player.transform.position = blueSpawns[blueIndex].transform.position;
                        blueIndex++;
                    }
                    else
                    {
                        player.transform.position = redSpawns[redIndex].transform.position;
                        redIndex++;
                    }
                    if (FindObjectOfType<PlayerData>().teams == true)
                        player.GetComponent<ParkourMasters_Player>().Withteams();
                    else player.GetComponent<ParkourMasters_Player>().Noteams();
                }
                else if (player.GetComponent<NexusDefenders_Player>() != null)
                {
                    if (player.GetComponent<NexusDefenders_Player>().team == 0)
                    {
                        player.transform.position = blueSpawns[blueIndex].transform.position;
                        if (blueIndex != 0)
                        {
                            player.GetComponent<NexusDefenders_Player>().DeactivateParts();
                            player.GetComponent<NexusDefenders_Player>().Invoke("RespawnParts", 5.5f);
                        }
                        blueIndex++;
                    }
                    else
                    {
                        player.transform.position = redSpawns[redIndex].transform.position;
                        if (redIndex != 0)
                        {
                            player.GetComponent<NexusDefenders_Player>().DeactivateParts();
                            player.GetComponent<NexusDefenders_Player>().Invoke("RespawnParts", 5.5f);
                        }
                        redIndex++;
                    }
                    if (FindObjectOfType<PlayerData>().teams == true)
                        player.GetComponent<NexusDefenders_Player>().Withteams();
                    else player.GetComponent<NexusDefenders_Player>().Noteams();
                }
                else if (player.GetComponent<FencingMates_Player>() != null)
                {
                    if (player.GetComponent<FencingMates_Player>().team == 0)
                    {
                        player.transform.position = blueSpawns[blueIndex].transform.position;
                        blueIndex++;
                    }
                    else
                    {
                        player.transform.position = redSpawns[redIndex].transform.position;
                        redIndex++;
                    }
                    if (FindObjectOfType<PlayerData>().teams == true)
                        player.GetComponent<FencingMates_Player>().Withteams();
                    else player.GetComponent<FencingMates_Player>().Noteams();
                }
                else if (player.GetComponent<Footballers_Player>() != null)
                {
                    if (player.GetComponent<Footballers_Player>().team == 0)
                    {
                        player.transform.position = blueSpawns[blueIndex].transform.position;
                        blueIndex++;
                    }
                    else
                    {
                        player.transform.position = redSpawns[redIndex].transform.position;
                        redIndex++;
                    }
                    if (FindObjectOfType<PlayerData>().teams == true)
                        player.GetComponent<Footballers_Player>().Withteams();
                    else player.GetComponent<Footballers_Player>().Noteams();
                }
                else if (player.GetComponent<ZombieSurvival_player>() != null)
                {
                    if (player.GetComponent<ZombieSurvival_player>().team == 0)
                    {
                        player.transform.position = blueSpawns[blueIndex].transform.position;
                        blueIndex++;
                    }
                    else
                    {
                        player.transform.position = redSpawns[redIndex].transform.position;
                        redIndex++;
                    }
                    if (FindObjectOfType<PlayerData>().teams == true)
                        player.GetComponent<ZombieSurvival_player>().Withteams();
                    else player.GetComponent<ZombieSurvival_player>().Noteams();
                }
                else if (player.GetComponent<Brawlers_player>() != null)
                {
                    if (player.GetComponent<Brawlers_player>().team == 0)
                    {
                        player.transform.position = blueSpawns[blueIndex].transform.position;
                        blueIndex++;
                    }
                    else
                    {
                        player.transform.position = redSpawns[redIndex].transform.position;
                        redIndex++;
                    }
                    if (FindObjectOfType<PlayerData>().teams == true)
                        player.GetComponent<Brawlers_player>().Withteams();
                    else player.GetComponent<Brawlers_player>().Noteams();
                }
                else if (player.GetComponent<Bombermen_player>() != null)
                {
                    if (player.GetComponent<Bombermen_player>().team == 0)
                    {
                        player.transform.position = blueSpawns[blueIndex].transform.position;
                        blueIndex++;
                    }
                    else
                    {
                        player.transform.position = redSpawns[redIndex].transform.position;
                        redIndex++;
                    }
                    if (FindObjectOfType<PlayerData>().teams == true)
                        player.GetComponent<Bombermen_player>().Withteams();
                    else player.GetComponent<Bombermen_player>().Noteams();
                }
            }
        }
    }
    private void Update()
    {
    }
}
