using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooter_Blocks : MonoBehaviour
{
    public GameObject[] blocks1;
    public GameObject[] blocks2;
    int block1Index = 0;
    int block2Index = 0;
    public AudioSource woosh;
    float voidFreq=2f;
    // Start is called before the first frame update
    void Start()
    {
        voidFreq = PlayerPrefs.GetFloat("VoidSpawnFrequency", voidFreq);
        Invoke("Void", 5.5f+voidFreq);  
    }
    private void Void()
    {
        blocks1[block1Index].SetActive(true);
        blocks2[block1Index].SetActive(true);
        blocks1[block1Index].GetComponent<SpaceShooter_Block>().enabled = true;
        blocks2[block1Index].GetComponent<SpaceShooter_Block>().enabled = true;
        blocks1[block1Index++].GetComponent<SpaceShooter_Block>().Spawn();
        blocks2[block2Index++].GetComponent<SpaceShooter_Block>().Spawn();
        woosh.Play();
        Invoke("Void", voidFreq);
    }
}
