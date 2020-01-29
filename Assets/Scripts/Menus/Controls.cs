using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour
{
    public bool controlsMenu;
    public void ControlsMenu()
    {
        if (!controlsMenu)
            SceneManager.LoadScene("Controls");
        else SceneManager.LoadScene("Start");
    }
}
