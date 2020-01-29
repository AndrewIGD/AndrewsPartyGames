using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("GameVolume", 1f);
    }
    public void ChangeVolume()
    {
        PlayerPrefs.SetFloat("GameVolume", GetComponent<Slider>().value);
        AudioListener.volume = GetComponent<Slider>().value;
    }
}
