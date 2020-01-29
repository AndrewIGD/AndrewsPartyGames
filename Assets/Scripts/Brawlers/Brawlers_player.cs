using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SharpDX.DirectInput;
using UnityEngine.SceneManagement;

public class Brawlers_player : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public bool moveLeft = true;
    public bool moveRight = true;
    public int playerNumber;
    public bool grounded = false;
    public GameObject deathVfx;
    public GameObject tiltedVfx;
    public AudioSource jump;
    public AudioSource death;
    public bool playingAnim = false;
    bool playingMidAirAnim = false;
    public SpriteRenderer[] limbs;
    public int time = 0;
    public int team;
    public GameObject weapon;
    public int face = 0;
    public bool flying = false;
    public float damage;
    public bool attacking = false;
    public Brawlers_weaponHitbox[] weaponHitBoxes;
    public PhysicsMaterial2D bounciness;
    public PhysicsMaterial2D normal;
    public Vector2 dash;
    public Coroutine currentDamageCoroutine;
        Color32[] colors;
    public bool recovery;
    public bool dair;
    public int jumps;
    public GameObject limbsObj;
    public Joystick joystick;
    public int sus;
    public int jos;
    public int dreapta;
    public int stanga;
    public int button1;
    public int button2;
    public bool bot;
    public string botPlayStyle;
    public bool outOfMapRadius = false;
    public bool touchingWall = false;
    public int points = 0;
    public int chanceToAttack;
    public string difficulty;
    public GameObject botObj;
    public List<GameObject> normalAttacks;
    public List<GameObject> swordAttacks;
    public List<GameObject> helbardAttacks;
    public int hitAttacks = 0;
    float attackSpeed = 1;
    float damageMultiplier = 1;

    public void Dair()
    {
        dair = true;
    }
    public void StopDamageCoroutines()
    {
        stopCoroutine = true;
    }
    public bool stopCoroutine;
    public GameObject arm1;
    public GameObject arm2;
    public GameObject hand1;
    public GameObject arm3;
    public GameObject arm4;
    public GameObject hand2;
    public void IncreaseDamage(float damageValue, float stun)
    {

        if(Random.Range(1,100000)%15 == 0)
        {
            if (weapon != null)
            {
                foreach(GameObject weaponz in FindObjectOfType<Brawlers_weaponSpawn>().weapons)
                {
                    if(weaponz.GetComponent<SpriteRenderer>().sprite == weapon.GetComponent<SpriteRenderer>().sprite)
                    {
                        GameObject weaponThrow = Instantiate(weaponz);
                        weaponThrow.GetComponent<Brawlers_groundWeapon>().death = true;
                        weaponThrow.transform.position = transform.position;
                        weaponThrow.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
                        float[] values = { -10f, 10f };
                        weaponThrow.GetComponent<Rigidbody2D>().angularVelocity = values[Random.Range(0, 100000) % 2];
                        Destroy(weapon);
                        weapon = null;
                        arm1.GetComponent<SpriteRenderer>().sortingOrder = 9;
                        arm2.GetComponent<SpriteRenderer>().sortingOrder = 10;
                        hand1.GetComponent<SpriteRenderer>().sortingOrder = 8;
                        arm3.GetComponent<SpriteRenderer>().sortingOrder = 60;
                        arm4.GetComponent<SpriteRenderer>().sortingOrder = 70;
                        hand2.GetComponent<SpriteRenderer>().sortingOrder = 55;
                        if (bot)
                        {
                            swordAttacks[0].SetActive(false);
                            swordAttacks[1].SetActive(false);
                            swordAttacks[2].SetActive(false);
                            swordAttacks[3].SetActive(false);
                            swordAttacks[4].SetActive(false);
                            helbardAttacks[0].SetActive(false);
                            helbardAttacks[1].SetActive(false);
                            helbardAttacks[2].SetActive(false);
                            helbardAttacks[3].SetActive(false);
                            helbardAttacks[4].SetActive(false);
                            normalAttacks[0].SetActive(true);
                            normalAttacks[1].SetActive(true);
                            if (difficulty == "expert")
                            {
                                normalAttacks[2].SetActive(true);
                                normalAttacks[3].SetActive(true);
                            }

                        }
                        break;
                    }
                }
            }
        }
        CancelInvoke("StopDamage");
            damage += damageValue*damageMultiplier;
        for (int i=0;i<limbs.Length;i++)
        {
            limbs[i].color = new Color32(255, 0, 0, 255);
        }
        damaged.Play();
        Invoke("StopDamage", stun);
    }
    public void StopDamage()
    {
        for (int i = 0; i < limbs.Length; i++)
        {
            limbs[i].color = colors[i];
        }
    }
    public void Launch(float x, float y, float stun, float xDamageMultiplier, float yDamageMultiplier, int face)
    {
        waypoint = null;
        CancelInvoke("StopFlying");
        GetComponent<Rigidbody2D>().sharedMaterial = bounciness;
        StopAttacking();
        if (permaDamage == false)
        {
            if (face == 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) + new Vector2(xDamageMultiplier, yDamageMultiplier) * damage;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-x, y) + new Vector2(-xDamageMultiplier, yDamageMultiplier) * damage;
            }
        }
        else
        {
            if (face == 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) + new Vector2(xDamageMultiplier, yDamageMultiplier) * damage + new Vector2(Random.Range(-3f,3f), Random.Range(-3f,3f));
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-x, y) + new Vector2(-xDamageMultiplier, yDamageMultiplier) * damage + new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            }
        }
        flying = true;
        Debug.Log(x + " " + y + " " + stun + " " + xDamageMultiplier + " " + yDamageMultiplier);
        Invoke("StopFlying", stun);
    }
    public void StopFlying()
    {
        GetComponent<Rigidbody2D>().sharedMaterial = normal;
        flying = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
    }
    public void Attacking()
    {
        attacking = true;
    }
    public void StopAttacking()
    {
        foreach(Brawlers_weaponHitbox hitbox in weaponHitBoxes)
        {
            hitbox.Stop();
        }
        attacking = false;
        playingAnim = false;
        GetComponent<Rigidbody2D>().drag = 0;
        recovery = false;

    }
    public void MoveInDirection()
    {
        if(face == 0)
            GetComponent<Rigidbody2D>().velocity = dash;
        else GetComponent<Rigidbody2D>().velocity = new Vector2(-dash.x, dash.y);
    }
    public void StopMovingInDirection()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
    }
    public void AllowLeft(bool allow)
    {
        moveLeft = allow;
    }
    public void AllowRight(bool allow)
    {
        moveRight = allow;
    }
    public void IsGrounded(bool allow)
    {
        grounded = allow;
        if (allow == true)
            jumps = 3;
    }
    private void Times()
    {
        time++;
        Invoke("Times", 1f);
    }
    public void Recovery()
    {
        recovery = false;
    }
    public TextMeshPro teamColor;
    public float defaultAxis;

    private void Enable()
    {
        if (bot)
        {
            if (permaDamage == false)
            {
                speed = PlayerPrefs.GetFloat("BrawlersPlayerSpeed", speed);
                jumpHeight = PlayerPrefs.GetFloat("BrawlersPlayerJump", jumpHeight);
                damageMultiplier = PlayerPrefs.GetFloat("BrawlersPlayerDamageMultiplier", damageMultiplier);
                attackSpeed = PlayerPrefs.GetFloat("BrawlersPlayerAttackSpeed", attackSpeed);
                GetComponent<Animator>().speed = attackSpeed;
            }
            else
            {
                speed = PlayerPrefs.GetFloat("KOTHPlayerSpeed", speed);
                jumpHeight = PlayerPrefs.GetFloat("KOTHPlayerJump", jumpHeight);
                permaDamageSetting = PlayerPrefs.GetFloat("KOTHPlayerDamage", permaDamageSetting);
                attackSpeed = PlayerPrefs.GetFloat("KOTHPlayerAttackSpeed", attackSpeed);
                GetComponent<Animator>().speed = attackSpeed;
            }
            Invoke("Times", 1f);
            foreach (Transform child in limbsObj.transform)
            {
                if (child.name == "arm")
                    arm3 = child.gameObject;
                if (child.name == "arm (1)")
                    arm1 = child.gameObject;
                if (child.name == "arm (2)")
                    arm4 = child.gameObject;
                if (child.name == "arm (3)")
                    arm2 = child.gameObject;
                if (child.name == "hand")
                    hand2 = child.gameObject;
                if (child.name == "hand (1)")
                    hand1 = child.gameObject;
            }
            tiltedVfx = GameObject.Find("Titled Vfx");
            deathVfx = GameObject.Find("Death vfx");
            normalAttacks[0].SetActive(true);
            normalAttacks[1].SetActive(true);
            if (difficulty == "expert")
            {
                normalAttacks[2].SetActive(true);
                normalAttacks[3].SetActive(true);
            }
            if (difficulty == "easy")
                chanceToAttack = 50;
            else if (difficulty == "normal")
                chanceToAttack = 20;
            else if (difficulty == "hard")
                chanceToAttack = 5;
            else chanceToAttack = 1;
            Brawlers_player[] players = FindObjectsOfType<Brawlers_player>();
            foreach (Brawlers_player player in players)
            {
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
            }


            Invoke("StartMoving", 5.5f);

            bool ok = false;
            foreach (int player in FindObjectOfType<PlayerData>().players)
            {
                if (playerNumber == player)
                {
                    ok = true;
                    for (int i = 0; i < limbs.Length; i++)
                    {
                        if (i <= 2)
                        {
                            limbs[i].color = FindObjectOfType<PlayerData>().color1[playerNumber];
                        }
                        else limbs[i].color = FindObjectOfType<PlayerData>().color2[playerNumber];
                    }
                }
            }
            if (ok == false)
            {
                Destroy(gameObject);
            }
            else
            {
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
                teamColor.text = FindObjectOfType<PlayerData>().playerNames[playerNumber];
            }
            colors = new Color32[limbs.Length];
            for (int i = 0; i < limbs.Length; i++)
            {
                colors[i] = limbs[i].color;
            }
        }
    }

    private void Awake()
    {
        if (permaDamage == false)
        {
            speed = PlayerPrefs.GetFloat("BrawlersPlayerSpeed", speed);
            jumpHeight = PlayerPrefs.GetFloat("BrawlersPlayerJump", jumpHeight);
            damageMultiplier = PlayerPrefs.GetFloat("BrawlersPlayerDamageMultiplier", damageMultiplier);
            attackSpeed = PlayerPrefs.GetFloat("BrawlersPlayerAttackSpeed", attackSpeed);
            GetComponent<Animator>().speed = attackSpeed;
        }
        else
        {
            speed = PlayerPrefs.GetFloat("KOTHPlayerSpeed", speed);
            jumpHeight = PlayerPrefs.GetFloat("KOTHPlayerJump", jumpHeight);
            permaDamageSetting = PlayerPrefs.GetFloat("KOTHPlayerDamage", permaDamageSetting);
            attackSpeed = PlayerPrefs.GetFloat("KOTHPlayerAttackSpeed", attackSpeed);
            GetComponent<Animator>().speed = attackSpeed;
        }
        foreach (Transform child in limbsObj.transform)
        {
            if (child.name == "arm")
                arm3 = child.gameObject;
            if (child.name == "arm (1)")
                arm1 = child.gameObject;
            if (child.name == "arm (2)")
                arm4 = child.gameObject;
            if (child.name == "arm (3)")
                arm2 = child.gameObject;
            if (child.name == "hand")
                hand2 = child.gameObject;
            if (child.name == "hand (1)")
                hand1 = child.gameObject;
        }
        tiltedVfx = GameObject.Find("Titled Vfx");
        deathVfx = GameObject.Find("Death vfx");
        Invoke("StartMoving", 5.5f);
        if (FindObjectOfType<PlayerData>().teams == false)
            team = playerNumber;
        if (FindObjectOfType<PlayerData>().bots[playerNumber] == true && bot == false)
        {
            GameObject botz = Instantiate(botObj);
            Brawlers_player[] players2 = FindObjectsOfType<Brawlers_player>();
            foreach (Brawlers_player player in players2)
            {
                Physics2D.IgnoreCollision(botz.GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
            }
            botz.transform.position = transform.position;
            botz.GetComponent<Brawlers_player>().difficulty = FindObjectOfType<PlayerData>().botDifficulties[playerNumber];
            botz.GetComponent<Brawlers_player>().playerNumber = playerNumber;
            botz.GetComponent<Brawlers_player>().team = team;
            botz.GetComponent<Brawlers_player>().permaDamage = permaDamage;
            botz.GetComponent<Brawlers_player>().permaDamageSetting = permaDamageSetting;
            FindObjectOfType<SpawnPlayers>().players.Add(botz);
            botz.GetComponent<Brawlers_player>().Enable();
            Destroy(gameObject);
        }
        Brawlers_player[] players = FindObjectsOfType<Brawlers_player>();
        foreach (Brawlers_player player in players)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        }
        if (!bot)
        {
            joystick = FindObjectOfType<PlayerData>().controllers[playerNumber];
            if (joystick == null || FindObjectOfType<PlayerData>().pollControllers[playerNumber] || FindObjectOfType<PlayerData>().players.Contains(playerNumber) == false)
                Destroy(gameObject);
            else
            {
                FindObjectOfType<SpawnPlayers>().players.Add(gameObject);
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

            bool ok = false;
            foreach (int player in FindObjectOfType<PlayerData>().players)
            {
                if (playerNumber == player)
                {
                    ok = true;
                    for (int i = 0; i < limbs.Length; i++)
                    {
                        if (i <= 2)
                        {
                            limbs[i].color = FindObjectOfType<PlayerData>().color1[playerNumber];
                        }
                        else limbs[i].color = FindObjectOfType<PlayerData>().color2[playerNumber];
                    }
                }
            }
            if (ok == false)
            {
                Destroy(gameObject);
            }
            else
            {
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
                teamColor.text = FindObjectOfType<PlayerData>().playerNames[playerNumber];
            }
            colors = new Color32[limbs.Length];
            for (int i = 0; i < limbs.Length; i++)
            {
                colors[i] = limbs[i].color;
            }
            Invoke("Times", 1f);
        }
    }
    public GameObject itemObj;

    public void GiveWeapon(GameObject weaponObj)
    {
        playingAnim = false;
        GameObject weaponClone = Instantiate(weaponObj);
        weapon = weaponClone;
        weapon.transform.SetParent(itemObj.transform);
        weapon.transform.localScale = new Vector3(1, 1, 1);
        weapon.transform.localPosition = new Vector3(0, 0, 0);
        weapon.transform.localEulerAngles = new Vector3(0, 0, 0);
        for (int i = 0; i < normalAttacks.Count; i++)
            normalAttacks[i].SetActive(false);
        if (bot)
        {
            if (weapon.tag == "sword")
            {
                swordAttacks[0].SetActive(true);
                swordAttacks[1].SetActive(true);
                if (difficulty == "expert")
                {
                    swordAttacks[2].SetActive(true);
                    swordAttacks[3].SetActive(true);
                    swordAttacks[4].SetActive(true);
                }
            }
            else
            {
                helbardAttacks[0].SetActive(true);
                helbardAttacks[1].SetActive(true);
                if (difficulty == "expert")
                {
                    helbardAttacks[2].SetActive(true);
                    helbardAttacks[3].SetActive(true);
                    helbardAttacks[4].SetActive(true);
                }
            }
        }
    }
    public void Noteams()
    {
        Invoke("Times", 1f);
        Brawlers_player[] players = FindObjectsOfType<Brawlers_player>();
        foreach (Brawlers_player player in players)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        }
        Invoke("StartMoving", 5.5f);
    }
    public void Withteams()
    {
        Invoke("Times", 1f);
        Brawlers_player[] players = FindObjectsOfType<Brawlers_player>();
        foreach (Brawlers_player player in players)
        { 
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        }

        Invoke("StartMoving", 5.5f);
    }
    public void SlightFollowup()
    {
        foreach(Brawlers_weaponHitbox hitbox in weaponHitBoxes)
        {
            if(hitbox.objects.Count >= 1)
            {
                foreach (Brawlers_weaponHitbox hitboxx in weaponHitBoxes)
                {
                    hitboxx.Stop();
                }
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal" + playerNumber)) > Mathf.Abs(Input.GetAxisRaw("Vertical" + playerNumber)))
                {
                    GetComponent<Animator>().Play("unarmed_sidelight_followup1");
                }
                else
                {
                    GetComponent<Animator>().Play("unarmed_sidelight_followup2");
                }
            }
        }
    }
    public AudioSource hit;
    public AudioSource damaged;
    public void HitSound()
    {
        hit.Play();
    }
    public void StopDair()
    {
        dair = false;
    }
    public void DairHitPlayer()
    {
        if(weapon != null)
        {
            if(weapon.tag == "halberd")
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 7.5f);
            }
        }
        else GetComponent<Animator>().Play("unarmed_dair_hit");
        GetComponent<Rigidbody2D>().gravityScale = 1;
        dair = false;
    }
    public void DairHitGround()
    {
        if(weapon != null)
        {
            if(weapon.tag == "halberd")
            {

            }
        }
        else GetComponent<Animator>().Play("unarmed_dair_ground");
        dair = false;
    }
    public bool onWall = false;
    public GameObject border1;
    public GameObject border2;
    bool keyboard = false;
    bool disconnected = false;
    // Update is called once per frame
    float delta = 0f;
    void Update()
    {
        bool ret = false;
        if(bot==false)
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
            if (permaDamage)
                damage = permaDamageSetting;
            if(GetComponent<Rigidbody2D>().gravityScale == 1 && GetComponent<Rigidbody2D>().velocity.y < -10f)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -10);
            }
            else if (GetComponent<Rigidbody2D>().gravityScale == 2.5f && GetComponent<Rigidbody2D>().velocity.y < -15f)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -15f);
            }
            if (!bot)
            {
                if (keyboard)
                {
                    if (CustomControls.GetButton(joystick, jos))
                    {
                        if (!dair)
                        {
                            GetComponent<Rigidbody2D>().gravityScale = 2.5f;
                        }
                    }
                    else
                    {
                        if (onWall)
                        {
                            GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                        }
                        else GetComponent<Rigidbody2D>().gravityScale = 1;
                    }
                }
                else
                {
                    if (CustomControls.GetAxis(joystick).Yaxis > 40000)
                    {
                        if (!dair)
                        {
                            GetComponent<Rigidbody2D>().gravityScale = 2.5f;
                        }
                    }
                    else
                    {
                        if (onWall)
                        {
                            GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                        }
                        else GetComponent<Rigidbody2D>().gravityScale = 1;
                    }
                }
                if (!grounded && !(moveRight && moveLeft || grounded) && !flying && !attacking)
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
                    }
                    else GetComponent<Rigidbody2D>().velocity = new Vector2(speed * (CustomControls.GetAxis(joystick).Xaxis - 32767) / 32767, GetComponent<Rigidbody2D>().velocity.y);
                    jumps = 3;
                    onWall = true;
                    if (weapon != null)
                    {
                        if (weapon.tag == "sword")
                            GetComponent<Animator>().Play("sword_idleOnWall");
                        else if (weapon.tag == "halberd")
                            GetComponent<Animator>().Play("halberd_idleOnWall");
                    }
                    else GetComponent<Animator>().Play("idleOnWall");
                }
                else onWall = false;
                if (dair)
                {
                    if (weapon == null)
                    {
                        if (CustomControls.GetButton(joystick, button2))
                        {
                            GetComponent<Animator>().Play("unarmed_dair_hold");
                        }
                        else
                        {
                            GetComponent<Animator>().Play("unarmed_dair_restart");
                            dair = false;
                        }
                    }

                }
                if (flying)
                {
                    if (weapon != null)
                    {
                        if (weapon.tag == "sword")
                            GetComponent<Animator>().Play("sword_hurt");
                        else if (weapon.tag == "halberd")
                            GetComponent<Animator>().Play("halberd_hurt");
                    }
                    else
                        GetComponent<Animator>().Play("hurt");
                }
                if (ableToMove && tutorialAbleToMove && !flying && !attacking)
                {
                    if (grounded == false)
                    {
                        if (!onWall)
                        {
                            if (weapon != null)
                            {
                                if (weapon.tag == "sword")
                                    GetComponent<Animator>().Play("sword_jump");
                                else if (weapon.tag == "halberd")
                                    GetComponent<Animator>().Play("halberd_jump");
                            }
                            else
                                GetComponent<Animator>().Play("jump");
                        }
                        playingMidAirAnim = true;
                        playingAnim = false;
                    }
                    else playingMidAirAnim = false;
                    if (keyboard)
                    {
                        if (CustomControls.GetButton(joystick, stanga) && (moveLeft || grounded))
                        {
                            border1.GetComponent<Brawlers_border>().type = 0;
                            border2.GetComponent<Brawlers_border>().type = 1;
                            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);

                            face = 1;
                            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
                            if (playingAnim == false && playingMidAirAnim == false)
                            {
                                playingAnim = true;
                                if (weapon != null)
                                {
                                    if (weapon.tag == "sword")
                                        GetComponent<Animator>().Play("sword_run");
                                    else if (weapon.tag == "halberd")
                                        GetComponent<Animator>().Play("halberd_run");
                                }
                                else GetComponent<Animator>().Play("run");
                            }
                        }
                        else if (CustomControls.GetButton(joystick, dreapta) && (moveRight||grounded))
                        {
                            border1.GetComponent<Brawlers_border>().type = 1;
                            border2.GetComponent<Brawlers_border>().type = 0;
                            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);

                            face = 0;
                            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
                            if (playingAnim == false && playingMidAirAnim == false)
                            {
                                playingAnim = true;
                                if (weapon != null)
                                {
                                    if (weapon.tag == "sword")
                                        GetComponent<Animator>().Play("sword_run");
                                    else if (weapon.tag == "halberd")
                                        GetComponent<Animator>().Play("halberd_run");
                                }
                                else GetComponent<Animator>().Play("run");
                            }
                        }
                        else
                        {
                            if (!onWall)
                            {
                                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                                if (playingMidAirAnim == false)
                                {
                                    playingAnim = false;
                                    if (weapon != null)
                                    {
                                        if (weapon.tag == "sword")
                                            GetComponent<Animator>().Play("sword_idle");
                                        else if (weapon.tag == "halberd")
                                            GetComponent<Animator>().Play("halberd_idle");
                                    }
                                    else GetComponent<Animator>().Play("idle");

                                }
                            }
                            else
                            {
                                if (moveLeft == false)
                                {
                                    border1.GetComponent<Brawlers_border>().type = 1;
                                    border2.GetComponent<Brawlers_border>().type = 0;
                                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);

                                    face = 0;
                                }
                                else
                                {
                                    border1.GetComponent<Brawlers_border>().type = 0;
                                    border2.GetComponent<Brawlers_border>().type = 1;
                                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);

                                    face = 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (CustomControls.GetAxis(joystick).Xaxis < 20000 && (moveLeft||grounded))
                        {
                            border1.GetComponent<Brawlers_border>().type = 0;
                            border2.GetComponent<Brawlers_border>().type = 1;
                            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);

                            face = 1;
                            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * (CustomControls.GetAxis(joystick).Xaxis - 32767) / 32767, GetComponent<Rigidbody2D>().velocity.y);
                            if (playingAnim == false && playingMidAirAnim == false)
                            {
                                playingAnim = true;
                                if (weapon != null)
                                {
                                    if (weapon.tag == "sword")
                                        GetComponent<Animator>().Play("sword_run");
                                    else if (weapon.tag == "halberd")
                                        GetComponent<Animator>().Play("halberd_run");
                                }
                                else GetComponent<Animator>().Play("run");
                            }
                        }
                        else if (CustomControls.GetAxis(joystick).Xaxis > 40000 && (moveRight||grounded))
                        {
                            border1.GetComponent<Brawlers_border>().type = 1;
                            border2.GetComponent<Brawlers_border>().type = 0;
                            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);

                            face = 0;
                            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * (CustomControls.GetAxis(joystick).Xaxis - 32767) / 32767, GetComponent<Rigidbody2D>().velocity.y);
                            if (playingAnim == false && playingMidAirAnim == false)
                            {
                                playingAnim = true;
                                if (weapon != null)
                                {
                                    if (weapon.tag == "sword")
                                        GetComponent<Animator>().Play("sword_run");
                                    else if (weapon.tag == "halberd")
                                        GetComponent<Animator>().Play("halberd_run");
                                }
                                else GetComponent<Animator>().Play("run");
                            }
                        }
                        else
                        {
                            if (!onWall)
                            {
                                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                                if (playingMidAirAnim == false)
                                {
                                    playingAnim = false;
                                    if (weapon != null)
                                    {
                                        if (weapon.tag == "sword")
                                            GetComponent<Animator>().Play("sword_idle");
                                        else if (weapon.tag == "halberd")
                                            GetComponent<Animator>().Play("halberd_idle");
                                    }
                                    else GetComponent<Animator>().Play("idle");

                                }
                            }
                            else
                            {
                                if (moveLeft == false)
                                {
                                    border1.GetComponent<Brawlers_border>().type = 1;
                                    border2.GetComponent<Brawlers_border>().type = 0;
                                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);

                                    face = 0;
                                }
                                else
                                {
                                    border1.GetComponent<Brawlers_border>().type = 0;
                                    border2.GetComponent<Brawlers_border>().type = 1;
                                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);

                                    face = 1;
                                }
                            }
                        }
                    }
                    if (CustomControls.GetButton(joystick, button1))
                    {
                        if (hasJumpedGround == false && hasJumpedGround == false)
                        {
                            if (grounded)
                            {
                                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
                                jump.Play();
                                hasJumpedGround = true;
                            }
                        }

                    }
                    else hasJumpedGround = false;
                    if (CustomControls.GetButton(joystick, button1))
                    {
                        if (hasJumpedAir == false && hasJumpedGround == false)
                        {
                            if (jumps > 0)
                            {
                                jumps--;
                                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
                                if (onWall)
                                {
                                    if (moveRight == true)
                                        GetComponent<Rigidbody2D>().velocity = new Vector2(-jumpHeight, jumpHeight);
                                    else GetComponent<Rigidbody2D>().velocity = new Vector2(jumpHeight, jumpHeight);
                                }
                                jump.Play();
                            }
                            hasJumpedAir = true;
                        }
                    }
                    else hasJumpedAir = false;
                    if (CustomControls.GetButton(joystick, button2) && !onWall)
                    {
                        if (hasAttacked == false)
                        {
                            if (keyboard)
                            {
                                if (grounded)
                                {
                                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                                    if ((CustomControls.GetButton(joystick, stanga) || CustomControls.GetButton(joystick, dreapta)))
                                    {
                                        if (weapon != null)
                                        {
                                            if (weapon.tag == "sword")
                                                GetComponent<Animator>().Play("sword_sidelight");
                                            else if (weapon.tag == "halberd")
                                                GetComponent<Animator>().Play("halberd_sidelight");
                                        }
                                        else GetComponent<Animator>().Play("unarmed_sidelight");
                                    }
                                    else
                                    {
                                        if (CustomControls.GetButton(joystick, jos))
                                        {
                                            if (weapon != null)
                                            {
                                                if (weapon.tag == "sword")
                                                    GetComponent<Animator>().Play("sword_downlight");
                                                else if (weapon.tag == "halberd")
                                                    GetComponent<Animator>().Play("halberd_downlight");
                                            }
                                            else GetComponent<Animator>().Play("unarmed_downlight");
                                        }
                                        else
                                        {
                                            if (weapon != null)
                                            {
                                                if (weapon.tag == "sword")
                                                    GetComponent<Animator>().Play("sword_neutrallight");
                                                else if (weapon.tag == "halberd")
                                                    GetComponent<Animator>().Play("halberd_neutrallight");
                                            }
                                            else GetComponent<Animator>().Play("unarmed_neutrallight");
                                        }
                                    }
                                }
                                else
                                {
                                    GetComponent<Rigidbody2D>().drag = 3;
                                    if ((CustomControls.GetButton(joystick, stanga) || CustomControls.GetButton(joystick, dreapta)))
                                    {
                                        if (weapon != null)
                                        {
                                            if (weapon.tag == "sword")
                                                GetComponent<Animator>().Play("sword_sair");
                                            else if (weapon.tag == "halberd")
                                                GetComponent<Animator>().Play("halberd_sair");
                                        }
                                        else GetComponent<Animator>().Play("unarmed_sair");
                                    }
                                    else
                                    {
                                        if (CustomControls.GetButton(joystick, jos))
                                        {
                                            if (weapon != null)
                                            {
                                                if (weapon.tag == "sword")
                                                    GetComponent<Animator>().Play("sword_dair");
                                                else if (weapon.tag == "halberd")
                                                    GetComponent<Animator>().Play("halberd_dair");
                                            }
                                            else GetComponent<Animator>().Play("unarmed_dair");
                                        }
                                        else
                                        {
                                            if (weapon != null)
                                            {
                                                if (weapon.tag == "sword")
                                                    GetComponent<Animator>().Play("sword_nair");
                                                else if (weapon.tag == "halberd")
                                                    GetComponent<Animator>().Play("halberd_nair");
                                            }
                                            else GetComponent<Animator>().Play("unarmed_nair");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (grounded)
                                {
                                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                                    if (Mathf.Abs(CustomControls.GetAxis(joystick).Xaxis - 32767) > Mathf.Abs(CustomControls.GetAxis(joystick).Yaxis - 32767))
                                    {
                                        if (weapon != null)
                                        {
                                            if (weapon.tag == "sword")
                                                GetComponent<Animator>().Play("sword_sidelight");
                                            else if (weapon.tag == "halberd")
                                                GetComponent<Animator>().Play("halberd_sidelight");
                                        }
                                        else GetComponent<Animator>().Play("unarmed_sidelight");
                                    }
                                    else
                                    {
                                        if (CustomControls.GetAxis(joystick).Yaxis > 40000)
                                        {
                                            if (weapon != null)
                                            {
                                                if (weapon.tag == "sword")
                                                    GetComponent<Animator>().Play("sword_downlight");
                                                else if (weapon.tag == "halberd")
                                                    GetComponent<Animator>().Play("halberd_downlight");
                                            }
                                            else GetComponent<Animator>().Play("unarmed_downlight");
                                        }
                                        else
                                        {
                                            if (weapon != null)
                                            {
                                                if (weapon.tag == "sword")
                                                    GetComponent<Animator>().Play("sword_neutrallight");
                                                else if (weapon.tag == "halberd")
                                                    GetComponent<Animator>().Play("halberd_neutrallight");
                                            }
                                            else GetComponent<Animator>().Play("unarmed_neutrallight");
                                        }
                                    }
                                }
                                else
                                {
                                    GetComponent<Rigidbody2D>().drag = 3;
                                    if (Mathf.Abs(CustomControls.GetAxis(joystick).Xaxis - 32767) > Mathf.Abs(CustomControls.GetAxis(joystick).Yaxis - 32767))
                                    {
                                        if (weapon != null)
                                        {
                                            if (weapon.tag == "sword")
                                                GetComponent<Animator>().Play("sword_sair");
                                            else if (weapon.tag == "halberd")
                                                GetComponent<Animator>().Play("halberd_sair");
                                        }
                                        else GetComponent<Animator>().Play("unarmed_sair");
                                    }
                                    else
                                    {
                                        if (CustomControls.GetAxis(joystick).Yaxis > 40000)
                                        {
                                            if (weapon != null)
                                            {
                                                if (weapon.tag == "sword")
                                                    GetComponent<Animator>().Play("sword_dair");
                                                else if (weapon.tag == "halberd")
                                                    GetComponent<Animator>().Play("halberd_dair");
                                            }
                                            else GetComponent<Animator>().Play("unarmed_dair");
                                        }
                                        else
                                        {
                                            if (weapon != null)
                                            {
                                                if (weapon.tag == "sword")
                                                    GetComponent<Animator>().Play("sword_nair");
                                                else if (weapon.tag == "halberd")
                                                    GetComponent<Animator>().Play("halberd_nair");
                                            }
                                            else GetComponent<Animator>().Play("unarmed_nair");
                                        }
                                    }
                                }
                            }
                            hasAttacked = true;
                        }

                    }
                    else hasAttacked = false;
                }
            }
            else if(tutorialAbleToMove)
            {
                timeTillNew += Time.deltaTime;
                if(timeTillNew>= 5)
                {
                    timeTillNew = 0f;
                    if (Vector2.Distance(playerWaypoint.transform.position, transform.position) < 0.1f)
                    {
                        GameObject waypointObj = GameObject.Find("Waypoints");
                        List<GameObject> waypoints = new List<GameObject>();
                        foreach (Transform child in waypointObj.transform)
                        {
                            waypoints.Add(child.gameObject);
                        }
                        waypoint = waypoints[Random.Range(1, 1000000) % waypoints.Count];
                    }
                }
                oldPos = newPos;
                newPos = transform.position;
                if(playerWaypoint == null)
                {
                    Debug.Log("exec" + playerNumber);
                    Brawlers_player[] players2 = FindObjectsOfType<Brawlers_player>();
                    GameObject nearestObj2 = null;
                    float distance2 = 1000;
                    for(int player2=0;player2<players2.Length;player2++)
                    {
                        if (Vector2.Distance(transform.position, players2[player2].transform.position) < distance2 && players2[player2].gameObject != gameObject && players2[player2].gameObject != playerWaypoint && players2[player2].team != team)
                        {
                            distance2 = Vector2.Distance(transform.position, players2[player2].transform.position);
                            nearestObj2 = players2[player2].gameObject;
                        }
                    }
                    playerWaypoint = nearestObj2;
                }
                if (flying)
                {
                    if (weapon != null)
                    {
                        if (weapon.tag == "sword")
                            GetComponent<Animator>().Play("sword_hurt");
                        else if (weapon.tag == "halberd")
                            GetComponent<Animator>().Play("halberd_hurt");
                    }
                    else
                        GetComponent<Animator>().Play("hurt");
                }
                if (!grounded && !(moveRight && moveLeft) && !flying && !attacking)
                {
                    jumps = 3;
                    onWall = true;
                    if (weapon != null)
                    {
                        if (weapon.tag == "sword")
                            GetComponent<Animator>().Play("sword_idleOnWall");
                        else if (weapon.tag == "halberd")
                            GetComponent<Animator>().Play("halberd_idleOnWall");
                    }
                    else GetComponent<Animator>().Play("idleOnWall");
                }
                else onWall = false;
                if (ableToMove && !flying && !attacking && !touchingWall)
                {
                    if (grounded == false)
                    {
                        if (!onWall)
                        {
                            if (weapon != null)
                            {
                                if (weapon.tag == "sword")
                                    GetComponent<Animator>().Play("sword_jump");
                                else if (weapon.tag == "halberd")
                                    GetComponent<Animator>().Play("halberd_jump");
                            }
                            else
                                GetComponent<Animator>().Play("jump");
                        }
                    }
                    else
                    {
                        if (Vector2.Distance(newPos, oldPos) > Time.deltaTime)
                        {
                            if (weapon != null)
                            {
                                if (weapon.tag == "sword")
                                    GetComponent<Animator>().Play("sword_run");
                                else if (weapon.tag == "halberd")
                                    GetComponent<Animator>().Play("halberd_run");
                            }
                            else
                                GetComponent<Animator>().Play("run");
                        }
                        else
                        {
                            if (weapon != null)
                            {
                                if (weapon.tag == "sword")
                                    GetComponent<Animator>().Play("sword_idle");
                                else if (weapon.tag == "halberd")
                                    GetComponent<Animator>().Play("halberd_idle");
                            }
                            else
                                GetComponent<Animator>().Play("idle");
                        }
                    }
                }
                if(onWall)
                {
                    jumps--;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
                    if (onWall)
                    {
                        if (moveRight == true)
                            GetComponent<Rigidbody2D>().velocity = new Vector2(-jumpHeight, jumpHeight);
                        else GetComponent<Rigidbody2D>().velocity = new Vector2(jumpHeight, jumpHeight);
                    }
                    jump.Play();
                    if (permaDamage == false)
                    {
                        botJump = false;
                        Invoke("BotJump", 0.5f);
                    }
                }
                if (!attacking && !flying)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                    if (waypoint != null && difficulty != "easy")
                    {
                        if (!touchingWall)
                        {
                            Vector2 Pos = Vector2.MoveTowards(transform.position, new Vector2(waypoint.transform.position.x, transform.position.y), Time.deltaTime * speed);
                            transform.position = new Vector2(Pos.x, transform.position.y);
                        }
                            if (transform.position.x < waypoint.transform.position.x && face == 1)
                            {
                                border1.GetComponent<Brawlers_border>().type = 1;
                                border2.GetComponent<Brawlers_border>().type = 0;
                                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);

                                face = 0;
                            }
                            else if (face == 0 && transform.position.x > waypoint.transform.position.x)
                            {
                                border1.GetComponent<Brawlers_border>().type = 0;
                                border2.GetComponent<Brawlers_border>().type = 1;
                                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);

                                face = 1;
                            }
                            if (Vector2.Distance(transform.position, new Vector2(transform.position.x, waypoint.transform.position.y)) > 0.5f && botJump && jumps > 0 && transform.position.y < waypoint.transform.position.y)
                            {
                                jumps--;
                                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
                                if (onWall)
                                {
                                    if (moveRight == true)
                                        GetComponent<Rigidbody2D>().velocity = new Vector2(-jumpHeight, jumpHeight);
                                    else GetComponent<Rigidbody2D>().velocity = new Vector2(jumpHeight, jumpHeight);
                                }
                                jump.Play();
                            if (permaDamage == false)
                            {
                                botJump = false;
                                Invoke("BotJump", 0.5f);
                            }
                            }
                        if (Vector2.Distance(transform.position, waypoint.transform.position) < 0.5f && Physics2D.IsTouching(GetComponent<BoxCollider2D>(), waypoint.GetComponent<CircleCollider2D>()))
                            waypoint = null;

                    }
                    else
                    {
                        if (outOfMapRadius && waypoint == null)
                        {
                            GameObject waypointObj = GameObject.Find("Waypoints");
                            GameObject nearestObj = null;
                            float distance = 1000;
                            foreach (Transform child in waypointObj.transform)
                            {
                                if (Vector2.Distance(transform.position, child.transform.position) < distance)
                                {
                                    distance = Vector2.Distance(transform.position, child.transform.position);
                                    nearestObj = child.gameObject;
                                }
                            }
                            waypoint = nearestObj;
                        }
                        else
                        {
                            if (playerWaypoint != null)
                            {
                                if (Vector2.Distance(playerWaypoint.transform.position, transform.position) < 0.1f)
                                {
                                    GameObject waypointObj = GameObject.Find("Waypoints");
                                    List<GameObject> waypoints = new List<GameObject>();
                                    foreach (Transform child in waypointObj.transform)
                                    {
                                        waypoints.Add(child.gameObject);
                                    }
                                    waypoint = waypoints[Random.Range(1, 1000000) % waypoints.Count];
                                }
                                if (Vector2.Distance(transform.position, new Vector2(playerWaypoint.transform.position.x, transform.position.y)) < 0.5f && Physics2D.Linecast(transform.position,playerWaypoint.transform.position, 11) == true)
                                {
                                    bool ok = false;
                                    Brawlers_player[] players2 = FindObjectsOfType<Brawlers_player>();
                                    GameObject nearestObj2 = null;
                                    float distance2 = 1000;
                                    for(int player2=0;player2<players2.Length;player2++)
                                    {
                                        if (Vector2.Distance(transform.position, players2[player2].transform.position) < distance2 && players2[player2].gameObject != gameObject && players2[player2].gameObject != playerWaypoint && players2[player2].team != team)
                                        {
                                            distance2 = Vector2.Distance(transform.position, players2[player2].transform.position);
                                            nearestObj2 = players2[player2].gameObject;
                                            ok = true;
                                        }
                                    }
                                    playerWaypoint = nearestObj2;
                                    if (ok == false)
                                    {
                                        GameObject waypointObj = GameObject.Find("Waypoints");
                                        foreach (Transform child in waypointObj.transform)
                                        {
                                            if (child.gameObject.name == "Waypoint (2)")
                                            {
                                                waypoint = child.gameObject;
                                            }
                                        }
                                    }
                                }
                                if (playerWaypoint != null)
                                {
                                    if (transform.position.x < playerWaypoint.transform.position.x && face == 1)
                                    {
                                        border1.GetComponent<Brawlers_border>().type = 1;
                                        border2.GetComponent<Brawlers_border>().type = 0;
                                        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);

                                        face = 0;
                                    }
                                    else if (face == 0 && transform.position.x > playerWaypoint.transform.position.x)
                                    {
                                        border1.GetComponent<Brawlers_border>().type = 0;
                                        border2.GetComponent<Brawlers_border>().type = 1;
                                        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);

                                        face = 1;
                                    }
                                    if (!touchingWall)
                                    {
                                        Vector2 Pos = Vector2.MoveTowards(transform.position, new Vector2(playerWaypoint.transform.position.x, transform.position.y), Time.deltaTime * speed);
                                        transform.position = new Vector2(Pos.x, transform.position.y);
                                    }
                                    if (Vector2.Distance(transform.position, new Vector2(transform.position.x, playerWaypoint.transform.position.y)) > 0.5f && botJump && jumps > 0 && transform.position.y < playerWaypoint.transform.position.y)
                                    {
                                        jumps--;
                                        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
                                        if (onWall)
                                        {
                                            if (moveRight == true)
                                                GetComponent<Rigidbody2D>().velocity = new Vector2(-jumpHeight, jumpHeight);
                                            else GetComponent<Rigidbody2D>().velocity = new Vector2(jumpHeight, jumpHeight);
                                        }
                                        jump.Play();
                                        Vector2 Pos = Vector2.MoveTowards(transform.position, playerWaypoint.transform.position, Time.deltaTime * speed);
                                        if (permaDamage == false)
                                        {
                                            botJump = false;
                                            Invoke("BotJump", 0.5f);
                                        }
                                    }
                                }
                                
                            }
                            
                        }
                    }
                }
            }
            teamColor.transform.localEulerAngles = -transform.eulerAngles;
        }
 
    }
    public float timeTillNew = 0f;
    public bool permaDamage;
    public float permaDamageSetting;
    Vector2 oldPos;
    Vector2 newPos;
    public bool botJump = true;
    private void BotJump()
    {
        botJump = true;
    }
    public GameObject playerWaypoint;
    public GameObject waypoint;
    private void StartMoving()
    {
        tutorialAbleToMove = true;
    }
    public bool tutorialAbleToMove = false;
    private void StopDashing()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        ableToMove = true;
    }
    public void TriggerDeath(int direction)
    {
        try
        {
            if (direction % 2 == 0)
            {
                GameObject vfx = Instantiate(deathVfx);
                vfx.transform.position = transform.position;
                if (direction == 2)
                    vfx.transform.eulerAngles = new Vector3(0, 180, 0);
                Destroy(vfx, 1.5f);
            }
            else
            {
                GameObject vfx = Instantiate(tiltedVfx);
                vfx.transform.position = transform.position;
                if (direction == 3)
                    vfx.transform.eulerAngles = new Vector3(0, 180, 0);
                Destroy(vfx, 1.5f);
            }
            if (FindObjectOfType<PlayerData>().teams == false)
            {
                FindObjectOfType<PlayerData>().playerObjectiveScores[playerNumber] = time;
            }


            

            CancelInvoke("Times");
            death.Play();
            death.transform.parent = null;
            Destroy(gameObject);
        }
        catch
        {
            if (direction % 2 == 0)
            {
                GameObject vfx = Instantiate(deathVfx);
                vfx.transform.position = transform.position;
                if (direction == 2)
                    vfx.transform.eulerAngles = new Vector3(0, 180, 0);
                Destroy(vfx, 1.5f);
            }
            else
            {
                GameObject vfx = Instantiate(tiltedVfx);
                vfx.transform.position = transform.position;
                if (direction == 3)
                    vfx.transform.eulerAngles = new Vector3(0, 180, 0);
                Destroy(vfx, 1.5f);
            }
            death.Play();
            death.transform.parent = null;
            Destroy(gameObject);
        }
    }
    public bool ableToMove = true;
    bool hasJumpedGround = false;
    bool hasJumpedAir = false;
    public bool hasAttacked = false;
}
