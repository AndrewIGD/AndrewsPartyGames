using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooter_LaserUltimate : MonoBehaviour
{
    public GameObject particles;
    public GameObject laser;
    public int team;
    public AudioClip charge;
    public bool active = false;
    // Start is called before the first frame update
    float chargeTime=1.5f;
    void Start()
    {
        if (active)
        {
            chargeTime = PlayerPrefs.GetFloat("LaserChargeTime", chargeTime);
            AudioSource.PlayClipAtPoint(charge, Camera.main.transform.position, 1f);
            GameObject chargeVfx = Instantiate(particles);
            chargeVfx.transform.position = transform.position;
            Destroy(chargeVfx, chargeTime);
            Invoke("Shoot", chargeTime);
        }
    }
    void Shoot()
    {
        GameObject laserVfx = Instantiate(laser);
        if (team == 0)
            laserVfx.transform.eulerAngles = new Vector3(180, 0, 0);
        laserVfx.transform.position = transform.position;
        laserVfx.GetComponent<SpaceShooter_laser>().team = team;
        FindObjectOfType<SpaceShooter_CameraShake>().Shake(1f);
        Destroy(laserVfx, 1f);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(active)
             transform.localScale = new Vector3(transform.localScale.x + 0.065f * Time.deltaTime, transform.localScale.x + 0.065f * Time.deltaTime, 1);
    }
}
