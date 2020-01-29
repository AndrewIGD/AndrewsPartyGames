using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourMasters_CheckPlayers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    int team=-1;
    bool invoked = false;
    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<PlayerData>().teams && FindObjectOfType<ParkourMasters_Player>() != null)
        {
            team = FindObjectOfType<ParkourMasters_Player>().team;
        }
        if(FindObjectsOfType<ParkourMasters_Player>().Length <= 0)
        {
            if (FindObjectOfType<PlayerData>().teams && invoked == false)
            {
                if (team == 0)
                    FindObjectOfType<PlayerData>().AddBlue();
                else if(team == 1)
                    FindObjectOfType<PlayerData>().AddRed();
                invoked = true;
                FindObjectOfType<PlayerData>().InvokeScores();
            }
            else if(invoked ==false)
            {
                invoked = true;
                FindObjectOfType<PlayerData>().InvokeScores();
            }
        }
    }
}
