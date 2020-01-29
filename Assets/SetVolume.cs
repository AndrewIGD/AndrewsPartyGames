using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVolume : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.pause = false;
        AudioListener.volume = PlayerPrefs.GetFloat("GameVolume", 1f);
    }
}
