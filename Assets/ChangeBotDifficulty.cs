using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBotDifficulty : MonoBehaviour
{
    int diff;
    public string[] diffs;
    // Start is called before the first frame update
    void Start()
    {
        diff = PlayerPrefs.GetInt("BotDifficulty", 0);
        GetComponentInChildren<Text>().text = diffs[diff];
    }
    public void ChangeDiff()
    {
        diff++;
        if (diff > 3)
            diff = 0;
        PlayerPrefs.SetInt("BotDifficulty", diff);
        GetComponentInChildren<Text>().text = diffs[diff];
    }
}
