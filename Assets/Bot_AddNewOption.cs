using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_AddNewOption : MonoBehaviour
{
    public GameObject newOption;
    public GameObject canvas;
    public int currentHeight;
    public int distanceBetweenObj;
    public int currentNum = 1;
    public int grow;
    public void AddNewOption()
    {
        GameObject option = Instantiate(newOption);
        option.transform.SetParent(canvas.transform);
        option.GetComponent<RectTransform>().localPosition = new Vector2(-400, currentHeight);
        currentHeight -= distanceBetweenObj;
        GetComponent<RectTransform>().localPosition = new Vector2(0, option.GetComponent<RectTransform>().localPosition.y - distanceBetweenObj*2);
        option.GetComponent<OptionNum>().numText.text = "Option " + currentNum + ":";
        currentNum++;
        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(3000, canvas.GetComponent<RectTransform>().sizeDelta.y + grow);
        foreach (Transform transf in canvas.transform)
            transf.localPosition = new Vector2(transf.localPosition.x, transf.localPosition.y + distanceBetweenObj);
        if(currentNum>2)
        canvas.transform.localPosition = new Vector2(0, -transform.localPosition.y);
        PlayerPrefs.SetInt("BotCustomizedSettings", currentNum);
        
    }
    private void Awake()
    {
        for(int i=1;i<PlayerPrefs.GetInt("BotCustomizedSettings", 0);i++)
        {
            GameObject option = Instantiate(newOption);
            option.transform.SetParent(canvas.transform);
            option.GetComponent<RectTransform>().localPosition = new Vector2(-400, currentHeight);
            currentHeight -= distanceBetweenObj;
            GetComponent<RectTransform>().localPosition = new Vector2(0, option.GetComponent<RectTransform>().localPosition.y - distanceBetweenObj * 2);
            option.GetComponent<OptionNum>().numText.text = "Option " + currentNum + ":";
            currentNum++;
            canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(3000, canvas.GetComponent<RectTransform>().sizeDelta.y + grow);
            foreach (Transform transf in canvas.transform)
                transf.localPosition = new Vector2(transf.localPosition.x, transf.localPosition.y + distanceBetweenObj);
            if (currentNum > 2)
                canvas.transform.localPosition = new Vector2(0, -transform.localPosition.y);
        }
    }
}
