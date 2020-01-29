using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpDX.DirectInput;
using Pathfinding;

public class ZombieSurvival_zombie : MonoBehaviour
{
    public float health;
    public float speed;
    public bool bot;
    public GameObject target;
    public GameObject parent;
    public GameObject team;
    public AudioClip[] sounds;
    public GameObject painVfx;
    public bool painSound = true;
    public int playerNumber;
    public GameObject bomb;
    public bool bombThrown = false;
    int frames = 0;
    public Joystick joystick;
    public int sus;
    public int jos;
    public int dreapta;
    public int stanga;
    public int button1;
    public int button2;
    public  bool keyboard = false;
    bool trapped = false;
    public bool infection = false;
    public GameObject spritesObj;
    float trapDuration = 2f;
    // Start is called before the first frame update
    public void Trap()
    {
        trapped = true;
        Invoke("StopTrap", trapDuration);
    }
    void StopTrap()
    {
        trapped = false;
        speed = originalSpeed;
        GetComponent<AIPath>().maxSpeed = speed;
    }
    private void Awake()
    {
        bombForce = PlayerPrefs.GetFloat("ZombieInfectionBombForce", bombForce);
        trapDuration = PlayerPrefs.GetFloat("ZombieInfectionTrapDuration", trapDuration);
        foreach (ZombieSurvival_player player in FindObjectsOfType<ZombieSurvival_player>())
        {
            if (player.bot)
                Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
            else Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }
    }
    void Start()
    {
        foreach (ZombieSurvival_player player in FindObjectsOfType<ZombieSurvival_player>())
        {
            if (player.bot)
                Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
            else Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }
        if (!bot)
            originalSpeed = PlayerPrefs.GetFloat("ZombieInfectionZombieSpeed", originalSpeed); 
        if (bot)
        {
            GetComponent<AIPath>().maxSpeed = speed;
            originalSpeed = speed;
            GetComponent<AIDestinationSetter>().target = target.transform;
            if (infection)
            {

                Invoke("Bomb", Random.Range(2f, 5f));
            }
            else Destroy(team);
        }
        else
        {
            ZombieSurvival_player[] players = FindObjectsOfType<ZombieSurvival_player>();
            target = players[Random.Range(0, 10000000) % players.Length].gameObject;
            while (target.GetComponent<ZombieSurvival_player>().playerNumber == playerNumber && players.Length >= 2)
                target = players[Random.Range(0, 10000000) % players.Length].gameObject;

            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().offset = new Vector2(0.2293444f, 0.01647902f);
            GetComponent<BoxCollider2D>().size = new Vector2(2.13199f, 2.150464f);
            GetComponent<BoxCollider2D>().isTrigger = false;
            GetComponent<AIPath>().enabled = false;
            GetComponent<AIDestinationSetter>().enabled = false;
            joystick.Acquire();

            
        }
    }
    public void Damage(float damage)
    {
        health -= damage;
        GameObject pain = Instantiate(painVfx);
        pain.transform.position = transform.position;
        Destroy(pain, 1f);
        if (health <= 0)
        {
            if (!bot && FindObjectsOfType<ZombieSurvival_zombie>().Length == 1)
            {

                    foreach (ZombieSurvival_player player in FindObjectsOfType<ZombieSurvival_player>())
                    {
                        FindObjectOfType<PlayerData>().playerObjectiveScores[player.playerNumber] = player.damageDealt;
                    }
                
                FindObjectOfType<PlayerData>().InvokeScores();
            }
            if (parent != null)
                parent.GetComponent<ZombieSurvival_zombieSpawner>().zombies.Remove(gameObject);
            Destroy(gameObject);
        }
        else
        {
            if (painSound == true)
            {
                GetComponent<AudioSource>().clip = sounds[Random.Range(0, 100000) % sounds.Length];
                GetComponent<AudioSource>().Play();
                painSound = false;
                Invoke("PainSound", 1f);
            }
        }
            CancelInvoke("StopSlow");
            speed = originalSpeed / 2;
            Invoke("StopSlow", 0.2f);
        
    }
    void StopSlow()
    {
        speed = originalSpeed;
    }
    float originalSpeed=6f;
    public void PainSound()
    {
        painSound = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bot)
        {
            if (collision.gameObject.GetComponent<ZombieSurvival_player>() != null)
            {
                if(!infection)
                    collision.gameObject.GetComponent<ZombieSurvival_player>().Death();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!bot)
        {
            if (collision.gameObject.GetComponent<ZombieSurvival_player>() != null)
            {

                collision.gameObject.GetComponent<ZombieSurvival_player>().Infect();
            }
            Debug.Log(collision.gameObject.name);
        }
    }
    private void FixedUpdate()
    {
        oldPos = newPos;
        newPos = transform.position;
        if (bot)
        {
            Vector3 diff = newPos - oldPos;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
        }
    }
    private void Update()
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
        if (infection == true)
            team.transform.localEulerAngles = -transform.eulerAngles;

