using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourMasters_CameraMovement : MonoBehaviour
{
    public float cameraSpeed;
    public float cameraYSpeed;
    public GameObject upVfxPos;
    public GameObject tiltedVfxPos;
    public GameObject downVfxPos;
    bool tutorialAbleToMove = false;
    float cameraSpeedAmplifier = 1f;
    private void StartMoving()
    {
        tutorialAbleToMove = true;
    }

        private void Start()
    {
        cameraSpeedAmplifier = PlayerPrefs.GetFloat("ParkourCameraSpeedAmplifier", cameraSpeedAmplifier);
        Invoke("StartMoving", 5.5f);
    }
    // Update is called once per frame
    void Update()
    {
        if(tutorialAbleToMove)
            GetComponent<Rigidbody2D>().velocity = new Vector2(cameraSpeed, cameraYSpeed) * cameraSpeedAmplifier;
    }
}
