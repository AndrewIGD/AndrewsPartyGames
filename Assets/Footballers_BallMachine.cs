using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footballers_BallMachine : MonoBehaviour
{
    public GameObject ball;
    public Vector2 ballDirection;
    public float timeBetweenSpawns;
    public GameObject ballSpawnPos;
    bool shootAnim = false;
    public AudioClip shoot;
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenSpawns = PlayerPrefs.GetFloat("FootballersBallTimeBetweenSpawns", timeBetweenSpawns);
        Invoke("SpawnBall", timeBetweenSpawns + 5.5f);
    }
    void SpawnBall()
    {
        GameObject ballClone = Instantiate(ball);
        ballClone.transform.position = ballSpawnPos.transform.position;
        ballClone.GetComponent<Rigidbody2D>().velocity = ballDirection;
        GetComponent<Animator>().Play("ballShoot");
        AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position);
        Invoke("SpawnBall", timeBetweenSpawns);
    }
    public void ShootAnim()
    {
        shootAnim = true;
    }
    public void StopShoot()
    {
        shootAnim = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(!shootAnim)
        {
            GetComponent<Animator>().Play("ballIdle");
        }
    }
}
