using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SharpDX.DirectInput;

public class NexusDefenders_Player : MonoBehaviour
{
    public int playerNumber;
    public bool playingAnim = false;
    public bool playingSwordAnim = false;
    public bool ableToMove = true;
    public bool moveLeft = true;
    public bool moveRight = true;
    public float speed;
    public float health;
    public GameObject[] swordColliders;
    public GameObject sprite1;
    public GameObject sprite2;
    public GameObject sprite3;
    public GameObject border1;
    public GameObject border2;
    public GameObject swordRotation;
    public GameObject axe;
    public GameObject spawnPoint;
    public float axeThrowSpeed;
    public float axeTimer;
    public bool allowAxe;
    public int team;
    bool turnedLeft = false;
    bool turnedRight = true;
    int face = 0;
    Color32 sprite1Color;
    Color32 sprite2Color;
    Color32 sprite3Color;
    public SpriteRenderer[] spriteColor1;
    public SpriteRenderer[] spriteColor2;
    public float damageToObjectives = 0;
    public AudioClip attack;
    public AudioClip throwAxe;
    public AudioClip damaged;
    public TextMeshPro teamColor;
    public Joystick joystick;
    public int sus;
    public int jos;
    public int dreapta;
    public int stanga;
    public int button1;
    public int button2;
    bool keyboard = false;
    public bool bot;
    public int path;
    public GameObject botRadius;
    public string botPlayStyle;
    public GameObject axeRadius;
    public GameObject jumpPadDetect;
    public bool botAbleToMove = false;
    public GameObject botObj;
    public string difficulty;
    public string[] attacks;
    public SpriteRenderer sword;
    public SpriteRenderer blueSword;
    public SpriteRenderer redSword;
    public List<GameObject> waypoints;
    public int waypointIndex = 0;
    public int paths;
    bool invulnerable = false;
    // Start is called before the first frame update
    private void StartMoving()
    {
        tutorialAbleToMove = true;
        GameObject spawn = new GameObject();
        spawnPoint = spawn;
        spawnPoint.transform.position = transform.position;
        sword.sortingLayerName = "Layer5";
        sword.sortingOrder = 0;
    }
    bool tutorialAbleToMove = false;
    int chanceToAttack;
    public void ChangeTeam(int team)
    {

            teamColor.text = FindObjectOfType<PlayerData>().playerNames[playerNumber];

            if (team == 0)
            {
                teamColor.color = new Color32(0, 0, 255, 255);
            }
            else teamColor.color = new Color32(255, 0, 0, 255);

        blueSword = GameObject.Find("BlueSword").GetComponent<SpriteRenderer>();
        redSword = GameObject.Find("RedSword").GetComponent<SpriteRenderer>();
        if (team == 1)
        {
            sword.sprite = redSword.sprite;
            transform.eulerAngles = new Vector3(0, 180, 0);
            swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);
            turnedLeft = true;
            turnedRight = false;
        }
        else
        {
            sword.sprite = blueSword.sprite;
            transform.eulerAngles = new Vector3(0, 0, 0);
            swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0, swordRotation.transform.eulerAngles.z);
            turnedLeft = false;
            turnedRight = true;
        }
            foreach (GameObject swordCollider in swordColliders)
            {
                swordCollider.GetComponent<NexusDefenders_SwordCollider>().team = team;
            }
        this.team = team;
        if(bot)
        {
            waypoints.Clear();
            GameObject waypointObj = GameObject.Find("path" + path + team);
            foreach (Transform child in waypointObj.transform)
            {
                waypoints.Add(child.gameObject);
            }
        }
    }
    private void Enable()
    {

        GetComponent<Animator>().Play("firstAppearance");
        if (bot)
        {
            defensiveTimer = 0;
            health = PlayerPrefs.GetFloat("NexusPlayerHealth", health);
            speed = PlayerPrefs.GetFloat("NexusPlayerSpeed", speed);
            axeThrowSpeed = PlayerPrefs.GetFloat("NexusAxeThrowSpeed", axeThrowSpeed);
            axeTimer = PlayerPrefs.GetFloat("NexusAxeCooldown", axeTimer);
            attackSpeed = PlayerPrefs.GetFloat("NexusPlayerAttackSpeed", attackSpeed);
            axe = GameObject.Find("Flying Axe");
            deathVfx = GameObject.Find("deathVfx");
            path = Random.Range(1, 100000) % paths;
            string[] playStyles = { "aggresive", "defensive", "aggresive", "aggresive" };
            botPlayStyle = playStyles[Random.Range(1, 100000) % 4];
            Invoke("BotMove", Random.Range(5.5f, 7f));

            if(difficulty == "easy")
            {
                botRadius.GetComponent<BoxCollider2D>().size = new Vector2(5, 1);
                chanceToAttack = 50;
            }
            else if(difficulty == "normal")
            {
                botRadius.GetComponent<BoxCollider2D>().size = new Vector2(15, 1);
                chanceToAttack = 20;
            }
            else if(difficulty == "hard")
            {
                botRadius.GetComponent<BoxCollider2D>().size = new Vector2(25, 1);
                chanceToAttack = 5;
            }
            else
            {
                botRadius.GetComponent<BoxCollider2D>().size = new Vector2(30, 1);
                chanceToAttack = 1;
            }


            if (FindObjectOfType<PlayerData>().teams == true)
            {
                team = FindObjectOfType<PlayerData>().playerTeams[playerNumber];
            }
            bool ok = false;
            foreach (int player in FindObjectOfType<PlayerData>().players)
            {
                if (playerNumber == player)
                {
                    ok = true;
                    for (int i = 0; i < spriteColor1.Length; i++)
                    {
                        spriteColor1[i].color = FindObjectOfType<PlayerData>().color1[playerNumber];
                    }
                    for (int i = 0; i < spriteColor2.Length; i++)
                    {
                        spriteColor2[i].color = FindObjectOfType<PlayerData>().color2[playerNumber];
                    }
                }
            }

                teamColor.text = FindObjectOfType<PlayerData>().playerNames[playerNumber];

                    if (team == 0)
                    {
                        teamColor.color = new Color32(0, 0, 255, 255);
                    }
                    else teamColor.color = new Color32(255, 0, 0, 255);

            
            blueSword = GameObject.Find("BlueSword").GetComponent<SpriteRenderer>();
            redSword = GameObject.Find("RedSword").GetComponent<SpriteRenderer>();
            if (team == 1)
            {
                sword.sprite = redSword.sprite;
                transform.eulerAngles = new Vector3(0, 180, 0);
                swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);
                turnedLeft = true;
                turnedRight = false;
            }
            else
            {
                sword.sprite = blueSword.sprite;
                transform.eulerAngles = new Vector3(0, 0, 0);
                swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0, swordRotation.transform.eulerAngles.z);
                turnedLeft = false;
                turnedRight = true;
            }
            GameObject waypointObj = GameObject.Find("path" + path + team);
            foreach (Transform child in waypointObj.transform)
            {
                waypoints.Add(child.gameObject);
            }
            foreach (GameObject swordCollider in swordColliders)
            {
                swordCollider.GetComponent<NexusDefenders_SwordCollider>().team = team;
            }
        }
    }
    float attackSpeed = 1f;
    private void Awake()
    {
        health = PlayerPrefs.GetFloat("NexusPlayerHealth", health);
        speed = PlayerPrefs.GetFloat("NexusPlayerSpeed", speed);
        axeThrowSpeed = PlayerPrefs.GetFloat("NexusAxeThrowSpeed", axeThrowSpeed);
        axeTimer = PlayerPrefs.GetFloat("NexusAxeCooldown", axeTimer);
        attackSpeed = PlayerPrefs.GetFloat("NexusPlayerAttackSpeed", attackSpeed);
        GetComponent<Animator>().speed = attackSpeed;
        GetComponent<Animator>().Play("firstAppearance");
        NexusDefenders_Player[] players = FindObjectsOfType<NexusDefenders_Player>();
        foreach (NexusDefenders_Player player in players)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        }
        if (FindObjectOfType<PlayerData>().bots[playerNumber] == true && bot == false)
        {
            GameObject botz = Instantiate(botObj);
            NexusDefenders_Player[] players2 = FindObjectsOfType<NexusDefenders_Player>();
            foreach (NexusDefenders_Player player in players2)
            {
                Physics2D.IgnoreCollision(botz.GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
            }
            botz.transform.position = transform.position;
            botz.GetComponent<NexusDefenders_Player>().difficulty = FindObjectOfType<PlayerData>().botDifficulties[playerNumber];
            botz.GetComponent<NexusDefenders_Player>().paths = paths;
            botz.GetComponent<NexusDefenders_Player>().playerNumber = playerNumber;
            botz.GetComponent<NexusDefenders_Player>().team = team;
            FindObjectOfType<SpawnPlayers>().players.Add(botz);
            botz.GetComponent<NexusDefenders_Player>().Enable();
            Destroy(gameObject);
        }
        else if (!bot)
        {
            joystick = FindObjectOfType<PlayerData>().controllers[playerNumber];
            if (joystick == null || FindObjectOfType<PlayerData>().pollControllers[playerNumber] || FindObjectOfType<PlayerData>().players.Contains(playerNumber) == false)
            {

                Destroy(gameObject);
            }
            else
            {
                axe = GameObject.Find("Flying Axe");
                FindObjectOfType<SpawnPlayers>().players.Add(gameObject);
                Debug.Log(playerNumber);
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

            if (FindObjectOfType<PlayerData>().teams == true)
            {
                team = FindObjectOfType<PlayerData>().playerTeams[playerNumber];
            }
            bool ok = false;
            foreach (int player in FindObjectOfType<PlayerData>().players)
            {
                if (playerNumber == player)
                {
                    ok = true;
                    for (int i = 0; i < spriteColor1.Length; i++)
                    {
                        spriteColor1[i].color = FindObjectOfType<PlayerData>().color1[playerNumber];
                    }
                    for (int i = 0; i < spriteColor2.Length; i++)
                    {
                        spriteColor2[i].color = FindObjectOfType<PlayerData>().color2[playerNumber];
                    }
                }
            }
            if (ok == false)
                Destroy(gameObject);
            else
            {
                teamColor.text = FindObjectOfType<PlayerData>().playerNames[playerNumber];

                if (team == 0)
                {
                    teamColor.color = new Color32(0, 0, 255, 255);
                }
                else teamColor.color = new Color32(255, 0, 0, 255);

            }
            blueSword = GameObject.Find("BlueSword").GetComponent<SpriteRenderer>();
            redSword = GameObject.Find("RedSword").GetComponent<SpriteRenderer>();
            if (team == 1)
            {
                sword.sprite = redSword.sprite;
                transform.eulerAngles = new Vector3(0, 180, 0);
                swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);
                turnedLeft = true;
                turnedRight = false;
            }
            else
            {
                sword.sprite = blueSword.sprite;
                transform.eulerAngles = new Vector3(0, 0, 0);
                swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0, swordRotation.transform.eulerAngles.z);
                turnedLeft = false;
                turnedRight = true;
            }
            foreach (GameObject swordCollider in swordColliders)
            {
                swordCollider.GetComponent<NexusDefenders_SwordCollider>().team = team;
            }
        }


    }
    private void BotMove()
    {
        botAbleToMove = true;
    }
    public void Noteams()
    {
        NexusDefenders_Player[] players = FindObjectsOfType<NexusDefenders_Player>();
        foreach (NexusDefenders_Player player in players)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        }
        bodyPartsColors = new Color32[bodyParts.Length+1];
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyPartsColors[i] = bodyParts[i].GetComponent<SpriteRenderer>().color;
        }
        Invoke("StartMoving", 5.5f);
    }
    private void Vulnerable()
    {
        invulnerable = false;
    }
    public void Withteams()
    {
        NexusDefenders_Player[] players = FindObjectsOfType<NexusDefenders_Player>();
        foreach (NexusDefenders_Player player in players)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        }
        bodyPartsColors = new Color32[bodyParts.Length + 1];
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyPartsColors[i] = bodyParts[i].GetComponent<SpriteRenderer>().color;
        }
        Invoke("StartMoving", 5.5f);
    }
    public GameObject[] bodyParts;
    public Color32[] bodyPartsColors;
    public GameObject deathVfx;
    public IEnumerator DecreaseHp(float damage)
    {
        if (invulnerable == false)
        {
            AudioSource.PlayClipAtPoint(damaged, Camera.main.transform.position);
            for (int i = 0; i < bodyParts.Length; i++)
            {
                bodyParts[i].GetComponent<SpriteRenderer>().color = new Color32(189, 0, 0, 255);
            }
            health -= damage;
            if (health <= 0)
            {
                GameObject vfx = Instantiate(deathVfx);
                vfx.transform.position = transform.position;
                if (bot)
                {
                    waypointIndex = 0;
                    waypoints.Clear();
                    path = Random.Range(1, 100000) % paths;
                    GameObject waypointObj = GameObject.Find("path" + path + team);
                    foreach (Transform child in waypointObj.transform)
                    {
                        waypoints.Add(child.gameObject);
                    }
                    defensiveTimer = 0;
                    string[] playStyles = { "aggresive", "defensive", "aggresive", "aggresive" };
                    botPlayStyle = playStyles[Random.Range(1, 100000) % 4];
                    jumpPadDetect.GetComponent<BoxCollider2D>().enabled = false;
                    axeRadius.GetComponent<BoxCollider2D>().enabled = false;
                    botRadius.GetComponent<BoxCollider2D>().enabled = false;
                    transform.position = new Vector2(100, 100);
                }
                gameObject.SetActive(false);
                playingAnim = false;
                playingSwordAnim = false;
                invulnerable = true;
                Invoke("Spawn", 3);
                Invoke("Vulnerable", 5f);
                yield break;
            }
            float t = 0f;
            while (t < 0.2f)
            {
                t += Time.deltaTime;
                yield return null;
            }
            for (int i = 0; i < bodyParts.Length; i++)
            {
                bodyParts[i].GetComponent<SpriteRenderer>().color = bodyPartsColors[i];
            }
            yield break;
        }
        else
        {
            yield break;
        }
    }
    public void DeactivateParts()
    {
        foreach (GameObject part in bodyParts)
        {
            part.SetActive(false);
        }
    }
    public void RespawnParts()
    {
        foreach (GameObject part in bodyParts)
        {
            part.SetActive(true);
        }
    }
    private void Spawn()
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].GetComponent<SpriteRenderer>().color = bodyPartsColors[i];
        }
        health = 100;
        transform.position = spawnPoint.transform.position;
        gameObject.SetActive(true);
        if (bot)
        {
            jumpPadDetect.GetComponent<NexusDefenders_JumpPadDetect>().jumpPad = null;
            axeRadius.GetComponent<NexusDefenders_BotRadius>().enemy = null;
            botRadius.GetComponent<NexusDefenders_BotRadius>().enemy = null;
            botRadius.GetComponent<NexusDefenders_BotRadius>().nexus = null;
            botRadius.GetComponent<NexusDefenders_BotRadius>().wall = null;
            jumpPadDetect.GetComponent<BoxCollider2D>().enabled = true;
            axeRadius.GetComponent<BoxCollider2D>().enabled = true;
            botRadius.GetComponent<BoxCollider2D>().enabled = true;
        }
        NexusDefenders_Player[] players = FindObjectsOfType<NexusDefenders_Player>();
        foreach (NexusDefenders_Player player in players)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        }
        NexusDefenders_Wall[] walls = FindObjectsOfType<NexusDefenders_Wall>();
        foreach(NexusDefenders_Wall wall in walls)
        {
            wall.Start();
        }
        GetComponent<Animator>().Play("respawn");
        SwordReset();
        foreach (GameObject scol in swordColliders)
        {
            if (scol.GetComponent<BoxCollider2D>())
                scol.GetComponent<BoxCollider2D>().enabled = false;
            else scol.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
    public void SwordReset()
    {
        foreach (GameObject sword in swordColliders)
        {
            sword.GetComponent<NexusDefenders_SwordCollider>().StartCoroutine(sword.GetComponent<NexusDefenders_SwordCollider>().ResetPlayers());
        }
    }
    public void StopSwordAnimation()
    {
        playingSwordAnim = false;
        playingAnim = false;
        if (bot)
        {
            playingSwordAnim = false;
            playingAnim = false;
        }
        else
        {
            playingSwordAnim = false;
            if (CustomControls.GetButton(joystick, button1))
            {
                if (keyboard)
                {
                    if (CustomControls.GetButton(joystick, stanga) && turnedLeft == false)
                    {
                        transform.eulerAngles = new Vector3(0, 180, 0);
                        swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);
                        turnedLeft = true;
                        turnedRight = false;
                    }
                    else if (CustomControls.GetButton(joystick, dreapta) && turnedRight == false)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0f, swordRotation.transform.eulerAngles.z);
                        turnedRight = true;
                        turnedLeft = false;
                    }
                }
                else
                {
                    if (CustomControls.GetAxis(joystick).Xaxis < 20000 && turnedLeft == false)
                    {
                        transform.eulerAngles = new Vector3(0, 180, 0);
                        swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);
                        turnedLeft = true;
                        turnedRight = false;
                    }
                    else if (CustomControls.GetAxis(joystick).Xaxis > 40000 && turnedRight == false)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0f, swordRotation.transform.eulerAngles.z);
                        turnedRight = true;
                        turnedLeft = false;
                    }
                }
                GetComponent<Animator>().Play(attacks[Random.Range(1,100000)%attacks.Length]);
            }
            else playingSwordAnim = false;
        }
    }
    public void AllowLeft(bool allow)
    {
        moveLeft = allow;
    }
    public void AllowRight(bool allow)
    {
        moveRight = allow;
    }
    public void AllowAxe()
    {
        allowAxe = true;
    }
    float delta = 0;
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
            teamColor.transform.localEulerAngles = -transform.eulerAngles;
            if (!bot)
            {
                if (ableToMove && tutorialAbleToMove)
                {
                    if (keyboard)
                    {
                        if (CustomControls.GetButton(joystick, stanga) && moveLeft && !CustomControls.GetButton(joystick, button1) && !playingSwordAnim && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack1") && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack2") && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack3") && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack4"))
                        {
                            face = 1;
                            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
                            if (turnedLeft == false)
                            {
                                transform.eulerAngles = new Vector3(0, 180, 0);

                                swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);
                                turnedLeft = true;
                                turnedRight = false;
                            }
                            if (playingAnim == false && playingSwordAnim == false)
                            {
                                GetComponent<Animator>().Play("run");
                                playingAnim = true;

                            }
                        }
                        else if (CustomControls.GetButton(joystick, dreapta) && moveRight && !playingSwordAnim && !CustomControls.GetButton(joystick, button1) && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack1") && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack2") && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack3") && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack4"))
                        {
                            face = 0;
                            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
                            if (turnedRight == false)
                            {
                                transform.eulerAngles = new Vector3(0, 0, 0);

                                swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0f, swordRotation.transform.eulerAngles.z);
                                turnedRight = true;
                                turnedLeft = false;
                            }
                            if (playingAnim == false && playingSwordAnim == false)
                            {
                                GetComponent<Animator>().Play("run");
                                playingAnim = true;

                            }
                        }
                        else
                        {
                            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                            if (playingSwordAnim == false)
                            {
                                playingAnim = false;
                                GetComponent<Animator>().Play("idle");
                            }
                        }
                    }
                    else
                    {
                        if (CustomControls.GetAxis(joystick).Xaxis < 20000 && moveLeft && !CustomControls.GetButton(joystick, button1) && !playingSwordAnim && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack1") && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack2") && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack3") && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack4"))
                        {
                            face = 1;
                            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * (CustomControls.GetAxis(joystick).Xaxis - 32767) / 32767, GetComponent<Rigidbody2D>().velocity.y);
                            if (turnedLeft == false)
                            {
                                transform.eulerAngles = new Vector3(0, 180, 0);

                                swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);
                                turnedLeft = true;
                                turnedRight = false;
                            }
                            if (playingAnim == false && playingSwordAnim == false)
                            {
                                GetComponent<Animator>().Play("run");
                                playingAnim = true;

                            }
                        }
                        else if (CustomControls.GetAxis(joystick).Xaxis > 40000 && moveRight && !playingSwordAnim && !CustomControls.GetButton(joystick, button1) && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack1") && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack2") && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack3") && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack4"))
                        {
                            face = 0;
                            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * (CustomControls.GetAxis(joystick).Xaxis - 32767) / 32767, GetComponent<Rigidbody2D>().velocity.y);
                            if (turnedRight == false)
                            {
                                transform.eulerAngles = new Vector3(0, 0, 0);

                                swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0f, swordRotation.transform.eulerAngles.z);
                                turnedRight = true;
                                turnedLeft = false;
                            }
                            if (playingAnim == false && playingSwordAnim == false)
                            {
                                GetComponent<Animator>().Play("run");
                                playingAnim = true;

                            }
                        }
                        else
                        {
                            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                            if (playingSwordAnim == false)
                            {
                                playingAnim = false;
                                GetComponent<Animator>().Play("idle");
                            }
                        }
                    }
                    if (CustomControls.GetButton(joystick, button1))
                    {
                        if (playingSwordAnim == false)
                        {
                            GetComponent<Animator>().Play(attacks[Random.Range(1, 100000) % attacks.Length]);
                        }
                    }
                    if (CustomControls.GetButton(joystick, button2))
                    {
                        if (allowAxe)
                        {
                            GameObject axeThrow = Instantiate(axe);
                            axeThrow.GetComponent<NexusDefenders_FlyingAxe>().active = true;
                            axeThrow.GetComponent<Rigidbody2D>().gravityScale = 1;
                            axeThrow.transform.position = transform.position;
                            if (keyboard)
                            {
                                if (CustomControls.GetButton(joystick, stanga) || CustomControls.GetButton(joystick, dreapta) || CustomControls.GetButton(joystick, jos) || CustomControls.GetButton(joystick, sus))
                                {
                                    if (CustomControls.GetButton(joystick, stanga))
                                    {
                                        axeThrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-axeThrowSpeed, GetComponent<Rigidbody2D>().velocity.y);
                                        Debug.Log("st");
                                    }
                                    else if (CustomControls.GetButton(joystick, dreapta))
                                    {
                                        axeThrow.GetComponent<Rigidbody2D>().velocity = new Vector2(axeThrowSpeed, GetComponent<Rigidbody2D>().velocity.y);
                                        Debug.Log("dr");
                                    }
                                    else axeThrow.GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                                    if (CustomControls.GetButton(joystick, sus))
                                    {
                                        axeThrow.GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, axeThrowSpeed);
                                        Debug.Log("su");
                                    }
                                    else if (CustomControls.GetButton(joystick, jos))
                                    {
                                        axeThrow.GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -axeThrowSpeed);
                                        Debug.Log("jo");
                                    }
                                    else axeThrow.GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
                                }
                                else
                                {
                                    if (face == 0)
                                    {
                                        axeThrow.GetComponent<Rigidbody2D>().velocity = new Vector2(axeThrowSpeed, 0);
                                    }
                                    else axeThrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-axeThrowSpeed, 0);
                                }
                            }
                            else
                            {
                                if (CustomControls.GetAxis(joystick).Yaxis > 40000 || CustomControls.GetAxis(joystick).Yaxis < 20000 || CustomControls.GetAxis(joystick).Xaxis > 40000 || CustomControls.GetAxis(joystick).Xaxis < 20000)
                                {
                                    axeThrow.GetComponent<Rigidbody2D>().velocity = new Vector2(axeThrowSpeed * (CustomControls.GetAxis(joystick).Xaxis - 32767) / 32767, -axeThrowSpeed * (CustomControls.GetAxis(joystick).Yaxis - 32767) / 32767);
                                }
                                else
                                {
                                    if (face == 0)
                                    {
                                        axeThrow.GetComponent<Rigidbody2D>().velocity = new Vector2(axeThrowSpeed, 0);
                                    }
                                    else axeThrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-axeThrowSpeed, 0);
                                }
                            }
                            axeThrow.GetComponent<NexusDefenders_FlyingAxe>().player = gameObject;
                            axeThrow.GetComponent<NexusDefenders_FlyingAxe>().face = face;
                            axeThrow.GetComponent<NexusDefenders_FlyingAxe>().team = team;
                            allowAxe = false;
                            AudioSource.PlayClipAtPoint(throwAxe, Camera.main.transform.position);
                            Invoke("AllowAxe", axeTimer);
                        }
                    }
                }
            }
            else if(botAbleToMove)
            {
                if (allowAxe && axeRadius.GetComponent<NexusDefenders_BotRadius>().enemy != null && difficulty == "expert" && invulnerable == false)
                {
                    GameObject axeThrow = Instantiate(axe);
                    axeThrow.GetComponent<Rigidbody2D>().gravityScale = 1;
                    axeThrow.GetComponent<NexusDefenders_FlyingAxe>().active = true;
                    axeThrow.transform.position = transform.position;
                    axeThrow.transform.LookAt(axeRadius.GetComponent<NexusDefenders_BotRadius>().enemy.transform.position);
                    axeThrow.GetComponent<Rigidbody2D>().velocity = axeThrowSpeed * axeThrow.transform.forward;
                    axeThrow.transform.rotation = new Quaternion(0f, 0f, axeThrow.transform.rotation.z, axeThrow.transform.rotation.w);
                    axeThrow.GetComponent<NexusDefenders_FlyingAxe>().player = gameObject;
                    axeThrow.GetComponent<NexusDefenders_FlyingAxe>().face = face;
                    axeThrow.GetComponent<NexusDefenders_FlyingAxe>().team = team;
                    allowAxe = false;
                    AudioSource.PlayClipAtPoint(throwAxe, Camera.main.transform.position);
                    Invoke("AllowAxe", axeTimer);
                }
                if (playingSwordAnim == false)
                {
                    if (botRadius.GetComponent<NexusDefenders_BotRadius>().enemy != null)
                    {
                        if (botPlayStyle == "aggresive")
                        {
                            if (botRadius.GetComponent<NexusDefenders_BotRadius>().wall != null)
                            {
                                if (!(Vector2.Distance(transform.position, new Vector2(botRadius.GetComponent<NexusDefenders_BotRadius>().wall.transform.position.x, transform.position.y)) > 1f) && Random.Range(1, 100000) % chanceToAttack == 0)
                                {
                                    if (botRadius.GetComponent<NexusDefenders_BotRadius>().wall.transform.position.x - transform.position.x > 0 && face == 1)
                                    {
                                        face = 0;

                                        transform.eulerAngles = new Vector3(0, 0, 0);
                                        swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0f, swordRotation.transform.eulerAngles.z);
                                    }
                                    else if (botRadius.GetComponent<NexusDefenders_BotRadius>().wall.transform.position.x - transform.position.x < 0 && face == 0)
                                    {
                                        face = 1;

                                        transform.eulerAngles = new Vector3(0, 180, 0);
                                        swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);
                                    }
                                    Debug.Log("true");
                                    GetComponent<Animator>().Play(attacks[Random.Range(1, 100000) % attacks.Length]);
                                }
                                else
                                {
                                    if (Vector2.Distance(transform.position, new Vector2(botRadius.GetComponent<NexusDefenders_BotRadius>().enemy.transform.position.x, transform.position.y)) > 0.25f || Random.Range(1, 100000) % chanceToAttack != 0)
                                    {
                                        Vector2 newPos;
                                        newPos = Vector2.MoveTowards(transform.position, new Vector2(botRadius.GetComponent<NexusDefenders_BotRadius>().enemy.transform.position.x, transform.position.y), Time.deltaTime * speed);
                                        if (newPos.x - transform.position.x > 0 && face == 1)
                                        {
                                            face = 0;

                                            transform.eulerAngles = new Vector3(0, 0, 0);
                                            swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0f, swordRotation.transform.eulerAngles.z);
                                        }
                                        else if (newPos.x - transform.position.x < 0 && face == 0)
                                        {
                                            face = 1;

                                            transform.eulerAngles = new Vector3(0, 180, 0);
                                            swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);
                                        }
                                        if (playingAnim == false && playingSwordAnim == false)
                                        {
                                            GetComponent<Animator>().Play("run");
                                            playingAnim = true;
                                        }
                                        transform.position = newPos;
                                    }

                                    else
                                    {
                                        if (playingSwordAnim == false)
                                        {
                                            if (botRadius.GetComponent<NexusDefenders_BotRadius>().enemy.transform.position.x - transform.position.x > 0 && face == 1)
                                            {
                                                face = 0;

                                                transform.eulerAngles = new Vector3(0, 0, 0);

                                                swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0f, swordRotation.transform.eulerAngles.z);
                                            }
                                            else if (botRadius.GetComponent<NexusDefenders_BotRadius>().enemy.transform.position.x - transform.position.x < 0 && face == 0)
                                            {
                                                face = 1;

                                                transform.eulerAngles = new Vector3(0, 180, 0);
                                                swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);
                                            }
                                            GetComponent<Animator>().Play(attacks[Random.Range(1, 100000) % attacks.Length]);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (Vector2.Distance(transform.position, new Vector2(botRadius.GetComponent<NexusDefenders_BotRadius>().enemy.transform.position.x, transform.position.y)) > 0.5f)
                                {
                                    Vector2 newPos;
                                    newPos = Vector2.MoveTowards(transform.position, new Vector2(botRadius.GetComponent<NexusDefenders_BotRadius>().enemy.transform.position.x, transform.position.y), Time.deltaTime * speed);
                                    if (newPos.x - transform.position.x > 0 && face == 1)
                                    {
                                        face = 0;

                                        transform.eulerAngles = new Vector3(0, 0, 0);

                                        swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0f, swordRotation.transform.eulerAngles.z);
                                    }
                                    else if (newPos.x - transform.position.x < 0 && face == 0)
                                    {
                                        face = 1;

                                        transform.eulerAngles = new Vector3(0, 180, 0);

                                        swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);
                                    }
                                    if (playingAnim == false && playingSwordAnim == false)
                                    {
                                        GetComponent<Animator>().Play("run");
                                        playingAnim = true;
                                    }
                                    transform.position = newPos;
                                }

                                else
                                {
                                    if (playingSwordAnim == false && Random.Range(1, 100000) % chanceToAttack == 0)
                                    {
                                        if (botRadius.GetComponent<NexusDefenders_BotRadius>().enemy.transform.position.x - transform.position.x > 0 && face == 1)
                                        {
                                            face = 0;

                                            transform.eulerAngles = new Vector3(0, 0, 0);

                                            swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0f, swordRotation.transform.eulerAngles.z);
                                        }
                                        else if (botRadius.GetComponent<NexusDefenders_BotRadius>().enemy.transform.position.x - transform.position.x < 0 && face == 0)
                                        {
                                            face = 1;

                                            transform.eulerAngles = new Vector3(0, 180, 0);

                                            swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);
                                        }
                                        GetComponent<Animator>().Play(attacks[Random.Range(1, 100000) % attacks.Length]);
                                    }
                                }
                            }

                        }
                        else if (botPlayStyle == "defensive")
                        {
                            defensiveTimer += Time.deltaTime;
                            if(defensiveTimer>5)
                            {
                                botPlayStyle = "aggresive";
                            }
                            if (Vector2.Distance(transform.position, new Vector2(botRadius.GetComponent<NexusDefenders_BotRadius>().enemy.transform.position.x, transform.position.y)) > 0.5f)
                            {
                                if (timeTillAttack >= Random.Range(0.5f, 1.25f))
                                {
                                    timeTillAttack = 2;

                                    if (playingSwordAnim == false && Random.Range(1, 100000) % chanceToAttack == 0)
                                    {
                                        if (Vector2.Distance(transform.position, new Vector2(botRadius.GetComponent<NexusDefenders_BotRadius>().enemy.transform.position.x, transform.position.y)) > 0.5f)
                                        {
                                            Vector2 newPos;
                                            newPos = Vector2.MoveTowards(transform.position, new Vector2(botRadius.GetComponent<NexusDefenders_BotRadius>().enemy.transform.position.x, transform.position.y), Time.deltaTime * speed);
                                            if (newPos.x - transform.position.x > 0 && face == 1)
                                            {
                                                face = 0;

                                                transform.eulerAngles = new Vector3(0, 0, 0);

                                                swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0f, swordRotation.transform.eulerAngles.z);
                                            }
                                            else if (newPos.x - transform.position.x < 0 && face == 0)
                                            {
                                                face = 1;

                                                transform.eulerAngles = new Vector3(0, 180, 0);

                                                swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);
                                            }
                                            if (playingAnim == false && playingSwordAnim == false)
                                            {
                                                GetComponent<Animator>().Play("run");
                                                playingAnim = true;
                                            }
                                            transform.position = newPos;
                                        }
                                        if (botRadius.GetComponent<NexusDefenders_BotRadius>().enemy.transform.position.x - transform.position.x > 0 && face == 1)
                                        {
                                            face = 0;

                                            transform.eulerAngles = new Vector3(0, 0, 0);

                                            swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0f, swordRotation.transform.eulerAngles.z);
                                        }
                                        else if (botRadius.GetComponent<NexusDefenders_BotRadius>().enemy.transform.position.x - transform.position.x < 0 && face == 0)
                                        {
                                            face = 1;

                                            transform.eulerAngles = new Vector3(0, 180, 0);

                                            swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);
                                        }
                                        GetComponent<Animator>().Play(attacks[Random.Range(1, 100000) % attacks.Length]);
                                        timeTillAttack = 0;
                                    }
                                }
                                else
                                {
                                    timeTillAttack += Time.deltaTime;
                                    Vector2 newPos;
                                    newPos = Vector2.MoveTowards(transform.position, new Vector2(botRadius.GetComponent<NexusDefenders_BotRadius>().enemy.transform.position.x, transform.position.y), Time.deltaTime * speed);
                                    newPos = new Vector2(transform.position.x - (newPos.x - transform.position.x), transform.position.y);
                                    if (newPos.x - transform.position.x > 0 && face == 1)
                                    {
                                        face = 0;

                                        transform.eulerAngles = new Vector3(0, 0, 0);

                                        swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0f, swordRotation.transform.eulerAngles.z);
                                    }
                                    else if (newPos.x - transform.position.x < 0 && face == 0)
                                    {
                                        face = 1;

                                        transform.eulerAngles = new Vector3(0, 180, 0);

                                        swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);
                                    }
                                    if (playingAnim == false && playingSwordAnim == false)
                                    {
                                        GetComponent<Animator>().Play("run");
                                        playingAnim = true;
                                    }
                                    transform.position = newPos;
                                }
                            }
                        }
                    }
                    else if (botRadius.GetComponent<NexusDefenders_BotRadius>().nexus != null)
                    {
                        if (Vector2.Distance(transform.position, new Vector2(botRadius.GetComponent<NexusDefenders_BotRadius>().nexus.transform.position.x, transform.position.y)) > 1f && !playingSwordAnim)
                        {
                            Vector2 newPos;
                            newPos = Vector2.MoveTowards(transform.position, new Vector2(botRadius.GetComponent<NexusDefenders_BotRadius>().nexus.transform.position.x, transform.position.y), Time.deltaTime * speed);
                            if (newPos.x - transform.position.x > 0 && face == 1)
                            {
                                face = 0;

                                transform.eulerAngles = new Vector3(0, 0, 0);

                                swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0f, swordRotation.transform.eulerAngles.z);
                            }
                            else if (newPos.x - transform.position.x < 0 && face == 0)
                            {
                                face = 1;

                                transform.eulerAngles = new Vector3(0, 180, 0);

                                swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);

                            }
                            if (playingAnim == false && playingSwordAnim == false)
                            {
                                GetComponent<Animator>().Play("run");
                                playingAnim = true;
                            }
                            transform.position = newPos;
                        }
                        else
                        {
                            if (playingSwordAnim == false && Random.Range(1, 100000) % chanceToAttack == 0)
                            {
                                if (botRadius.GetComponent<NexusDefenders_BotRadius>().nexus.transform.position.x - transform.position.x > 0 && face == 1)
                                {
                                    face = 0;

                                    transform.eulerAngles = new Vector3(0, 0, 0);

                                    swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0f, swordRotation.transform.eulerAngles.z);
                                }
                                else if (botRadius.GetComponent<NexusDefenders_BotRadius>().nexus.transform.position.x - transform.position.x < 0 && face == 0)
                                {
                                    face = 1;

                                    transform.eulerAngles = new Vector3(0, 180, 0);

                                    swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);
                                }
                                GetComponent<Animator>().Play(attacks[Random.Range(1, 100000) % attacks.Length]);
                            }
                        }
                    }
                    else if (botRadius.GetComponent<NexusDefenders_BotRadius>().wall != null)
                    {
                        if (Vector2.Distance(transform.position, new Vector2(botRadius.GetComponent<NexusDefenders_BotRadius>().wall.transform.position.x, transform.position.y)) > 1f && !playingSwordAnim)
                        {
                            Vector2 newPos;
                            newPos = Vector2.MoveTowards(transform.position, new Vector2(botRadius.GetComponent<NexusDefenders_BotRadius>().wall.transform.position.x, transform.position.y), Time.deltaTime * speed);
                            if (newPos.x - transform.position.x > 0 && face == 1)
                            {
                                face = 0;

                                transform.eulerAngles = new Vector3(0, 0, 0);

                                swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0f, swordRotation.transform.eulerAngles.z);
                            }
                            else if (newPos.x - transform.position.x < 0 && face == 0)
                            {
                                face = 1;

                                transform.eulerAngles = new Vector3(0, 180, 0);

                                swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);

                            }
                            if (playingAnim == false && playingSwordAnim == false)
                            {
                                GetComponent<Animator>().Play("run");
                                playingAnim = true;
                            }
                            transform.position = newPos;
                        }

                        else
                        {
                            if (playingSwordAnim == false && Random.Range(1, 100000) % chanceToAttack == 0)
                            {
                                if (botRadius.GetComponent<NexusDefenders_BotRadius>().wall.transform.position.x - transform.position.x > 0 && face == 1)
                                {
                                    face = 0;

                                    transform.eulerAngles = new Vector3(0, 0, 0);

                                    swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0f, swordRotation.transform.eulerAngles.z);
                                }
                                else if (botRadius.GetComponent<NexusDefenders_BotRadius>().wall.transform.position.x - transform.position.x < 0 && face == 0)
                                {
                                    face = 1;

                                    transform.eulerAngles = new Vector3(0, 180, 0);

                                    swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);
                                }
                                Debug.Log("true");
                                GetComponent<Animator>().Play(attacks[Random.Range(1, 100000) % attacks.Length]);
                            }
                        }
                    }
                    else
                    {
                        Vector2 newPos = Vector2.zero;
                        if (waypointIndex < waypoints.Count)
                        {
                            newPos = Vector2.MoveTowards(transform.position, new Vector2(waypoints[waypointIndex].transform.position.x, transform.position.y), speed * Time.deltaTime);
                            if (Vector2.Distance(transform.position, waypoints[waypointIndex].transform.position) < 0.5f)
                                waypointIndex++;

                            if (newPos.x - transform.position.x > 0 && face == 1)
                            {
                                face = 0;

                                transform.eulerAngles = new Vector3(0, 0, 0);

                                swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 0f, swordRotation.transform.eulerAngles.z);
                            }
                            else if (newPos.x - transform.position.x < 0 && face == 0)
                            {
                                face = 1;

                                transform.eulerAngles = new Vector3(0, 180, 0);

                                swordRotation.transform.eulerAngles = new Vector3(swordRotation.transform.eulerAngles.x, 180f, swordRotation.transform.eulerAngles.z);
                            }
                            if (playingAnim == false && playingSwordAnim == false)
                            {
                                GetComponent<Animator>().Play("run");
                                playingAnim = true;
                            }
                            transform.position = newPos;
                        }
                        else
                        {
                            GetComponent<Animator>().Play("idle");
                            playingAnim = false;
                        }
                    }
                }
            }
        }
    }
    
    private void Attack()
    {
        playingSwordAnim = true;
        if(!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("firstAppearance") && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("respawn"))
            AudioSource.PlayClipAtPoint(attack, Camera.main.transform.position);
    }
    float timeTillAttack = 0;
    float defensiveTimer = 0f;
}
