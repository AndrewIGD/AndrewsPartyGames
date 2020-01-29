using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Footballers_goals : MonoBehaviour
{
    public int redGoals=0;
    public int blueGoals=0;
    public Text blueScore;
    public Text redScore;
    public void BlueGoal()
    {
        blueGoals++;
        blueScore.text = blueGoals.ToString();
        if (blueGoals >=3)
        {
            Invoke("EndGame", 5f);
        }
    }
    public void RedGoal()
    {
        redGoals++;
        redScore.text = redGoals.ToString();
        if (redGoals >= 3)
        {
            Invoke("EndGame", 5f);
        }
    }
    private void EndGame()
    {
        if (FindObjectOfType<PlayerData>().teams == false)
        { Footballers_Player[] players = FindObjectsOfType<Footballers_Player>();
            foreach (Footballers_Player player in players)
            {

                FindObjectOfType<PlayerData>().playerObjectiveScores[player.playerNumber] = player.passes;
            }
            
        }
        else
        {
            if (redGoals == 3)
                FindObjectOfType<PlayerData>().AddRed();
            else FindObjectOfType<PlayerData>().AddBlue();

        }
        FindObjectOfType<PlayerData>().InvokeScores();
    }
    
    
}
