using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathrunners_Blinking : MonoBehaviour
{
    public float timeBetweenBlinks;
    public bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenBlinks = PlayerPrefs.GetFloat("DeathrunnersTimeBetweenBlinks", timeBetweenBlinks);
        Invoke("Blink", timeBetweenBlinks);
    }
    private void Blink()
    {
        active = !active;
        GetComponent<SpriteRenderer>().enabled = active;
        GetComponent<BoxCollider2D>().enabled = active;
        foreach (Transform child in transform)
            child.gameObject.SetActive(active);
        Invoke("Blink", timeBetweenBlinks);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
