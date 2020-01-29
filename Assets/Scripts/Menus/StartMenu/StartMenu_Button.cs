using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu_Button : MonoBehaviour
{
    public GameObject UI;
    public GameObject minigameUI;
    public AudioClip button;
    bool active = false;
    public bool startButton;
    public int number;
    private void Update()
    {
        if(startButton)
        {
            if (FindObjectOfType<PlayerData>().players.Count >= 2)
            {
                
                if(FindObjectOfType<PlayerData>().random == true)
                {
                    if (FindObjectOfType<Lobby_Random>() != null)
                    {
                        foreach (char c in FindObjectOfType<Lobby_Random>().inputField.GetComponent<InputField>().text)
                        {
                            if (c < '0' || c > '9')
                            {
                                GetComponent<Image>().enabled = false;
                                GetComponent<Button>().enabled = false;
                                GetComponentInChildren<Text>().enabled = false;
                                return;
                            }
                        }
                        number = int.Parse(FindObjectOfType<Lobby_Random>().inputField.GetComponent<InputField>().text);
                        GetComponent<Image>().enabled = true;
                        GetComponent<Button>().enabled = true;
                        GetComponentInChildren<Text>().enabled = true;
                    }
                        
                    
                }
                else
                {
                    if(!(FindObjectOfType<PlayerData>().menuSceneCount >= 1))
                    {

                        GetComponent<Image>().enabled = false;
                        GetComponent<Button>().enabled = false;
                        GetComponentInChildren<Text>().enabled = false;
                        return;
                    }
                }
                if(PlayerPrefs.GetInt("FoundCustomize", 0) == 1)
                {
                    for(int i =1;i<=18;i++)
                    {
                        if (FindObjectOfType<PlayerData>().players.Contains(i) && FindObjectOfType<PlayerData>().bots[i])
                        {
                            try
                            {
                                int.Parse(FindObjectOfType<PlayerData>().botCustomize[i]);
                                if (int.Parse(FindObjectOfType<PlayerData>().botCustomize[i]) == 0)
                                {
                                    GetComponent<Image>().enabled = false;
                                    GetComponent<Button>().enabled = false;
                                    GetComponentInChildren<Text>().enabled = false;
                                    return;
                                }
                            }
                            catch
                            {
                                GetComponent<Image>().enabled = false;
                                GetComponent<Button>().enabled = false;
                                GetComponentInChildren<Text>().enabled = false;
                                return;
                            }
                        }
                    }
                }
                if (number <= 0 && FindObjectOfType<PlayerData>().random == true)
                {
                    GetComponent<Image>().enabled = false;
                    GetComponent<Button>().enabled = false;
                    GetComponentInChildren<Text>().enabled = false;
                    return;
                }
                if (FindObjectOfType<PlayerData>().teams == true)
                {
                    bool ok1 = false;
                    bool ok2 = false;
                    foreach(int team in FindObjectOfType<PlayerData>().playerTeams)
                    {
                        if (team == 0)
                            ok1 = true;
                        else ok2 = true;
                    }
                    if (ok1 && ok2)
                    {
                        GetComponent<Image>().enabled = true;
                        GetComponent<Button>().enabled = true;
                        GetComponentInChildren<Text>().enabled = true;
                    }
                    else
                    {

                        GetComponent<Image>().enabled = false;
                        GetComponent<Button>().enabled = false;
                        GetComponentInChildren<Text>().enabled = false;
                    }
                }
                else
                {
                    GetComponent<Image>().enabled = true;
                    GetComponent<Button>().enabled = true;
                    GetComponentInChildren<Text>().enabled = true;
                }
            }
            else
            {

                GetComponent<Image>().enabled = false;
                GetComponent<Button>().enabled = false;
                GetComponentInChildren<Text>().enabled = false;
            }
        }
    }
    public void StartFirstGame()
    {
        for(int i=1;i<=18;i++)
        {
            if(FindObjectOfType<PlayerData>().bots[i] == true)
            {
                if (PlayerPrefs.GetInt("FoundCustomize", 0) == 1 &&PlayerPrefs.GetInt("BotCustomizedSettings",0)>= int.Parse(FindObjectOfType<PlayerData>().botCustomize[i]))
                {
                    FindObjectOfType<PlayerData>().playerNames[i] = FindObjectOfType<Lobby_Player>().characters[PlayerPrefs.GetInt("BotChar1"+FindObjectOfType<PlayerData>().botCustomize[i])].ToString() + FindObjectOfType<Lobby_Player>().characters[PlayerPrefs.GetInt("BotChar2" + FindObjectOfType<PlayerData>().botCustomize[i])].ToString() + FindObjectOfType<Lobby_Player>().characters[PlayerPrefs.GetInt("BotChar3" + FindObjectOfType<PlayerData>().botCustomize[i])].ToString();
                    FindObjectOfType<PlayerData>().color1[i] = FindObjectOfType<Lobby_Player>().colors[PlayerPrefs.GetInt("BotColor1" + FindObjectOfType<PlayerData>().botCustomize[i])];
                    FindObjectOfType<PlayerData>().color2[i] = FindObjectOfType<Lobby_Player>().colors[PlayerPrefs.GetInt("BotColor2" + FindObjectOfType<PlayerData>().botCustomize[i])];
                }
                else
                {
                    FindObjectOfType<PlayerData>().playerNames[i] = FindObjectOfType<Lobby_Player>().characters[Random.Range(1, 1000000) % FindObjectOfType<Lobby_Player>().characters.Length].ToString() + FindObjectOfType<Lobby_Player>().characters[Random.Range(1, 1000000) % FindObjectOfType<Lobby_Player>().characters.Length].ToString() + FindObjectOfType<Lobby_Player>().characters[Random.Range(1, 1000000) % FindObjectOfType<Lobby_Player>().characters.Length].ToString();
                    FindObjectOfType<PlayerData>().color1[i] = FindObjectOfType<Lobby_Player>().colors[Random.Range(1, 100000) % FindObjectOfType<Lobby_Player>().colors.Length];
                    FindObjectOfType<PlayerData>().color2[i] = FindObjectOfType<Lobby_Player>().colors[Random.Range(1, 100000) % FindObjectOfType<Lobby_Player>().colors.Length];
                }
            }
        }
        if(FindObjectOfType<PlayerData>().random == true)
            FindObjectOfType<PlayerData>().randomNumber = number;
        GameObject.Find("Everything2").GetComponent<Animator>().Play("Transition2");
        AudioSource.PlayClipAtPoint(button, Camera.main.transform.position);
        Start_Music[] musics = FindObjectsOfType<Start_Music>();
        foreach (Start_Music music in musics)
        {
            if(music.GetComponent<Animator>()!= null)
            music.GetComponent<Animator>().Play("MusicFade");
        }
        Invoke("FirstMinigame", 0.667f);
    }
    private void FirstMinigame()
    {
        if (FindObjectOfType<PlayerData>().players.Count >= 2)
        {
            FindObjectOfType<PlayerData>().StopCoroutine(FindObjectOfType<PlayerData>().TestControllers());
            Start_Music[] musics = FindObjectsOfType<Start_Music>();
            foreach (Start_Music music in musics)
            {
                Destroy(music.gameObject);
            }
            FindObjectOfType<PlayerData>().LoadNextMinigame();
        }
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void BotCustom()
    {
        SceneManager.LoadScene("BotCustomize");
    }
    public void Mutators()
    {
        SceneManager.LoadScene("Mutators");
    }
    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }
    public void Minigames()
    {
        if (active == false)
        {
            FindObjectOfType<PlayerData>().StopCoroutine(FindObjectOfType<PlayerData>().TestControllers());
            UI.SetActive(true);
            active = true;
        }
        else
        {
            FindObjectOfType<PlayerData>().StartCoroutine(FindObjectOfType<PlayerData>().TestControllers());
            active = false;
            UI.SetActive(false);
        }
        AudioSource.PlayClipAtPoint(button, Camera.main.transform.position);
    }
    public void StartGame()
    {
        GameObject.Find("Everything").GetComponent<Animator>().Play("Transition");
        AudioSource.PlayClipAtPoint(button, Camera.main.transform.position);
    }
    public void Lobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void MainMenu()
    {
        FindObjectOfType<PlayerData>().StopCoroutine(FindObjectOfType<PlayerData>().TestControllers());
        Destroy(FindObjectOfType<PlayerData>());
        AudioSource.PlayClipAtPoint(button, Camera.main.transform.position);
        GameObject.Find("Everything2").GetComponent<Animator>().Play("Transition2");
        Invoke("ChangeScene", 0.667f);
    }
    private void ChangeScene()
    {
        SceneManager.LoadScene("Start");
    }
    public void ControlsScene()
    {
        Application.Quit();
    }
}
