using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby_MinigameButton : MonoBehaviour
{
    public string minigameName;
    public GameObject currentMinigames;
    public GameObject miniMinigame;
    public AudioClip button;
    public void AddMinigame()
    {
        GameObject minigame = Instantiate(miniMinigame);
        minigame.transform.parent = currentMinigames.transform;
        minigame.GetComponent<Lobby_CancelMinigame>().indexx = FindObjectOfType<PlayerData>().menuSceneCount;
        FindObjectOfType<PlayerData>().AddScene(minigameName, minigame);
        AudioSource.PlayClipAtPoint(button, Camera.main.transform.position);
    }
}
