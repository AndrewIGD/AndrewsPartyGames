using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SharpDX.DirectInput;
using System;

public class SpaceShooter_Player : MonoBehaviour
{
    [Header("Ship info")]
    public float health;
    public float maxhealth;
    public float shipSpeed;
    public float bulletDamage;
    public float ultimateBulletDamage;
    public float ultimateSpeed;
    public float bulletSpeed;
    public float attackSpeed;
    public int team;
    public int playerNumber;
    public GameObject healthbar;
    public GameObject deathVfx;
    [Space(2)]
    [Header("Bullets")]
    public GameObject bullet;
    public GameObject ultimateBullet;
    public GameObject bulletPosition;

    [Space(2)]
    [Header("Audio")]
    public AudioSource damaged;
    public AudioSource shoot;
    public AudioSource ultimate;
    public AudioClip death;

    public SpriteRenderer sprite1;
    public SpriteRenderer sprite2;
    public TextMeshPro teamColor;
    public float damageDealt = 0;
    //Bools
    public bool usedUltimate = false;
    bool ableToShoot = true;
    public bool tutorialAbleToMove = false;
    public Joystick joystick;
    public int sus;
    public int jos;
    public int dreapta;
    public int stanga;
    public int button1;
    public int button2;
    bool keyboard = false;
    public bool bot = false;
    public bool training = false;
    public bool isTrained = false;
    public string botPlayStyle;
    float timeTillAggro;
    float timeTillDef;
    public GameObject botObj;
    public GameObject botExpert;
    public string difficulty;
    public SpaceShooter_Player[] players;
    public GameObject botDetectionArea;

