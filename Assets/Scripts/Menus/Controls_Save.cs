using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controls_Save : MonoBehaviour
{
    public Controls_ButtonDetection button1;
    public Controls_ButtonDetection button2;
    public Text productName;
    public GameObject deviceObject;
    public GameObject content;
    public void Save()
    {
        if (productName.text != "No Device Selected")
        {
            bool ok = true;
            Controls_DevicePress[] devices = FindObjectsOfType<Controls_DevicePress>();
            foreach (Controls_DevicePress device in devices)
            {
                if (device.saved == true && device.productName == productName.text)
                {
                    ok = false;
                    device.GetComponentInChildren<Controls_DevicePress>().button1Num = button1.button;
                    device.GetComponentInChildren<Controls_DevicePress>().button2Num = button2.button;
                    Debug.Log(device.transform.GetSiblingIndex());
                    int deviceNumber = device.GetComponentInChildren<Controls_DevicePress>().index;
                    PlayerPrefs.SetInt("Button1" + deviceNumber, button1.button);
                    PlayerPrefs.SetInt("Button2" + deviceNumber, button2.button);
                    PlayerPrefs.SetInt(productName.text + "1", button1.button);
                    PlayerPrefs.SetInt(productName.text + "2", button2.button);
                }
            }
            if (ok == true)
            {
                GameObject deviceObj = Instantiate(deviceObject);
                deviceObj.transform.SetParent(content.transform);
                deviceObj.GetComponentInChildren<TextMeshProUGUI>().text = productName.text;
                deviceObj.GetComponentInChildren<Controls_DevicePress>().button1Num = button1.button;
                deviceObj.GetComponentInChildren<Controls_DevicePress>().button2Num = button2.button;
                deviceObj.GetComponentInChildren<Controls_DevicePress>().productName = productName.text;
                deviceObj.GetComponentInChildren<Controls_DevicePress>().saved = true;
                if (productName.text == "Keyboard #1" || productName.text == "Keyboard #2")
                {
                    deviceObj.GetComponentInChildren<Controls_DevicePress>().keyboard = true;
                    deviceObj.GetComponentInChildren<Controls_DevicePress>().keyboardName = productName.text;
                }
                deviceObj.GetComponentInChildren<Controls_DevicePress>().button1 = FindObjectOfType<Controls_DeviceDetection>().button1;
                deviceObj.GetComponentInChildren<Controls_DevicePress>().button2 = FindObjectOfType<Controls_DeviceDetection>().button2;
                deviceObj.GetComponentInChildren<Controls_DevicePress>().currentDeviceText = FindObjectOfType<Controls_DeviceDetection>().currentDeviceText;
                int deviceIndex = 0;
                foreach (Controls_DevicePress device in FindObjectsOfType<Controls_DevicePress>())
                {
                    if (device.saved == true)
                        deviceIndex++;
                }
                deviceObj.GetComponentInChildren<Controls_DevicePress>().index = deviceIndex;
                int deviceNumber = PlayerPrefs.GetInt("DeviceNumber", 0) + 1;
                PlayerPrefs.SetInt("DeviceNumber", deviceNumber);
                PlayerPrefs.SetString("DeviceName" + deviceNumber, productName.text);
                PlayerPrefs.SetInt("Button1" + deviceNumber, button1.button);
                PlayerPrefs.SetInt("Button2" + deviceNumber, button2.button);
                PlayerPrefs.SetInt(productName.text + "1", button1.button);
                PlayerPrefs.SetInt(productName.text + "2", button2.button);
                deviceObj.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
