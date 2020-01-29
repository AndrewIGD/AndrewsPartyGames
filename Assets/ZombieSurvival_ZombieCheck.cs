using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSurvival_ZombieCheck : MonoBehaviour
{
    bool check = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Check", 12f);
    }
    private void Check()
    {
        check = true;
    }
    bool invoked = false;
    // Update is called once per frame
    void Update()
    {
        if (FindObjectsOfType<ZombieSurvival_zombie>().Length<=1 && check)
        {
            if (FindObjectOfType<PlayerData>().teams && invoked == false)
            {
                int blue=0, red=0;
                foreach(ZombieSurvival_player player in FindObjectsOfType<ZombieSurvival_player>())
                {
                    if (player.team == 0)
                        blue++;
                    else red++;
                }
                if (blue > red)
                    FindObjectOfType<PlayerData>().AddBlue();
                else if (blue < red)
                        FindObjectOfType<PlayerData>().AddRed();
                else
                {
                    float blueDmg=0, redDmg=0;
                    foreach (ZombieSurvival_player player in FindObjectsOfType<ZombieSurvival_player>())
                    {
                        if (player.team == 0)
                            blueDmg += player.damageDealt;
                        else redDmg += player.damageDealt;
                    }
                    if(blueDmg>redDmg)
                        FindObjectOfType<PlayerData>().AddBlue();
                    else if (blueDmg < redDmg)
                        FindObjectOfType<PlayerData>().AddRed();
                }
                invoked = true;
                FindObjectOfType<PlayerData>().InvokeScores();
            }
            else if  (FindObjectOfType<PlayerData>().teams==false && invoked == false)
            {
                foreach (ZombieSurvival_player player in FindObjectsOfType<ZombieSurvival_player>())
                {
                    FindObjectOfType<PlayerData>().playerObjectiveScores[player.playerNumber] = player.time + player.damageDealt;
                }
                invoked = true;
                FindObjectOfType<PlayerData>().InvokeScores();
            }
                   
        }
    }
}