    public void ChangeTeam(int team)
    {
        bullet = GameObject.Find("bullet" + team.ToString());
        ultimateBullet = GameObject.Find("ult" + team.ToString());
        laserUlt = GameObject.Find("charge" + team.ToString());
        if (team == 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            teamColor.color = new Color32(0, 175, 255, 255);
            teamColor.text = FindObjectOfType<PlayerData>().playerNames[playerNumber];
        }
        else
        {
            transform.eulerAngles = new Vector3(180, 0, 0);
            teamColor.color = new Color32(255, 0, 0, 255);
            teamColor.text = FindObjectOfType<PlayerData>().playerNames[playerNumber];
        }
        if (team == 0)
        {
            FindObjectOfType<SpaceShooter_PlayerCounter>().AddPlayerToTeamOne();
        }
        else FindObjectOfType<SpaceShooter_PlayerCounter>().AddPlayerToTeamTwo();
        this.team = team;

    }
    private void Enable()
    {

        if(bot)
        {
            timeTillAggro = UnityEngine.Random.Range(0.5f, 3f);
            timeTillDef = UnityEngine.Random.Range(0.5f, 3f);
            if (difficulty == "easy")
                botDetectionArea.GetComponent<BoxCollider2D>().size = new Vector2(1, 100);
            players = FindObjectsOfType<SpaceShooter_Player>();
            if (FindObjectOfType<PlayerData>().teams == true)
                team = FindObjectOfType<PlayerData>().playerTeams[playerNumber];
            sprite1.color = FindObjectOfType<PlayerData>().color1[playerNumber];
            sprite2.color = FindObjectOfType<PlayerData>().color2[playerNumber];
            bullet = GameObject.Find("bullet" + team.ToString());
            ultimateBullet = GameObject.Find("ult" + team.ToString());
            laserUlt = GameObject.Find("charge" + team.ToString());
            deathVfx = GameObject.Find("Death Vfx");
            if (team == 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                teamColor.color = new Color32(0, 175, 255, 255);
                teamColor.text = FindObjectOfType<PlayerData>().playerNames[playerNumber];
            }
            else
            {
                transform.eulerAngles = new Vector3(180, 0, 0);
                teamColor.color = new Color32(255, 0, 0, 255);
                teamColor.text = FindObjectOfType<PlayerData>().playerNames[playerNumber];
            }
            if (difficulty == "easy")
                botPlayStyle = "aggresive";
            else if (difficulty == "normal")
                botPlayStyle = "defensive";
        }
    }
    public bool tripleShot = false;
    public void TripleShot()
    {
        tripleShot = true;
    }
    float fastSpeed=10f;
    float fastAttack=0.125f;
    float extraHp=50f;
    private void Awake()
    {
        health = PlayerPrefs.GetFloat("ShipHealth",health);
        shipSpeed = PlayerPrefs.GetFloat("ShipSpeed", shipSpeed);
        bulletDamage = PlayerPrefs.GetFloat("BulletDamage", bulletDamage);
        ultimateBulletDamage = PlayerPrefs.GetFloat("UltimateDamage", ultimateBulletDamage);
        ultimateSpeed = PlayerPrefs.GetFloat("UltimateSpeed", ultimateSpeed);
        attackSpeed = PlayerPrefs.GetFloat("AttackSpeed", attackSpeed);
        fastSpeed = PlayerPrefs.GetFloat("FastShipSpeed", fastSpeed);
        fastAttack = PlayerPrefs.GetFloat("FastAttackSpeed", fastAttack);
        extraHp = PlayerPrefs.GetFloat("BonusHealth", extraHp);
        maxhealth = health;
        Invoke("StartMoving", 5.5f);
        FindObjectOfType<SpawnPlayers>().type = 1;
        if (FindObjectOfType<PlayerData>().bots[playerNumber] == true && bot == false)
        {
            if (FindObjectOfType<PlayerData>().botDifficulties[playerNumber] == "expert")
            {
                GameObject botz = Instantiate(botExpert);
                botz.transform.position = transform.position;
                botz.GetComponent<SpaceShooter_Player>().difficulty = FindObjectOfType<PlayerData>().botDifficulties[playerNumber];
                botz.GetComponent<SpaceShooter_Player>().playerNumber = playerNumber;
                botz.GetComponent<SpaceShooter_Player>().team = team;
                FindObjectOfType<SpawnPlayers>().players.Add(botz);
                botz.GetComponent<SpaceShooter_Player>().Enable();
            }
            else
            {
                GameObject botz = Instantiate(botObj);
                botz.transform.position = transform.position;
                botz.GetComponent<SpaceShooter_Player>().difficulty = FindObjectOfType<PlayerData>().botDifficulties[playerNumber];
                botz.GetComponent<SpaceShooter_Player>().playerNumber = playerNumber;
                botz.GetComponent<SpaceShooter_Player>().team = team;
                FindObjectOfType<SpawnPlayers>().players.Add(botz);
                botz.GetComponent<SpaceShooter_Player>().Enable();
            }
            

            Destroy(gameObject);
        }
        else if (!bot)
        {
            if (!bot)
            {
                joystick = FindObjectOfType<PlayerData>().controllers[playerNumber];
                if (joystick == null || FindObjectOfType<PlayerData>().pollControllers[playerNumber] || FindObjectOfType<PlayerData>().players.Contains(playerNumber) == false)
                    Destroy(gameObject);
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
                            jos = 30;
                            sus = 16;
                            dreapta = 31;
                            stanga = 29;
                        }
                        else
                        {
                            jos = 109;
                            sus = 104;
                            dreapta = 107;
                            stanga = 106;
                        }
                    }
                    button1 = FindObjectOfType<PlayerData>().button1[playerNumber];
                    button2 = FindObjectOfType<PlayerData>().button2[playerNumber];
                    joystick.Acquire();
                }
            }
            else
            {
                string[] botStyles = { "aggresive", "defensive" };
                timeTillAggro = UnityEngine.Random.Range(3, 5);
                timeTillDef = UnityEngine.Random.Range(2, 5);
                botPlayStyle = botStyles[UnityEngine.Random.Range(0, 100000) % 2];
                bullet = GameObject.Find("bullet" + team.ToString());
                ultimateBullet = GameObject.Find("ult" + team.ToString());
                laserUlt = GameObject.Find("charge" + team.ToString());
                if (team == 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    teamColor.color = new Color32(0, 175, 255, 255);
                }
                else
                {
                    transform.eulerAngles = new Vector3(180, 0, 0);
                    teamColor.color = new Color32(255, 0, 0, 255);
                }
            }

            bool ok = false;
            foreach (int player in FindObjectOfType<PlayerData>().players)
            {
                if (playerNumber == player)
                {
                    ok = true;
                    sprite1.color = FindObjectOfType<PlayerData>().color1[playerNumber];
                    sprite2.color = FindObjectOfType<PlayerData>().color2[playerNumber];
                }
            }
            if (ok == false && bot == false)
                Destroy(gameObject);
            else
            {
                FindObjectOfType<SpawnPlayers>().players.Add(gameObject);
                if (FindObjectOfType<PlayerData>().teams == true)
                    team = FindObjectOfType<PlayerData>().playerTeams[playerNumber];
                bullet = GameObject.Find("bullet" + team.ToString());
                ultimateBullet = GameObject.Find("ult" + team.ToString());
                laserUlt = GameObject.Find("charge" + team.ToString());
                if (team == 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    teamColor.color = new Color32(0, 175, 255, 255);
                    teamColor.text = FindObjectOfType<PlayerData>().playerNames[playerNumber];
                }
                else
                {
                    transform.eulerAngles = new Vector3(180, 0, 0);
                    teamColor.color = new Color32(255, 0, 0, 255);
                    teamColor.text = FindObjectOfType<PlayerData>().playerNames[playerNumber];
                }

            }
        }
            
        
    }

    public void Noteams()
    {
        SpaceShooter_Player[] players = FindObjectsOfType<SpaceShooter_Player>();
        foreach (SpaceShooter_Player player in players)
        {
            if (player.team == team)
            {
                Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), player.GetComponent<PolygonCollider2D>());
            }
        }
            Invoke("StartMoving", 5.5f);
        
    }
    public void Withteams()
    {
        SpaceShooter_Player[] players = FindObjectsOfType<SpaceShooter_Player>();
        foreach (SpaceShooter_Player player in players)
        {
            if (player.team == team)
            {
                Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), player.GetComponent<PolygonCollider2D>());
            }
        }
            Invoke("StartMoving", 5.5f);
        
    }
    public bool botShoot = false;
    private void StartMoving()
    {
        tutorialAbleToMove = true;
    }
    float delta = 0f;
    // Update is called once per frame
    private void LateUpdate()
    {

    }
    public void Update()
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

        if (Time.timeScale != 0)
        {
            if (!bot)
            {
                if (tutorialAbleToMove)
                {
                    Movement();
                    Shoot();
                    Ultimate();
                }
            }
            else if(tutorialAbleToMove)
            {
                timeBetweenStyles += Time.deltaTime;
                if(difficulty == "easy")
                {
                    if(botShoot)
                        if(UnityEngine.Random.Range(1, 100000) % 20 == 0)
                        GetComponent<SpaceShooter_Player>().BotShoot();
                }
                else if(difficulty == "normal")
                {
                    if (botShoot)
                        if (UnityEngine.Random.Range(1, 100000) % 5 == 0)
                            GetComponent<SpaceShooter_Player>().BotShoot();
                }
                else GetComponent<SpaceShooter_Player>().BotShoot();
                if(!isTrained)
                {
                    if (botPlayStyle == "aggresive")
                    {
                        if (currentTarget == null)
                        {
                            float minDistance = 9999;
                            GameObject currentPlayer = null;
                            players = FindObjectsOfType<SpaceShooter_Player>();
                            for (int i = 0; i < players.Length; i++)
                            {
                                if (players[i] != null)
                                {
                                    if (Vector2.Distance(players[i].transform.position, transform.position) < minDistance && players[i] != GetComponent<SpaceShooter_Player>() && players[i].team != GetComponent<SpaceShooter_Player>().team)
                                    {
                                        minDistance = Vector2.Distance(players[i].transform.position, transform.position);
                                        currentPlayer = players[i].gameObject;

                                    }
                                }
                            }
                            currentTarget = currentPlayer;
                        }
                        if (timeBetweenStyles >= timeTillAggro && difficulty != "easy")
                        {
                            timeBetweenStyles = 0;
                            botPlayStyle = "defensive";
                            currentTarget = null;
                        }
                        if (currentTarget != null)
                        {
                            if (difficulty != "easy")
                                transform.position = Vector2.MoveTowards(transform.position, new Vector2(currentTarget.transform.position.x, transform.position.y), Time.deltaTime * shipSpeed / 4 * 3);
                            else transform.position = Vector2.MoveTowards(transform.position, new Vector2(currentTarget.transform.position.x, transform.position.y), Time.deltaTime * shipSpeed / 2);
                        }
                    }
                    else
                    {
                        if (timeBetweenStyles >= timeTillDef)
                        {
                            timeBetweenStyles = 0;
                            botPlayStyle = "aggresive";
                        }
                        if (target != null)
                        {
                            Vector3 newPos = Vector2.MoveTowards(transform.position, new Vector2(target.transform.position.x, transform.position.y), Time.deltaTime * shipSpeed/4*3);
                            transform.position = transform.position + (transform.position - newPos);
                            if (newPos.x == transform.position.x)
                            {
                                newPos = Vector2.MoveTowards(transform.position, new Vector2(target.transform.position.x + UnityEngine.Random.Range(0.1f, -0.1f), transform.position.y), Time.deltaTime * shipSpeed / 4 * 3);
                                transform.position = transform.position + (transform.position - newPos);
                            }
                        }
                    }
                }
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
        }
        teamColor.transform.localEulerAngles = -transform.eulerAngles;
    }

    //Functions
    public GameObject target;
    public GameObject currentTarget;
    float timeBetweenStyles = 0f;
    private void Movement()
    {
        if (!bot)
        {
            if (keyboard)
            {
                if (CustomControls.GetButton(joystick, stanga))
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-shipSpeed, GetComponent<Rigidbody2D>().velocity.y);
                }
                else if (CustomControls.GetButton(joystick, dreapta))
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(shipSpeed, GetComponent<Rigidbody2D>().velocity.y);
                }
                else GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                if (CustomControls.GetButton(joystick, sus))
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, shipSpeed);
                }
                else if (CustomControls.GetButton(joystick, jos))
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -shipSpeed);
                }
                else GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            }
            else if ((CustomControls.GetAxis(joystick).Yaxis > 40000 || CustomControls.GetAxis(joystick).Yaxis < 20000 || CustomControls.GetAxis(joystick).Xaxis > 40000 || CustomControls.GetAxis(joystick).Xaxis < 20000))
                GetComponent<Rigidbody2D>().velocity = new Vector2(shipSpeed * (CustomControls.GetAxis(joystick).Xaxis - 32767) / 32767, -shipSpeed * (CustomControls.GetAxis(joystick).Yaxis - 32767) / 32767);
            else GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
    public void Move(float x, float y)
    {
        if(tutorialAbleToMove)
            GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * shipSpeed;
    }
    public void Reward(float amount)
    {

    }
    public void BotShoot()
    {
        if (ableToShoot && botShoot && tutorialAbleToMove)
        {
            ableToShoot = false;
            GameObject cloneBullet = Instantiate(bullet) as GameObject;
            cloneBullet.transform.position = bulletPosition.transform.position;
            if (team == 0)
            {
                cloneBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, bulletSpeed);
            }
            else if (team == 1)
            {
                cloneBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -bulletSpeed);
            }
            cloneBullet.GetComponent<SpaceShooter_Bullet>().GetDamageInfo(bulletDamage);
            cloneBullet.GetComponent<SpaceShooter_Bullet>().GetTeamInfo(team);
            cloneBullet.GetComponent<SpaceShooter_Bullet>().player = gameObject;
            if (tripleShot)
            {
                GameObject cloneBullet2 = Instantiate(bullet) as GameObject;
                cloneBullet2.transform.position = bulletPosition.transform.position;
                if (team == 0)
                {
                    cloneBullet2.GetComponent<Rigidbody2D>().velocity = new Vector2(2f, bulletSpeed);
                }
                else if (team == 1)
                {
                    cloneBullet2.GetComponent<Rigidbody2D>().velocity = new Vector2(2f, -bulletSpeed);
                }
                cloneBullet2.GetComponent<SpaceShooter_Bullet>().GetDamageInfo(bulletDamage);
                cloneBullet2.GetComponent<SpaceShooter_Bullet>().GetTeamInfo(team);
                cloneBullet2.GetComponent<SpaceShooter_Bullet>().player = gameObject;
                GameObject cloneBullet3 = Instantiate(bullet) as GameObject;
                cloneBullet3.transform.position = bulletPosition.transform.position;
                if (team == 0)
                {
                    cloneBullet3.GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, bulletSpeed);
                }
                else if (team == 1)
                {
                    cloneBullet3.GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, -bulletSpeed);
                }
                cloneBullet3.GetComponent<SpaceShooter_Bullet>().GetDamageInfo(bulletDamage);
                cloneBullet3.GetComponent<SpaceShooter_Bullet>().GetTeamInfo(team);
                cloneBullet3.GetComponent<SpaceShooter_Bullet>().player = gameObject;
            }
            shoot.Play();
            Invoke("AbleToShoot", attackSpeed);
        }
    }
    public bool laserult = false;
    public void FastAttack()
    {
        attackSpeed = fastAttack;
    }
    public void FastSpeed()
    {
        shipSpeed = fastSpeed;
    }
    public void ExtraHp()
    {
        maxhealth += extraHp;
        DecreaseHp(-extraHp);
    }
    public void RechargeUlt()
    {
        usedUltimate = false;
    }
    public void LaserUlt()
    {
        laserult = true;
        usedUltimate = false;
    }
    public GameObject laserUlt;
    public GameObject laserUltPos;
    public void BotUltimate()
    {
        Debug.Log("use");
        if (!usedUltimate && tutorialAbleToMove)
        {
            if (!laserult)
            {
                GameObject cloneBullet = Instantiate(ultimateBullet) as GameObject;
                cloneBullet.transform.position = bulletPosition.transform.position;
                if (team == 0)
                {
                    cloneBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, ultimateSpeed);
                }
                else if (team == 1)
                {
                    cloneBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -ultimateSpeed);
                }
                cloneBullet.GetComponent<SpaceShooter_Bullet>().GetDamageInfo(ultimateBulletDamage);
                cloneBullet.GetComponent<SpaceShooter_Bullet>().GetTeamInfo(team);
                cloneBullet.GetComponent<SpaceShooter_Bullet>().player = gameObject;
                ultimate.Play();
                usedUltimate = true;
            }
            else
            {
                GameObject ult = Instantiate(laserUlt);
                ult.GetComponent<SpaceShooter_LaserUltimate>().team = team;
                ult.transform.position = laserUltPos.transform.position;
                ult.GetComponent<SpaceShooter_LaserUltimate>().active = true;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                tutorialAbleToMove = false;
                laserult = false;
                usedUltimate = true;
                Invoke("StartMoving", 2.5f);
            }
        }
    }

    public void Shoot()
    {
        if (CustomControls.GetButton(joystick, button1) && ableToShoot)
        {
                ableToShoot = false;
                GameObject cloneBullet = Instantiate(bullet) as GameObject;
                cloneBullet.transform.position = bulletPosition.transform.position;
                if (team == 0)
                {
                    cloneBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, bulletSpeed);
                }
                else if (team == 1)
                {
                    cloneBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -bulletSpeed);
                }
                cloneBullet.GetComponent<SpaceShooter_Bullet>().GetDamageInfo(bulletDamage);
                cloneBullet.GetComponent<SpaceShooter_Bullet>().GetTeamInfo(team);
                cloneBullet.GetComponent<SpaceShooter_Bullet>().player = gameObject;
                if (tripleShot)
                {
                    GameObject cloneBullet2 = Instantiate(bullet) as GameObject;
                    cloneBullet2.transform.position = bulletPosition.transform.position;
                    if (team == 0)
                    {
                        cloneBullet2.GetComponent<Rigidbody2D>().velocity = new Vector2(2f, bulletSpeed);
                    }
                    else if (team == 1)
                    {
                        cloneBullet2.GetComponent<Rigidbody2D>().velocity = new Vector2(2f, -bulletSpeed);
                    }
                    cloneBullet2.GetComponent<SpaceShooter_Bullet>().GetDamageInfo(bulletDamage);
                    cloneBullet2.GetComponent<SpaceShooter_Bullet>().GetTeamInfo(team);
                    cloneBullet2.GetComponent<SpaceShooter_Bullet>().player = gameObject;
                    GameObject cloneBullet3 = Instantiate(bullet) as GameObject;
                    cloneBullet3.transform.position = bulletPosition.transform.position;
                    if (team == 0)
                    {
                        cloneBullet3.GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, bulletSpeed);
                    }
                    else if (team == 1)
                    {
                        cloneBullet3.GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, -bulletSpeed);
                    }
                    cloneBullet3.GetComponent<SpaceShooter_Bullet>().GetDamageInfo(bulletDamage);
                    cloneBullet3.GetComponent<SpaceShooter_Bullet>().GetTeamInfo(team);
                    cloneBullet3.GetComponent<SpaceShooter_Bullet>().player = gameObject;
                }
                shoot.Play();
                Invoke("AbleToShoot", attackSpeed);
            
        }
    }

    public void Ultimate()
    {
        if (CustomControls.GetButton(joystick, button2) && !usedUltimate)
        {
            if (!laserult)
            {
                GameObject cloneBullet = Instantiate(ultimateBullet) as GameObject;
                cloneBullet.transform.position = bulletPosition.transform.position;
                if (team == 0)
                {
                    cloneBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, ultimateSpeed);
                }
                else if (team == 1)
                {
                    cloneBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -ultimateSpeed);
                }
                cloneBullet.GetComponent<SpaceShooter_Bullet>().GetDamageInfo(ultimateBulletDamage);
                cloneBullet.GetComponent<SpaceShooter_Bullet>().GetTeamInfo(team);
                cloneBullet.GetComponent<SpaceShooter_Bullet>().player = gameObject;
                ultimate.Play();
                usedUltimate = true;
            }
            else
            {
                GameObject ult = Instantiate(laserUlt);
                ult.GetComponent<SpaceShooter_LaserUltimate>().team = team;
                ult.transform.position = laserUltPos.transform.position;
                ult.GetComponent<SpaceShooter_LaserUltimate>().active = true;
                tutorialAbleToMove = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                laserult = false;
                usedUltimate = true;
                Invoke("StartMoving", 2.5f);
            }
        }
    }

    public void DecreaseHp(float damage)
    {
        health -= damage;
        if (health < 0)
            health = 0;
        healthbar.transform.localScale = new Vector3(100 * health / maxhealth / 100, healthbar.transform.localScale.y, healthbar.transform.localScale.z);
        if (bot && difficulty != "easy")
        {
            botPlayStyle = "defensive";
            timeBetweenStyles = 0;
        }
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(death, Camera.main.transform.position, 1f);
            FindObjectOfType<SpawnPlayers>().players.Remove(gameObject);

            try
            {
                
                GameObject vfx = Instantiate(deathVfx);
                vfx.transform.position = transform.position;
                AudioSource.PlayClipAtPoint(death, Camera.main.transform.position, 1f);
                if (FindObjectOfType<PlayerData>().teams == false)
                    FindObjectOfType<PlayerData>().playerObjectiveScores[playerNumber] = damage;
                Destroy(gameObject);
            }
            catch
            {
                Destroy(gameObject);
            }
        }
        else if (damage > 0)
            damaged.Play();
        
    }

    private void AbleToShoot()
    {
        ableToShoot = true;
    }
}
