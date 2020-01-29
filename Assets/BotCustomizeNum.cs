using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotCustomizeNum : MonoBehaviour
{
    public int playerNum;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("FoundCustomize", 0) == 1)
        {
            gameObject.SetActive(true);
        }
        else gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<PlayerData>().botCustomize[playerNum] = GetComponent<InputField>().text;
    }
}
