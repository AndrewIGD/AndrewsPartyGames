using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusDefenders_StationaryAxe : MonoBehaviour
{
    public float timeBeforeDestroy;
    public bool active = false;
    void Start()
    {
        if(active)
            Invoke("DestroyAxe", timeBeforeDestroy);
    }
    public void DestroyAxe()
    {
        Destroy(gameObject);
    }
}
