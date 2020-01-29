using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSurvival_PlayerCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    int team = -1;
    bool invoked = false;

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerData>().teams)
        {
            if(FindObjectsOfType<ZombieSurvival_player>().Length >= 1)
                team = FindObjectOfType<ZombieSurvival_player>().team;
        }
        if (FindObjectsOfType<ZombieSurvival_player>().Length<1)
        {
            if (FindObjectOfType<PlayerData>().teams && invoked == false)
            {
                if (team == 0)
                    FindObjectOfType<PlayerData>().AddBlue();
                else if (team == 1)
                    FindObjectOfType<PlayerData>().AddRed();
                invoked = true;
                FindObjectOfType<PlayerData>().InvokeScores();
            }
           
        }
    }
}
