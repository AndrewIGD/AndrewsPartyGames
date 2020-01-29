using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourMasters_ChangeSpeed : MonoBehaviour
{
    public float newSpeed;
    public float newPlayerSpeed;
    public float newJumpHeight;
    public float newYSpeed;
    bool activated = false;
    public bool tpCamera = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ParkourMasters_Player>() != null)
        {
            if (activated == false)
            {
                if (tpCamera)
                    Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
                Camera.main.GetComponent<ParkourMasters_CameraMovement>().cameraSpeed = newSpeed;
                Camera.main.GetComponent<ParkourMasters_CameraMovement>().cameraYSpeed = newYSpeed;
            }
            collision.gameObject.GetComponent<ParkourMasters_Player>().speed = newPlayerSpeed;
            collision.gameObject.GetComponent<ParkourMasters_Player>().jumpHeight = newJumpHeight;
            activated = true;
        }
    }
}
