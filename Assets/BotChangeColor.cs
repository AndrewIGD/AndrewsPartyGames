using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotChangeColor : MonoBehaviour
{
    public Color32[] colors;
    public Text text;
    int index = -1;
    public int colorNum;
    public int currentNum=-1;
    public void ChangeColor()
    {
        index++;
        if(index>=colors.Length)
        {
            index = 0;

        }
        GetComponent<Image>().color = colors[index];

        PlayerPrefs.SetInt("BotColor" + colorNum + currentNum, index);
    }
    public void ChangeColor2()
    {
        index--;
        if (index < 0)
        {
            index = colors.Length-1;

        }
        GetComponent<Image>().color = colors[index];

        PlayerPrefs.SetInt("BotColor" + colorNum + currentNum, index);
    }
    private void Start()
    {
        if (!text.text.Contains("Player"))
        {
            string tex = text.text.Split(' ')[1];
    
            tex = tex.Split(':')[0];
            
            currentNum = int.Parse(tex);
            if (PlayerPrefs.GetInt("BotColor" + colorNum + currentNum, -1) == -1)
                PlayerPrefs.SetInt("BotColor" + colorNum + currentNum, 0);
            index = PlayerPrefs.GetInt("BotColor" + colorNum + currentNum, -1);

            GetComponent<Image>().color = colors[index];
        }
        
    }

}
