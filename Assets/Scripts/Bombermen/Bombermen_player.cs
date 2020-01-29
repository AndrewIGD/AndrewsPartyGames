using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SharpDX.DirectInput;

public class Bombermen_player : MonoBehaviour
{
    public int playerNumber;
    public bool ableToMove = true;
    public float speed;
    public GameObject sprite1;
    public GameObject sprite2;
    public GameObject sprite3;
    public GameObject spawnPoint;
    public int team;
    public Joystick joystick;
    public int sus;
    public int jos;
    public int dreapta;
    public int stanga;
    public int button1;
    public int button2;
    bool keyboard = false;
    public SpriteRenderer spriteColor1;
    public SpriteRenderer spriteColor2;
    public TextMeshPro teamColor;
    Color32 sprite1Color;
    Color32 sprite2Color;
    Color32 sprite3Color;
    bool playingAnim = false;
    bool playindAirAnim = false;
    public bool grounded = false;
    public AudioClip jump;
    public float jumpHeight;
    public GameObject bomb;
    bool allowBomb = true;
    public AudioClip death;
    public int score;
    private void AllowBomb()
    {
        allowBomb = true;
    }
    public void AbleToMove()
    {
        Invoke("Move", 0.25f);
    }
    private void Move()
    {
        ableToMove = true;
    }
    public void Death()
    {
        gameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(death, Camera.main.transform.position);
        Invoke("Spawn", 3);
    }
    private void StartMoving()
    {
        tutorialAbleToMove = true;
        spawnPoint.transform.position = transform.position;
    }
    private void Start()
    {
        Invoke("StartMoving", 5.5f);
    }
    public bool tutorialAbleToMove = false;
    public void IsGrounded(bool allow)
    {
        grounded = allow;
    }
    private void Awake()
    {
        joystick = FindObjectOfType<PlayerData>().controllers[playerNumber];
        if (joystick == null)
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
                    button1 = 33;
                    button2 = 34;
                    dreapta = 31;
                    stanga = 29;
                    jos = 30;
                    sus = 16;
                }
                else
                {
                    button1 = 53;
                    button2 = 91;
                    dreapta = 107;
                    stanga = 106;
                    jos = 109;
                    sus = 104;
                }
            }
            else
            {
                if (joystick.Information.ProductName == "USB Network Joystick")
                {
                    button1 = 2;
                    button2 = 3;
                }
                else
                {
                    button1 = 0;
                    button2 = 2;
                }
            }
            joystick.Acquire();
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
        if (ok == false)
            Destroy(gameObject);
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
            FindObjectOfType<SpawnPlayers>().players.Add(gameObject);
        }
    }

    public void Noteams()
    {
        Bombermen_player[] players = FindObjectsOfType<Bombermen_player>();
        foreach (Bombermen_player player in players)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        }
        sprite1Color = sprite1.GetComponent<SpriteRenderer>().color;
        sprite2Color = sprite2.GetComponent<SpriteRenderer>().color;
        sprite3Color = sprite3.GetComponent<SpriteRenderer>().color;
        Invoke("StartMoving", 5.5f);
    }
    public void Withteams()
    {
        Bombermen_player[] players = FindObjectsOfType<Bombermen_player>();
        foreach (Bombermen_player player in players)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        }

        sprite1Color = sprite1.GetComponent<SpriteRenderer>().color;
        sprite2Color = sprite2.GetComponent<SpriteRenderer>().color;
        sprite3Color = sprite3.GetComponent<SpriteRenderer>().color;
        Invoke("StartMoving", 5.5f);
    }
    private void Spawn()
    {
        foreach(Bombermen_bomb bomb in FindObjectsOfType<Bombermen_bomb>())
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), bomb.GetComponent<CircleCollider2D>());
        }
        transform.position = spawnPoint.transform.position;
        gameObject.SetActive(true);
        Bombermen_player[] players = FindObjectsOfType<Bombermen_player>();
        foreach (Bombermen_player player in players)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        }
    }
    void Update()
    {
        if (Time.timeScale != 0)
        {
            teamColor.transform.localEulerAngles = -transform.eulerAngles;
            if (ableToMove && tutorialAbleToMove)
            {
                if (grounded == false)
                {
                    if (GetComponent<Rigidbody2D>().velocity.y > 1)
                    {
                        GetComponent<Animator>().Play("jumpBomberman");
                        playindAirAnim = true;
                        playingAnim = false;
                    }
                    else if (GetComponent<Rigidbody2D>().velocity.y < -1)
                    {
                        GetComponent<Animator>().Play("fallBomberman");
                        playindAirAnim = true;
                        playingAnim = false;
                    }
                    else
                    {
                        playindAirAnim = false;
                    }
                }
                else playindAirAnim = false;
                if (keyboard)
                {
                    if (CustomControls.GetButton(joystick, stanga))
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
                        transform.eulerAngles = new Vector3(0, 180, 0);
                        if (playingAnim == false && playindAirAnim == false)
                        {
                            GetComponent<Animator>().Play("runBomberman");
                            playingAnim = true;

                        }
                    }
                    else if (CustomControls.GetButton(joystick, dreapta))
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        if (playingAnim == false && playindAirAnim == false)
                        {
                            GetComponent<Animator>().Play("runBomberman");
                            playingAnim = true;

                        }
                    }
                    else
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                        if (playindAirAnim == false)
                        {
                            playingAnim = false;
                            GetComponent<Animator>().Play("idle");
                        }
                    }
                }
                else
                {
                    if (CustomControls.GetAxis(joystick).Xaxis < 20000)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * (CustomControls.GetAxis(joystick).Xaxis - 32767) / 32767, GetComponent<Rigidbody2D>().velocity.y);
                        transform.eulerAngles = new Vector3(0, 180, 0);
                        if (playingAnim == false && playindAirAnim == false)
                        {
                            GetComponent<Animator>().Play("runBomberman");
                            playingAnim = true;

                        }
                    }
                    else if (CustomControls.GetAxis(joystick).Xaxis > 40000)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * (CustomControls.GetAxis(joystick).Xaxis - 32767) / 32767, GetComponent<Rigidbody2D>().velocity.y);
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        if (playingAnim == false && playindAirAnim == false)
                        {
                            GetComponent<Animator>().Play("runBomberman");
                            playingAnim = true;

                        }
                    }
                    else
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                        if (playindAirAnim == false)
                        {
                            playingAnim = false;
                            GetComponent<Animator>().Play("idle");
                        }
                    }
                }
                if (CustomControls.GetButton(joystick, button1))
                {
                    if (grounded == true)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
                        if (allowSound == true)
                        {
                            AudioSource.PlayClipAtPoint(jump, Camera.main.transform.position);
                            allowSound = false;
                            Invoke("AllowSound", 0.25f);
                        }
                    }
                }
                if (CustomControls.GetButton(joystick, button2))
                {
                    if (allowBomb)
                    {
                        GameObject bomba = Instantiate(bomb);
                        bomba.transform.position = transform.position;
                        bomba.GetComponent<Bombermen_bomb>().team = team;
                        bomba.GetComponent<Bombermen_bomb>().parent = gameObject;
                        foreach(Bombermen_player player in FindObjectsOfType<Bombermen_player>())
                        {
                                Physics2D.IgnoreCollision(bomba.GetComponent<CircleCollider2D>(), player.GetComponent<BoxCollider2D>());
                        }
                        allowBomb = false;
                        Invoke("AllowBomb", 2.5f);
                    }
                }
            }
        }
        
    }
    private void AllowSound()
    {
        allowSound = true;
    }
    bool allowSound = true;
}
