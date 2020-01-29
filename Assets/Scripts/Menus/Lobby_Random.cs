using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_Random : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;
    public GameObject inputField;
    public bool active = false;
    public Text buttonText;
    public void Random()
    {
        if(active)
        {
            active = false;
            image1.SetActive(false);
            image2.SetActive(false);
            inputField.SetActive(false);
            buttonText.text = "Random : OFF";
            FindObjectOfType<PlayerData>().random = false;
        }
        else
        {
            active = true;
            image1.SetActive(true);
            image2.SetActive(true);
            inputField.SetActive(true);
            buttonText.text = "Random : ON";
            FindObjectOfType<PlayerData>().random = true;
        }
    }
}
