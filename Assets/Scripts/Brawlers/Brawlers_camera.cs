using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brawlers_camera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float maxSize = 5;
        Brawlers_player[] players = FindObjectsOfType<Brawlers_player>();
        foreach(Brawlers_player player in players)
        {
            if (player.transform.position.x - 0.5f <= -maxSize|| player.transform.position.x + 0.5f >= maxSize)
            {
                if(Mathf.Abs(player.transform.position.x) + 0.5f > maxSize)
                    maxSize = Mathf.Abs(player.transform.position.x);
            }
            if (player.transform.position.y -1 <= -maxSize || player.transform.position.y + 1 >= maxSize)
            {
                if (Mathf.Abs(player.transform.position.y) + 1f > maxSize)
                    maxSize = Mathf.Abs(player.transform.position.y) + 1f;
            }
            if (maxSize > 15)
                maxSize = 15;
        }
        foreach (ParticleSystem p in FindObjectsOfType<ParticleSystem>())
        {
            if (Vector2.Distance(p.gameObject.transform.position, transform.position) < 20)
            {
                if (p.transform.position.x - 0.5f <= -maxSize || p.transform.position.x + 0.5f >= maxSize)
                {
                    if (Mathf.Abs(p.transform.position.x) + 0.5f > maxSize)
                        maxSize = Mathf.Abs(p.transform.position.x) + 0.5f;
                }
                if (p.transform.position.y -1f <= -maxSize || p.transform.position.y +1f >= maxSize)
                {
                    if (Mathf.Abs(p.transform.position.y) + 1f > maxSize)
                        maxSize = Mathf.Abs(p.transform.position.y) + 1f;

                }
                if (maxSize > 15)
                    maxSize = 15;
            }
        }
        GetComponent<Camera>().orthographicSize = maxSize;
    }
}
