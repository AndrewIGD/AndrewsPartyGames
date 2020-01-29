using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencingMates_Volcano : MonoBehaviour
{
    public GameObject targetPrefab;
    public bool shooting = false;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public AudioClip shoot;
    float timeBetweenShots = 4.5f;
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenShots = PlayerPrefs.GetFloat("FencingVolcanoTimeBetweenShots", timeBetweenShots);
        Invoke("ShootAnim", timeBetweenShots+5.5f);
    }
    private void Shoot()
    {
        GameObject target = Instantiate(targetPrefab);
        target.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        target.GetComponent<FencingMates_target>().isEnabled = true;
        target.GetComponent<SpaceShooter_target>().isEnabled = true;
        AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position);
    }
    private void ShootAnim()
    {
        shooting = true;
        GetComponent<Animator>().Play("shoot");
    }
    private void StopShoot()
    {
        shooting = false;
        Invoke("ShootAnim", timeBetweenShots);
    }
    // Update is called once per frame
    void Update()
    {
        if (!shooting)
            GetComponent<Animator>().Play("idle");
    }
}
