using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SharpDX.DirectInput;

public class Controls_DevicePress : MonoBehaviour
{
    public Text currentDeviceText;
    public string productName;
    public Joystick joystick;
    public Controls_ButtonDetection button1;
    public Controls_ButtonDetection button2;
    public bool keyboard = false;
    public string keyboardName;
    public bool saved;
    public int button1Num;
    public int button2Num;
    public int index;
    public void SelectDevice()
    {

        if (saved == true)
        {
            if (!keyboard)
            {
                bool ok = false;
                DirectInput di = new DirectInput();
                foreach (var device in di.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly))
                {
                    if (device.ProductName == productName)
                    {
                        joystick = new Joystick(di, device.InstanceGuid);
                        joystick.Acquire();
                        ok = true;
                    }
                }

                if (ok == true)
                {
                    button1.joystick = joystick;
                    button2.joystick = joystick;
                    button1.button = button1Num;
                    button2.button = button2Num;
                    button1.buttonText.text = "Button " + button1Num;
                    button2.buttonText.text = "Button " + button2Num;
                    currentDeviceText.text = productName;
                }
            }
            else
            {
                    DirectInput di = new DirectInput();
                    foreach (var device in di.GetDevices(SharpDX.DirectInput.DeviceType.Keyboard, DeviceEnumerationFlags.AttachedOnly))
                    {
                        joystick = new Joystick(di, device.InstanceGuid);
                        joystick.Acquire();
                    }
                if (joystick != null)
                {
                    currentDeviceText.text = keyboardName;
                    button1.joystick = joystick;
                    button2.joystick = joystick;
                    button1.button = button1Num;
                    button2.button = button2Num;
                    button1.buttonText.text = "Button " + button1Num;
                    button2.buttonText.text = "Button " + button2Num;
                }
            }
        }
        else
        {
            Controls_DevicePress[] devices = FindObjectsOfType<Controls_DevicePress>();
            bool ok = true;
            foreach (Controls_DevicePress device in devices)
            {
                if (device.saved == true && device.productName == productName)
                {
                    ok = false;
                }
            }
            if (ok == true)
            {
                button1.buttonText.text = "No button";
                button2.buttonText.text = "No button";
                if (!keyboard)
                {
                    currentDeviceText.text = productName;
                    button1.joystick = joystick;
                    button2.joystick = joystick;
                }
                else
                {
                    currentDeviceText.text = keyboardName;
                    DirectInput di = new DirectInput();
                    foreach (var device in di.GetDevices(SharpDX.DirectInput.DeviceType.Keyboard, DeviceEnumerationFlags.AttachedOnly))
                    {
                        joystick = new Joystick(di, device.InstanceGuid);
                        joystick.Acquire();
                    }
                    button1.joystick = joystick;
                    button2.joystick = joystick;
                }
            }
        }
    }
}
