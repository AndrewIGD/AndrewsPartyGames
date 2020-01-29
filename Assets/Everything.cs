using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Everything : MonoBehaviour
{
    public void ChangeScene()
    {
        FindObjectOfType<StartMenu_Button>().Lobby();
    }
}
