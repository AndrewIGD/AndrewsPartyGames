using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooter_CheckPlayers : MonoBehaviour
{
    bool invoked = false;
    // Update is called once per frame
    void Update()
    {

            bool ok1 = false;
            bool ok2 = false;
            foreach (SpaceShooter_Player player in FindObjectsOfType<SpaceShooter_Player>())
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
                else if (FindObjectOfType<PlayerData>().teams == false && invoked == false)
                {
                    foreach (SpaceShooter_Player player in FindObjectsOfType<SpaceShooter_Player>())
                    {
                        FindObjectOfType<PlayerData>().playerObjectiveScores[player.playerNumber] = player.damageDealt;
                    }
                    FindObjectOfType<PlayerData>().InvokeScores();
                }
            }
        
    }
}
