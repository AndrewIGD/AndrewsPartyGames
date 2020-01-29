using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class FencingMates_Bot : Agent
{
    FencingMates_Player player;
    int team;
    float[] rayAngles = { 0, 10,20,30,40,50,60,70,80,90,100,110,120,130,140,150,160,170,180,190,200,210,220,230,240,250,260,270,280,290,300,310,320,330,340,350 };
    string[] detectableObjects = {"fencer","wall" };
    public RayPerception2D rayPer;
    Vector2 position;

    public override void InitializeAgent()
    {
        oldPos = transform.position;
        Invoke("PunishForNotMoving", 0.5f);
        base.InitializeAgent();
        player = GetComponent<FencingMates_Player>();
        team = player.team;
        rayPer = GetComponent<RayPerception2D>();
        position = transform.position;
        FencingMates_Player[] players = FindObjectsOfType<FencingMates_Player>();
    }
    public override void CollectObservations()
    {
        float rayDistance = 40f;
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects));
        AddVectorObs(transform.position.x);
        AddVectorObs(transform.position.y);
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
        {
            player.BotDash();
        }
        if (button2 == 1)
            player.BotAttack();

        player.BotMove(x, y);


        SetReward(-0.0001f);
    }

    public override void AgentReset()
    {
        player.lives = 3;
        transform.position = position;
    }
}
