using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathrunners_PlayerCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Check", 6f);
    }
    bool invoked = false;
    bool check = false;
    void Check()
    {
        check = true;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            bool ok1 = false;
            bool ok2 = false;
            bool ok3 = false;
            bool ok4 = false;
            foreach (FencingMates_Player player in FindObjectsOfType<FencingMates_Player>())
            {
                if (player.team == 2)
                    ok1 = true;
                else ok2 = true;
                if (player.team == 1)
                    ok4 = true;
                else if (player.team == 0)
                    ok3 = true;
            }
            if (ok1 == false || ok2 == false&&invoked == false)
            {
                invoked = true;
                FindObjectOfType<PlayerData>().InvokeScores();
            }
        }
    }
}
