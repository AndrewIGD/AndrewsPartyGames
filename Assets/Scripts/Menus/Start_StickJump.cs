using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_StickJump : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { 
        foreach(Start_StickJump stick in FindObjectsOfType<Start_StickJump>())
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), stick.GetComponent<BoxCollider2D>());
        }
        if (SceneManager.GetActiveScene().name == "Start")
            Jump();
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, Random.Range(3, 7));
        Invoke("Jump", Random.Range(0.5f, 2));
    }
    private void Update()
    {
        foreach (Start_StickJump stick in FindObjectsOfType<Start_StickJump>())
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), stick.GetComponent<BoxCollider2D>());
        }
    }
}
