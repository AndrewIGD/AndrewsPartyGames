using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controls_Load : MonoBehaviour
{
    public GameObject content;
    public GameObject deviceObject;
    public Controls_ButtonDetection button1;
    public Controls_ButtonDetection button2;
    public Text currentDeviceText;
    // Start is called before the first frame update
    void Start()
    {
        int devicesIndex = PlayerPrefs.GetInt("DeviceNumber", 0);
        for(int i=1;i<=devicesIndex;i++)
        {
            GameObject deviceObj = Instantiate(deviceObject);
            deviceObj.transform.SetParent(content.transform);
            deviceObj.GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetString("DeviceName" + i, "");
            deviceObj.GetComponentInChildren<Controls_DevicePress>().productName = PlayerPrefs.GetString("DeviceName" + i, "");
            deviceObj.GetComponentInChildren<Controls_DevicePress>().currentDeviceText = currentDeviceText;
            deviceObj.GetComponentInChildren<Controls_DevicePress>().button1 = button1;
            deviceObj.GetComponentInChildren<Controls_DevicePress>().button2 = button2;
            deviceObj.GetComponentInChildren<Controls_DevicePress>().saved = true;
            if (PlayerPrefs.GetString("DeviceName" + i, "") == "Keyboard #1" || PlayerPrefs.GetString("DeviceName" + i, "") == "Keyboard #2")
            {
                deviceObj.GetComponentInChildren<Controls_DevicePress>().keyboard = true;
                deviceObj.GetComponentInChildren<Controls_DevicePress>().keyboardName = PlayerPrefs.GetString("DeviceName" + i, "");
            }
            deviceObj.GetComponentInChildren<Controls_DevicePress>().button1Num = PlayerPrefs.GetInt("Button1" + i, 0);
            deviceObj.GetComponentInChildren<Controls_DevicePress>().button2Num = PlayerPrefs.GetInt("Button2" + i, 0);
            int deviceIndex = 0;
            foreach (Controls_DevicePress device in FindObjectsOfType<Controls_DevicePress>())
            {
                if (device.saved == true)
                    deviceIndex++;
            }
            deviceObj.GetComponentInChildren<Controls_DevicePress>().index = deviceIndex;
            deviceObj.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
