using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls_Clear : MonoBehaviour
{
    public GameObject content;
    public void Clear()
    {
        foreach(GameObject obj in FindObjectsOfType<GameObject>())
        {
            if(obj.transform.parent == content.transform)
            {
                Destroy(obj);
            }
        }
        int deviceNumber = PlayerPrefs.GetInt("DeviceNumber", 0);
        for(int i=1;i<=deviceNumber;i++)
        {
            PlayerPrefs.DeleteKey("DeviceName" + deviceNumber);
            PlayerPrefs.DeleteKey("Button1" + deviceNumber);
            PlayerPrefs.DeleteKey("Button2" + deviceNumber);
            PlayerPrefs.SetInt("DeviceNumber", 0);
        }
    }
}
