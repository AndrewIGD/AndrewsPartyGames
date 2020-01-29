using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Music : MonoBehaviour
{
    public AudioSource music;
    // Start is called before the first frame update
    void Awake()
    {
        Start_Music[] musics = FindObjectsOfType<Start_Music>();
        if (musics.Length > 1)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
