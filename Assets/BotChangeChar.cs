using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotChangeChar : MonoBehaviour
{
    public char[] chars;
    public Text text;
    int index = -1;
    public int charNum;
    public int currentNum = -1;
    public void ChangeColor()
    {
        index++;
        if (index >= chars.Length)
        {
            index = 0;

        }
        GetComponent<Text>().text = chars[index].ToString();

        PlayerPrefs.SetInt("BotChar" + charNum + currentNum, index);
    }
    public void ChangeColor2()
    {
        index--;
        if (index < 0)
        {
            index = chars.Length-1;

        }
        GetComponent<Text>().text = chars[index].ToString();

        PlayerPrefs.SetInt("BotChar" + charNum + currentNum, index);
    }
    private void Start()
    {
        if (!text.text.Contains("Player"))
        {
            string tex = text.text.Split(' ')[1];

            tex = tex.Split(':')[0];

            currentNum = int.Parse(tex);
            if (PlayerPrefs.GetInt("BotChar" + charNum + currentNum, -1) == -1)
                PlayerPrefs.SetInt("BotChar" + charNum + currentNum, 0);
            index = PlayerPrefs.GetInt("BotChar" + charNum + currentNum, -1);

            GetComponent<Text>().text = chars[index].ToString();
        }

    }
}
