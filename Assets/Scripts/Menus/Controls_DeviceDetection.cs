using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpDX.DirectInput;
using TMPro;
using UnityEngine.UI;

public class Controls_DeviceDetection : MonoBehaviour
{
    public GameObject content;
    public GameObject deviceObject;
    public Controls_ButtonDetection button1;
    public Controls_ButtonDetection button2;
    public Text currentDeviceText;
    void Start()
    {
        var di = new DirectInput();
        foreach (var device in di.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly))
        {
            GameObject deviceObj = Instantiate(deviceObject);
            deviceObj.transform.SetParent(content.transform);
            deviceObj.GetComponentInChildren<TextMeshProUGUI>().text = device.ProductName;
            deviceObj.GetComponentInChildren<Controls_DevicePress>().joystick = new Joystick(di, device.InstanceGuid);
            deviceObj.GetComponentInChildren<Controls_DevicePress>().productName = device.ProductName;
            deviceObj.GetComponentInChildren<Controls_DevicePress>().currentDeviceText = currentDeviceText;
            deviceObj.GetComponentInChildren<Controls_DevicePress>().button1 = button1;
            deviceObj.GetComponentInChildren<Controls_DevicePress>().button2 = button2;
            deviceObj.GetComponentInChildren<Controls_DevicePress>().joystick.Acquire();
            deviceObj.transform.localScale = new Vector3(1, 1, 1);
        }
        foreach (var device in di.GetDevices(SharpDX.DirectInput.DeviceType.Joystick, DeviceEnumerationFlags.AttachedOnly))
        {
            GameObject deviceObj = Instantiate(deviceObject);
            deviceObj.transform.SetParent(content.transform);
            deviceObj.GetComponentInChildren<TextMeshProUGUI>().text = device.ProductName;
            deviceObj.GetComponentInChildren<Controls_DevicePress>().joystick = new Joystick(di, device.InstanceGuid);
            deviceObj.GetComponentInChildren<Controls_DevicePress>().productName = device.ProductName;
            deviceObj.GetComponentInChildren<Controls_DevicePress>().currentDeviceText = currentDeviceText;
            deviceObj.GetComponentInChildren<Controls_DevicePress>().button1 = button1;
            deviceObj.GetComponentInChildren<Controls_DevicePress>().button2 = button2;
            deviceObj.GetComponentInChildren<Controls_DevicePress>().joystick.Acquire();
            deviceObj.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
