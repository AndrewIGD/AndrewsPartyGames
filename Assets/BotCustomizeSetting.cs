using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotCustomizeSetting : MonoBehaviour
{
    int setting = 0;
    // Start is called before the first frame update
    void Start()
    {
        setting = PlayerPrefs.GetInt("FoundCustomize", 0);
        if(setting == 0)
        {
            GetComponent<Image>().color = new Color32(0, 255, 255, 255);
        }
        else GetComponent<Image>().color = new Color32(255, 0, 0, 255);
    }
    public void ChangeSetting()
    {
        if (setting == 0)
            setting = 1;
        else setting = 0;
        PlayerPrefs.SetInt("FoundCustomize", setting);
        if (setting == 0)
        {
            GetComponent<Image>().color = new Color32(0, 255, 255, 255);
        }
        else GetComponent<Image>().color = new Color32(255, 0, 0, 255);
    }
}
