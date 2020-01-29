using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject parent;
    public void Continue()
    {
        Time.timeScale = 1;
        parent.SetActive(false);
        AudioListener.pause = false;
    }
    public void LoadNextMinigame()
    {
        Time.timeScale = 1;
        FindObjectOfType<PlayerData>().LoadNextMinigame();
    }
    public void RestartMinigame()
    {
        Time.timeScale = 1;
        FindObjectOfType<PlayerData>().RestartMinigame();
    }
    public void TitleScreen()
    {
        Time.timeScale = 1;
        FindObjectOfType<PlayerData>().Lobby();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
