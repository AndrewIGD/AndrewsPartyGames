using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby_CancelMinigame : MonoBehaviour
{
    public int indexx;
    public AudioClip button;
    public void CancelMinigame()
    {
        FindObjectOfType<PlayerData>().RemoveScene(indexx);
        AudioSource.PlayClipAtPoint(button, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
