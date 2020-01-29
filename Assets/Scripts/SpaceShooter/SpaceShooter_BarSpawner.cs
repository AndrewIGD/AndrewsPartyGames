using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooter_BarSpawner : MonoBehaviour
{
    public GameObject bar;
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;
    public AudioSource barWoosh;
    public float barDestruct1 = 2.5f;
    public float barDestruct2 = 1.5f;
    public float speed1 = 7.5f;
    public float speed2 = 3f;
    public float timeBetweenSpawns = 1.5f;
    public bool changeScale = true;
    float barVerSpeed=7.5f;
    float barHorSpeed=3f;
    float barMin=0.05f;
    float barMax=0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
        if (changeScale)
        {
            barVerSpeed = PlayerPrefs.GetFloat("BarVerticalSpeed", barVerSpeed);
            barHorSpeed = PlayerPrefs.GetFloat("BarHorizontalSpeed", barHorSpeed);
            timeBetweenSpawns = PlayerPrefs.GetFloat("BarSpawnFrequency", timeBetweenSpawns);
        }
        else
        {
            barVerSpeed = 3f;
            barHorSpeed = 1.5f;
            timeBetweenSpawns = 7.5f;
            barVerSpeed = PlayerPrefs.GetFloat("ShipVerticalSpeed", barVerSpeed);
            barHorSpeed = PlayerPrefs.GetFloat("ShipHorizontalSpeed", barHorSpeed);
            timeBetweenSpawns = PlayerPrefs.GetFloat("ShipSpawnFrequency", timeBetweenSpawns);
        }
        barMin = PlayerPrefs.GetFloat("BarMinimumSize", barMin);
        barMax = PlayerPrefs.GetFloat("BarMaximumSize", barMax);
        Invoke("SpawnBar", 5.5f+timeBetweenSpawns);
    }

    private void SpawnBar()
    {
        Invoke("SpawnBar", timeBetweenSpawns);
        Bar();
    }


    private void Bar()
    {
        GameObject newBar = Instantiate(bar);
        int num = Random.Range(1, 1000000) % 4;
        if (num==0)
        {
            newBar.transform.position = new Vector2(spawn1.transform.position.x, Random.Range(minY, maxY));
            newBar.transform.eulerAngles = new Vector3(0, 0, 90);
            newBar.GetComponent<Rigidbody2D>().velocity = new Vector2(barHorSpeed, 0);
            float scale = Random.Range(barMin, barMax);
            if(changeScale)
            newBar.transform.localScale = new Vector3(scale, newBar.transform.localScale.y, 1);
            Destroy(newBar, barDestruct1);
        }
        else if(num == 1)
        {
            newBar.transform.position = new Vector2(Random.Range(minX, maxX) , spawn2.transform.position.y);
            newBar.transform.eulerAngles = new Vector3(0, 0, 0);
            newBar.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -barVerSpeed);
            float scale = Random.Range(barMin, barMax)*4;
            if (changeScale)
                newBar.transform.localScale = new Vector3(scale, newBar.transform.localScale.y, 1);
            Destroy(newBar, barDestruct2);
        }
        else if (num == 2)
        {
            newBar.transform.position = new Vector2(spawn3.transform.position.x, Random.Range(minY, maxY));
            newBar.transform.eulerAngles = new Vector3(0, 0, 270);
            newBar.GetComponent<Rigidbody2D>().velocity = new Vector2(-barHorSpeed, 0);
            float scale = Random.Range(barMin, barMax);
            if (changeScale)
                newBar.transform.localScale = new Vector3(scale, newBar.transform.localScale.y, 1);
            Destroy(newBar, barDestruct1);
        }
        else if (num == 3)
        {
            newBar.transform.position = new Vector2(Random.Range(minX, maxX), spawn4.transform.position.y);
            newBar.transform.eulerAngles = new Vector3(0, 0, 180);
            newBar.GetComponent<Rigidbody2D>().velocity = new Vector2(0, barVerSpeed);
            float scale = Random.Range(barMax, barMin)*4;
            if (changeScale)
                newBar.transform.localScale = new Vector3(scale, newBar.transform.localScale.y, 1);
            Destroy(newBar, barDestruct2);
        }
        if (barWoosh != null)
        {
            barWoosh.pitch = Random.Range(1.3f, 1.8f);
            barWoosh.Play();
        }
    }
}
