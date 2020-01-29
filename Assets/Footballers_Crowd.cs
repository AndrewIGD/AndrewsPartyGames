using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footballers_Crowd : MonoBehaviour
{
    public AudioClip shot;
    public void Play()
    {
        if(canPlay)
        {
            canPlay = false;
            AudioSource.PlayClipAtPoint(shot, Camera.main.transform.position, 1f);
            Invoke("CanPlay", 2f);
        }
    }
    bool canPlay = true;
    void CanPlay()
    {
        canPlay = true;
    }
}
