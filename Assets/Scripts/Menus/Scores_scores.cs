using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores_scores : MonoBehaviour
{
    public Text[] scores;
    public List<int> alreadyMentioned;
    public AudioClip sfx;
    // Start is called before the first frame update
    int index = 0;
    void AppearText()
    {
        if (scores[index].text.Contains(":"))
        {
            scores[index++].gameObject.SetActive(true);
            AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position);
            Invoke("AppearText", 0.2f);
        }
    }
    void DissapearText()
    {
        foreach(Text objs in FindObjectsOfType<Text>())
        {
            objs.gameObject.SetActive(false);
        }
    }
    void Start()
    {
        Invoke("AppearText", 0.1f);
        Invoke("DissapearText", 4.5f);
        if (FindObjectOfType<PlayerData>().teams == false)
        {
            int[] score = (int[])FindObjectOfType<PlayerData>().scores.Clone();
            for (int i = 0; i < FindObjectOfType<PlayerData>().players.Count; i++)
            {
                int max = -5;
                int maxPlayer = 0;
                for (int j = 0; j < FindObjectOfType<PlayerData>().players.Count; j++)
                {
                    if (max < score[FindObjectOfType<PlayerData>().players[j]])
                    {
                        max = score[FindObjectOfType<PlayerData>().players[j]];
                        maxPlayer = FindObjectOfType<PlayerData>().players[j];
                    }
                }
                string playerName = "";
                for (int j = 0;j<18;j++)
                {
                    if(score[FindObjectOfType<PlayerData>().players[j]] == max)
                    {
                            playerName = FindObjectOfType<PlayerData>().playerNames[FindObjectOfType<PlayerData>().players[j]];
                            alreadyMentioned.Add(FindObjectOfType<PlayerData>().players[j]);
                        
                        break;
                    }
                }
                scores[i].text = playerName + ": " + max + " points";
                score[maxPlayer] = -5;
            }
            FindObjectOfType<PlayerData>().Invoke("LoadNextMinigame", 5f);
        }
        else
        {
            if(FindObjectOfType<PlayerData>().bluePoints> FindObjectOfType<PlayerData>().redPoints)
            {
                scores[0].text = "Team 1: " + FindObjectOfType<PlayerData>().bluePoints + " points";
                scores[0].color = new Color32(0, 175, 255, 255);
                scores[1].text = "Team 2: " + FindObjectOfType<PlayerData>().redPoints + " points";
                scores[1].color = new Color32(255,0,0,255);
            }
            else
            {
                scores[0].text = "Team 2: " + FindObjectOfType<PlayerData>().redPoints + " points";
                scores[0].color = new Color32(255,0,0,255);
                scores[1].text = "Team 1: " + FindObjectOfType<PlayerData>().bluePoints + " points";
                scores[1].color = new Color32(0, 175, 255, 255);
            }
            FindObjectOfType<PlayerData>().Invoke("LoadNextMinigame", 5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
