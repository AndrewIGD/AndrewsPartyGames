using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusDefenders_NexusTurret : MonoBehaviour
{
    public GameObject blueBullet;
    public GameObject redBullet;
    public int team = 0;

    public float targetAngle;
    public GameObject gun;
    public float speed;
    public float bulletSpeed;
    public float bulletDmg;
    public float bulletReloadTime;
    public bool canShoot = true;
    bool shot1 = true;

    public GameObject playerDetect;
    public AudioClip shoot;

    private void Awake()
    {
        speed = PlayerPrefs.GetFloat("NexusTurretGunRotation", speed);
        bulletSpeed = PlayerPrefs.GetFloat("NexusTurretBulletSpeed", bulletSpeed);
        bulletDmg = PlayerPrefs.GetFloat("NexusTurretBulletDamage", bulletDmg);
        bulletReloadTime = PlayerPrefs.GetFloat("NexusTurretReloadTime", bulletReloadTime);
    }
    private void CanShoot()
    {
        canShoot = true;
        GetComponent<Animator>().enabled = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (1 / bulletReloadTime > 1)
            GetComponent<Animator>().speed = 1 / bulletReloadTime;
    }

    // Update is called once per frame
    void Update()
    {

            if(playerDetect.GetComponent<NexusDefenders_TurretPlayerDetect>().target != null)
            {
                Vector3 diff = playerDetect.GetComponent<NexusDefenders_TurretPlayerDetect>().target.transform.position - transform.position;
                diff.Normalize();

                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 180;
                targetAngle = rot_z;
                gun.transform.rotation = Quaternion.RotateTowards(gun.transform.rotation, Quaternion.Euler(0, 0, targetAngle), speed * Time.deltaTime);
                if (canShoot && Physics2D.Raycast(bltPos.transform.position, -gun.transform.right, 50, 1 << LayerMask.NameToLayer("nexusPlayer")).collider.gameObject != null)
                {
                    GameObject bullet = new GameObject();
                    if (team == 0)
                        bullet = Instantiate(blueBullet);
                    else bullet = Instantiate(redBullet);
                bullet.GetComponent<NexusDefenders_bullet>().hitPlayers = true;
                bullet.GetComponent<NexusDefenders_bullet>().hitWalls = false;
                bullet.transform.position = bltPos.transform.position;
                bullet.GetComponent<NexusDefenders_bullet>().team = team;
                    bullet.GetComponent<NexusDefenders_bullet>().damage = bulletDmg;
                    bullet.transform.localEulerAngles = new Vector3(0, 0, gun.transform.localEulerAngles.z + 90);
                    bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed;
                    bullet.transform.localEulerAngles = new Vector3(0, 0, gun.transform.localEulerAngles.z);
                    GetComponent<Animator>().enabled = true;
                    if (shot1)
                        GetComponent<Animator>().Play("shoot");
                    else GetComponent<Animator>().Play("shoot2");
                    shot1 = !shot1;
                    canShoot = false;
                AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position);
                    Invoke("CanShoot", bulletReloadTime);
                }

            }
        }
    public GameObject bltPos;
}
