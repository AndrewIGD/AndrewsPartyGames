using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SharpDX.DirectInput;

public class Lobby_Player : MonoBehaviour
{
    public GameObject UI;
    public Image color1;
    public Image color2;
    public Image selected1;
    public Image selected2;
    public Image selected3;
    public Text teamNumber;
    public Color32[] colors;
    public int playerNumber;
    public bool joined = false;
    public int color1Index = 0;
    public int color2Index = 0;
    Vector2 movement;
    bool action1;
    bool action2;
    public bool teams = false;
    public int team;
    public int selected = 0;
    public bool opposite = false;
    public char[] characters;
    public char char1;
    public char char2;
    public char char3;
    public int char1Index;
    public int char2Index;
    public int char3Index;
    public Text char1Text;
    public Text char2Text;
    public Text char3Text;
    public Image char1Bg;
    public Image char2Bg;
    public Image char3Bg;
    public int button1;
    public int button2;
    public bool keyboard;
    int sus;
    int jos;
    int dreapta;
    int stanga;
    public GameObject botObj;
    public GameObject botText;
    public bool bot;
    public AudioClip joinSound;


    public SharpDX.DirectInput.Joystick joystick;
    public List<SharpDX.DirectInput.Joystick> controllers;

    int defaultAxis;

    private void Awake()
    {
        controllers = new List<SharpDX.DirectInput.Joystick>();
        var joystickGuid = Guid.Empty;
        var di = new DirectInput();
        IList<DeviceInstance> keyboards = di.GetDevices(SharpDX.DirectInput.DeviceType.Keyboard, DeviceEnumerationFlags.AttachedOnly);
        for(int device=0;device<keyboards.Count;device++)
        {
            joystickGuid = keyboards[device].InstanceGuid;
            controllers.Add(new Joystick(di, joystickGuid));
        }
        IList<DeviceInstance> gamepads = di.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly);
        for (int device = 0; device < gamepads.Count; device++)
        {
            joystickGuid = gamepads[device].InstanceGuid;
            controllers.Add(new Joystick(di, joystickGuid));
        }
        IList<DeviceInstance> joysticks = di.GetDevices(SharpDX.DirectInput.DeviceType.Joystick, DeviceEnumerationFlags.AttachedOnly);
        for (int device = 0; device < joysticks.Count; device++)
        {
            joystickGuid = joysticks[device].InstanceGuid;
            controllers.Add(new Joystick(di, joystickGuid));
        }
        if (playerNumber <= 2)
        {
            joystick = controllers[0];

            joystick.Acquire();
            if (playerNumber == 1)
            {
                button1 = PlayerPrefs.GetInt("Keyboard #1" + "1", 33);
                button2 = PlayerPrefs.GetInt("Keyboard #1" + "2", 34);
                dreapta = 31;
                stanga = 29;
                jos = 30;
                sus = 16;
            }
            else
            {
                button1 = PlayerPrefs.GetInt("Keyboard #2" + "1", 50);
                button2 = PlayerPrefs.GetInt("Keyboard #2" + "2", 51);
                dreapta = 107;
                stanga = 106;
                jos = 109;
                sus = 104;
            }
        }
        else if (playerNumber <= controllers.Count + 1)
        {
            joystick = controllers[playerNumber - 2];
            joystick.Acquire();
            button1 = PlayerPrefs.GetInt(joystick.Information.ProductName + "1", 0);
            button2 = PlayerPrefs.GetInt(joystick.Information.ProductName + "2", 2);
            Debug.Log(joystick.Properties.InterfacePath);
        }
        FindObjectOfType<PlayerData>().button1[playerNumber] = button1;
        FindObjectOfType<PlayerData>().button2[playerNumber] = button2;
        if(joystick != null)
            defaultAxis = joystick.GetCurrentState().X;
        teamNumber.text = "Team " + (team + 1);
        if (playerNumber >= 10)
            opposite = true;
        teamNumber.gameObject.SetActive(false);
        selected2.gameObject.SetActive(false);
        selected3.gameObject.SetActive(false);
        char1Bg.gameObject.SetActive(false);
        char2Bg.gameObject.SetActive(false);
        char3Bg.gameObject.SetActive(false);
        char1 = characters[0];
        char2 = characters[0];
        char3 = characters[0];
        char1Text.text = char1.ToString();
        char2Text.text = char2.ToString();
        char3Text.text = char3.ToString();
        FindObjectOfType<PlayerData>().playerNames[playerNumber] = char1.ToString() + char2.ToString() + char3.ToString();
    }
    public void Action1()
    {
        if (action1 == false)
        {
            action1 = true;
            if (action2)
            {
                color1Index = 0;
                color2Index = 0;
                color1.color = colors[0];
                color2.color = colors[0];
                FindObjectOfType<PlayerData>().color1[playerNumber] = color1.color;
                FindObjectOfType<PlayerData>().color2[playerNumber] = color2.color;
                UI.SetActive(false);
                joined = false;
                botObj.SetActive(true);
                FindObjectOfType<PlayerData>().players.Remove(playerNumber);
                return;
            }
            if (joined == false)
            {
                Join();
            }
            else if (selected == 0)
            {
                color1Index++;
                if (color1Index >= colors.Length)
                    color1Index = 0;
                color1.color = colors[color1Index];
                FindObjectOfType<PlayerData>().color1Num[playerNumber] = color1Index;
                FindObjectOfType<PlayerData>().color1[playerNumber] = color1.color;
            }
            else if (selected == 1)
            {
                color2Index++;
                if (color2Index >= colors.Length)
                    color2Index = 0;
                color2.color = colors[color2Index];
                FindObjectOfType<PlayerData>().color2Num[playerNumber] = color2Index;
                FindObjectOfType<PlayerData>().color2[playerNumber] = color2.color;
            }
            else if (selected == 2)
            {
                if (team == 0)
                { team = 1; teamNumber.color = new Color32(255, 0, 0, 255); botTeam.GetComponent<Lobby_BotTeam>().team = 1;
                    botTeam.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                }
                else { team = 0; teamNumber.color = new Color32(0, 191, 255, 255); botTeam.GetComponent<Lobby_BotTeam>().team = 0;
                    botTeam.GetComponent<Image>().color = new Color32(0, 191, 255, 255);
                }
                teamNumber.text = "Team " + (team + 1);
                FindObjectOfType<PlayerData>().playerTeams[playerNumber] = team;
            }
            else if (selected == 3)
            {
                char1Index++;
                if (char1Index > 25)
                    char1Index = 0;
                char1 = characters[char1Index];
                char1Text.text = char1.ToString();
                FindObjectOfType<PlayerData>().char1Num[playerNumber] = char1Index;
                FindObjectOfType<PlayerData>().playerNames[playerNumber] = char1.ToString() + char2.ToString() + char3.ToString();
            }
            else if (selected == 4)
            {
                char2Index++;
                if (char2Index > 25)
                    char2Index = 0;
                char2 = characters[char2Index];
                char3Text.text = char2.ToString();
                FindObjectOfType<PlayerData>().char2Num[playerNumber] = char2Index;
                FindObjectOfType<PlayerData>().playerNames[playerNumber] = char1.ToString() + char2.ToString() + char3.ToString();

            }
            else if (selected == 5)
            {
                char3Index++;
                if (char3Index > 25)
                    char3Index = 0;
                char3 = characters[char3Index];
                char2Text.text = char3.ToString();
                FindObjectOfType<PlayerData>().char3Num[playerNumber] = char3Index;
                FindObjectOfType<PlayerData>().playerNames[playerNumber] = char1.ToString() + char2.ToString() + char3.ToString();
            }
        }
    }
    public float timeTillScene = 0;
    public void Join()
    {
        if (timeTillScene >= 1)
        {
            UI.SetActive(true);
            joined = true;
            if(bot == false)
                FindObjectOfType<PlayerData>().players.Add(playerNumber);
            botObj.SetActive(false);
            botText.SetActive(false);
            bot = false;
            FindObjectOfType<PlayerData>().bots[playerNumber] = false;
        }
    }

    private void CancelAction1()
    {
        action1 = false;
    }
    private void CancelAction2()
    {
        action2 = false;
    }
    private void Action2()
    {
        if (action2 == false)
        {
            action2 = true;
            if (action1)
            {
                color1Index = 0;
                color2Index = 0;
                color1.color = colors[0];
                color2.color = colors[0];
                FindObjectOfType<PlayerData>().color1[playerNumber] = color1.color;
                FindObjectOfType<PlayerData>().color2[playerNumber] = color2.color;
                UI.SetActive(false);
                joined = false;
                botObj.SetActive(true);
                FindObjectOfType<PlayerData>().players.Remove(playerNumber);
                return;
            }
            if (joined == true)
            {
                if (selected == 0)
                {
                    color1Index--;
                    if (color1Index < 0)
                        color1Index = colors.Length - 1;
                    color1.color = colors[color1Index];
                    FindObjectOfType<PlayerData>().color1Num[playerNumber] = color1Index;
                    FindObjectOfType<PlayerData>().color1[playerNumber] = color1.color;
                }
                else if (selected == 1)
                {
                    color2Index--;
                    if (color2Index < 0)
                        color2Index = colors.Length - 1;
                    color2.color = colors[color2Index];
                    FindObjectOfType<PlayerData>().color2Num[playerNumber] = color2Index;
                    FindObjectOfType<PlayerData>().color2[playerNumber] = color2.color;
                }
                else if (selected == 2)
                {
                    if (team == 0)
                    {
                        team = 1; teamNumber.color = new Color32(255, 0, 0, 255); botTeam.GetComponent<Lobby_BotTeam>().team = 1;
                        botTeam.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                    }
                    else
                    {
                        team = 0; teamNumber.color = new Color32(0, 191, 255, 255); botTeam.GetComponent<Lobby_BotTeam>().team = 0;
                        botTeam.GetComponent<Image>().color = new Color32(0, 191, 255, 255);
                    }
                    teamNumber.text = "Team " + (team+1);
                    FindObjectOfType<PlayerData>().playerTeams[playerNumber] = team;
                    botTeam.GetComponent<Lobby_BotTeam>().ChangeTeam();
                }
                else if (selected == 3)
                {
                    char1Index--;
                    if (char1Index < 0)
                        char1Index = 25;
                    char1 = characters[char1Index];
                    char1Text.text = char1.ToString();
                    FindObjectOfType<PlayerData>().char1Num[playerNumber] = char1Index;
                    FindObjectOfType<PlayerData>().playerNames[playerNumber] = char1.ToString() + char2.ToString() + char3.ToString();
                }
                else if (selected == 4)
                {
                    char2Index--;
                    if (char2Index < 0)
                        char2Index = 25;
                    char2 = characters[char2Index];
                    char3Text.text = char2.ToString();
                    FindObjectOfType<PlayerData>().char2Num[playerNumber] = char2Index;
                    FindObjectOfType<PlayerData>().playerNames[playerNumber] = char1.ToString() + char2.ToString() + char3.ToString();
                }
                else if (selected == 5)
                {
                    char3Index--;
                    if (char3Index < 0)
                        char3Index = 25;
                    char3 = characters[char3Index];
                    char2Text.text = char3.ToString();
                    FindObjectOfType<PlayerData>().char3Num[playerNumber] = char3Index;
                    FindObjectOfType<PlayerData>().playerNames[playerNumber] = char1.ToString() + char2.ToString() + char3.ToString();
                }
            }
        }
    }
    private void Start()
    {
        if (joystick != null)
        {
            if (joystick.Information.Type == SharpDX.DirectInput.DeviceType.Keyboard)
            {
                keyboard = true;
            }
            FindObjectOfType<PlayerData>().controllers[playerNumber] = joystick;
        }
    }
    bool axis = false;
    void Update()
    {
        if (FindObjectOfType<PlayerData>() != null)
        {
            if (joystick != null)
            {
                bool ret = false;
                if (bot == false)
                {

                    try
                    {
                        joystick.Poll();
                    }
                    catch
                    {

                        joystick = FindObjectOfType<PlayerData>().controllers[playerNumber];
                        try
                        {
                            joystick.Poll();
                        }
                        catch
                        {
                            FindObjectOfType<PlayerData>().pendingControllers[playerNumber] = true;
                        }
                        button1 = FindObjectOfType<PlayerData>().button1[playerNumber];
                        button2 = FindObjectOfType<PlayerData>().button2[playerNumber];
                        ret = true;
                        color1Index = 0;
                        color2Index = 0;
                        color1.color = colors[0];
                        color2.color = colors[0];
                        FindObjectOfType<PlayerData>().color1[playerNumber] = color1.color;
                        FindObjectOfType<PlayerData>().color2[playerNumber] = color2.color;
                        UI.SetActive(false);
                        joined = false;
                        botObj.SetActive(true);
                        FindObjectOfType<PlayerData>().players.Remove(playerNumber);
                        return;
                    }

                }
                if (ret)
                    return;
            }
            else
            {
                if (FindObjectOfType<PlayerData>().controllers[playerNumber] != null)
                {
                    joystick = FindObjectOfType<PlayerData>().controllers[playerNumber];
                    try
                    {
                        joystick.Poll();
                    }
                    catch
                    {
                        FindObjectOfType<PlayerData>().pendingControllers[playerNumber] = true;
                    }
                    button1 = FindObjectOfType<PlayerData>().button1[playerNumber];
                    button2 = FindObjectOfType<PlayerData>().button2[playerNumber];
                }
            }
        }
        
        timeTillScene += Time.deltaTime;
            if (joystick != null)
            {
                if (CustomControls.GetButton(joystick, button1))
                {
                    Action1();
                }
                else CancelAction1();
                if (CustomControls.GetButton(joystick, button2))
                {
                    Action2();
                }
                else CancelAction2();
                if (keyboard)
                {
                    if (CustomControls.GetButton(joystick, dreapta))
                    {
                        if (axis == false)
                        {
                            axis = true;
                            if (opposite == false)
                            {
                                SelectDreapta();
                            }
                            else SelectStanga();
                        }
                    }
                    else if (CustomControls.GetButton(joystick, stanga))
                    {
                        if (axis == false)
                        {
                            axis = true;
                            if (opposite == false)
                            {
                                SelectStanga();
                            }
                            else SelectDreapta();
                        }
                    }
                    else axis = false;
                }
                else
                {
                    if (CustomControls.GetAxis(joystick).Xaxis > 40000)
                    {
                        if (axis == false)
                        {
                            axis = true;
                            if (opposite == false)
                            {
                                SelectDreapta();
                            }
                            else SelectStanga();
                        }
                    }
                    else if (CustomControls.GetAxis(joystick).Xaxis < 20000)
                    {
                        if (axis == false)
                        {
                            axis = true;
                            if (opposite == false)
                            {
                                SelectStanga();
                            }
                            else SelectDreapta();
                        }
                    }
                    else axis = false;
                }
            }
        
    }

    public void SelectStanga()
    {
        axis = true;
        if (selected == 3 && teams == false)
            selected = 1;
        else selected--;
        if (selected < 0)
            selected = 0;
        if (selected == 0)
        {
            selected1.gameObject.SetActive(true);
            selected2.gameObject.SetActive(false);
            selected3.gameObject.SetActive(false);
            char1Bg.gameObject.SetActive(false);
            char2Bg.gameObject.SetActive(false);
            char3Bg.gameObject.SetActive(false);
        }
        else if (selected == 1)
        {
            selected1.gameObject.SetActive(false);
            selected2.gameObject.SetActive(true);
            selected3.gameObject.SetActive(false);
            char1Bg.gameObject.SetActive(false);
            char2Bg.gameObject.SetActive(false);
            char3Bg.gameObject.SetActive(false);
        }
        else if (selected == 2)
        {
            selected1.gameObject.SetActive(false);
            selected2.gameObject.SetActive(false);
            selected3.gameObject.SetActive(true);
            char1Bg.gameObject.SetActive(false);
            char2Bg.gameObject.SetActive(false);
            char3Bg.gameObject.SetActive(false);
        }
        else if (selected == 3)
        {
            selected1.gameObject.SetActive(false);
            selected2.gameObject.SetActive(false);
            selected3.gameObject.SetActive(false);
            char1Bg.gameObject.SetActive(true);
            char2Bg.gameObject.SetActive(false);
            char3Bg.gameObject.SetActive(false);
        }
        else if (selected == 4)
        {
            selected1.gameObject.SetActive(false);
            selected2.gameObject.SetActive(false);
            selected3.gameObject.SetActive(false);
            char1Bg.gameObject.SetActive(false);
            char2Bg.gameObject.SetActive(true);
            char3Bg.gameObject.SetActive(false);
        }
        else if (selected == 5)
        {
            selected1.gameObject.SetActive(false);
            selected2.gameObject.SetActive(false);
            selected3.gameObject.SetActive(false);
            char1Bg.gameObject.SetActive(false);
            char2Bg.gameObject.SetActive(false);
            char3Bg.gameObject.SetActive(true);
        }
    }

    public void SelectDreapta()
    {
        axis = true;
        if (selected == 1 && teams == false)
            selected = 3;
        else selected++;
        if (selected > 5)
            selected = 5;
        if (selected == 0)
        {
            selected1.gameObject.SetActive(true);
            selected2.gameObject.SetActive(false);
            selected3.gameObject.SetActive(false);
            char1Bg.gameObject.SetActive(false);
            char2Bg.gameObject.SetActive(false);
            char3Bg.gameObject.SetActive(false);
        }
        else if (selected == 1)
        {
            selected1.gameObject.SetActive(false);
            selected2.gameObject.SetActive(true);
            selected3.gameObject.SetActive(false);
            char1Bg.gameObject.SetActive(false);
            char2Bg.gameObject.SetActive(false);
            char3Bg.gameObject.SetActive(false);
        }
        else if (selected == 2)
        {
            selected1.gameObject.SetActive(false);
            selected2.gameObject.SetActive(false);
            selected3.gameObject.SetActive(true);
            char1Bg.gameObject.SetActive(false);
            char2Bg.gameObject.SetActive(false);
            char3Bg.gameObject.SetActive(false);
        }
        else if (selected == 3)
        {
            selected1.gameObject.SetActive(false);
            selected2.gameObject.SetActive(false);
            selected3.gameObject.SetActive(false);
            char1Bg.gameObject.SetActive(true);
            char2Bg.gameObject.SetActive(false);
            char3Bg.gameObject.SetActive(false);
        }
        else if (selected == 4)
        {
            selected1.gameObject.SetActive(false);
            selected2.gameObject.SetActive(false);
            selected3.gameObject.SetActive(false);
            char1Bg.gameObject.SetActive(false);
            char2Bg.gameObject.SetActive(true);
            char3Bg.gameObject.SetActive(false);
        }
        else if (selected == 5)
        {
            selected1.gameObject.SetActive(false);
            selected2.gameObject.SetActive(false);
            selected3.gameObject.SetActive(false);
            char1Bg.gameObject.SetActive(false);
            char2Bg.gameObject.SetActive(false);
            char3Bg.gameObject.SetActive(true);
        }
    }
    public GameObject botTeam;
}
