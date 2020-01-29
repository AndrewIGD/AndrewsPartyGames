using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    public Color32[] colors;
    float t = 0;
    int index = 0;
    Color32 previousColor;
    // Start is called before the first frame update
    void Start()
    {
        previousColor = new Color32(255, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        GetComponent<Image>().color = Color32.Lerp(previousColor, colors[index], t);
        if(t>=1)
        {
            previousColor = colors[index];
            index++;
            if (index >= colors.Length)
                index = 0;
            t = 0;
        }
    }
}
