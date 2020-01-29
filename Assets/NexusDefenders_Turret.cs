using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusDefenders_Turret : MonoBehaviour
{
    public GameObject blueBullet;
    public GameObject redBullet;
    public int team = 0;

    public GameObject[] charges;

    public int redHealth = 0;
    public int blueHealth = 0;

    public int maxHealth;

    public GameObject target;
    public float targetAngle;
    public GameObject gun;
    public float speed;
    public float bulletSpeed;
    public float bulletDmg;
    public float bulletReloadTime;
    public bool canShoot = true;
    public GameObject target2;
    bool shot1 = true;

    public AudioClip hit;
    public AudioClip shoot;

    private void Awake()
    {
        speed = PlayerPrefs.GetFloat("NexusTurretGunRotation", speed*5)/5;
        bulletSpeed = PlayerPrefs.GetFloat("NexusTurretBulletSpeed", bulletSpeed);
        bulletDmg = PlayerPrefs.GetFloat("NexusTurretBulletDamage", bulletDmg/10)*10;
        bulletReloadTime = PlayerPrefs.GetFloat("NexusTurretReloadTime", bulletReloadTime/5)*5;
    }
    private void CanShoot()
    {
        canShoot = true;
        GetComponent<Animator>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<NexusDefenders_SwordCollider>() != null)
        {
            AudioSource.PlayClipAtPoint(hit, Camera.main.transform.position);
            collision.GetComponent<NexusDefenders_SwordCollider>().originalPlayer.GetComponent<NexusDefenders_Player>().damageToObjectives += collision.GetComponent<NexusDefenders_SwordCollider>().damage;
            if (collision.GetComponent<NexusDefenders_SwordCollider>().team == 0)
            {
                if (redHealth > 0)
                {
                    redHealth--;
                        charges[redHealth / (maxHealth / charges.Length)].GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                }
                else if(blueHealth < maxHealth)
                {
                    blueHealth++;
                    if (blueHealth / (maxHealth / charges.Length) - 1 >= 0)
                        charges[blueHealth / (maxHealth / charges.Length)-1].GetComponent<SpriteRenderer>().color = new Color32(0, 255, 255, 255);
                }
                
            }
            else
            {
                if (blueHealth > 0)
                {
                    blueHealth--;
                        charges[blueHealth / (maxHealth / charges.Length)].GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                }
                else if (redHealth<maxHealth)
                {
                    redHealth++;
                    if (redHealth / (maxHealth / charges.Length) - 1 >= 0)
                        charges[redHealth / (maxHealth / charges.Length)-1].GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(1/bulletReloadTime > 1)
            GetComponent<Animator>().speed = 1/bulletReloadTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(blueHealth == maxHealth)
        {
            team = 0;
        }
        else if(redHealth == maxHealth)
        {
            team = 1;
        }
        else
        {
            team = -1;
        }
        if (team != -1)
        {
            if (target == null || target2 == null || target2.GetComponent<NexusDefenders_Wall>().team == team)
            {
                float distance = 9999f;
                GameObject targetWall = null;
                foreach (NexusDefenders_Wall wall in FindObjectsOfType<NexusDefenders_Wall>())
                {
                    if (Vector2.Distance(wall.transform.position, transform.position) < distance && wall.team != team)
                    {
                        distance = Vector2.Distance(wall.transform.position, transform.position);
                        targetWall = wall.gameObject;
                    }
                }
                GameObject newTarget = new GameObject();
                newTarget.transform.position = new Vector3(targetWall.transform.position.x, targetWall.transform.position.y - 0.75f, 0);
                target = newTarget;
                Vector3 diff = target.transform.position - transform.position;
                diff.Normalize();

                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 180;
                targetAngle = rot_z;
                target2 = targetWall;

            }
            else
            {
                gun.transform.rotation = Quaternion.RotateTowards(gun.transform.rotation, Quaternion.Euler(0, 0, targetAngle), speed * Time.deltaTime);
                if (target2 != null)
                {
                    if (canShoot && Physics2D.Raycast(bltPos.transform.position, -gun.transform.right, 50, 1 << LayerMask.NameToLayer("wall")).collider.gameObject == target2)
                    {
                        GameObject bullet = new GameObject();
                        if (team == 0)
                            bullet = Instantiate(blueBullet);
                        else bullet = Instantiate(redBullet);
                        bullet.transform.position = bltPos.transform.position;
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
        }
    }
    public GameObject bltPos;
}
