using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooter_MeteorSpawner : MonoBehaviour
{
    public GameObject meteor;
    public GameObject targetPrefab;
    public GameObject[] corners;
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public AudioSource targetLocked;
    public AudioSource showerWarning;
    bool allowPlay = true;
    float metFreq=3f;
    float showerFreq=10f;
    float showerMets=10f;
    // Start is called before the first frame update
    void Start()
    {
        metFreq = PlayerPrefs.GetFloat("MeteorSpawnFrequency", metFreq);
        showerFreq = PlayerPrefs.GetFloat("MeteorShowerSpawnFrequency", showerFreq);
        showerMets = PlayerPrefs.GetFloat("MeteorShowerMeteorQuantity", showerMets);
        Invoke("SpawnMeteor", 5.5f+metFreq);
        Invoke("MeteorShower", 5.5f+showerFreq);
    }

    private void SpawnMeteor()
    {
        if(allowPlay)
            targetLocked.Play();
        Invoke("SpawnMeteor", metFreq);
        Meteor();
    }
    void AllowPlay()
    {
        allowPlay = true;
    }
    private void MeteorShower()
    {
        allowPlay = false;
        Invoke("AllowPlay", 2f);
        showerWarning.Play();
        Invoke("MeteorShower", showerFreq);
        int num = (int)showerMets;
        for(int i=0;i<num;i++)
        {
            Meteor();
        }
    }

    private void Meteor()
    {
        GameObject newMeteor = Instantiate(meteor);
        newMeteor.transform.position = corners[Random.Range(1, 100000) % corners.Length].transform.position;
        GameObject target = Instantiate(targetPrefab);
        target.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        target.GetComponent<SpaceShooter_target>().isEnabled = true;
        newMeteor.GetComponent<SpaceShooter_Meteor>().target = target;
    }
}
