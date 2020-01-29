using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_BotDifficulty : MonoBehaviour
{
    public string difficulty = "easy";
    public Text botDifText;
    public Lobby_AddBot botObj;
    public bool changedif = true;
    private void Start()
    {
        if (changedif)
        {
            int diff = PlayerPrefs.GetInt("BotDifficulty", 0);
            if (diff == 1)
            {
                difficulty = "normal";
                botDifText.text = "Normal";
            }
            else if (diff == 2)
            {
                difficulty = "hard";
                botDifText.text = "Hard";
            }
            else if (diff == 3)
            {
                difficulty = "expert";
                botDifText.text = "Expert";
            }
            else
            {
                difficulty = "easy";
                botDifText.text = "Easy";
            }
            FindObjectOfType<PlayerData>().botDifficulties[botObj.playerScript.playerNumber] = difficulty;
        }
    }
    public void DifficultyChange()
    {
        if (difficulty == "easy")
        {
            difficulty = "normal";
            botDifText.text = "Normal";
        }
        else if (difficulty == "normal")
        {
            difficulty = "hard";
            botDifText.text = "Hard";
        }
        else if (difficulty == "hard")
        {
            difficulty = "expert";
            botDifText.text = "Expert";
        }
        else
        {
            difficulty = "easy";
            botDifText.text = "Easy";
        }
        FindObjectOfType<PlayerData>().botDifficulties[botObj.playerScript.playerNumber] = difficulty;
    }
}
