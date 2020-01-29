using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BotDelete : MonoBehaviour
{
    public Text text;
    public GameObject parent;

    int currentNum=-1;
    // Start is called before the first frame update
    void Start()
    {
        string tex = text.text.Split(' ')[1];
        tex = tex.Split(':')[0];
        currentNum = int.Parse(tex);
    }
    public void DeleteBot()
    {
        int num = PlayerPrefs.GetInt("BotCustomizedSettings", 0);
        PlayerPrefs.SetInt("BotCustomizedSettings", PlayerPrefs.GetInt("BotCustomizedSettings", 0) - 1);
        for(int i=1;i<num;i++)
        {
            if(i!=currentNum && i>currentNum)
            {
                PlayerPrefs.SetInt("BotColor0" + (i - 1), PlayerPrefs.GetInt("BotColor0" + i, -1));
                PlayerPrefs.SetInt("BotColor1" + (i - 1), PlayerPrefs.GetInt("BotColor1" + i, -1));
                PlayerPrefs.SetInt("BotChar1" + (i - 1), PlayerPrefs.GetInt("BotChar1" + i, -1));
                PlayerPrefs.SetInt("BotChar2" + (i - 1), PlayerPrefs.GetInt("BotChar2" + i, -1));
                PlayerPrefs.SetInt("BotChar3" + (i - 1), PlayerPrefs.GetInt("BotChar3" + i, -1));
            }
        }

        PlayerPrefs.DeleteKey("BotColor0" + (num));
        PlayerPrefs.DeleteKey("BotColor1" + (num));
        PlayerPrefs.DeleteKey("BotChar1" + (num));
        PlayerPrefs.DeleteKey("BotChar2" + (num));
        PlayerPrefs.DeleteKey("BotChar3" + (num));
        SceneManager.LoadScene("BotCustomize");
    }
}
