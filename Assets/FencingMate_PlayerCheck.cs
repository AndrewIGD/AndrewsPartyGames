using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencingMate_PlayerCheck : MonoBehaviour
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
            foreach (FencingMates_Player player in FindObjectsOfType<FencingMates_Player>())
            {
                if (player.team == 0)
                {
                    ok1 = true;

                }
                if (player.team == 1)
                {
                    ok2 = true;

                }
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
        else
        {
            int index = 0;
            foreach (FencingMates_Player player in FindObjectsOfType<FencingMates_Player>())
            {
                index++;
            }
            if (index <= 1 && invoked == false)
            {
                invoked = true;
                if (index >= 1)
                {
                    FindObjectOfType<PlayerData>().playerObjectiveScores[FindObjectOfType<FencingMates_Player>().playerNumber] = FindObjectOfType<FencingMates_Player>().time+999;
                }
                FindObjectOfType<PlayerData>().InvokeScores();
            }

        }
    }
}
