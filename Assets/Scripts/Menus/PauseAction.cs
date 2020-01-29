using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAction : MonoBehaviour
{
    bool paused = false;
    public GameObject child;
    // Update is called once per frame
    void Update()
    {
        if (child.activeInHierarchy == false)
            paused = false;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false)
            {
                Time.timeScale = 0f;
                child.SetActive(true);
                paused = true;
            }
            else
            {
                Time.timeScale = 1;
                child.SetActive(false);
                paused = false;
            }
            AudioListener.pause = paused;
        }
    }
}
