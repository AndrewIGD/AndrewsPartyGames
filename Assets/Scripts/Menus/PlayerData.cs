using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SharpDX.DirectInput;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public List<int> players;
    public List<string> minigames;
    public int sceneCount = 0;
    public int menuSceneCount = 0;
    public GameObject[] minigameObjects;
    public List<Color32> color1;
    public List<Color32> color2;
    public int currentPointCount = 1;
    public int[] scores;
    public float[] playerObjectiveScores;
    public bool teams;
    public int[] playerTeams;
    public int bluePoints;
    public int redPoints;
    public string[] playerNames;
    public bool random;
    public int randomNumber;
    public int randomIndex;
    public string[] minigameNames;
    public Joystick[] controllers;
    public int[] button1;
    public int[] button2;
    public int[] color1Num;
    public int[] color2Num;
    public int[] char1Num;
    public int[] char2Num;
    public int[] char3Num;
    public bool[] bots;
    public string[] botDifficulties;
    public string[] botNames;
    public string[] botCustomize;

    float delta = 0f;
    float secondsAlive = 0;


    
    
    public void AddScene(string name, GameObject minigame)
    {
        minigames[menuSceneCount] = name;
        minigameObjects[menuSceneCount] = minigame;
        menuSceneCount++;
    }
    public void RemoveScene(int index)
    {
            for (int i = index; i <= menuSceneCount; i++)
            {
                minigames[i] = minigames[i+1];
            minigameObjects[i] = minigameObjects[i + 1];
            try
            { minigameObjects[i].GetComponent<Lobby_CancelMinigame>().indexx -= 1; }
            catch { }
        }
            minigames[menuSceneCount] = "";
            menuSceneCount--;

    }
    public bool[] pendingControllers;
    public bool[] pollControllers;
    public int currentNum = 0;
    public IEnumerator TestControllers()
    {
        while (true)
        {
            if (secondsAlive > 2)
            {

                if (SceneManager.GetActiveScene().name.Contains("Lobby"))
                {
                    bool canDo = true;
                    for (int i = 0; i <= 18; i++)
                    {
                        if (pendingControllers[i])
                        {
                            canDo = false;
                            if (controllers[i] != null || pollControllers[i] || SceneManager.GetActiveScene().name.Contains("Lobby"))
                            {
                                bool ok = false;
                                try
                                {
                                    controllers[i].Poll();
                                }
                                catch
                                {
                                    pollControllers[i] = true;
                                    controllers[i] = null;
                                    ok = true;
                                }
                                if (ok || pollControllers[i] == true)
                                {
                                    yield return new WaitForSecondsRealtime(0.2f);

                                    List<SharpDX.DirectInput.Joystick> joystickz = new List<SharpDX.DirectInput.Joystick>();
                                    var joystickGuid = Guid.Empty;
                                    var di = new DirectInput();
                                    IList<DeviceInstance> keyboards = di.GetDevices(SharpDX.DirectInput.DeviceType.Keyboard, DeviceEnumerationFlags.AttachedOnly);
                                    yield return new WaitForSecondsRealtime(0.2f);
                                    if (!(i <= 2 && keyboards.Count == 0))
                                    {
                                        IList<DeviceInstance> gamepads = di.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly);
                                        yield return new WaitForSecondsRealtime(0.2f);
                                        for (int device = 0; device < gamepads.Count; device++)
                                        {
                                            try
                                            {
                                                joystickGuid = gamepads[device].InstanceGuid;
                                                joystickz.Add(new Joystick(di, joystickGuid));
                                            }
                                            catch
                                            {

                                            }
                                        }
                                        IList<DeviceInstance> joysticks = di.GetDevices(SharpDX.DirectInput.DeviceType.Joystick, DeviceEnumerationFlags.AttachedOnly);
                                        yield return new WaitForSecondsRealtime(0.2f);
                                        for (int device = 0; device < joysticks.Count; device++)
                                        {
                                            try
                                            {
                                                joystickGuid = joysticks[device].InstanceGuid;
                                                joystickz.Add(new Joystick(di, joystickGuid));
                                            }
                                            catch
                                            {

                                            }
                                        }
                                        foreach (Joystick joy in joystickz)
                                        {
                                            try
                                            {
                                                bool ok2 = true;
                                                for (int j = 0; j <= 18; j++)
                                                {
                                                    try
                                                    {
                                                        if (controllers[j] != null)
                                                        {

                                                            string value1 = joy.Properties.InterfacePath;
                                                            string value2 = controllers[j].Properties.InterfacePath;
                                                            if (value1 == value2)
                                                            {

                                                                ok2 = false;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    catch
                                                    {

                                                    }
                                                }
                                                if (ok2)
                                                {
                                                    for(int j=0;j<=18;j++)
                                                    {
                                                        if(pendingControllers[j])
                                                        {
                                                            pollControllers[j] = false;
                                                            controllers[j] = joy;
                                                            controllers[j].Acquire();
                                                            pendingControllers[j] = false;
                                                            button1[j] = PlayerPrefs.GetInt(joy.Information.ProductName + "1", 0);
                                                            button2[j] = PlayerPrefs.GetInt(joy.Information.ProductName + "2", 2);
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                            catch
                                            {

                                            }
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            joystickGuid = keyboards[0].InstanceGuid;
                                            controllers[i] = new Joystick(di, joystickGuid);
                                            controllers[i].Acquire();
                                        }
                                        catch
                                        {

                                        }

                                    }



                                }
                            }
                        }
                    }
                    if (canDo)
                    {
                        for (int i = currentNum; i <= currentNum; i++)
                        {
                            if (controllers[i] != null || pollControllers[i] )
                            {
                                bool ok = false;
                                try
                                {
                                    controllers[i].Poll();
                                }
                                catch
                                {
                                    pollControllers[i] = true;
                                    controllers[i] = null;
                                    ok = true;
                                }
                                if (ok || pollControllers[i] == true)
                                {
                                    yield return new WaitForSecondsRealtime(0.2f);

                                    List<SharpDX.DirectInput.Joystick> joystickz = new List<SharpDX.DirectInput.Joystick>();
                                    var joystickGuid = Guid.Empty;
                                    var di = new DirectInput();
                                    IList<DeviceInstance> keyboards = di.GetDevices(SharpDX.DirectInput.DeviceType.Keyboard, DeviceEnumerationFlags.AttachedOnly);
                                    yield return new WaitForSecondsRealtime(0.2f);
                                    if (!(i <= 2 && keyboards.Count == 0))
                                    {
                                        IList<DeviceInstance> gamepads = di.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly);
                                        yield return new WaitForSecondsRealtime(0.2f);
                                        for (int device = 0; device < gamepads.Count; device++)
                                        {
                                            try
                                            {
                                                joystickGuid = gamepads[device].InstanceGuid;
                                                joystickz.Add(new Joystick(di, joystickGuid));
                                            }
                                            catch
                                            {

                                            }
                                        }
                                        IList<DeviceInstance> joysticks = di.GetDevices(SharpDX.DirectInput.DeviceType.Joystick, DeviceEnumerationFlags.AttachedOnly);
                                        yield return new WaitForSecondsRealtime(0.2f);
                                        for (int device = 0; device < joysticks.Count; device++)
                                        {
                                            try
                                            {
                                                joystickGuid = joysticks[device].InstanceGuid;
                                                joystickz.Add(new Joystick(di, joystickGuid));
                                            }
                                            catch
                                            {

                                            }
                                        }
                                        foreach (Joystick joy in joystickz)
                                        {
                                            try
                                            {
                                                bool ok2 = true;
                                                for (int j = 0; j <= 18; j++)
                                                {
                                                    try
                                                    {
                                                        if (controllers[j] != null)
                                                        {

                                                            string value1 = joy.Properties.InterfacePath;
                                                            string value2 = controllers[j].Properties.InterfacePath;
                                                            if (value1 == value2)
                                                            {

                                                                ok2 = false;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    catch
                                                    {

                                                    }
                                                }
                                                if (ok2)
                                                {
                                                    pollControllers[i] = false;
                                                    controllers[i] = joy;
                                                    controllers[i].Acquire();
                                                    currentNum++;
                                                    button1[i] = PlayerPrefs.GetInt(joy.Information.ProductName + "1", 0);
                                                    button2[i] = PlayerPrefs.GetInt(joy.Information.ProductName + "2", 2);
                                                    break;
                                                }
                                            }
                                            catch
                                            {

                                            }
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            joystickGuid = keyboards[0].InstanceGuid;
                                            controllers[i] = new Joystick(di, joystickGuid);
                                            controllers[i].Acquire();
                                        }
                                        catch
                                        {

                                        }

                                    }



                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i <= 18; i++)
                    {
                        if (controllers[i] != null || pollControllers[i])
                        {
                            bool ok = false;
                            try
                            {
                                controllers[i].Poll();
                            }
                            catch
                            {
                                pollControllers[i] = true;
                                controllers[i] = null;
                                ok = true;
                            }
                            if (ok || pollControllers[i] == true)
                            {
                                yield return new WaitForSecondsRealtime(0.2f);

                                List<SharpDX.DirectInput.Joystick> joystickz = new List<SharpDX.DirectInput.Joystick>();
                                var joystickGuid = Guid.Empty;
                                var di = new DirectInput();
                                IList<DeviceInstance> keyboards = di.GetDevices(SharpDX.DirectInput.DeviceType.Keyboard, DeviceEnumerationFlags.AttachedOnly);
                                yield return new WaitForSecondsRealtime(0.2f);
                                if (!(i <= 2 && keyboards.Count == 0))
                                {
                                    IList<DeviceInstance> gamepads = di.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly);
                                    yield return new WaitForSecondsRealtime(0.2f);
                                    for (int device = 0; device < gamepads.Count; device++)
                                    {
                                        try
                                        {
                                            joystickGuid = gamepads[device].InstanceGuid;
                                            joystickz.Add(new Joystick(di, joystickGuid));
                                        }
                                        catch
                                        {

                                        }
                                    }
                                    IList<DeviceInstance> joysticks = di.GetDevices(SharpDX.DirectInput.DeviceType.Joystick, DeviceEnumerationFlags.AttachedOnly);
                                    yield return new WaitForSecondsRealtime(0.2f);
                                    for (int device = 0; device < joysticks.Count; device++)
                                    {
                                        try
                                        {
                                            joystickGuid = joysticks[device].InstanceGuid;
                                            joystickz.Add(new Joystick(di, joystickGuid));
                                        }
                                        catch
                                        {

                                        }
                                    }
                                    foreach (Joystick joy in joystickz)
                                    {
                                        try
                                        {
                                            bool ok2 = true;
                                            for (int j = 0; j <= 18; j++)
                                            {
                                                try
                                                {
                                                    if (controllers[j] != null)
                                                    {

                                                        string value1 = joy.Properties.InterfacePath;
                                                        string value2 = controllers[j].Properties.InterfacePath;
                                                        if (value1 == value2)
                                                        {

                                                            ok2 = false;
                                                            break;
                                                        }
                                                    }
                                                }
                                                catch
                                                {

                                                }
                                            }
                                            if (ok2)
                                            {
                                                pollControllers[i] = false;
                                                controllers[i] = joy;
                                                controllers[i].Acquire();
                                                FindObjectOfType<ControllerDisconnect>().Reconnect(i);
                                                button1[i] = PlayerPrefs.GetInt(joy.Information.ProductName + "1", 0);
                                                button2[i] = PlayerPrefs.GetInt(joy.Information.ProductName + "2", 2);
                                                break;
                                            }
                                        }
                                        catch
                                        {

                                        }
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        joystickGuid = keyboards[0].InstanceGuid;
                                        controllers[i] = new Joystick(di, joystickGuid);
                                        controllers[i].Acquire();
                                    }
                                    catch
                                    {

                                    }

                                }



                            }
                        }
                    }
                }
            }
            yield return null;
        }
    }
    private void Awake()
    {
        
        if (FindObjectsOfType<PlayerData>().Length > 1)
        {
            Destroy(gameObject);
        }
        char1Num = new int[19];
        char2Num = new int[19];
        char3Num = new int[19];
        color1Num = new int[19];
        color2Num = new int[19];
        scores = new int[19];
        bots = new bool[19];
        pollControllers = new bool[19];
        pendingControllers = new bool[19];
        botCustomize = new string[19];
        for (int i = 0; i <= 18; i++)
            pollControllers[i] = false;
        controllers = new Joystick[19];
        DontDestroyOnLoad(gameObject);
    }
    bool alreadyInvoked = false;
    bool alreadyInvoked2 = false;
    public void InvokeScores()
    {
        if (alreadyInvoked2 == false)
        {
            Invoke("Fade", 2.333f);
            Invoke("LoadScores", 3f);
            alreadyInvoked2 = true;
        }
    }
    void Fade()
    {
        StopCoroutine(TestControllers());
        foreach(Animator anim in FindObjectsOfType<Animator>())
        {
            if(anim.runtimeAnimatorController.name.Contains("Canvas"))
            {
                anim.Play("Transition2");
                break;
            }
        }
    }
    private void Update()
    {
        secondsAlive += Time.deltaTime;
    }
    public void LoadScores()
    {
        if (alreadyInvoked == false)
        {
            StopCoroutine(TestControllers());
            alreadyInvoked = true;
            for (int i = 0; i < players.Count; i++)
            {
                int maxPlayer = 0;
                float max = 999999;
                for (int j = players.Count - 1; j >= 0; j--)
                {
                    if (playerObjectiveScores[players[j]] <= max)
                    {
                        max = playerObjectiveScores[players[j]];
                        maxPlayer = players[j];
                    }
                }
                playerObjectiveScores[maxPlayer] = 9999999;
                scores[maxPlayer] += currentPointCount;
                Debug.Log("Added " + currentPointCount + " to " + maxPlayer + " " + playerObjectiveScores[maxPlayer] + " " + i);
                currentPointCount++;
            }
            SceneManager.LoadScene("Scores");
        }
    }
    public void RestartMinigame()
    {
        invoked1 = false;
        invoked2 = false;
        alreadyInvoked = false;
        CancelInvoke("LoadScores");
        CancelInvoke("Fade");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Start()
    {
        Lobby_Player[] playerz = FindObjectsOfType<Lobby_Player>();
        int minNum = 20;
        for (int i = 0; i < playerz.Length; i++)
        {
            if (playerz[i].joystick == null)
            {
                if (playerz[i].playerNumber < minNum)
                    minNum = playerz[i].playerNumber;
            }
        }currentNum = minNum;
        StartCoroutine(TestControllers());
    }
    public void LoadNextMinigame()
    {
        alreadyInvoked = false;
        alreadyInvoked2 = false;
        invoked1 = false;
        invoked2 = false;
        CancelInvoke("LoadScores");
        CancelInvoke("Fade");
        
        oldPlayers = (int[])players.ToArray().Clone();
        if (random == false)
        {
            try
            {

                    int num = UnityEngine.Random.Range(1, 100000) % 6;
                    while (num == 0)
                        num = UnityEngine.Random.Range(1, 100000) % 6;
                
                    SceneManager.LoadScene(minigames[sceneCount]+num);
                currentPointCount = 1;
                for (int i = 1; i <= playerObjectiveScores.Length - 1; i++)
                {
                    playerObjectiveScores[i] = 0;
                }
                sceneCount++;
                if (sceneCount > menuSceneCount)
                {
                    sceneCount = 0;
                    menuSceneCount = 0;
                    for (int i = 1; i <= 18; i++)
                    {
                        scores[i] = 0;
                    }
                    foreach (int player in players)
                    {
                        players.Remove(player);
                    }
                    SceneManager.LoadScene("Start");
                    Destroy(gameObject);
                }
            }
            catch
            {
                Lobby();
            }
        }
        else
        {
            currentPointCount = 1;
            randomIndex++;
            if(randomIndex>randomNumber)
            {
                Lobby();
            }
            else
            {
                int num = UnityEngine.Random.Range(1, 100000) % 6;
                while (num == 0)
                    num = UnityEngine.Random.Range(1, 100000) % 6;
                
                SceneManager.LoadScene(minigameNames[new System.Random().Next(1,1000000) % minigameNames.Length]+num);
            }
        }
        StopCoroutine(TestControllers());
        StartCoroutine(TestControllers());
    }
    public void Lobby()
    {
        invoked1 = false;
        invoked2 = false;
        alreadyInvoked = false;
        sceneCount = 0;
        menuSceneCount = 0;
        bluePoints = 0;
        redPoints = 0;
        for (int i = 1; i <= 18; i++)
        {
            scores[i] = 0;
        }
        CancelInvoke("LoadScores");
        SceneManager.LoadScene("NoAnimLobby"); 
    }
    int[] oldPlayers;
    public void AddBlue()
    {
        if(invoked1==false)
        {
            bluePoints++;
            invoked1 = true;
        }
    }
    public void AddRed()
    {
        if (invoked1 == false)
        {
            redPoints++;
            invoked1 = true;
        }
    }
    bool invoked1 = false;
    bool invoked2 = false;
    public void LoadPlayers()
    {
        alreadyInvoked = false;
        if (teams == true)
            FindObjectOfType<Lobby_TeamsActivate>().ActivateTeams();
        players.Clear();
        Lobby_Player[] lobbyPlayers = FindObjectsOfType<Lobby_Player>();
        foreach (Lobby_Player player in lobbyPlayers)
        {
            foreach (int playerNum in oldPlayers)
            {
                if (player.playerNumber == playerNum)
                {
                    if (bots[player.playerNumber] == false)
                    {
                        player.Join();
                        player.color1Index = color1Num[player.playerNumber];
                        player.color1.color = color1[player.playerNumber];
                        player.color2Index = color2Num[player.playerNumber];
                        player.color2.color = color2[player.playerNumber];
                        if (teams == true)
                        {
                            player.team = playerTeams[player.playerNumber];
                            if (playerTeams[player.playerNumber] == 0)
                                player.teamNumber.color = new Color32(0, 191, 255, 255);
                            else player.teamNumber.color = new Color32(255, 0, 0, 255);
                            player.teamNumber.text = "Team " + (playerTeams[player.playerNumber] + 1);
                        }
                        player.char1Index = char1Num[player.playerNumber];
                        player.char1Text.text = player.characters[player.char1Index].ToString();
                        player.char1 = player.characters[player.char1Index];
                        player.char2Index = char2Num[player.playerNumber];
                        player.char3Text.text = player.characters[player.char2Index].ToString();
                        player.char3 = player.characters[player.char3Index];
                        player.char3Index = char3Num[player.playerNumber];
                        player.char2Text.text = player.characters[player.char3Index].ToString();
                        player.char2 = player.characters[player.char2Index];
                        playerNames[player.playerNumber] = player.char1.ToString() + player.char2.ToString() + player.char3.ToString();
                    }
                    else
                    {
                        players.Add(player.playerNumber);
                        player.botObj.SetActive(false);
                        player.botText.SetActive(true);
                        player.bot = true;
                        Lobby_BotDifficulty botDiff = player.botText.GetComponentInChildren<Lobby_BotDifficulty>();
                        botDiff.difficulty = botDifficulties[player.playerNumber];
                                if (botDiff.difficulty == "easy")
                                {
                            botDiff.botDifText.text = "Easy";
                                }
                                else if (botDiff.difficulty == "normal")
                                {
                            botDiff.botDifText.text = "Normal";
                                }
                                else if (botDiff.difficulty == "hard")
                                {
                            botDiff.botDifText.text = "Hard";
                                }
                                else
                                {
                            botDiff.botDifText.text = "Expert";
                                }
                        if(PlayerPrefs.GetInt("FoundCustomize", 0) == 1)
                        {
                            foreach(Transform trans in botDiff.transform.parent)
                            {
                                if(trans.name.Contains("InputField"))
                                {
                                    trans.GetComponent<InputField>().text = botCustomize[player.playerNumber];
                                }
                            }
                        }
                        
                        if (teams == true)
                        {
                            player.team = playerTeams[player.playerNumber];
                            if (playerTeams[player.playerNumber] == 1)
                            {
                                player.team = 1; player.teamNumber.color = new Color32(255, 0, 0, 255); player.botTeam.GetComponent<Lobby_BotTeam>().team = 1;
                                player.botTeam.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                            }
                            else
                            {
                                player.team = 0; player.teamNumber.color = new Color32(0, 191, 255, 255); player.botTeam.GetComponent<Lobby_BotTeam>().team = 0;
                                player.botTeam.GetComponent<Image>().color = new Color32(0, 191, 255, 255);
                            }
                            player.teamNumber.text = "Team " + (playerTeams[player.playerNumber] + 1);
                        }
                    }
                }
            }
        }
    }
}
