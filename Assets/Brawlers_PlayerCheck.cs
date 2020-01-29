using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawlers_PlayerCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool invoked = false;
    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerData>().teams == true)
        {
            bool ok1 = false;
            bool ok2 = false;
            foreach (Brawlers_player player in FindObjectsOfType<Brawlers_player>())
            {
                if (player.team == 0)
                    ok1 = true;
                if (player.team == 1)
                    ok2 = true;
            }
            if (ok1 == false || ok2 == false)
            {
                if (FindObjectOfType<PlayerData>().teams && invoked == false)
                {
                    if (ok1 == false)
                        FindObjectOfType<PlayerData>().AddRed();
                    else FindObjectOfType<PlayerData>().AddBlue();
                    invoked = true;
                    FindObjectOfType<PlayerData>().InvokeScores();
                }
              
            }
        }
        else if(invoked == false)
        {

            int index = 0;
            foreach (Brawlers_player player in FindObjectsOfType<Brawlers_player>())
            {
                index++;
            }
            if (index <= 1)
            {
                if (index >= 1)
                {
                    FindObjectOfType<PlayerData>().playerObjectiveScores[FindObjectOfType<Brawlers_player>().playerNumber] = FindObjectOfType<Brawlers_player>().time + 999;
                }
                invoked = true;
                FindObjectOfType<PlayerData>().InvokeScores();
            }

        }
    }
}
