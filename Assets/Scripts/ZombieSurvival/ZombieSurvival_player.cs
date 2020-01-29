using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SharpDX.DirectInput;
using Pathfinding;
using UnityEngine.SceneManagement;

public class ZombieSurvival_player : MonoBehaviour
{
    float dashSpeed = 10f;
    public int playerNumber;
    public float speed;
    public float bltDmg;
    public float atkSpd;
    public float bltSpd;
    public float coins;
    public GameObject sprite1;
    public GameObject sprite2;
    public GameObject sprite3;
    Color32 sprite1Color;
    Color32 sprite2Color;
    Color32 sprite3Color;
    public SpriteRenderer spriteColor1;
    public SpriteRenderer spriteColor2;
    public int time = 0;
    public AudioClip attack;
    public int team;
    public TextMeshPro teamColor;
    public GameObject bullet;
    public GameObject bulletPos;
    public bool canAttack;
    public bool lockedRot = false;
    public float newRot;
    public bool dashAnim = false;
    public AudioClip dash;
    public GameObject deathVfx;
    public AudioClip[] deathSfx;
    public float damageDealt;
    public GameObject zombie;
    public Joystick joystick;
    public int sus;
    public int jos;
    public int dreapta;
    public int stanga;
    public int button1;
    public int button2;
    bool keyboard = false;
    public bool trap = false;
    bool usedTrap = false;
    public GameObject trapObj;
    public AudioClip beartrapsetup;
    public bool bot;
    public string difficulty;
    public int chanceToAttack;
    public GameObject botObj;
    public bool infection = false;
    bool zombified = false;
    float zombieSpeed = 6f;
    float zombieHealth = 500f;
    // Start is called before the first frame update
    private void Times()
    {
        time++;
        Invoke("Times", 1f);
    }
    public void Infect()
    {
        if (zombified == false)
        {
            zombified = true;
            GameObject zom = Instantiate(zombie);
            zom.transform.position = transform.position;
            zom.GetComponent<ZombieSurvival_zombie>().enabled = true;
            zom.GetComponent<ZombieSurvival_zombie>().bot = false;
            zom.GetComponent<ZombieSurvival_zombie>().health = zombieHealth;
            zom.GetComponent<ZombieSurvival_zombie>().infection = infection;
            zom.GetComponent<ZombieSurvival_zombie>().difficulty = difficulty;
            if (bot)
            {
                if (difficulty == "easy")
                    zom.GetComponent<ZombieSurvival_zombie>().speed = 1f;
                else if (difficulty == "normal")
                    zom.GetComponent<ZombieSurvival_zombie>().speed = 2f;
                else if (difficulty == "hard")
                    zom.GetComponent<ZombieSurvival_zombie>().speed = 3f;
                else zom.GetComponent<ZombieSurvival_zombie>().speed = zombieSpeed/3*2;
            }
            else zom.GetComponent<ZombieSurvival_zombie>().speed = zombieSpeed;
            zom.GetComponent<ZombieSurvival_zombie>().playerNumber = playerNumber;
            zom.GetComponent<ZombieSurvival_zombie>().team.SetActive(true);
            zom.GetComponent<ZombieSurvival_zombie>().team.GetComponent<TextMeshPro>().text = teamColor.text;
            zom.GetComponent<ZombieSurvival_zombie>().team.GetComponent<TextMeshPro>().color = new Color32(0, 255, 0, 255);
            zom.GetComponent<ZombieSurvival_zombie>().bot = bot;
            zom.GetComponent<ZombieSurvival_zombie>().joystick = joystick;
            zom.GetComponent<ZombieSurvival_zombie>().button1 = button1;
            zom.GetComponent<ZombieSurvival_zombie>().button2 = button2;
            zom.GetComponent<ZombieSurvival_zombie>().keyboard = keyboard;
            zom.GetComponent<ZombieSurvival_zombie>().sus = sus;
            zom.GetComponent<ZombieSurvival_zombie>().jos = jos;
            zom.GetComponent<ZombieSurvival_zombie>().stanga = stanga;
            zom.GetComponent<ZombieSurvival_zombie>().dreapta = dreapta;
            foreach (ZombieSurvival_player player in FindObjectsOfType<ZombieSurvival_player>())
            {
                if (player.bot)
                    Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), zom.GetComponent<BoxCollider2D>());
                else Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), zom.GetComponent<BoxCollider2D>());
            }
            zom.transform.eulerAngles = transform.eulerAngles;
            GameObject vfx = Instantiate(deathVfx);
            vfx.transform.position = transform.position;
            AudioSource.PlayClipAtPoint(deathSfx[Random.Range(0, 1000000) % deathSfx.Length], Camera.main.transform.position);
     
                FindObjectOfType<PlayerData>().playerObjectiveScores[playerNumber] = time;
            Destroy(gameObject);
        }
    }
    public void Death()
    {
            GameObject death = Instantiate(deathVfx);
            death.transform.position = transform.position;
            AudioSource.PlayClipAtPoint(deathSfx[Random.Range(0, 1000000) % deathSfx.Length], Camera.main.transform.position);

                FindObjectOfType<PlayerData>().playerObjectiveScores[playerNumber] = time;
            Destroy(gameObject);
    }
    private void StartMoving()
    {
        tutorialAbleToMove = true;

    }
    public bool tutorialAbleToMove = false;
    private void Awake()
    {
        if (trap == false)
        {
            speed = PlayerPrefs.GetFloat("ZombieSurvivalPlayerSpeed", speed);
            bltDmg = PlayerPrefs.GetFloat("ZombieSurvivalPlayerBulletDamage", bltDmg);
            atkSpd = PlayerPrefs.GetFloat("ZombieSurvivalPlayerBulletReload", atkSpd);
            bltSpd = PlayerPrefs.GetFloat("ZombieSurvivalPlayerBulletSpeed", bltSpd);
            dashSpeed = PlayerPrefs.GetFloat("ZombieSurvivalPlayerDashSpeed", dashSpeed);
        }
        else
        {
            speed = PlayerPrefs.GetFloat("ZombieInfectionPlayerSpeed", speed);
            bltDmg = PlayerPrefs.GetFloat("ZombieInfectionPlayerBulletDamage", bltDmg);
            atkSpd = PlayerPrefs.GetFloat("ZombieInfectionPlayerBulletReload", atkSpd);
            bltSpd = PlayerPrefs.GetFloat("ZombieInfectionPlayerBulletSpeed", bltSpd);
            zombieSpeed = PlayerPrefs.GetFloat("ZombieInfectionZombieSpeed", zombieSpeed);
            zombieHealth = PlayerPrefs.GetFloat("ZombieInfectionZombieHealth", zombieHealth);
        }
        Invoke("Times", 1f);
        ZombieSurvival_player[] players = FindObjectsOfType<ZombieSurvival_player>();
        deathVfx = GameObject.Find("playerDeath");
        bullet = GameObject.Find("bullet");
        if(trap)
        {
            trapObj = GameObject.Find("beartrap1");
            zombie = GameObject.Find("Zombie");
        }

        foreach (ZombieSurvival_player player in players)
        {
            if (bot)
            {
                if (player.bot == true)
                    Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), player.GetComponent<CircleCollider2D>());
                else Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), player.GetComponent<BoxCollider2D>());
            }
            else
            {
                if (player.bot == true)
                    Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<CircleCollider2D>());
                else Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
            }


        }
        if (FindObjectOfType<PlayerData>().bots[playerNumber] == true && bot == false)
        {
            GameObject botz = Instantiate(botObj);
            botz.transform.position = transform.position;
            botz.GetComponent<ZombieSurvival_player>().difficulty = FindObjectOfType<PlayerData>().botDifficulties[playerNumber];
            botz.GetComponent<ZombieSurvival_player>().playerNumber = playerNumber;
            botz.GetComponent<ZombieSurvival_player>().team = team;
            botz.GetComponent<ZombieSurvival_player>().trap = trap;
            botz.GetComponent<ZombieSurvival_player>().infection = infection;
            FindObjectOfType<SpawnPlayers>().players.Add(botz);
            FindObjectOfType<SpawnPlayers>().players.Remove(gameObject);
            botz.GetComponent<ZombieSurvival_player>().Enable();
            Debug.Log("death1" + ' ' + playerNumber);
            Destroy(gameObject);
        }
        else if(!bot)
        {
            joystick = FindObjectOfType<PlayerData>().controllers[playerNumber];
            if (joystick == null || FindObjectOfType<PlayerData>().pollControllers[playerNumber] || FindObjectOfType<PlayerData>().players.Contains(playerNumber) == false)
            {
                FindObjectOfType<SpawnPlayers>().players.Remove(gameObject);
                Destroy(gameObject);
            }
            else
            {
                if (joystick.Information.Type == SharpDX.DirectInput.DeviceType.Keyboard)
                {
                    keyboard = true;
                }
                if (playerNumber <= 2)
                {
                    if (playerNumber == 1)
                    {
                        dreapta = 31;
                        stanga = 29;
                        jos = 30;
                        sus = 16;
                    }
                    else
                    {
                        dreapta = 107;
                        stanga = 106;
                        jos = 109;
                        sus = 104;
                    }
                }
                button1 = FindObjectOfType<PlayerData>().button1[playerNumber];
                button2 = FindObjectOfType<PlayerData>().button2[playerNumber];
                joystick.Acquire();
            }

            tutorialAbleToMove = false;
            GetComponent<AudioSource>().clip = attack;
            bool ok = false;
            foreach (int player in FindObjectOfType<PlayerData>().players)
            {
                if (playerNumber == player)
                {
                    ok = true;
                    spriteColor1.color = FindObjectOfType<PlayerData>().color1[playerNumber];
                    spriteColor2.color = FindObjectOfType<PlayerData>().color2[playerNumber];
                }
            }
            if (ok == false)
            {
                FindObjectOfType<SpawnPlayers>().players.Remove(gameObject);
                Destroy(gameObject);
            }
            else
            {
                teamColor.text = FindObjectOfType<PlayerData>().playerNames[playerNumber];
                if (FindObjectOfType<PlayerData>().teams == true)
                {
                    team = FindObjectOfType<PlayerData>().playerTeams[playerNumber];
                    if (team == 0)
                    {
                        teamColor.color = new Color32(0, 175, 255, 255);
                    }
                    else teamColor.color = new Color32(255, 0, 0, 255);
                }
                else teamColor.color = new Color32(255, 255, 255, 255);
            }
            Invoke("StartMoving", 5.5f);
        }
    }

    private void Enable()
    {
        if(bot)
        {
            if (trap == false)
            {
                speed = PlayerPrefs.GetFloat("ZombieSurvivalPlayerSpeed", speed);
                bltDmg = PlayerPrefs.GetFloat("ZombieSurvivalPlayerBulletDamage", bltDmg);
                atkSpd = PlayerPrefs.GetFloat("ZombieSurvivalPlayerBulletReload", atkSpd);
                bltSpd = PlayerPrefs.GetFloat("ZombieSurvivalPlayerBulletSpeed", bltSpd);
                dashSpeed = PlayerPrefs.GetFloat("ZombieSurvivalPlayerDashSpeed", dashSpeed);
            }
            else
            {
                speed = PlayerPrefs.GetFloat("ZombieInfectionPlayerSpeed", speed);
                bltDmg = PlayerPrefs.GetFloat("ZombieInfectionPlayerBulletDamage", bltDmg);
                atkSpd = PlayerPrefs.GetFloat("ZombieInfectionPlayerBulletReload", atkSpd);
                bltSpd = PlayerPrefs.GetFloat("ZombieInfectionPlayerBulletSpeed", bltSpd);
                zombieSpeed = PlayerPrefs.GetFloat("ZombieInfectionZombieSpeed", zombieSpeed);
                zombieHealth = PlayerPrefs.GetFloat("ZombieInfectionZombieHealth", zombieHealth);
            }
            Invoke("Times", 1f);
            deathVfx = GameObject.Find("playerDeath");
            bullet = GameObject.Find("bullet");
            tutorialAbleToMove = false;
            GetComponent<AudioSource>().clip = attack;
            bool ok = false;
            foreach (int player in FindObjectOfType<PlayerData>().players)
            {
                if (playerNumber == player)
                {
                    ok = true;
                    spriteColor1.color = FindObjectOfType<PlayerData>().color1[playerNumber];
                    spriteColor2.color = FindObjectOfType<PlayerData>().color2[playerNumber];
                }
            }
            if (ok == false)
            {
                Debug.Log("death4" + ' ' + playerNumber);
                Destroy(gameObject);
            }
            else
            {
                teamColor.text = FindObjectOfType<PlayerData>().playerNames[playerNumber];
                if (FindObjectOfType<PlayerData>().teams == true)
                {
                    team = FindObjectOfType<PlayerData>().playerTeams[playerNumber];
                    if (team == 0)
                    {
                        teamColor.color = new Color32(0, 175, 255, 255);
                    }
                    else teamColor.color = new Color32(255, 0, 0, 255);
                }
                else teamColor.color = new Color32(255, 255, 255, 255);
            }
            Invoke("StartMoving", 5.5f);

            GameObject waypointObj = GameObject.Find("waypoints");
            List<GameObject> waypoints = new List<GameObject>();
            foreach (Transform child in waypointObj.transform)
            {
                waypoints.Add(child.gameObject);
            }
            waypoint = waypoints[Random.Range(1, 100000) % waypoints.Count];
            GetComponent<AIDestinationSetter>().target = waypoint.transform;
            GameObject[] objs = FindObjectsOfType<GameObject>();
            foreach (GameObject obj in objs)
            {
                if (obj.layer == 8 && obj.GetComponent<BoxCollider2D>() != null)
                    Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), obj.GetComponent<BoxCollider2D>());
            }
            if (difficulty == "easy")
                chanceToAttack = 50;
            else if (difficulty == "normal")
                chanceToAttack = 20;
            else chanceToAttack = 1;
            if(SceneManager.GetActiveScene().name.Contains("Invasion"))
            {
                zombieCheck.GetComponent<CircleCollider2D>().radius = 10;
            }
            Invoke("Trap", Random.Range(6.5f, 9f));
        }
    }

    private void Trap()
    {
        if (difficulty == "expert" && trap)
        {
            GameObject trapClone = Instantiate(trapObj);
            trapClone.transform.position = transform.position;
            AudioSource.PlayClipAtPoint(beartrapsetup, Camera.main.transform.position);
        }
    }

    public void Noteams()
    {
        Invoke("Times", 1f);
        ZombieSurvival_player[] players = FindObjectsOfType<ZombieSurvival_player>();
        foreach (ZombieSurvival_player player in players)
        {
            if (bot)
            {
                if (player.bot == true)
                    Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), player.GetComponent<CircleCollider2D>());
                else Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), player.GetComponent<BoxCollider2D>());
            }
            else
            {
                if (player.bot == true)
                    Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<CircleCollider2D>());
                else Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
            }

        }
        sprite1Color = sprite1.GetComponent<SpriteRenderer>().color;
        sprite2Color = sprite2.GetComponent<SpriteRenderer>().color;
        sprite3Color = sprite3.GetComponent<SpriteRenderer>().color;
        Invoke("StartMoving", 5.5f);
    }
    public void Withteams()
    {
        Invoke("Times", 1f);
        ZombieSurvival_player[] players = FindObjectsOfType<ZombieSurvival_player>();
        foreach (ZombieSurvival_player player in players)
        {
            if (bot)
            {
                if (player.bot == true)
                    Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), player.GetComponent<CircleCollider2D>());
                else Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), player.GetComponent<BoxCollider2D>());
            }
            else
            {
                if (player.bot == true)
                    Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<CircleCollider2D>());
                else Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
            }
        }
        sprite1Color = sprite1.GetComponent<SpriteRenderer>().color;
        sprite2Color = sprite2.GetComponent<SpriteRenderer>().color;
        sprite3Color = sprite3.GetComponent<SpriteRenderer>().color;
        Invoke("StartMoving", 5.5f);
    }
    float delta = 0f;
    // Update is called once per frame
    void Update()
    {
        bool ret = false;
        if (bot == false)
        {

            try
            {
                CustomControls.GetButton(joystick, button1);
                joystick.Poll();
                FindObjectOfType<ControllerDisconnect>().Reconnect(playerNumber);
            }
            catch
            {
                FindObjectOfType<ControllerDisconnect>().Disconnect(playerNumber);
                joystick = FindObjectOfType<PlayerData>().controllers[playerNumber];
                button1 = FindObjectOfType<PlayerData>().button1[playerNumber];
                button2 = FindObjectOfType<PlayerData>().button2[playerNumber];
                ret = true;
            }

        }
        if (ret)
            return;
        if (bot == false)
        {
            delta += Time.deltaTime;
            if (delta >= 1)
            {
                delta = 0;
                try
                {
                    joystick.Poll();
                }
                catch
                {
                    joystick = FindObjectOfType<PlayerData>().controllers[playerNumber];
                    button1 = PlayerPrefs.GetInt(joystick.Information.ProductName + "1", 0);
                    button2 = PlayerPrefs.GetInt(joystick.Information.ProductName + "2", 2);
                    if ((joystick.Information.Type == SharpDX.DirectInput.DeviceType.Keyboard) == false)
                    {
                        keyboard = false;
                    }
                }
            }
        }
        if (Time.timeScale != 0)
        {
            if(teamColor != null)
                teamColor.transform.localEulerAngles = -transform.eulerAngles;
            if (!bot)
            {
                if (dashAnim == false && tutorialAbleToMove)
                {
                    if (keyboard)
                    {
                        if (CustomControls.GetButton(joystick, stanga))
                        {
                            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
                        }
                        else if (CustomControls.GetButton(joystick, dreapta))
                        {
                            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
                        }
                        else GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                        if (CustomControls.GetButton(joystick, sus))
                        {
                            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, speed);
                        }
                        else if (CustomControls.GetButton(joystick, jos))
                        {
                            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -speed);
                        }
                        else if ((CustomControls.GetAxis(joystick).Yaxis > 40000 || CustomControls.GetAxis(joystick).Yaxis < 20000 || CustomControls.GetAxis(joystick).Xaxis > 40000 || CustomControls.GetAxis(joystick).Xaxis < 20000))
                            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
                    }
                    else if ((CustomControls.GetAxis(joystick).Yaxis > 40000 || CustomControls.GetAxis(joystick).Yaxis < 20000 || CustomControls.GetAxis(joystick).Xaxis > 40000 || CustomControls.GetAxis(joystick).Xaxis < 20000))
                        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * (CustomControls.GetAxis(joystick).Xaxis - 32767) / 32767, -speed * (CustomControls.GetAxis(joystick).Yaxis - 32767) / 32767);
                    else GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

                    if (keyboard)
                    {
                        if ((CustomControls.GetButton(joystick, stanga) || CustomControls.GetButton(joystick, dreapta) || CustomControls.GetButton(joystick, jos) || CustomControls.GetButton(joystick, sus)))
                        {
                            float rotZ = transform.rotation.z;
                            transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody2D>().velocity * 100, new Vector3(0, 0, 1));
                            transform.rotation = new Quaternion(0, 0, transform.rotation.z, transform.rotation.w);
                            if (GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0))
                                transform.rotation = new Quaternion(0, 0, rotZ, transform.rotation.w);
                        }
                    }
                    else
                    {
                        if ((CustomControls.GetAxis(joystick).Yaxis > 40000 || CustomControls.GetAxis(joystick).Yaxis < 20000 || CustomControls.GetAxis(joystick).Xaxis > 40000 || CustomControls.GetAxis(joystick).Xaxis < 20000))
                        {
                            float rotZ = transform.rotation.z;
                            transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody2D>().velocity * 100, new Vector3(0, 0, 1));
                            transform.rotation = new Quaternion(0, 0, transform.rotation.z, transform.rotation.w);
                            if (GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0))
                                transform.rotation = new Quaternion(0, 0, rotZ, transform.rotation.w);
                        }
                    }
                    if (CustomControls.GetButton(joystick, button1))
                    {
                        if (trap == true)
                        {
                            if (usedTrap == false)
                            {
                                GameObject trapClone = Instantiate(trapObj);
                                trapClone.transform.position = transform.position;
                                AudioSource.PlayClipAtPoint(beartrapsetup, Camera.main.transform.position);
                                usedTrap = true;
                            }
                        }
                        else
                        {
                            GetComponent<Rigidbody2D>().velocity = -transform.up * dashSpeed;
                            dashAnim = true;
                            AudioSource.PlayClipAtPoint(dash, Camera.main.transform.position);
                            Invoke("StopDash", 0.25f);
                        }
                    }
                    else
                    {
                        lockedRot = false;
                    }
                    if (CustomControls.GetButton(joystick, button2) && canAttack)
                    {
                        if (bulletPos.GetComponent<ZombieSurvival_wallDetect>().wall == null) 
                        {
                            GameObject blt = Instantiate(bullet);
                            blt.transform.position = bulletPos.transform.position;
                            blt.GetComponent<ZombieSurvival_bullet>().damage = bltDmg;
                            blt.GetComponent<ZombieSurvival_bullet>().parent = gameObject;
                            blt.transform.rotation = transform.rotation;
                            blt.GetComponent<Rigidbody2D>().velocity = -transform.up * bltSpd;
                        }
                        GetComponent<AudioSource>().Play();
                        canAttack = false;
                        Invoke("CanAttack", atkSpd);
                    }
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (bot)
        {
            if (tutorialAbleToMove == false)
                GetComponent<AIPath>().enabled = false;
            else GetComponent<AIPath>().enabled = true;
        }
        if (bot && tutorialAbleToMove)
        {
            oldPos = newPos;
            newPos = transform.position;
            if (waypoint == null)
            {
                if (zombieCheck.GetComponent<ZombieSurvival_BotZombieCheck>().zombies.Count != 0)
                {
                    foreach (GameObject zombie in zombieCheck.GetComponent<ZombieSurvival_BotZombieCheck>().zombies)
                    {

                        RaycastHit2D hit = Physics2D.Linecast(transform.position, zombie.transform.position, 10);
                        if (hit == false)
                        {
                            if (Vector2.Distance(transform.position, zombie.transform.position) < 2f && difficulty == "expert")
                            {
                                GameObject waypointObj = GameObject.Find("waypoints");
                                List<GameObject> waypoints = new List<GameObject>();
                                foreach (Transform child in waypointObj.transform)
                                {
                                    waypoints.Add(child.gameObject);
                                }
                                waypoint = waypoints[Random.Range(1, 100000) % waypoints.Count];
                                GetComponent<AIDestinationSetter>().target = waypoint.transform;
                            }
                            else if(Random.Range(1,100000) % chanceToAttack == 0)
                            {
                                if (canAttack)
                                {
                                    Vector3 diff = zombie.transform.position - transform.position;
                                    diff.Normalize();

                                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                    if (bulletPos.GetComponent<ZombieSurvival_wallDetect>().wall == null)
                                    {
                                        GameObject blt = Instantiate(bullet);
                                        blt.transform.position = bulletPos.transform.position;
                                        blt.GetComponent<ZombieSurvival_bullet>().damage = bltDmg;
                                        blt.GetComponent<ZombieSurvival_bullet>().parent = gameObject;
                                        blt.transform.rotation = transform.rotation;
                                        blt.GetComponent<Rigidbody2D>().velocity = -transform.up * bltSpd;
                                    }
                                    GetComponent<AudioSource>().Play();
                                    canAttack = false;
                                    Invoke("CanAttack", atkSpd);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (firstWaypoint == true)
                {
                    if (canAttack)
                    {
                        GameObject blt = Instantiate(bullet);
                        blt.transform.position = bulletPos.transform.position;
                        blt.GetComponent<ZombieSurvival_bullet>().damage = bltDmg;
                        blt.GetComponent<ZombieSurvival_bullet>().parent = gameObject;
                        blt.transform.rotation = transform.rotation;
                        blt.GetComponent<Rigidbody2D>().velocity = -transform.up * bltSpd;
                        GetComponent<AudioSource>().Play();
                        canAttack = false;
                        Invoke("CanAttack", atkSpd);
                    }
                }
                Vector3 diff = newPos - oldPos;
                diff.Normalize();

                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
            }
            if (waypoint != null)
            {
                if (Vector2.Distance(transform.position, waypoint.transform.position) < 0.25f)
                {
                    firstWaypoint = true;
                    waypoint = null;
                    GetComponent<AIDestinationSetter>().target = null;
                }
            }
        }
    }
    public void StopDash()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        dashAnim = false;
    }
    private void CanAttack()
    {
        canAttack = true;
    }
    bool firstWaypoint = false;
    Vector2 newPos;
    Vector2 oldPos;
    public GameObject waypoint;
    public GameObject zombieCheck;

}
