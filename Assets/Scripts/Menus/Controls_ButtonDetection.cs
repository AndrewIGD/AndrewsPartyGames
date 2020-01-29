using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpDX.DirectInput;
using UnityEngine.UI;

public class Controls_ButtonDetection : MonoBehaviour
{
    public Joystick joystick;
    public bool startDetection;
    public int button;
    public Text buttonText;
    public void StartDetection()
    {
        if(joystick != null)
            startDetection = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(joystick != null && startDetection)
        {
            buttonText.text = "Detecting buttton...";
            bool[] buttons = joystick.GetCurrentState().Buttons;
            for (int i=0;i<buttons.Length; i++)
            {
                if(buttons[i])
                {
                    startDetection = false;
                    button = i;
                    buttonText.text = "Button " + i;
                }
            }
        }
    }
}
