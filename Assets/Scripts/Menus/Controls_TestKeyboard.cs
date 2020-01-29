using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpDX.DirectInput;

public class Controls_TestKeyboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var di = new DirectInput();
        if (di.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly).Count < 1)
            Destroy(gameObject);
    }
}
