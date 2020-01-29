using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencingMates_target : MonoBehaviour
{
    public float timeTillExpl;
    public GameObject particleVfx;
    public AudioClip expl;
    public bool isEnabled = false;
    int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        damage = (int)PlayerPrefs.GetFloat("FencingVolcanoDamage", damage);
        Invoke("Expl", timeTillExpl);
    }
    private void Expl()
    {
        if (isEnabled)
        {
            GameObject vfx = Instantiate(particleVfx);
            vfx.transform.position = transform.position;
            foreach (FencingMates_Player player in FindObjectsOfType<FencingMates_Player>())
            {
                if (Physics2D.IsTouching(player.GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>()))
                {
                    player.StartCoroutine(player.DecreaseHp(damage));
                }
            }
            if(expl != null)
                AudioSource.PlayClipAtPoint(expl, Camera.main.transform.position, 1f);
            Destroy(vfx, 3f);
            Destroy(gameObject);
        }
        
    }
}
