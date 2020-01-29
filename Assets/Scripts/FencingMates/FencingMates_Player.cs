using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SharpDX.DirectInput;

public class FencingMates_Player : MonoBehaviour
{
    public int playerNumber;
    public int lives;
    public float speed;
    public float dashSpeed;
    public bool dashAnim = false;
    public bool swordAnim = false;
    public bool ableToDash = true;
    public GameObject sprite1;
    public GameObject sprite2;
    public GameObject sprite3;
    public GameObject live1;
    public GameObject live2;
    public GameObject live3;
    public GameObject sword;
    Color32 sprite1Color;
    Color32 sprite2Color;
    Color32 sprite3Color;
    public SpriteRenderer spriteColor1;
    public SpriteRenderer spriteColor2;
    public int time = 0;
    public AudioClip dash;
    public AudioClip damaged;
    public AudioClip attack;
    public int team;
    public TextMeshPro teamColor;
    public Deathrunners_Death[] deaths;
    public Joystick joystick;
    public int sus;
    public int jos;
    public int dreapta;
    public int stanga;
    public int button1;
    public int button2;
    bool keyboard = false;
    public Deathrunners_Death[] unlistedDeaths;
    public bool bot;
    public string botPlayStyle;
    public string botCurrentMode;
    public GameObject playerDetection;
    public GameObject playerAttactTest;
    public GameObject playerDeffensiveDetect;
    public bool runAnim = false;
    public List<GameObject> waypoints;
    public int waypointIndex = 0;
    public string difficulty;
    bool botMove = false;
    public GameObject botObj;
    public int chanceToAttack;
    public bool ocean = false;
    public GameObject oceanWalkEffect;
    public GameObject oceanDashEffect;
    public bool lava = false;
    public bool freezing = false;
    public bool killPlayers = false;
    public bool darkerBlue = false;
    public bool darkerWhite = false;
    public void SpawnWalkEffect()
    {
        if (ocean)
        {
            GameObject effect = Instantiate(oceanWalkEffect);
            effect.transform.position = transform.position;
            effect.GetComponent<FencingMates_OceanWalkEffect>().active = true;
        }
    }
    public void SpawnDashEffect()
    {
        if (ocean)
        {
            GameObject effect = Instantiate(oceanDashEffect);
            effect.transform.position = transform.position;
            effect.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 90);
            effect.GetComponent<Rigidbody2D>().velocity = transform.up * dashSpeed;
            effect.GetComponent<FencingMates_OceanWalkEffect>().active = true;
        }
    }
    // Start is called before the first frame update
    private void Times()
    {
        time++;
        Invoke("Times", 1f);
    }
    public void StartMoving()
    {
        tutorialAbleToMove = true;
        if (FindObjectOfType<PlayerData>().teams == true)
        {
            FencingMates_Player[] players = FindObjectsOfType<FencingMates_Player>();
            foreach (FencingMates_Player player in players)
            {
                if (player.team == team)
                    Physics2D.IgnoreCollision(sword.GetComponent<PolygonCollider2D>(), player.GetComponent<BoxCollider2D>());
            }
        }
    }
    public void Reward(float amount)
    {

    }
    public bool tutorialAbleToMove = false;
    private void BotAbleToMove()
    {
        botMove = true;
    }
    private void Enable()
    {
        if(FindObjectOfType<Deathrunners_Camera>() == null)
            Invoke("StartMoving", 5.5f);
        Invoke("Times", 1f);
        if (bot)
        {
            if (FindObjectOfType<PlayerData>().teams == false)
                team = playerNumber;
            else team = FindObjectOfType<PlayerData>().playerTeams[playerNumber];
            originalTeam = team;
            if (unlistedDeaths.Length == 0)
            {
                lives = (int)PlayerPrefs.GetFloat("FencingPlayerLives", lives);
                speed = PlayerPrefs.GetFloat("FencingPlayerSpeed", speed);
                dashSpeed = PlayerPrefs.GetFloat("FencingPlayerDashSpeed", dashSpeed);
                attackSpeed = PlayerPrefs.GetFloat("FencingPlayerAttackSpeed", attackSpeed);
                GetComponent<Animator>().speed = attackSpeed;
            }
            else
            {
                lives = (int)PlayerPrefs.GetFloat("DeathrunnersPlayerLives", lives);
                speed = PlayerPrefs.GetFloat("DeathrunnersPlayerSpeed", speed);
                dashSpeed = PlayerPrefs.GetFloat("DeathrunnersPlayerDashSpeed", dashSpeed);
                attackSpeed = PlayerPrefs.GetFloat("DeathrunnersPlayerAttackSpeed", attackSpeed);
                GetComponent<Animator>().speed = attackSpeed;
            }
            deathVfx = GameObject.Find("deathVfx");
            FencingMates_Player[] players = FindObjectsOfType<FencingMates_Player>();
            foreach (FencingMates_Player player in players)
            {
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
            }


            Invoke("BotAbleToMove", Random.Range(5.5f, 6f));
            if (FindObjectOfType<Deathrunners_Camera>() == null)
            {
                if (difficulty == "easy")
                {
                    botPlayStyle = "aggresive";
                    playerAttactTest.GetComponent<CircleCollider2D>().radius = 0.1f;
                    playerDeffensiveDetect.GetComponent<CircleCollider2D>().radius = 5f;
                    playerDetection.GetComponent<CircleCollider2D>().radius = 2.5f;
                    chanceToAttack = 25;
                }
                else if (difficulty == "normal")
                {
                    string[] playStyles = { "aggresive", "deffensive" };
                    botPlayStyle = playStyles[Random.Range(1, 100000) % 2];
                    playerAttactTest.GetComponent<CircleCollider2D>().radius = 0.5f;
                    playerDeffensiveDetect.GetComponent<CircleCollider2D>().radius = 10f;
                    playerDetection.GetComponent<CircleCollider2D>().radius = 5f;
                    chanceToAttack = 5;
                }
                else
                {
                    string[] playStyles = { "aggresive", "deffensive", "camper" };
                    botPlayStyle = playStyles[Random.Range(1, 100000) % 3];
                    chanceToAttack = 1;
                    if(difficulty == "hard")
                    {
                        playerAttactTest.GetComponent<CircleCollider2D>().radius = 1.25f;
                        playerDeffensiveDetect.GetComponent<CircleCollider2D>().radius = 15f;
                        playerDetection.GetComponent<CircleCollider2D>().radius = 7.5f;
                    }
                    else
                    {
                        playerAttactTest.GetComponent<CircleCollider2D>().radius = 1.75f;
                        playerDeffensiveDetect.GetComponent<CircleCollider2D>().radius = 20f;
                        playerDetection.GetComponent<CircleCollider2D>().radius = 10f;
                    }
                }
            }
            else
            {
                foreach (Transform child in FindObjectOfType<Deathrunners_Camera>().transform)
                {
                    Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), child.GetComponent<BoxCollider2D>());
                }
                botPlayStyle = "runner";
                GameObject waypointObj = GameObject.Find(difficulty);
                foreach (Transform child in waypointObj.transform)
                {
                    waypoints.Add(child.gameObject);
                }
                if (difficulty == "normal")
                {
                    trapperRadius.GetComponent<BoxCollider2D>().size = new Vector2(15, 100);
                }
                else if(difficulty == "hard")
                {
                    trapperRadius.GetComponent<BoxCollider2D>().size = new Vector2(5, 100);
                }
                else if(difficulty == "expert")
                {
                    trapperRadius.GetComponent<BoxCollider2D>().size = new Vector2(1, 100);
                }
            }


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
  
                teamColor.text = FindObjectOfType<PlayerData>().playerNames[playerNumber];

            
            sprite1Color = sprite1.GetComponent<SpriteRenderer>().color;
            sprite2Color = sprite2.GetComponent<SpriteRenderer>().color;
            sprite3Color = sprite3.GetComponent<SpriteRenderer>().color;
            if (FindObjectOfType<Deathrunners_Camera>() != null)
                team = 0;
            
        }
    }
    PolygonCollider2D lavaColl;
    bool takeDamageFromLava = true;
    public int originalTeam;
    private void TakeDamageFromLava()
    {
        takeDamageFromLava = true;
    }
    float attackSpeed = 1f;
    private void Awake()
    {
        Invoke("Times", 1f);
        if (unlistedDeaths.Length == 0)
        {
            lives = (int)PlayerPrefs.GetFloat("FencingPlayerLives", lives);
            speed = PlayerPrefs.GetFloat("FencingPlayerSpeed", speed);
            dashSpeed = PlayerPrefs.GetFloat("FencingPlayerDashSpeed", dashSpeed);
            attackSpeed = PlayerPrefs.GetFloat("FencingPlayerAttackSpeed", attackSpeed);
            GetComponent<Animator>().speed = attackSpeed;
        }
        else
        {
            lives = (int)PlayerPrefs.GetFloat("DeathrunnersPlayerLives", lives);
            speed = PlayerPrefs.GetFloat("DeathrunnersPlayerSpeed", speed);
            dashSpeed = PlayerPrefs.GetFloat("DeathrunnersPlayerDashSpeed", dashSpeed);
            attackSpeed = PlayerPrefs.GetFloat("DeathrunnersPlayerAttackSpeed", attackSpeed);
            GetComponent<Animator>().speed = attackSpeed;
        }
        if (FindObjectOfType<Deathrunners_Camera>() == null)
            Invoke("StartMoving", 5.5f);
        if (ocean)
        {
            oceanWalkEffect = GameObject.Find("oceanWalkEffect");
            oceanDashEffect = GameObject.Find("oceanDashEffect");
        }
        deathVfx = GameObject.Find("deathVfx");
        if (lava)
        {
            lavaColl = GameObject.Find("lava").GetComponent<PolygonCollider2D>();
        }
        if (FindObjectOfType<PlayerData>().teams == false)
            team = playerNumber;
        if (FindObjectOfType<PlayerData>().bots[playerNumber] == true && bot == false)
        {
            GameObject botz = Instantiate(botObj);
            botz.transform.position = transform.position;
            botz.GetComponent<FencingMates_Player>().difficulty = FindObjectOfType<PlayerData>().botDifficulties[playerNumber];
            botz.GetComponent<FencingMates_Player>().playerNumber = playerNumber;
            botz.GetComponent<FencingMates_Player>().team = team;
            botz.GetComponent<FencingMates_Player>().unlistedDeaths = unlistedDeaths;
            FindObjectOfType<SpawnPlayers>().players.Add(botz);
            botz.GetComponent<FencingMates_Player>().Enable();
            botz.GetComponent<FencingMates_Player>().ocean = ocean;
            botz.GetComponent<FencingMates_Player>().darkerWhite = darkerWhite;
            botz.GetComponent<FencingMates_Player>().darkerBlue = darkerBlue;
            if (FindObjectOfType<PlayerData>().teams == true)
            {
                team = FindObjectOfType<PlayerData>().playerTeams[playerNumber];
                if (team == 0)
                {
                    if (darkerBlue)
                        botz.GetComponent<FencingMates_Player>().teamColor.color = new Color32(0, 0, 190, 255);
                    else botz.GetComponent<FencingMates_Player>().teamColor.color = new Color32(0, 0, 190, 255);
                }
                else botz.GetComponent<FencingMates_Player>().teamColor.color = new Color32(180, 0, 0, 255);
            }
            else
            {
                if (darkerWhite)
                    botz.GetComponent<FencingMates_Player>().teamColor.color = new Color32(120, 120, 120, 255);
                else botz.GetComponent<FencingMates_Player>().teamColor.color = new Color32(120, 120, 120, 255);
            }
            if (ocean)
            {
                botz.GetComponent<FencingMates_Player>().oceanWalkEffect = oceanWalkEffect;
                botz.GetComponent<FencingMates_Player>().oceanDashEffect = oceanDashEffect;
            }
            if (lava)
            {
                botz.GetComponent<FencingMates_Player>().lavaColl = lavaColl;
            }
            Destroy(gameObject);
        }
        else if (!bot)
        {
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
        }
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
        if (ok == false && bot == false)
            Destroy(gameObject);
        else
        {
            teamColor.text = FindObjectOfType<PlayerData>().playerNames[playerNumber];
            if (FindObjectOfType<PlayerData>().teams == true)
            {
                    team = FindObjectOfType<PlayerData>().playerTeams[playerNumber];
                    if (team == 0)
                    {
                        if (darkerBlue)
                            teamColor.color = new Color32(0, 0, 190, 255);
                        else teamColor.color = new Color32(0, 0, 190, 255);
                    }
                    else teamColor.color = new Color32(180, 0, 0, 255);
                }
                else
                {
                    if (darkerWhite)
                        teamColor.color = new Color32(120, 120, 120, 255);
                    else teamColor.color = new Color32(120, 120, 120, 255);
                }
            }
        sprite1Color = sprite1.GetComponent<SpriteRenderer>().color;
        sprite2Color = sprite2.GetComponent<SpriteRenderer>().color;
        sprite3Color = sprite3.GetComponent<SpriteRenderer>().color;
        if (FindObjectOfType<Deathrunners_Camera>() != null)
            team = 0;
        FencingMates_Player[] players = FindObjectsOfType<FencingMates_Player>();
        foreach (FencingMates_Player player in players)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        }
            originalTeam = team;

    }

    }
 
    public void Noteams()
    {
        Invoke("Times", 1f);
        FencingMates_Player[] players = FindObjectsOfType<FencingMates_Player>();
        foreach (FencingMates_Player player in players)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());

        }
        FindObjectOfType<FencingMates_PlayerCounter>().playerCount++;
        sprite1Color = sprite1.GetComponent<SpriteRenderer>().color;
        sprite2Color = sprite2.GetComponent<SpriteRenderer>().color;
        sprite3Color = sprite3.GetComponent<SpriteRenderer>().color;
        Invoke("StartMoving", 5.5f);
    }
    public void Withteams()
    {
        Invoke("Times", 1f);
        FencingMates_Player[] players = FindObjectsOfType<FencingMates_Player>();
        foreach (FencingMates_Player player in players)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        }
        FindObjectOfType<FencingMates_PlayerCounter>().playerCount++;
        sprite1Color = sprite1.GetComponent<SpriteRenderer>().color;
        sprite2Color = sprite2.GetComponent<SpriteRenderer>().color;
        sprite3Color = sprite3.GetComponent<SpriteRenderer>().color;
        Invoke("StartMoving", 5.5f);
    }
    private void StopDashing()
    {
        deaths = FindObjectsOfType<Deathrunners_Death>();
        foreach (Deathrunners_Death death in deaths)
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), death.GetComponent<BoxCollider2D>(), false);
        foreach (Deathrunners_Death death in unlistedDeaths)
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), death.GetComponent<BoxCollider2D>(), false);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            dashAnim = false;
        
    }
    public void SwordReset()
    {
        sword.GetComponent<FencingMates_SwordCollider>().StartCoroutine(sword.GetComponent<FencingMates_SwordCollider>().ResetPlayers());
        swordAnim = false;
        if (bot == false)
        {
            if (CustomControls.GetButton(joystick, button2))
            {
                if (keyboard)
                {
                    if (CustomControls.GetButton(joystick, stanga) || CustomControls.GetButton(joystick, dreapta) || CustomControls.GetButton(joystick, jos) || CustomControls.GetButton(joystick, sus))
                    {
                        float rotZ = transform.rotation.z;
                        Vector2 vector = new Vector2();
                        if (CustomControls.GetButton(joystick, stanga))
                        {
                            vector = new Vector2(-speed, vector.y);
                        }
                        else if (CustomControls.GetButton(joystick, dreapta))
                        {
                            vector = new Vector2(speed, vector.y);
                        }
                        else vector = new Vector2(0, vector.y);
                        if (CustomControls.GetButton(joystick, sus))
                        {
                            vector = new Vector2(vector.x, speed);
                        }
                        else if (CustomControls.GetButton(joystick, jos))
                        {
                            vector = new Vector2(vector.x, -speed);
                        }
                        else vector = new Vector2(vector.x, 0);
                        transform.rotation = Quaternion.LookRotation((vector) * 100, new Vector3(0, 0, 1));
                        transform.rotation = new Quaternion(0, 0, transform.rotation.z, transform.rotation.w);
                        if (vector == new Vector2(0, 0))
                            transform.rotation = new Quaternion(0, 0, rotZ, transform.rotation.w);
                    }
                }
                else
                {
                    if (CustomControls.GetAxis(joystick).Yaxis > 40000 || CustomControls.GetAxis(joystick).Yaxis < 20000 || CustomControls.GetAxis(joystick).Xaxis > 40000 || CustomControls.GetAxis(joystick).Xaxis < 20000)
                    {
                        float rotZ = transform.rotation.z;
                        transform.rotation = Quaternion.LookRotation(new Vector2(speed * (CustomControls.GetAxis(joystick).Xaxis - 32767) / 32767, -speed * (CustomControls.GetAxis(joystick).Yaxis - 32767) / 32767) * 100, new Vector3(0, 0, 1));
                        transform.rotation = new Quaternion(0, 0, transform.rotation.z, transform.rotation.w);
                        if (new Vector2(speed * (CustomControls.GetAxis(joystick).Xaxis - 32767) / 32767, -speed * (CustomControls.GetAxis(joystick).Yaxis - 32767) / 32767) == new Vector2(0, 0))
                            transform.rotation = new Quaternion(0, 0, rotZ, transform.rotation.w);
                    }
                }
                GetComponent<Animator>().Play("attack");
            }
            else
            {
                swordAnim = false;
            }
        }
    }
    public GameObject deathVfx;
    public IEnumerator DecreaseHp(int damage)
    {
        sprite1.GetComponent<SpriteRenderer>().color = new Color32(189, 0, 0, 255);
        sprite2.GetComponent<SpriteRenderer>().color = new Color32(189, 0, 0, 255);
        sprite3.GetComponent<SpriteRenderer>().color = new Color32(189, 0, 0, 255);
        lives -= damage;
        GetComponent<AudioSource>().clip = damaged;
        GetComponent<AudioSource>().Play();
        if (lives == 2)
        {
            live1.GetComponent<Animator>().Play("heart_break");
        }
        else if (lives == 1)
        {
            if (live1.GetComponent<SpriteRenderer>().enabled)
                live1.GetComponent<Animator>().Play("heart_break");
            live2.GetComponent<Animator>().Play("heart_break");
        }
        else if (lives <= 0)
        {
            if (team == 2 && FindObjectOfType<Deathrunners_Camera>() != null)
                FindObjectOfType<Deathrunners_Camera>().enabled = false;
            GameObject vfx = Instantiate(deathVfx);
            vfx.transform.position = transform.position;
            AudioSource.PlayClipAtPoint(damaged, Camera.main.transform.position, 1f);
            Destroy(gameObject);
            if (FindObjectOfType<Deathrunners_Camera>() != null)
                FindObjectOfType<Deathrunners_Camera>().players.Remove(gameObject.GetComponent<FencingMates_Player>());
            if (FindObjectOfType<PlayerData>().teams == false)
                FindObjectOfType<PlayerData>().playerObjectiveScores[playerNumber] = time;

            Destroy(gameObject);
            yield break;
        }
        float t = 0f;
        while (t < 0.2f)
        {
            t += Time.deltaTime;
            yield return null;
        }
        sprite1.GetComponent<SpriteRenderer>().color = sprite1Color;
        sprite2.GetComponent<SpriteRenderer>().color = sprite2Color;
        sprite3.GetComponent<SpriteRenderer>().color = sprite3Color;
        yield break;
    }
    private void FixedUpdate()
    {
        if (FindObjectOfType<Deathrunners_Camera>() != null && !bot)
        {
            if (transform.position.x < Camera.main.transform.position.x - 8.5f)
                transform.position = new Vector2(Camera.main.transform.position.x - 8.5f, transform.position.y);
            if (transform.position.x > Camera.main.transform.position.x + 8.5f)
                transform.position = new Vector2(Camera.main.transform.position.x + 8.5f, transform.position.y);
            if (transform.position.y < Camera.main.transform.position.y - 4.5f)
                transform.position = new Vector2(transform.position.x, Camera.main.transform.position.y - 4.5f);
            if (transform.position.y > Camera.main.transform.position.y + 4.5f)
                transform.position = new Vector2(transform.position.x, Camera.main.transform.position.y + 4.5f);
        }
    }
    public void BotMove(float x, float y)
    {
        if (dashAnim == false && swordAnim == false)
        {
            if (x < 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else if (x > 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            if (y > 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, speed);
            }
            else if (y < 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -speed);
            }
            else GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            if (Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0)
            {
                GetComponent<Animator>().Play("run");
                float rotZ = transform.rotation.z;
                transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody2D>().velocity * 100, new Vector3(0, 0, 1));
                transform.rotation = new Quaternion(0, 0, transform.rotation.z, transform.rotation.w);
                if (GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0))
                    transform.rotation = new Quaternion(0, 0, rotZ, transform.rotation.w);
            }
        }
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
            teamColor.transform.localEulerAngles = -transform.eulerAngles;
            if (!bot)
            {
                if (dashAnim == false && swordAnim == false && tutorialAbleToMove)
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
                        else if(freezing == false) GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                        if (CustomControls.GetButton(joystick, sus))
                        {
                            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, speed);
                        }
                        else if (CustomControls.GetButton(joystick, jos))
                        {
                            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -speed);
                        }
                        else if (freezing == false) GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
                    }
                    else if ((CustomControls.GetAxis(joystick).Yaxis > 40000 || CustomControls.GetAxis(joystick).Yaxis < 20000 || CustomControls.GetAxis(joystick).Xaxis > 40000 || CustomControls.GetAxis(joystick).Xaxis < 20000))
                        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * (CustomControls.GetAxis(joystick).Xaxis - 32767) / 32767, -speed * (CustomControls.GetAxis(joystick).Yaxis - 32767) / 32767);
                    else if(freezing == false) GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    if (keyboard)
                    {
                        if ((CustomControls.GetButton(joystick, stanga) || CustomControls.GetButton(joystick, dreapta) || CustomControls.GetButton(joystick, jos) || CustomControls.GetButton(joystick, sus)) && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack"))
                        {
                            GetComponent<Animator>().Play("run");
                            float rotZ = transform.rotation.z;
                            transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody2D>().velocity * 100, new Vector3(0, 0, 1));
                            transform.rotation = new Quaternion(0, 0, transform.rotation.z, transform.rotation.w);
                            if (GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0))
                                transform.rotation = new Quaternion(0, 0, rotZ, transform.rotation.w);
                        }
                        else
                        {
                            GetComponent<Animator>().Play("idle");
                        }
                    }
                    else
                    {
                        if ((CustomControls.GetAxis(joystick).Yaxis > 40000 || CustomControls.GetAxis(joystick).Yaxis < 20000 || CustomControls.GetAxis(joystick).Xaxis > 40000 || CustomControls.GetAxis(joystick).Xaxis < 20000) && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack"))
                        {
                            GetComponent<Animator>().Play("run");
                            float rotZ = transform.rotation.z;
                            transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody2D>().velocity * 100, new Vector3(0, 0, 1));
                            transform.rotation = new Quaternion(0, 0, transform.rotation.z, transform.rotation.w);
                            if (GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0))
                                transform.rotation = new Quaternion(0, 0, rotZ, transform.rotation.w);
                        }
                        else
                        {
                            GetComponent<Animator>().Play("idle");
                        }
                    }
                    if (CustomControls.GetButton(joystick, button1) && ableToDash)
                    {
                        GetComponent<Animator>().Play("dash");
                    }
                    else if (!CustomControls.GetButton(joystick, button1))
                    {
                        ableToDash = true;
                    }
                    if (CustomControls.GetButton(joystick, button2))
                    {

                        GetComponent<Animator>().Play("attack");
                    }
                }
            }
            else if(botMove)
            {
                if (dashAnim == false && swordAnim == false && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("run") == false)
                    GetComponent<Animator>().Play("idle");
                if(!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack"))
                    swordAnim = false;
                if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("dash"))
                    dashAnim = false;
                if (botPlayStyle == "aggresive")
                {
                    if (playerDetection.GetComponent<FencingBot_PlayerDetect>().target != null)
                    {
                        if (playerAttactTest.GetComponent<FencingBot_PlayerDetect>().target != null)
                        {
                            if (dashAnim == false && swordAnim == false)
                            {
                                if (playerAttactTest.GetComponent<FencingBot_PlayerDetect>().target.GetComponent<FencingMates_Player>().lives >= lives && Random.Range(1, 1000000) % 2 == 0 && difficulty == "expert")
                                {
                                    Vector3 diff = playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position - transform.position;
                                    diff.Normalize();

                                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                                    GetComponent<Animator>().Play("dash");
                                }
                                else if (Random.Range(1, 100000) % chanceToAttack == 0)
                                {
                                    transform.position = Vector2.MoveTowards(transform.position, playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position, Time.deltaTime * speed);
                                    Vector3 diff = playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position - transform.position;
                                    diff.Normalize();

                                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                    GetComponent<Animator>().Play("attack");
                                }
                                else
                                {
                                    GetComponent<Animator>().Play("idle");
                                }
                            }
                        }
                        else
                        {
                            if (dashAnim == false && swordAnim == false)
                            {
                                if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("run"))
                                    GetComponent<Animator>().Play("run");
                                transform.position = Vector2.MoveTowards(transform.position, playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position, Time.deltaTime * speed);
                                Vector3 diff = playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position - transform.position;
                                diff.Normalize();

                                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                            }
                        }
                    }
                    else
                    {
                        float minDistance = 9999;
                        GameObject currentPlayer = null;
                        foreach (FencingMates_Player player in FindObjectsOfType<FencingMates_Player>())
                        {
                            if (Vector2.Distance(player.transform.position, transform.position) < minDistance && player != GetComponent<FencingMates_Player>() && player.team != team)
                            {
                                minDistance = Vector2.Distance(player.transform.position, transform.position);
                                currentPlayer = player.gameObject;
                            }
                        }
                        if (difficulty != "easy")
                        {
                            if (dashAnim == false && swordAnim == false)
                            {
                                if (currentPlayer != null)
                                {
                                    Vector3 diff = currentPlayer.transform.position - transform.position;
                                    diff.Normalize();

                                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                    GetComponent<Animator>().Play("dash");
                                }
                            }
                        }
                        else
                        {
                            if (dashAnim == false && swordAnim == false)
                            {
                                if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("run"))
                                    GetComponent<Animator>().Play("run");
                                transform.position = Vector2.MoveTowards(transform.position, currentPlayer.transform.position, Time.deltaTime * speed);
                                Vector3 diff = currentPlayer.transform.position - transform.position;
                                diff.Normalize();

                                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                            }
                        }
                    }
                }
                else if (botPlayStyle == "deffensive")
                {
                    if (timeTillStill >= 3)
                    {
                        if (playerDetection.GetComponent<FencingBot_PlayerDetect>().target != null)
                        {
                            if (playerAttactTest.GetComponent<FencingBot_PlayerDetect>().target != null)
                            {
                                if (dashAnim == false && swordAnim == false)
                                {
                                    if (playerAttactTest.GetComponent<FencingBot_PlayerDetect>().target.GetComponent<FencingMates_Player>().lives >= lives && Random.Range(1, 1000000) % 2 == 0 && difficulty == "expert")
                                    {
                                        Vector3 diff = playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position - transform.position;
                                        diff.Normalize();

                                        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                                        GetComponent<Animator>().Play("dash");
                                    }
                                    else if(Random.Range(1,1000000)%chanceToAttack == 0)
                                    {
                                        transform.position = Vector2.MoveTowards(transform.position, playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position, Time.deltaTime * speed);
                                        Vector3 diff = playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position - transform.position;
                                        diff.Normalize();

                                        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                        GetComponent<Animator>().Play("attack");
                                    }
                                    else
                                    {
                                    GetComponent<Animator>().Play("idle");
                                    }
                                    timeTillStill = 0;
                                }
                            }
                            else
                            {
                                if (dashAnim == false && swordAnim == false)
                                {
                                    if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("run"))
                                        GetComponent<Animator>().Play("run");
                                    transform.position = Vector2.MoveTowards(transform.position, playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position, Time.deltaTime * speed);
                                    Vector3 diff = playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position - transform.position;
                                    diff.Normalize();

                                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                }
                            }
                        }
                        else
                        {
                            float minDistance = 9999;
                            GameObject currentPlayer = null;
                            foreach (FencingMates_Player player in FindObjectsOfType<FencingMates_Player>())
                            {
                                if (Vector2.Distance(player.transform.position, transform.position) < minDistance && player != GetComponent<FencingMates_Player>() && player.team != team)
                                {
                                    minDistance = Vector2.Distance(player.transform.position, transform.position);
                                    currentPlayer = player.gameObject;
                                }
                            }
                            if (dashAnim == false && swordAnim == false)
                            {
                                Vector3 diff = currentPlayer.transform.position - transform.position;
                                diff.Normalize();

                                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                GetComponent<Animator>().Play("dash");
                            }
                        }
                    }
                    else
                    {
                        if (playerDeffensiveDetect.GetComponent<FencingBot_PlayerDetect>().target != null)
                        {
                            if (playerAttactTest.GetComponent<FencingBot_PlayerDetect>().target != null)
                            {
                                timeTillTrigger = 0;
                                if (dashAnim == false && swordAnim == false)
                                {
                                    if (hasAttacked == false)
                                    {
                                        transform.position = Vector2.MoveTowards(transform.position, playerAttactTest.GetComponent<FencingBot_PlayerDetect>().target.transform.position, Time.deltaTime * speed);
                                        Vector3 diff = playerAttactTest.GetComponent<FencingBot_PlayerDetect>().target.transform.position - transform.position;
                                        diff.Normalize();

                                        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                        GetComponent<Animator>().Play("attack");
                                        hasAttacked = true;
                                    }
                                    else
                                    {
                                        Vector3 diff = playerAttactTest.GetComponent<FencingBot_PlayerDetect>().target.transform.position - transform.position;
                                        diff.Normalize();

                                        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                                        GetComponent<Animator>().Play("dash");
                                        hasAttacked = false;
                                    }
                                }
                            }
                            else
                            {
                                timeTillTrigger += Time.deltaTime;
                                hasAttacked = false;
                                if (timeTillTrigger >= 1.5f)
                                {
                                    if (dashAnim == false && swordAnim == false)
                                    {
                                        if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("run"))
                                            GetComponent<Animator>().Play("run");
                                        transform.position = Vector2.MoveTowards(transform.position, playerDeffensiveDetect.GetComponent<FencingBot_PlayerDetect>().target.transform.position, Time.deltaTime * speed);
                                        Vector3 diff = playerDeffensiveDetect.GetComponent<FencingBot_PlayerDetect>().target.transform.position - transform.position;
                                        diff.Normalize();

                                        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                    }
                                }
                                else
                                {
                                    if (dashAnim == false && swordAnim == false)
                                    {
                                        if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("run"))
                                            GetComponent<Animator>().Play("run");
                                        Vector3 newTransform = transform.position;
                                        transform.position = Vector2.MoveTowards(transform.position, playerDeffensiveDetect.GetComponent<FencingBot_PlayerDetect>().target.transform.position, Time.deltaTime * speed);
                                        transform.position = newTransform + (newTransform - transform.position);
                                        Vector3 diff = transform.position - newTransform;
                                        diff.Normalize();

                                        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (dashAnim == false && swordAnim == false)
                            {
                                timeTillStill += Time.deltaTime;
                                GetComponent<Animator>().Play("idle");
                                timeTillTrigger = 0;
                                hasAttacked = false;
                            }
                        }
                    }
                }
                else if (botPlayStyle == "camper")
                {
                    if (timeTillStill >= 7.5f)
                    {
                        if (playerDetection.GetComponent<FencingBot_PlayerDetect>().target != null)
                        {
                            if (playerAttactTest.GetComponent<FencingBot_PlayerDetect>().target != null)
                            {
                                if (dashAnim == false && swordAnim == false)
                                {
                                    if (playerAttactTest.GetComponent<FencingBot_PlayerDetect>().target.GetComponent<FencingMates_Player>().lives >= lives && Random.Range(1, 1000000) % 2 == 0)
                                    {
                                        Vector3 diff = playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position - transform.position;
                                        diff.Normalize();

                                        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                                        GetComponent<Animator>().Play("dash");
                                    }
                                    else
                                    {
                                        transform.position = Vector2.MoveTowards(transform.position, playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position, Time.deltaTime * speed);
                                        Vector3 diff = playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position - transform.position;
                                        diff.Normalize();

                                        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                        GetComponent<Animator>().Play("attack");
                                    }
                                    timeTillStill = 0;
                                }
                            }
                            else
                            {
                                if (dashAnim == false && swordAnim == false)
                                {
                                    if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("run"))
                                        GetComponent<Animator>().Play("run");
                                    transform.position = Vector2.MoveTowards(transform.position, playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position, Time.deltaTime * speed);
                                    Vector3 diff = playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position - transform.position;
                                    diff.Normalize();

                                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                }
                            }
                        }
                        else
                        {
                            float minDistance = 9999;
                            GameObject currentPlayer = null;
                            foreach (FencingMates_Player player in FindObjectsOfType<FencingMates_Player>())
                            {
                                if (Vector2.Distance(player.transform.position, transform.position) < minDistance && player != GetComponent<FencingMates_Player>() && player.team != team)
                                {
                                    minDistance = Vector2.Distance(player.transform.position, transform.position);
                                    currentPlayer = player.gameObject;
                                }
                            }
                            if (dashAnim == false && swordAnim == false)
                            {
                                Vector3 diff = currentPlayer.transform.position - transform.position;
                                diff.Normalize();

                                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                GetComponent<Animator>().Play("dash");
                            }
                        }
                    }
                    else
                    {
                        if (playerDeffensiveDetect.GetComponent<FencingMate_CornerDetect>().target != null)
                        {
                            if (playerDetection.GetComponent<FencingBot_PlayerDetect>().target != null)
                            {
                                if (playerAttactTest.GetComponent<FencingBot_PlayerDetect>().target != null)
                                {
                                    if (dashAnim == false && swordAnim == false)
                                    {
                                        if (playerAttactTest.GetComponent<FencingBot_PlayerDetect>().target.GetComponent<FencingMates_Player>().lives >= lives && Random.Range(1, 1000000) % 2 == 0)
                                        {
                                            Vector3 diff = playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position - transform.position;
                                            diff.Normalize();

                                            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                                            GetComponent<Animator>().Play("dash");
                                        }
                                        else
                                        {
                                            transform.position = Vector2.MoveTowards(transform.position, playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position, Time.deltaTime * speed);
                                            Vector3 diff = playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position - transform.position;
                                            diff.Normalize();

                                            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                            GetComponent<Animator>().Play("attack");
                                        }
                                        timeTillStill = 0;
                                    }
                                }
                                else
                                {
                                    if (dashAnim == false && swordAnim == false)
                                    {
                                        if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("run"))
                                            GetComponent<Animator>().Play("run");
                                        transform.position = Vector2.MoveTowards(transform.position, playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position, Time.deltaTime * speed);
                                        Vector3 diff = playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position - transform.position;
                                        diff.Normalize();

                                        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                    }
                                }
                            }
                            else
                            {
                                if (Vector2.Distance(transform.position, playerDeffensiveDetect.GetComponent<FencingMate_CornerDetect>().target.transform.position) > 0.1f)
                                {
                                    if (dashAnim == false && swordAnim == false)
                                    {
                                        if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("run"))
                                            GetComponent<Animator>().Play("run");
                                        transform.position = Vector2.MoveTowards(transform.position, playerDeffensiveDetect.GetComponent<FencingMate_CornerDetect>().target.transform.position, Time.deltaTime * speed);
                                        Vector3 diff = playerDeffensiveDetect.GetComponent<FencingMate_CornerDetect>().target.transform.position - transform.position;
                                        diff.Normalize();

                                        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                    }
                                }
                                else
                                {
                                    if (dashAnim == false && swordAnim == false)
                                    {
                                        timeTillStill += Time.deltaTime;
                                        GetComponent<Animator>().Play("idle");
                                        timeTillTrigger = 0;
                                        hasAttacked = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            playerDeffensiveDetect.GetComponent<CircleCollider2D>().enabled = false;
                            playerDeffensiveDetect.GetComponent<CircleCollider2D>().enabled = true;
                            float minDistance = 9999;
                            GameObject currentPlayer = null;
                            foreach (FencingMate_Corner player in FindObjectsOfType<FencingMate_Corner>())
                            {
                                if (Vector2.Distance(player.transform.position, transform.position) < minDistance && player != GetComponent<FencingMate_Corner>())
                                {
                                    minDistance = Vector2.Distance(player.transform.position, transform.position);
                                    currentPlayer = player.gameObject;
                                }
                            }
                            if (dashAnim == false && swordAnim == false)
                            {
                                Vector3 diff = currentPlayer.transform.position - transform.position;
                                diff.Normalize();

                                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                GetComponent<Animator>().Play("dash");
                            }
                        }
                    }
                }
                else if (botPlayStyle == "runner")
                {
                    if (!dashAnim && !swordAnim)
                    {
                        if (playerDetection.GetComponent<FencingBot_PlayerDetect>().target != null && killPlayers)
                        {
                            if (playerAttactTest.GetComponent<FencingBot_PlayerDetect>().target != null)
                            {
                                GetComponent<Animator>().Play("attack");
                            }
                            else
                            {
                                transform.position = Vector2.MoveTowards(transform.position, playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position, Time.deltaTime * speed);
                                Vector3 diff = playerDetection.GetComponent<FencingBot_PlayerDetect>().target.transform.position - transform.position;
                                diff.Normalize();

                                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                GetComponent<Animator>().Play("run");
                            }
                        }
                        else if(waypoints.Count != waypointIndex)
                        {
                            if ((!dashAnim && waypoints[waypointIndex].GetComponent<Deathrunners_BotRadius>().block == null) || (!dashAnim && difficulty == "easy"))
                            {
                                while (Vector2.Distance(transform.position, waypoints[waypointIndex].transform.position) < 0.25f && difficulty == "expert")
                                    waypointIndex++;
                                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                                if (waypoints[waypointIndex].name == "dash")
                                {
                                    Vector3 diff = waypoints[waypointIndex].transform.position - transform.position;
                                    diff.Normalize();

                                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                    GetComponent<Animator>().Play("dash");
                                    savedWaypoint = waypointIndex;
                                }
                                else if (waypoints[waypointIndex].name == "fakedash")
                                {
                                    if (difficulty == "expert")
                                    {
                                        if (Random.Range(1, 1000000) % 4 == 0)
                                        {
                                            waypointIndex++;
                                        }
                                        else
                                        {
                                            Vector3 diff = waypoints[waypointIndex].transform.position - transform.position;
                                            diff.Normalize();

                                            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                            GetComponent<Animator>().Play("dash");
                                            savedWaypoint = waypointIndex;
                                            waypointIndex--;
                                        }
                                    }
                                    else
                                    {
                                        Vector3 diff = waypoints[waypointIndex].transform.position - transform.position;
                                        diff.Normalize();

                                        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                        GetComponent<Animator>().Play("dash");
                                        savedWaypoint = waypointIndex;
                                    }
                                }
                                else
                                {
                                    Vector3 newPos = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, Time.deltaTime * speed);
                                    Vector3 diff = newPos - transform.position;
                                    diff.Normalize();

                                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                    transform.position = newPos;
                                    GetComponent<Animator>().Play("run");
                                }
                            }
                            else
                            {
                                GetComponent<Animator>().Play("idle");
                            }
                        }
                        else if (!dashAnim)
                            GetComponent<Animator>().Play("idle");
                    }
                    if (((Vector2.Distance(transform.position, waypoints[waypointIndex].transform.position) < 0.25f && difficulty == "expert")|| (Vector2.Distance(transform.position, waypoints[waypointIndex].transform.position) < 0.5f && difficulty != "expert")) && !(waypoints[waypointIndex].name == "fakedash" && dashAnim))
                    {
                        waypointIndex++;
                    }
                }
                else if(botPlayStyle == "trapper")
                {
                    if (!swordAnim && !dashAnim)
                    {
                        if (waypointIndex == waypoints.Count && playerDeffensiveDetect.GetComponent<FencingBot_PlayerDetect>().target != null)
                            if(playerDeffensiveDetect.GetComponent<FencingBot_PlayerDetect>().target.GetComponent<FencingMates_Player>().killPlayers)
                                botPlayStyle = "deffensive";
                        if (difficulty != "easy")
                        {
                            if (foundWaypoint == false)
                            {
                                if (Vector2.Distance(transform.position, waypoints[waypointIndex].transform.position) < 0.25f)
                                {
                                    if (waypoints[waypointIndex].name == "90")
                                        transform.eulerAngles = new Vector3(0, 0, 90);
                                    if (waypoints[waypointIndex].name == "180")
                                        transform.eulerAngles = new Vector3(0, 0, 180);
                                    if (waypoints[waypointIndex].name == "270")
                                        transform.eulerAngles = new Vector3(0, 0, 270);
                                    if (waypoints[waypointIndex].name == "0")
                                        transform.eulerAngles = new Vector3(0, 0, 0);
                                    if (waypoints[waypointIndex].name == "-45")
                                        transform.eulerAngles = new Vector3(0, 0, -45);
                                    if(waypoints[waypointIndex].name != "pass")
                                        foundWaypoint = true;
                                    waypointIndex++;
                                }
                                else
                                {
                                    Vector3 newPos = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, Time.deltaTime * speed);
                                    Vector3 diff = newPos - transform.position;
                                    diff.Normalize();

                                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                    transform.position = newPos;
                                    if (Vector2.Distance(transform.position, waypoints[waypointIndex].transform.position) > 2 && difficulty != "normal")
                                        GetComponent<Animator>().Play("dash");
                                    else GetComponent<Animator>().Play("run");
                                }
                            }
                            else
                            {
                                if (trapperRadius.GetComponent<Deathrunners_TrapperRadius>().player != null)
                                {
                                    GetComponent<Animator>().Play("attack");
                                    foundWaypoint = false;
                                }
                                else
                                {
                                    GetComponent<Animator>().Play("idle");
                                }
                            }
                        }
                    }
                }
            }
            if (lavaColl != null)
            {
                if (Physics2D.IsTouching(GetComponent<BoxCollider2D>(), lavaColl) == true && dashAnim == false)
                {
                    if (takeDamageFromLava)
                    {
                        takeDamageFromLava = false;
                        Invoke("TakeDamageFromLava", 1.5f);
                        StartCoroutine(DecreaseHp(1));
                    }
                }
            }
        }
    }
    public GameObject trapperRadius;
    bool foundWaypoint = false;
    int savedWaypoint;
    public float timeTillStill;
    public float timeTillTrigger;
    bool hasAttacked = false;
    public void BotDash()
    {
        if (dashAnim == false && swordAnim == false)
        {
            GetComponent<Animator>().Play("dash");
        }
    }
    public void Dash()
    {
        deaths = FindObjectsOfType<Deathrunners_Death>();
        foreach (Deathrunners_Death death in deaths)
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), death.GetComponent<BoxCollider2D>());
        foreach (Deathrunners_Death death in unlistedDeaths)
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), death.GetComponent<BoxCollider2D>());
        ableToDash = false;
        GetComponent<Rigidbody2D>().velocity = -transform.up * dashSpeed;
        dashAnim = true;
        GetComponent<AudioSource>().clip = dash;
        GetComponent<AudioSource>().Play();
    }
    public void BotAttack()
    {
        if (dashAnim == false && swordAnim == false)
            GetComponent<Animator>().Play("attack");
    }
    public void Attack()
    {
        swordAnim = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<AudioSource>().clip = attack;
        GetComponent<AudioSource>().Play();
    }
}
