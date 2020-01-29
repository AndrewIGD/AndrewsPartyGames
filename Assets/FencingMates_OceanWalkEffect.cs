using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencingMates_OceanWalkEffect : MonoBehaviour
{
    public float growModifier;
    public float decolorModifier;
    public bool active = false;
    float t = 0;
    void Update()
    {
        if (active)
        {
            t += Time.deltaTime * decolorModifier;
            if (t >= 1)
                Destroy(gameObject);
            transform.localScale = new Vector3(transform.localScale.x + growModifier * Time.deltaTime, transform.localScale.x + growModifier * Time.deltaTime, 1);
            GetComponent<SpriteRenderer>().color = Color32.Lerp(new Color32(255, 255, 255, 255), new Color32(255, 255, 255, 0), t);
        }

    }
}
