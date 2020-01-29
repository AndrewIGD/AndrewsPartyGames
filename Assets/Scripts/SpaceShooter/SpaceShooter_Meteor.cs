using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooter_Meteor : MonoBehaviour
{

    public GameObject target;
    public float speed;
    public GameObject particleVfx;
    bool ableToMove = false;
    public AudioClip expl;
    public bool explode = true;
    float damage=50f;
    private void Start()
    {
        speed = PlayerPrefs.GetFloat("MeteorSpeed", speed);
        damage = PlayerPrefs.GetFloat("MeteorDamage", damage);
        Invoke("Move", 2f);
    }
    private void Move()
    {
        ableToMove = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (ableToMove && target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
            Vector3 diff = target.transform.position - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
            if (Vector2.Distance(transform.position, target.transform.position) < 0.75f)
            {
                Explode();
            }
        }
    }
    private void Explode()
    {
        if (explode)
        {
            GameObject vfx = Instantiate(particleVfx);
            vfx.transform.position = transform.position;
            Destroy(vfx, 2);
            foreach (SpaceShooter_Player player in FindObjectsOfType<SpaceShooter_Player>())
            {
                if (Physics2D.IsTouching(player.GetComponent<PolygonCollider2D>(), target.GetComponent<CircleCollider2D>()))
                {
                    player.DecreaseHp(damage);
                }
            }
            AudioSource.PlayClipAtPoint(expl, Camera.main.transform.position, 1f);
        }
        Destroy(target);
        Destroy(gameObject);
    }
}
