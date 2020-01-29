using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using TMPro;
    
public class SpaceShooter_bot : Agent
{
    public float maxX;
    public float maxY;
    public float minX;
    public float minY;
    SpaceShooter_Player ship;
    int team;
    float[] rayAngles = { 0,45,90,135,180,110,70};
    string[] detectableObjectsRed = { "blueShip", "blueBullet","blueUltimate", "wall"};
    string[] detectableObjectsBlue = { "redShip", "redBullet","redUltimate", "wall" };
    public RayPerception2D rayPer;
    Vector2 position;

    public override void InitializeAgent()
    {
        Invoke("PunishForNotMoving", 0.5f);
        oldPos = transform.position;
        base.InitializeAgent();
        ship = GetComponent<SpaceShooter_Player>();
        team = ship.team;
        rayPer = GetComponent<RayPerception2D>();
        position = transform.position;
        SpaceShooter_Player[] players = FindObjectsOfType<SpaceShooter_Player>();
        foreach (SpaceShooter_Player player in players)
        {
            if (player.team == team)
            {
                Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), player.GetComponent<PolygonCollider2D>());
            }
        }
    }
    Vector2 oldPos;
    Vector2 newPos;
    private void PunishForNotMoving()
    {
        newPos = transform.position;
        if (Mathf.Abs(newPos.x - oldPos.x) < 0.5f)
        {
            SetReward(-0.1f);
            Debug.Log("Punished");
        }
        else Debug.Log("Safe");
        oldPos = newPos;
        Invoke("PunishForNotMoving", 0.5f);
        
    }
    public override void CollectObservations()
    {
        float rayDistance = 40f;
        string[] detectableObjects;
        if (team == 1)
        {
            detectableObjects = detectableObjectsRed;
        }
        else
        {
            detectableObjects = detectableObjectsBlue;
        }
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects));
        Vector3 localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody2D>().velocity);
        AddVectorObs(localVelocity.x);
        AddVectorObs(localVelocity.z);
        AddVectorObs(transform.position.x);
        AddVectorObs(transform.position.y);
        AddVectorObs(System.Convert.ToInt32(GetComponent<SpaceShooter_Player>().usedUltimate));
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        float x = vectorAction[0];
        float y = vectorAction[1];
        if (x > 0)
            x = 1;
        else if (x < 0)
            x = -1;
        if (y > 0)
            y = 1;
        else if (y < 0)
            y = -1;
        int button1 = Mathf.FloorToInt(vectorAction[2]);
        int button2 = Mathf.FloorToInt(vectorAction[3]);

        if (button1 == 1)
            ship.BotShoot();
        if (button2 == 1)
            ship.BotUltimate();

        ship.Move(x,y);


        SetReward(-0.0001f);
    }

    public override void AgentReset()
    {
        ship.health = 100;
        ship.usedUltimate = false;
    }
}