        if (Time.timeScale != 0)
            UpdateMe();
    }
    Vector2 oldPos;
    Vector2 newPos;
    float delta = 0f;
    // Update is called once per frame
    public void UpdateMe()
    {
        
        if (trapped && bot)
        {
            GetComponent<AIPath>().enabled = false;
        }
        else if(trapped == false && bot == true) GetComponent<AIPath>().enabled = true;
        if (bot)
        {

            if (target != null)
            {
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                ZombieSurvival_player[] players = FindObjectsOfType<ZombieSurvival_player>();
                if (players.Length != 0)
                {
                    target = players[Random.Range(0, 10000000) % players.Length].gameObject;
                    GetComponent<AIDestinationSetter>().target = target.transform;
                }
            }
        }
        else
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
            if (trapped == false)
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
                    else GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
                }
                else if ((CustomControls.GetAxis(joystick).Yaxis > 40000 || CustomControls.GetAxis(joystick).Yaxis < 20000 || CustomControls.GetAxis(joystick).Xaxis > 40000 || CustomControls.GetAxis(joystick).Xaxis < 20000))
                    GetComponent<Rigidbody2D>().velocity = new Vector2(speed * (CustomControls.GetAxis(joystick).Xaxis - 32767) / 32767, -speed * (CustomControls.GetAxis(joystick).Yaxis - 32767) / 32767);
                else GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }

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
            if (CustomControls.GetButton(joystick, button1) && bombThrown == false)
                {
                bombThrown = true;
                GameObject bombClone = Instantiate(bomb);
                bombClone.GetComponent<ZombieInfection_bomb>().active = true;
                bombClone.transform.position = transform.position;
                bombClone.transform.rotation = transform.rotation;
                bombClone.GetComponent<Rigidbody2D>().velocity = -transform.up * bombForce;
            }
                if (CustomControls.GetButton(joystick, button2) && attacking == false)
                {
                attacking = true;
                GetComponent<CircleCollider2D>().enabled = true;
                }
                else
                {
                    if(CustomControls.GetButton(joystick, button2) == true && attacking == true)
                {
                    GetComponent<CircleCollider2D>().enabled = false;
                }
                    if(CustomControls.GetButton(joystick, button2) == false && attacking == true)
                    {
                    attacking = false;
                    }
                }
            }
        }
    private void Bomb()
    {
        if (difficulty == "expert" && bombThrown == false)
        {
            GameObject bombClone = Instantiate(bomb);
            bombClone.GetComponent<ZombieInfection_bomb>().active = true;
            bombClone.transform.position = transform.position;
            bombClone.transform.rotation = transform.rotation;
            bombClone.GetComponent<Rigidbody2D>().velocity = -transform.up * bombForce;
        }
    }
    public string difficulty;
    public bool attacking = false;
    float bombForce = 10f;
    }