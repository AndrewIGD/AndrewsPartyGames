using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SharpDX.DirectInput;

public class ParkourMasters_Player : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public bool moveLeft = true;
    public bool moveRight = true;
    public int playerNumber;
    public bool grounded = false;
    public int action2Type = 0;
    public GameObject deathVfx;
    public GameObject tiltedVfx;
    public GameObject mine;
    [Space(2)]
    [Header("Audio")]
    public AudioSource jump;
    public AudioSource pickup;
    public AudioSource death;
    bool playingAnim = false;
    bool playingMidAirAnim = false;
    public SpriteRenderer sprite1;
    public SpriteRenderer sprite2;
    public float time = 0;
    public int team;
    public Joystick joystick;
    public int sus;
    public int jos;
    public int dreapta;
    public int stanga;
    public int button1;
    public int button2;
    bool keyboard = false;
    public bool bot;
    public List<GameObject> waypoints;
    public int waypointIndex = 0;
    public GameObject botObj;
    public string difficulty;
    public bool freezing = false;
    public bool Vel0 = true;
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
    }
    public TextMeshPro teamColor;
    float mineSize = 1f;
    float dash = 15f;
    float speedAmplifier = 1f;
    float jumpHeightAmplifier = 1f;
    private void Awake()
    {
        speedAmplifier = PlayerPrefs.GetFloat("ParkourMasterSpeed", speedAmplifier);
        jumpHeightAmplifier = PlayerPrefs.GetFloat("ParkourMasterJumpHeight", jumpHeightAmplifier);
        dash = PlayerPrefs.GetFloat("ParkourMasterDash", dash);
        mineSize = PlayerPrefs.GetFloat("ParkourMineSize", mineSize);
        ParkourMasters_Player[] players = FindObjectsOfType<ParkourMasters_Player>();
        foreach (ParkourMasters_Player player in players)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        }
        FindObjectOfType<ParkourMasters_PlayerCount>().playerCount++;
        if (FindObjectOfType<PlayerData>().bots[playerNumber] == true)
        {
            Invoke("StartMoving", Random.Range(5.5f,7f));
            bot = true;
            difficulty = FindObjectOfType<PlayerData>().botDifficulties[playerNumber];
            GameObject waypointObj = GameObject.Find(difficulty);
            foreach(Transform child in waypointObj.transform)
            {
                waypoints.Add(child.gameObject);
            }
        }
        else
        {
            Invoke("StartMoving", 5.5f);
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
            if (ok == false)
                Destroy(gameObject);
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
        
    }
    // Start is called before the first frame update

    public void Noteams()
    {
        ParkourMasters_Player[] players = FindObjectsOfType<ParkourMasters_Player>();
        foreach (ParkourMasters_Player player in players)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        }FindObjectOfType<ParkourMasters_PlayerCount>().playerCount++;
    }
    public void Withteams()
    {
        ParkourMasters_Player[] players = FindObjectsOfType<ParkourMasters_Player>();
        foreach (ParkourMasters_Player player in players)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        }
        
        FindObjectOfType<ParkourMasters_PlayerCount>().playerCount++;
        
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
        if (Vel0)
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (Time.timeScale != 0)
        {
            time += Time.deltaTime;
            teamColor.transform.localEulerAngles = -transform.eulerAngles;
            if (!bot)
            {
                if (ableToMove && tutorialAbleToMove)
                {
                    oldScale = GetComponent<Rigidbody2D>().gravityScale;
                    if (grounded == false)
                    {
                        if (GetComponent<Rigidbody2D>().gravityScale > 0)
                        {
                            if (GetComponent<Rigidbody2D>().velocity.y > 1)
                            {
                                GetComponent<Animator>().Play("jump");
                                playingMidAirAnim = true;
                                playingAnim = false;
                            }
                            else if (GetComponent<Rigidbody2D>().velocity.y < -1)
                            {
                                GetComponent<Animator>().Play("fall");
                                playingMidAirAnim = true;
                                playingAnim = false;
                            }
                            else
                            {
                                playingMidAirAnim = false;
                            }
                        }
                        else
                        {
                            if (GetComponent<Rigidbody2D>().velocity.y < -1)
                            {
                                GetComponent<Animator>().Play("jump");
                                playingMidAirAnim = true;
                                playingAnim = false;
                            }
                            else if (GetComponent<Rigidbody2D>().velocity.y > 1)
                            {
                                GetComponent<Animator>().Play("fall");
                                playingMidAirAnim = true;
                                playingAnim = false;
                            }
                            else
                            {
                                playingMidAirAnim = false;
                            }
                        }
                    }
                    else playingMidAirAnim = false;

                    if (keyboard)
                    {
                        if (CustomControls.GetButton(joystick, stanga) && moveLeft)
                        { GetComponent<Rigidbody2D>().velocity = new Vector2(-speed*speedAmplifier, GetComponent<Rigidbody2D>().velocity.y); if (playingAnim == false && playingMidAirAnim == false) { playingAnim = true; GetComponent<Animator>().Play("run"); } }
                        else if (CustomControls.GetButton(joystick, dreapta) && moveRight)
                        { GetComponent<Rigidbody2D>().velocity = new Vector2(speed*speedAmplifier, GetComponent<Rigidbody2D>().velocity.y); if (playingAnim == false && playingMidAirAnim == false) { playingAnim = true; GetComponent<Animator>().Play("run"); } }
                        else {
                            if(!freezing || !grounded)
                                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                            if (playingMidAirAnim == false) { playingAnim = false; GetComponent<Animator>().Play("idle"); } }
                    }
                    else
                    {
                        if (CustomControls.GetAxis(joystick).Xaxis < 20000 && moveLeft)
                        { GetComponent<Rigidbody2D>().velocity = new Vector2(speed*speedAmplifier * (CustomControls.GetAxis(joystick).Xaxis - 32767) / 32767, GetComponent<Rigidbody2D>().velocity.y); if (playingAnim == false && playingMidAirAnim == false) { playingAnim = true; GetComponent<Animator>().Play("run"); } }
                        else if (CustomControls.GetAxis(joystick).Xaxis > 40000 && moveRight)
                        { GetComponent<Rigidbody2D>().velocity = new Vector2(speed*speedAmplifier * (CustomControls.GetAxis(joystick).Xaxis - 32767) / 32767, GetComponent<Rigidbody2D>().velocity.y); if (playingAnim == false && playingMidAirAnim == false) { playingAnim = true; GetComponent<Animator>().Play("run"); } }
                        else {
                            if (!freezing || !grounded)
                                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y); if (playingMidAirAnim == false) { playingAnim = false; GetComponent<Animator>().Play("idle"); } }
                    }
                    if (CustomControls.GetButton(joystick, button1) && grounded)
                    {
                        if(GetComponent<Rigidbody2D>().gravityScale < 0)
                        {
                            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -jumpHeight*jumpHeightAmplifier);
                        }
                        else GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight*jumpHeightAmplifier);
                        jump.Play();
                    }
                    if (CustomControls.GetButton(joystick, button2) && action2Type != 0)
                    {
                        if (action2Type == 1)
                        {
                            if (GetComponent<Rigidbody2D>().gravityScale < 0)
                            {
                                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -jumpHeight*jumpHeightAmplifier);
                            }
                            else GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight*jumpHeightAmplifier);
                            action2Type = 0;
                            jump.Play();
                        }
                        else if (action2Type == 2)
                        {
                            
                            GameObject cloneMine = Instantiate(mine);
                            cloneMine.transform.position = transform.position;
                            cloneMine.GetComponent<ParkourMasters_Mine>().player = gameObject;
                            cloneMine.GetComponent<Rigidbody2D>().gravityScale = oldScale;
                            cloneMine.transform.rotation = transform.rotation;
                            cloneMine.transform.localScale = new Vector3(mineSize, mineSize, 1);

                            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), cloneMine.GetComponent<PolygonCollider2D>());
                            action2Type = 0;
                        }
                        else if (action2Type == 3)
                        {
                            GetComponent<Rigidbody2D>().velocity = new Vector2(dash, 0f);
                            oldScale = GetComponent<Rigidbody2D>().gravityScale;
                            GetComponent<Rigidbody2D>().gravityScale = 0;
                            ableToMove = false;
                            action2Type = 0;
                            Invoke("StopDashing", 0.5f);
                        }
                    }
                }
            }
            else if(tutorialAbleToMove)
            {
                if(waypointIndex<waypoints.Count)
                    transform.position = Vector2.MoveTowards(transform.position,new Vector2(waypoints[waypointIndex].transform.position.x, transform.position.y),speed * speedAmplifier * Time.deltaTime);
                if (GetComponent<Rigidbody2D>().velocity.y > 1)
                {
                    GetComponent<Animator>().Play("jump");
                }
                else if (GetComponent<Rigidbody2D>().velocity.y < -1)
                {
                    GetComponent<Animator>().Play("fall");
                }
                else GetComponent<Animator>().Play("run");
                if (waypointIndex < waypoints.Count)
                {
                    if (waypoints[waypointIndex].transform.position.y - transform.position.y > 0.25f && GetComponent<Rigidbody2D>().gravityScale == 1)
                    {
                        if (grounded)
                        {
                            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight*jumpHeightAmplifier);
                            jump.Play();
                        }
                    }
                    else if(waypoints[waypointIndex].transform.position.y - transform.position.y < -0.25f && GetComponent<Rigidbody2D>().gravityScale == -1)
                    {
                        if (grounded)
                        {
                            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -jumpHeight*jumpHeightAmplifier);
                            jump.Play();
                        }
                    }
                    if (Vector2.Distance(transform.position, waypoints[waypointIndex].transform.position) < 0.5f)
                        waypointIndex++;
                }
            }
        }
    }
    public GameObject winVfx;
    public void TriggerWin(float angle, int time, bool score)
    {
        GameObject win = Instantiate(winVfx);
        win.GetComponent<ParkourMasters_winVfx>().active = true;
        win.transform.position = transform.position;
        win.transform.localEulerAngles = new Vector3(0, 0, angle);

        ParkourMasters_Player[] players = FindObjectsOfType<ParkourMasters_Player>();
        if (FindObjectOfType<PlayerData>().teams == false)
        {
            FindObjectOfType<PlayerData>().playerObjectiveScores[playerNumber] = time;
        }
        else
        {
            if (score)
            {
                if (team == 0)
                    FindObjectOfType<PlayerData>().AddBlue();
                else FindObjectOfType<PlayerData>().AddRed();
            }
            FindObjectOfType<PlayerData>().InvokeScores();
        }
        Destroy(gameObject);

    }
    private void StartMoving()
    {
        tutorialAbleToMove = true;
        Vel0 = false;
    }
    public bool tutorialAbleToMove = false;
    public float oldScale;
    public void StopDashing()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        GetComponent<Rigidbody2D>().gravityScale = oldScale;
        ableToMove = true;
    }
    public GameObject upperVfx;
    public void TriggerDeath(GameObject camera, bool tilted, bool upper, bool toCamera)
    {
        if (toCamera)
        {
            if (tilted == true)
            {
                GameObject vfx = Instantiate(tiltedVfx);
                vfx.transform.parent = camera.transform;
                if (transform.position.y < camera.GetComponent<ParkourMasters_CameraMovement>().downVfxPos.transform.position.y)
                    transform.position = new Vector2(transform.position.x, camera.GetComponent<ParkourMasters_CameraMovement>().tiltedVfxPos.transform.position.y);
                vfx.transform.position = new Vector2(camera.GetComponent<ParkourMasters_CameraMovement>().tiltedVfxPos.transform.position.x, transform.position.y);
            }
            else if (upper == true)
            {
                GameObject vfx = Instantiate(upperVfx);
                if (transform.position.x < camera.GetComponent<ParkourMasters_CameraMovement>().tiltedVfxPos.transform.position.x)
                    transform.position = new Vector2(camera.GetComponent<ParkourMasters_CameraMovement>().tiltedVfxPos.transform.position.x, transform.position.y);
                vfx.transform.position = new Vector2(transform.position.x, camera.GetComponent<ParkourMasters_CameraMovement>().upVfxPos.transform.position.y);
                vfx.transform.parent = camera.transform;
            }
            else
            {
                GameObject vfx = Instantiate(deathVfx);
                if (transform.position.x < camera.GetComponent<ParkourMasters_CameraMovement>().tiltedVfxPos.transform.position.x)
                    transform.position = new Vector2(camera.GetComponent<ParkourMasters_CameraMovement>().tiltedVfxPos.transform.position.x, transform.position.y);
                vfx.transform.position = new Vector2(transform.position.x, camera.GetComponent<ParkourMasters_CameraMovement>().downVfxPos.transform.position.y);
                vfx.transform.parent = camera.transform;
            }
        }
        else
        {
            GameObject vfx = Instantiate(deathVfx);
            vfx.transform.position = transform.position;
        }
        ParkourMasters_Player[] players = FindObjectsOfType<ParkourMasters_Player>();
        if (FindObjectOfType<PlayerData>().teams == false)
        {
            FindObjectOfType<PlayerData>().playerObjectiveScores[playerNumber] = time;
        }
        death.Play();
        death.transform.parent = null;
        Destroy(gameObject);
    }
    bool ableToMove = true;
}
