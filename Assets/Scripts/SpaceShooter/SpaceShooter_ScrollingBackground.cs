using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooter_ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed;
    Material material;
    Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector2(0f, scrollSpeed);
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
