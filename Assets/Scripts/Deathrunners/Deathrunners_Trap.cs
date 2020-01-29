using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathrunners_Trap : MonoBehaviour
{
    public Sprite sprite;
    public bool executed = false;
    public int type;
    public GameObject blockToAppear;
    public Vector2 arrowSpeed;
    public AudioClip trap;
    float arrowSpeedMultiplier = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ParkourMasters_Player>() != null)
            ExecuteTrap();
    }
    private void Start()
    {
        arrowSpeedMultiplier = PlayerPrefs.GetFloat("DeathrunnersArrowSpeedMultiplier", arrowSpeedMultiplier);
    }
    public void ExecuteTrap()
    {
        if (executed == false)
        {
            
            if(type == 0)
            {
                blockToAppear.SetActive(true);
                Invoke("Disappear", 1.5f);
            }
            else if (type == 1)
            {
                blockToAppear.SetActive(true);
            }
            else if (type == 2)
            {
                blockToAppear.GetComponent<Rigidbody2D>().velocity = arrowSpeed*arrowSpeedMultiplier;
            }
            executed = true;
            GetComponent<SpriteRenderer>().sprite = sprite;
            AudioSource.PlayClipAtPoint(trap, Camera.main.transform.position);

        }
    }
    public void Disappear()
    {
        blockToAppear.SetActive(false);
    }
}
