using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby_TeamsActivate : MonoBehaviour
{
    public Lobby_Teams teams;
    public void ActivateTeams()
    {
        teams.Teams();
    }
}
