using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mutator : MonoBehaviour
{
    public InputField inputField;
    public string mutatedVariable;
    public float defaultValue;
    // Start is called before the first frame update
    void Start()
    {
        inputField.text = PlayerPrefs.GetFloat(mutatedVariable, defaultValue).ToString();
    }
    public void ResetMutation()
    {
        PlayerPrefs.SetFloat(mutatedVariable, defaultValue);
        inputField.text = defaultValue.ToString();
    }
    public void SaveMutation()
    {
        try
        {
            PlayerPrefs.SetFloat(mutatedVariable, float.Parse(inputField.text));

        }
        catch
        {

        }
    }
}
