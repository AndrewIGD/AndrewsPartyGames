using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerData>().LoadPlayers();
    }
}
