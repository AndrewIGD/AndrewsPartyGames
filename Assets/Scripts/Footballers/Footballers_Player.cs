using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SharpDX.DirectInput;
using Pathfinding;

public class Footballers_Player : MonoBehaviour
{
    public int playerNumber;
    public float speed;
    public float weakShot;
    public float powerfulShot;
    public GameObject ball;
    public float ballDistanceFromPlayer;
    public GameObject spawnpoint;
    public SpriteRenderer sprite1;
    public SpriteRenderer sprite2;
    public int passes = 0;
    public AudioClip pass;
    public AudioClip shot;
    public int team;
    public TextMeshPro teamColor;
    public Joystick joystick;
    public int sus;
    public int jos;
    public int dreapta;
    public int stanga;
    public int button1;
    public int button2;
    bool keyboard = false;
    public bool bot = false;
    public string botPlayStyle;
    public List<GameObject> waypoints;
    public GameObject waypoint;
    public GameObject ballFind;
    public GameObject botObj;
    public string difficulty;

    // Start is called before the first frame update
    public void ChangeTeam(int team)
    {
        if (team == 0)
        {
            teamColor.color = new Color32(0, 0, 255, 255);
        }
        else teamColor.color = new Color32(255, 0, 0, 255);
        this.team = team;
    }
    private void Enable()
    {
        if (activateChargeBar)
            chargeBar.SetActive(true);
        if (bot)
        {
            FencingMates_Player[] players = FindObjectsOfType<FencingMates_Player>();
            foreach (FencingMates_Player player in players)
            {
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
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
                teamColor.text = FindObjectOfType<PlayerData>().playerNames[playerNumber];
                if (FindObjectOfType<PlayerData>().teams == true)
                {
                    team = FindObjectOfType<PlayerData>().playerTeams[playerNumber];
                    if (team == 0)
                    {
                        teamColor.color = new Color32(0, 0, 255, 255);
                    }
                    else teamColor.color = new Color32(255, 0, 0, 255);

                }
                else if (team == 0)
                {
                    teamColor.color = new Color32(0, 0, 255, 255);
                }
                else teamColor.color = new Color32(255, 0, 0, 255);
            }
            Invoke("LateStart", 2f);
            Invoke("StartMoving", 5.5f);
        }
    }

    public bool activateChargeBar = false;
    public GameObject chargeBar;
    public float chargeTime = 0f;
    float dribbleMultiplier = 5f;
    private void Awake()
    {
        speed = PlayerPrefs.GetFloat("FootballersPlayerSpeed", speed);
        weakShot = PlayerPrefs.GetFloat("FootballersPass", weakShot);
        powerfulShot = PlayerPrefs.GetFloat("FootballersPowerfulShot", powerfulShot);
        dribbleMultiplier = PlayerPrefs.GetFloat("FootballersDribbleMultiplier", dribbleMultiplier);
        if (activateChargeBar)
        {
            chargeBar.SetActive(true);
        }
        if (FindObjectOfType<PlayerData>().teams == true)
            team = FindObjectOfType<PlayerData>().playerTeams[playerNumber];
        Footballers_Player[] players = FindObjectsOfType<Footballers_Player>();
        foreach (Footballers_Player player in players)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        }
        if (FindObjectOfType<PlayerData>().bots[playerNumber] == true && bot == false)
        {
            GameObject botz = Instantiate(botObj);
            botz.transform.position = transform.position;
            botz.GetComponent<Footballers_Player>().difficulty = FindObjectOfType<PlayerData>().botDifficulties[playerNumber];
            botz.GetComponent<Footballers_Player>().playerNumber = playerNumber;
            botz.GetComponent<Footballers_Player>().team = team;
            botz.GetComponent<Footballers_Player>().activateChargeBar = activateChargeBar;
            FindObjectOfType<SpawnPlayers>().players.Add(botz);
            botz.GetComponent<Footballers_Player>().Enable();
            bot = true;
            Destroy(gameObject);
        }
        if (team == 0)
            teamColor.GetComponent<TextMeshPro>().color = new Color32(0, 255, 255, 255);
        else teamColor.GetComponent<TextMeshPro>().color = new Color32(255, 0, 0, 255);
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
                    sprite1.color = FindObjectOfType<PlayerData>().color1[playerNumber];
                    sprite2.color = FindObjectOfType<PlayerData>().color2[playerNumber];
                }
            }
            if (ok == false)
                Destroy(gameObject);
            else
            {
                FindObjectOfType<SpawnPlayers>().players.Add(gameObject);
                Debug.Log(gameObject.name);
                teamColor.text = FindObjectOfType<PlayerData>().playerNames[playerNumber];
                if (FindObjectOfType<PlayerData>().teams == true)
                {
                    team = FindObjectOfType<PlayerData>().playerTeams[playerNumber];
                    if (team == 0)
                    {
                        teamColor.color = new Color32(0, 0, 255, 255);
                    }
                    else teamColor.color = new Color32(255, 0, 0, 255);

                }
                else if (team == 0)
                {
                    teamColor.color = new Color32(0, 0, 255, 255);
                }
                else teamColor.color = new Color32(255, 0, 0, 255);
            }
            Invoke("StartMoving", 5.5f);
        }

    }

    public void LateStart()
    {
        if (botPlayStyle == "goalkeeper" && (difficulty == "easy" || difficulty == "normal"))
            botPlayStyle = "bot";
        List<GameObject> porti = new List<GameObject>();
        GameObject obj2 = null;
        if(team == 0)
            obj2 = GameObject.Find("goalkeeper0");
        else obj2 = GameObject.Find("goalkeeper1");
        foreach (Transform child in obj2.transform)
        {
            porti.Add(child.gameObject);
            Debug.Log("yess");
        }
        poartaTransform = porti[Random.Range(1, 1000000) % porti.Count];
        GameObject obj = GameObject.Find(botPlayStyle + team);
        foreach(Transform child in obj.transform)
        {
            waypoints.Add(child.gameObject);
        }
        waypoint = waypoints[Random.Range(1, 1000000) % waypoints.Count];
    }

    public void Noteams()
    {
        Footballers_Player[] players = FindObjectsOfType<Footballers_Player>();
        foreach (Footballers_Player player in players)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        }
        Invoke("StartMoving", 5.5f);
    }
    public void Withteams()
    {
        Footballers_Player[] players = FindObjectsOfType<Footballers_Player>();
        foreach (Footballers_Player player in players)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
        }
        Invoke("StartMoving", 5.5f);
    }

    private void StartMoving()
    {
        GameObject spawn = new GameObject();
        spawnpoint = spawn;
        spawnpoint.transform.position = transform.position;
        tutorialAbleToMove = true;
    }
    public bool tutorialAbleToMove = false;
    Vector2 oldPos;
    Vector2 newPos;
    GameObject poartaTransform;
    public GameObject charge;
    // Update is called once per frame
    float delta = 0f;
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
    }
    void FixedUpdate()
    {
        
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
            if (activateChargeBar && ball != null)
            {
                chargeBar.SetActive(true);
                chargeTime += Time.deltaTime / 5;
                charge.transform.localScale = new Vector3(chargeTime * 2, charge.transform.localScale.y, 1);
                if (chargeTime >= 1)
                    chargeTime = 1;
            }
            else
            {
                chargeBar.SetActive(false);
                chargeTime = 0;
            }
            teamColor.transform.localEulerAngles = -transform.eulerAngles;
            if (!bot)
            {
                if (tutorialAbleToMove)
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
                    else GetComponent<Rigidbody2D>().velocity = new Vector2(speed * (CustomControls.GetAxis(joystick).Xaxis - 32767) / 32767, -speed * (CustomControls.GetAxis(joystick).Yaxis - 32767) / 32767);
                    if (ball != null)
                    {
                        ball.transform.localPosition = new Vector2(0, -ballDistanceFromPlayer);
                        ball.transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                    if (keyboard)
                    {
                        if ((CustomControls.GetButton(joystick, stanga) || CustomControls.GetButton(joystick, dreapta) || CustomControls.GetButton(joystick, jos) || CustomControls.GetButton(joystick, sus)) && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack"))
                        {
                            GetComponent<Animator>().Play("runAnim");
                            float rotZ = transform.rotation.z;
                            transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody2D>().velocity * 100, new Vector3(0, 0, 1));
                            transform.rotation = new Quaternion(0, 0, transform.rotation.z, transform.rotation.w);
                            if (GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0))
                                transform.rotation = new Quaternion(0, 0, rotZ, transform.rotation.w);
                        }
                        else
                        {
                            GetComponent<Animator>().Play("idleAnim");
                        }
                    }
                    else
                    {
                        if ((CustomControls.GetAxis(joystick).Yaxis > 34000 || CustomControls.GetAxis(joystick).Yaxis < 32000 || CustomControls.GetAxis(joystick).Xaxis > 34000 || CustomControls.GetAxis(joystick).Xaxis < 32000) && !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack"))
                        {
                            GetComponent<Animator>().Play("runAnim");
                            float rotZ = transform.rotation.z;
                            transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody2D>().velocity * 100, new Vector3(0, 0, 1));
                            transform.rotation = new Quaternion(0, 0, transform.rotation.z, transform.rotation.w);
                            if (GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0))
                                transform.rotation = new Quaternion(0, 0, rotZ, transform.rotation.w);
                        }
                        else
                        {
                            GetComponent<Animator>().Play("idleAnim");
                        }
                    }
                    if (CustomControls.GetButton(joystick, button1))
                    {
                        if (ball != null)
                        {
                            ball.transform.parent = null;
                            ball.GetComponent<Footballers_Ball>().player = null;
                            ball.GetComponent<Rigidbody2D>().velocity = -transform.up * weakShot;
                            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;

                            passes += 2;
                            GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                            GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                            AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                        }
                    }
                    if (CustomControls.GetButton(joystick, button2))
                    {
                        if (ball != null)
                        {
                            ball.transform.parent = null;
                            ball.GetComponent<Footballers_Ball>().player = null;
                            if(activateChargeBar)
                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                            else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                            passes++;
                            GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                            GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                            AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                            foreach (Footballers_Poarta poarta in FindObjectsOfType<Footballers_Poarta>())
                            {
                                Vector2 dirFromAtoB = (poarta.transform.position - transform.position).normalized;
                                float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                if (Vector2.Distance(transform.position, poarta.transform.position) < 5 && dotProd > 0.25f)
                                    FindObjectOfType<Footballers_Crowd>().Play();
                            }
                        }
                    }
                }
                else
                {
                    GetComponent<Animator>().Play("idleAnim");
                }
            }
            else if (tutorialAbleToMove)
            {
                gameObject.layer = 1;
                oldPos = newPos;
                newPos = transform.position;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                if (botPlayStyle == "single")
                {

                    gameObject.layer = 8;
                    if (ball != null)
                    {
                        ball.transform.localPosition = new Vector2(0, -ballDistanceFromPlayer);
                        ball.transform.eulerAngles = new Vector3(0, 0, 0);
                        if (difficulty == "easy")
                        {
                            foreach (Footballers_Poarta poarta in FindObjectsOfType<Footballers_Poarta>())
                            {
                                if (poarta.team != team)
                                {
                                    transform.position = Vector2.MoveTowards(transform.position, poarta.transform.position, Time.deltaTime * speed);
                                    Vector3 diff = poarta.transform.position - transform.position;
                                    diff.Normalize();

                                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                }
                            }

                        }
                        else
                        {
                            FindObjectOfType<AstarPath>().Scan();
                            foreach (Footballers_Poarta poarta in FindObjectsOfType<Footballers_Poarta>())
                            {
                                if (poarta.team != team)
                                {
                                    GetComponent<AIDestinationSetter>().target = poarta.transform;
                                }
                            }
                            GetComponent<AIPath>().enabled = true;
                            if (Vector2.Distance(GetComponent<AIDestinationSetter>().target.position, transform.position) < 3f && difficulty !="easy" && difficulty != "normal")
                            {
                                Vector3 diff = new Vector3(GetComponent<AIDestinationSetter>().target.position.x + Random.Range(-1.5f, 1.5f), GetComponent<AIDestinationSetter>().target.position.y, GetComponent<AIDestinationSetter>().target.position.z) - transform.position;
                                diff.Normalize();

                                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                ball.transform.parent = null;
                                ball.GetComponent<Footballers_Ball>().player = null;
                                Quaternion oldrot = ball.transform.rotation;
                                ball.transform.rotation = transform.rotation;
                                if(difficulty == "hard")
                                    ball.GetComponent<Rigidbody2D>().velocity = -transform.up * weakShot;
                                else if (activateChargeBar)
                                    ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                ball.transform.rotation = oldrot;
                                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                passes++;
                                AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                foreach (Footballers_Poarta poarta in FindObjectsOfType<Footballers_Poarta>())
                                {
                                    Vector2 dirFromAtoB = (poarta.transform.position - transform.position).normalized;
                                    float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                    if (Vector2.Distance(transform.position, poarta.transform.position) < 5 && dotProd > 0.25f)
                                        FindObjectOfType<Footballers_Crowd>().Play();
                                }
                            }
                            else
                            {
                                Vector3 diff = newPos - oldPos;
                                diff.Normalize();

                                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                            }
                        }

                    }
                    else
                    {
                        GetComponent<AIDestinationSetter>().target = null;
                        GetComponent<AIPath>().enabled = false;
                        if (FindObjectOfType<Footballers_Ball>().player == null)
                        {
                            Vector3 newPos = Vector2.MoveTowards(transform.position, FindObjectOfType<Footballers_Ball>().transform.position, Time.deltaTime * speed);
                            Vector3 diff = newPos - transform.position;
                            diff.Normalize();

                            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                            transform.position = newPos;
                            GetComponent<Animator>().Play("runAnim");
                        }
                        else
                        {
                            GetComponent<AIPath>().enabled = false;
                            if (Vector2.Distance(poartaTransform.transform.position, transform.position) > 2)
                            {
                                GameObject obj = null;
                                if (team == 0)
                                    obj = GameObject.Find("goalkeeper0");
                                else obj = GameObject.Find("goalkeeper1");
                                foreach (Transform child in obj.transform)
                                {
                                    waypoints.Add(child.gameObject);
                                }
                                waypoint = waypoints[Random.Range(1, 1000000) % waypoints.Count];
                            }
                            if (waypoint != null)
                            {
                                Vector3 newPos = Vector2.MoveTowards(transform.position, waypoint.transform.position, Time.deltaTime * speed);
                                Vector3 diff = newPos - transform.position;
                                diff.Normalize();

                                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                transform.position = newPos;
                                if (waypoint != null)
                                    GetComponent<Animator>().Play("runAnim");
                                else GetComponent<Animator>().Play("idleAnim");
                                if (Vector2.Distance(transform.position, waypoint.transform.position) < 0.25f)
                                {
                                    waypoint = null;
                                    xTransform = transform.position.x;
                                }
                                Debug.Log("ye");
                            }
                            else
                            {
                                Vector2 newPos2 = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, FindObjectOfType<Footballers_Ball>().transform.position.y), Time.deltaTime * speed);

                                if (newPos2.y > 0.8f)
                                    newPos2.y = 0.8f;
                                if (newPos2.y < -1.3f)
                                    newPos2.y = -1.3f;
                                if (Mathf.Abs(transform.position.y - newPos2.y) < Time.deltaTime * speed) GetComponent<Animator>().Play("idleAnim");
                                else
                                {
                                    if (transform.position.y > newPos2.y)
                                    {
                                        transform.eulerAngles = new Vector3(0, 0, -0);
                                        GetComponent<Animator>().Play("runAnim");
                                    }
                                    else if (transform.position.y < newPos2.y)
                                    {
                                        transform.eulerAngles = new Vector3(0, 0, 180);
                                        GetComponent<Animator>().Play("runAnim");
                                    }
                                }
                                transform.position = new Vector2(xTransform, newPos2.y);
                                Debug.Log("test");
                            }
                        }
                    }
                }
                else
                {
                    if (ball != null)
                    {
                        ball.transform.localPosition = new Vector2(0, -ballDistanceFromPlayer);
                        ball.transform.eulerAngles = new Vector3(0, 0, 0);
                        if ((difficulty == "easy" || difficulty == "normal") && botPlayStyle != "goalkeeper")
                        {
                            gameObject.layer = 1;
                            ball.transform.localPosition = new Vector2(0, -ballDistanceFromPlayer);
                            ball.transform.eulerAngles = new Vector3(0, 0, 0);
                            if (difficulty == "easy")
                            {
                                foreach (Footballers_Poarta poarta in FindObjectsOfType<Footballers_Poarta>())
                                {
                                    if (poarta.team != team)
                                    {
                                        transform.position = Vector2.MoveTowards(transform.position, poarta.transform.position, Time.deltaTime * speed);
                                        Vector3 diff = poarta.transform.position - transform.position;
                                        diff.Normalize();

                                        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                    }
                                }
                            }
                            else
                            {
                                FindObjectOfType<AstarPath>().Scan();
                                foreach (Footballers_Poarta poarta in FindObjectsOfType<Footballers_Poarta>())
                                {
                                    if (poarta.team != team)
                                    {
                                        GetComponent<AIDestinationSetter>().target = poarta.transform;
                                    }
                                }
                                GetComponent<AIPath>().enabled = true;
                                if (Vector2.Distance(GetComponent<AIDestinationSetter>().target.position, transform.position) < 3f)
                                {
                                    Vector3 diff = new Vector3(GetComponent<AIDestinationSetter>().target.position.x + Random.Range(-1.5f, 1.5f), GetComponent<AIDestinationSetter>().target.position.y, GetComponent<AIDestinationSetter>().target.position.z) - transform.position;
                                    diff.Normalize();

                                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                    ball.transform.parent = null;
                                    ball.GetComponent<Footballers_Ball>().player = null;
                                    Quaternion oldrot = ball.transform.rotation;
                                    ball.transform.rotation = transform.rotation;
                                    if (activateChargeBar)
                                        ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                    else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                    ball.transform.rotation = oldrot;
                                    Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                    GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                    GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                    passes++;
                                    AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                    foreach (Footballers_Poarta poarta2 in FindObjectsOfType<Footballers_Poarta>())
                                    {
                                        Vector2 dirFromAtoB = (poarta2.transform.position - transform.position).normalized;
                                        float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                        if (Vector2.Distance(transform.position, poarta2.transform.position) < 7.5f && dotProd > 0.25f)
                                            FindObjectOfType<Footballers_Crowd>().Play();
                                    }
                                }
                                else
                                {
                                    Vector3 diff = newPos - oldPos;
                                    diff.Normalize();

                                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                }
                            }
                        }
                        else if(ball != null)
                        {
                            GetComponent<AIDestinationSetter>().target = null;
                            GetComponent<AIPath>().enabled = false;
                            if (botPlayStyle == "goalkeeper")
                            {
                                bool ok = false;
                                foreach (Footballers_Player player in FindObjectsOfType<Footballers_Player>())
                                {
                                    if (player.team == team && Vector2.Distance(transform.position, player.transform.position) > 0.5f && player.botPlayStyle == "bot" && Physics2D.Linecast(transform.position, player.transform.position, 8).collider == null)
                                    {
                                        ok = true;
                                        Vector3 diff = player.transform.position - transform.position;
                                        diff.Normalize();

                                        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

                                        ball.transform.parent = null;
                                        ball.GetComponent<Footballers_Ball>().player = null;
                                        Quaternion oldrot = ball.transform.rotation;
                                        ball.transform.rotation = transform.rotation;
                                        if (activateChargeBar)
                                            ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                        else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                        ball.transform.rotation = oldrot;
                                        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                        GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                        GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                        passes++;
                                        AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                        foreach (Footballers_Poarta poarta2 in FindObjectsOfType<Footballers_Poarta>())
                                        {
                                            Vector2 dirFromAtoB = (poarta2.transform.position - transform.position).normalized;
                                            float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                            if (Vector2.Distance(transform.position, poarta2.transform.position) < 7.5f && dotProd > 0.25f)
                                                FindObjectOfType<Footballers_Crowd>().Play();
                                        }
                                        break;
                                        GetComponent<Animator>().Play("idleAnim");
                                    }
                                }
                                if (ok == false)
                                {
                                    foreach (Footballers_Player player in FindObjectsOfType<Footballers_Player>())
                                    {
                                        if (player.team == team && Vector2.Distance(transform.position, player.transform.position) > 0.5f && player.botPlayStyle == "middle" && Physics2D.Linecast(transform.position, player.transform.position, 8).collider == null)
                                        {
                                            ok = true;
                                            Vector3 diff = player.transform.position - transform.position;
                                            diff.Normalize();

                                            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

                                            ball.transform.parent = null;
                                            ball.GetComponent<Footballers_Ball>().player = null;
                                            Quaternion oldrot = ball.transform.rotation;
                                            ball.transform.rotation = transform.rotation;
                                            if (activateChargeBar)
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                            else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                            ball.transform.rotation = oldrot;
                                            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                            GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                            GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                            passes++;
                                            AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                            foreach (Footballers_Poarta poarta2 in FindObjectsOfType<Footballers_Poarta>())
                                            {
                                                Vector2 dirFromAtoB = (poarta2.transform.position - transform.position).normalized;
                                                float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                                if (Vector2.Distance(transform.position, poarta2.transform.position) < 7.5f && dotProd > 0.25f)
                                                    FindObjectOfType<Footballers_Crowd>().Play();
                                            }
                                            break;
                                            GetComponent<Animator>().Play("idleAnim");
                                        }
                                    }
                                }
                                if (ok == false)
                                {
                                    foreach (Footballers_Player player in FindObjectsOfType<Footballers_Player>())
                                    {
                                        if (player.team == team && Vector2.Distance(transform.position, player.transform.position) > 0.5f && player.botPlayStyle == "top" && Physics2D.Linecast(transform.position, player.transform.position, 8).collider == null)
                                        {
                                            ok = true;
                                            Vector3 diff = player.transform.position - transform.position;
                                            diff.Normalize();

                                            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

                                            ball.transform.parent = null;
                                            ball.GetComponent<Footballers_Ball>().player = null;
                                            Quaternion oldrot = ball.transform.rotation;
                                            ball.transform.rotation = transform.rotation;
                                            if (activateChargeBar)
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                            else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                            ball.transform.rotation = oldrot;
                                            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                            GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                            GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                            passes++;
                                            AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                            foreach (Footballers_Poarta poarta2 in FindObjectsOfType<Footballers_Poarta>())
                                            {
                                                Vector2 dirFromAtoB = (poarta2.transform.position - transform.position).normalized;
                                                float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                                if (Vector2.Distance(transform.position, poarta2.transform.position) < 7.5f && dotProd > 0.25f)
                                                    FindObjectOfType<Footballers_Crowd>().Play();
                                            }
                                            break;
                                            GetComponent<Animator>().Play("idleAnim");
                                        }
                                    }
                                }
                                if (ok == false)
                                {
                                    foreach (Footballers_Poarta poarta in FindObjectsOfType<Footballers_Poarta>())
                                    {
                                        if (poarta.team != team)
                                        {
                                            ok = true;
                                            Vector3 diff = new Vector3(poarta.transform.position.x + Random.Range(-1.5f, 1.5f), poarta.transform.position.y, poarta.transform.position.z) - transform.position;
                                            diff.Normalize();

                                            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                            ball.transform.parent = null;
                                            ball.GetComponent<Footballers_Ball>().player = null;
                                            Quaternion oldrot = ball.transform.rotation;
                                            ball.transform.rotation = transform.rotation;
                                            if (activateChargeBar)
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                            else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                            ball.transform.rotation = oldrot;
                                            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                            GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                            GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                            passes++;
                                            AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                            foreach (Footballers_Poarta poarta2 in FindObjectsOfType<Footballers_Poarta>())
                                            {
                                                Vector2 dirFromAtoB = (poarta2.transform.position - transform.position).normalized;
                                                float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                                if (Vector2.Distance(transform.position, poarta2.transform.position) < 7.5f && dotProd > 0.25f)
                                                    FindObjectOfType<Footballers_Crowd>().Play();
                                            }
                                            break;
                                            GetComponent<Animator>().Play("idleAnim");
                                        }
                                    }
                                }
                            }
                            else if (botPlayStyle == "bot")
                            {
                                bool ok = false;
                                foreach (Footballers_Player player in FindObjectsOfType<Footballers_Player>())
                                {
                                    if (player.team == team && Vector2.Distance(transform.position, player.transform.position) > 0.5f && player.botPlayStyle == "middle" && Physics2D.Linecast(transform.position, player.transform.position, 8).collider == null)
                                    {
                                        ok = true;
                                        Vector3 diff = player.transform.position - transform.position;
                                        diff.Normalize();

                                        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

                                        ball.transform.parent = null;
                                        ball.GetComponent<Footballers_Ball>().player = null;
                                        Quaternion oldrot = ball.transform.rotation;
                                        ball.transform.rotation = transform.rotation;
                                        if (difficulty == "hard")
                                            ball.GetComponent<Rigidbody2D>().velocity = -transform.up * weakShot;
                                        else if (activateChargeBar)
                                            ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                        else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                        ball.transform.rotation = oldrot;
                                        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                        GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                        GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                        passes++;
                                        AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                        foreach (Footballers_Poarta poarta2 in FindObjectsOfType<Footballers_Poarta>())
                                        {
                                            Vector2 dirFromAtoB = (poarta2.transform.position - transform.position).normalized;
                                            float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                            if (Vector2.Distance(transform.position, poarta2.transform.position) < 7.5f && dotProd > 0.25f)
                                                FindObjectOfType<Footballers_Crowd>().Play();
                                        }
                                        break;
                                        Debug.Log("Shooting at " + player.gameObject.name + ' ' + transform.rotation.z);
                                    }
                                }
                                if (ok == false)
                                {
                                    foreach (Footballers_Player player in FindObjectsOfType<Footballers_Player>())
                                    {
                                        if (player.team == team && Vector2.Distance(transform.position, player.transform.position) > 0.5f && player.botPlayStyle == "bot" && Physics2D.Linecast(transform.position, player.transform.position, 8).collider == null)
                                        {
                                            ok = true;
                                            Vector3 diff = player.transform.position - transform.position;
                                            diff.Normalize();

                                            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

                                            ball.transform.parent = null;
                                            ball.GetComponent<Footballers_Ball>().player = null;
                                            Quaternion oldrot = ball.transform.rotation;
                                            ball.transform.rotation = transform.rotation;
                                            if (difficulty == "hard")
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * weakShot;
                                            else if (activateChargeBar)
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                            else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                            ball.transform.rotation = oldrot;
                                            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                            GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                            GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                            passes++;
                                            AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                            foreach (Footballers_Poarta poarta2 in FindObjectsOfType<Footballers_Poarta>())
                                            {
                                                Vector2 dirFromAtoB = (poarta2.transform.position - transform.position).normalized;
                                                float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                                if (Vector2.Distance(transform.position, poarta2.transform.position) < 7.5f && dotProd > 0.25f)
                                                    FindObjectOfType<Footballers_Crowd>().Play();
                                            }
                                            break;
                                            Debug.Log("Shooting at " + player.gameObject.name + ' ' + transform.rotation.z);
                                        }
                                    }
                                }
                                if (ok == false)
                                {
                                    foreach (Footballers_Player player in FindObjectsOfType<Footballers_Player>())
                                    {
                                        if (player.team == team && Vector2.Distance(transform.position, player.transform.position) > 0.5f && player.botPlayStyle == "top" && Physics2D.Linecast(transform.position, player.transform.position, 8).collider == null)
                                        {
                                            ok = true;
                                            Vector3 diff = player.transform.position - transform.position;
                                            diff.Normalize();

                                            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

                                            ball.transform.parent = null;
                                            ball.GetComponent<Footballers_Ball>().player = null;
                                            Quaternion oldrot = ball.transform.rotation;
                                            ball.transform.rotation = transform.rotation;
                                            if (difficulty == "hard")
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * weakShot;
                                            else if (activateChargeBar)
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                            else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                            ball.transform.rotation = oldrot;
                                            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                            GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                            GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                            passes++;
                                            AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                            foreach (Footballers_Poarta poarta2 in FindObjectsOfType<Footballers_Poarta>())
                                            {
                                                Vector2 dirFromAtoB = (poarta2.transform.position - transform.position).normalized;
                                                float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                                if (Vector2.Distance(transform.position, poarta2.transform.position) < 7.5f && dotProd > 0.25f)
                                                    FindObjectOfType<Footballers_Crowd>().Play();
                                            }
                                            break;
                                            Debug.Log("Shooting at " + player.gameObject.name + ' ' + transform.rotation.z);
                                        }
                                    }
                                }
                                if (ok == false)
                                {
                                    foreach (Footballers_Poarta poarta in FindObjectsOfType<Footballers_Poarta>())
                                    {
                                        if (poarta.team != team)
                                        {
                                            ok = true;
                                            Vector3 diff = new Vector3(poarta.transform.position.x + Random.Range(-1.5f, 1.5f), poarta.transform.position.y, poarta.transform.position.z) - transform.position;
                                            diff.Normalize();

                                            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                            ball.transform.parent = null;
                                            ball.GetComponent<Footballers_Ball>().player = null;
                                            Quaternion oldrot = ball.transform.rotation;
                                            ball.transform.rotation = transform.rotation;
                                            if (difficulty == "hard")
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * weakShot;
                                            else if (activateChargeBar)
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                            else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                            ball.transform.rotation = oldrot;
                                            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                            GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                            GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                            passes++;
                                            AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                            foreach (Footballers_Poarta poarta2 in FindObjectsOfType<Footballers_Poarta>())
                                            {
                                                Vector2 dirFromAtoB = (poarta2.transform.position - transform.position).normalized;
                                                float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                                if (Vector2.Distance(transform.position, poarta2.transform.position) < 7.5f && dotProd > 0.25f)
                                                    FindObjectOfType<Footballers_Crowd>().Play();
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                            else if (botPlayStyle == "middle")
                            {
                                bool ok = false;
                                foreach (Footballers_Player player in FindObjectsOfType<Footballers_Player>())
                                {
                                    if (player.team == team && Vector2.Distance(transform.position, player.transform.position) > 0.5f && player.botPlayStyle == "top" && Physics2D.Linecast(transform.position, player.transform.position, 8).collider == null)
                                    {
                                        ok = true;
                                        Vector3 diff = player.transform.position - transform.position;
                                        diff.Normalize();

                                        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

                                        ball.transform.parent = null;
                                        ball.GetComponent<Footballers_Ball>().player = null;
                                        Quaternion oldrot = ball.transform.rotation;
                                        ball.transform.rotation = transform.rotation;
                                        if (difficulty == "hard")
                                            ball.GetComponent<Rigidbody2D>().velocity = -transform.up * weakShot;
                                        else if (activateChargeBar)
                                            ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                        else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                        ball.transform.rotation = oldrot;
                                        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                        GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                        GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                        passes++;
                                        AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                        foreach (Footballers_Poarta poarta2 in FindObjectsOfType<Footballers_Poarta>())
                                        {
                                            Vector2 dirFromAtoB = (poarta2.transform.position - transform.position).normalized;
                                            float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                            if (Vector2.Distance(transform.position, poarta2.transform.position) < 7.5f && dotProd > 0.25f)
                                                FindObjectOfType<Footballers_Crowd>().Play();
                                        }
                                        break;
                                        Debug.Log("Shooting at " + player.gameObject.name + ' ' + transform.rotation.z);
                                    }
                                }
                                if (ok == false)
                                {
                                    foreach (Footballers_Player player in FindObjectsOfType<Footballers_Player>())
                                    {
                                        if (player.team == team && Vector2.Distance(transform.position, player.transform.position) > 0.5f && player.botPlayStyle == "middle" && Physics2D.Linecast(transform.position, player.transform.position, 8).collider == null)
                                        {
                                            ok = true;
                                            Vector3 diff = player.transform.position - transform.position;
                                            diff.Normalize();

                                            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

                                            ball.transform.parent = null;
                                            ball.GetComponent<Footballers_Ball>().player = null;
                                            Quaternion oldrot = ball.transform.rotation;
                                            ball.transform.rotation = transform.rotation;
                                            if (difficulty == "hard")
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * weakShot;
                                            else if (activateChargeBar)
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                            else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                            ball.transform.rotation = oldrot;
                                            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                            GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                            GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                            passes++;
                                            AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                            foreach (Footballers_Poarta poarta2 in FindObjectsOfType<Footballers_Poarta>())
                                            {
                                                Vector2 dirFromAtoB = (poarta2.transform.position - transform.position).normalized;
                                                float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                                if (Vector2.Distance(transform.position, poarta2.transform.position) < 7.5f && dotProd > 0.25f)
                                                    FindObjectOfType<Footballers_Crowd>().Play();
                                            }
                                            break;
                                            Debug.Log("Shooting at " + player.gameObject.name + ' ' + transform.rotation.z);
                                        }
                                    }
                                }
                                if (ok == false)
                                {
                                    foreach (Footballers_Player player in FindObjectsOfType<Footballers_Player>())
                                    {
                                        if (player.team == team && Vector2.Distance(transform.position, player.transform.position) > 0.5f && player.botPlayStyle == "bot" && Physics2D.Linecast(transform.position, player.transform.position, 8).collider == null)
                                        {
                                            ok = true;
                                            Vector3 diff = player.transform.position - transform.position;
                                            diff.Normalize();

                                            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

                                            ball.transform.parent = null;
                                            ball.GetComponent<Footballers_Ball>().player = null;
                                            Quaternion oldrot = ball.transform.rotation;
                                            ball.transform.rotation = transform.rotation;
                                            if (difficulty == "hard")
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * weakShot;
                                            else if (activateChargeBar)
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                            else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                            ball.transform.rotation = oldrot;
                                            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                            GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                            GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                            passes++;
                                            AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                            foreach (Footballers_Poarta poarta2 in FindObjectsOfType<Footballers_Poarta>())
                                            {
                                                Vector2 dirFromAtoB = (poarta2.transform.position - transform.position).normalized;
                                                float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                                if (Vector2.Distance(transform.position, poarta2.transform.position) < 7.5f && dotProd > 0.25f)
                                                    FindObjectOfType<Footballers_Crowd>().Play();
                                            }
                                            break;
                                            Debug.Log("Shooting at " + player.gameObject.name + ' ' + transform.rotation.z);
                                        }
                                    }
                                }
                                if (ok == false)
                                {
                                    foreach (Footballers_Poarta poarta in FindObjectsOfType<Footballers_Poarta>())
                                    {
                                        if (poarta.team != team)
                                        {
                                            ok = true;
                                            Vector3 diff = new Vector3(poarta.transform.position.x + Random.Range(-1.5f, 1.5f), poarta.transform.position.y, poarta.transform.position.z) - transform.position;
                                            diff.Normalize();

                                            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                            ball.transform.parent = null;
                                            ball.GetComponent<Footballers_Ball>().player = null;
                                            Quaternion oldrot = ball.transform.rotation;
                                            ball.transform.rotation = transform.rotation;
                                            if (difficulty == "hard")
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * weakShot;
                                            else if (activateChargeBar)
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                            else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                            ball.transform.rotation = oldrot;
                                            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                            GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                            GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                            passes++;
                                            AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                            foreach (Footballers_Poarta poarta2 in FindObjectsOfType<Footballers_Poarta>())
                                            {
                                                Vector2 dirFromAtoB = (poarta2.transform.position - transform.position).normalized;
                                                float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                                if (Vector2.Distance(transform.position, poarta2.transform.position) < 7.5f && dotProd > 0.25f)
                                                    FindObjectOfType<Footballers_Crowd>().Play();
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                            else if (botPlayStyle == "top")
                            {
                                if (Random.Range(1, 1000000) % 2 == 0)
                                {
                                    bool ok = false;
                                    foreach (Footballers_Player player in FindObjectsOfType<Footballers_Player>())
                                    {
                                        if (player.team == team && Vector2.Distance(transform.position, player.transform.position) > 0.5f && player.botPlayStyle == "top" && Physics2D.Linecast(transform.position, player.transform.position, 8).collider == null)
                                        {
                                            ok = true;
                                            Vector3 diff = player.transform.position - transform.position;
                                            diff.Normalize();

                                            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                            ball.transform.parent = null;
                                            ball.GetComponent<Footballers_Ball>().player = null;
                                            Quaternion oldrot = ball.transform.rotation;
                                            ball.transform.rotation = transform.rotation;
                                            if (difficulty == "hard")
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * weakShot;
                                            else if (activateChargeBar)
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                            else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                            ball.transform.rotation = oldrot;
                                            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                            GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                            GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                            passes++;
                                            AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                            foreach (Footballers_Poarta poarta2 in FindObjectsOfType<Footballers_Poarta>())
                                            {
                                                Vector2 dirFromAtoB = (poarta2.transform.position - transform.position).normalized;
                                                float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                                if (Vector2.Distance(transform.position, poarta2.transform.position) < 7.5f && dotProd > 0.25f)
                                                    FindObjectOfType<Footballers_Crowd>().Play();
                                            }
                                            break;
                                            Debug.Log("Shooting at " + player.gameObject.name + ' ' + transform.rotation.z);
                                        }
                                    }
                                    if (ok == false)
                                    {
                                        foreach (Footballers_Player player in FindObjectsOfType<Footballers_Player>())
                                        {
                                            if (player.team == team && Vector2.Distance(transform.position, player.transform.position) > 0.5f && player.botPlayStyle == "middle" && Physics2D.Linecast(transform.position, player.transform.position, 8).collider == null)
                                            {
                                                ok = true;
                                                Vector3 diff = player.transform.position - transform.position;
                                                diff.Normalize();

                                                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                                transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

                                                ball.transform.parent = null;
                                                ball.GetComponent<Footballers_Ball>().player = null;
                                                Quaternion oldrot = ball.transform.rotation;
                                                ball.transform.rotation = transform.rotation;
                                                if (difficulty == "hard")
                                                    ball.GetComponent<Rigidbody2D>().velocity = -transform.up * weakShot;
                                                else if (activateChargeBar)
                                                    ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                                else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                                ball.transform.rotation = oldrot;
                                                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                                GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                                GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                                passes++;
                                                AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                                foreach (Footballers_Poarta poarta2 in FindObjectsOfType<Footballers_Poarta>())
                                                {
                                                    Vector2 dirFromAtoB = (poarta2.transform.position - transform.position).normalized;
                                                    float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                                    if (Vector2.Distance(transform.position, poarta2.transform.position) < 7.5f && dotProd > 0.25f)
                                                        FindObjectOfType<Footballers_Crowd>().Play();
                                                }
                                                break;
                                                Debug.Log("Shooting at " + player.gameObject.name + ' ' + transform.rotation.z);
                                            }
                                        }
                                    }
                                    if (ok == false)
                                    {
                                        foreach (Footballers_Player player in FindObjectsOfType<Footballers_Player>())
                                        {
                                            if (player.team == team && Vector2.Distance(transform.position, player.transform.position) > 0.5f && player.botPlayStyle == "bot" && Physics2D.Linecast(transform.position, player.transform.position, 8).collider == null)
                                            {
                                                ok = true;
                                                Vector3 diff = player.transform.position - transform.position;
                                                diff.Normalize();

                                                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                                transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

                                                ball.transform.parent = null;
                                                ball.GetComponent<Footballers_Ball>().player = null;
                                                Quaternion oldrot = ball.transform.rotation;
                                                ball.transform.rotation = transform.rotation;
                                                if (difficulty == "hard")
                                                    ball.GetComponent<Rigidbody2D>().velocity = -transform.up * weakShot;
                                                else if (activateChargeBar)
                                                    ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                                else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                                ball.transform.rotation = oldrot;
                                                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                                GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                                GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                                passes++;
                                                AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                                foreach (Footballers_Poarta poarta2 in FindObjectsOfType<Footballers_Poarta>())
                                                {
                                                    Vector2 dirFromAtoB = (poarta2.transform.position - transform.position).normalized;
                                                    float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                                    if (Vector2.Distance(transform.position, poarta2.transform.position) < 7.5f && dotProd > 0.25f)
                                                        FindObjectOfType<Footballers_Crowd>().Play();
                                                }
                                                break;
                                                Debug.Log("Shooting at " + player.gameObject.name + ' ' + transform.rotation.z);
                                            }
                                        }
                                    }
                                    if (ok == false)
                                    {
                                        foreach (Footballers_Poarta poarta in FindObjectsOfType<Footballers_Poarta>())
                                        {
                                            if (poarta.team != team)
                                            {
                                                ok = true;
                                                Vector3 diff = new Vector3(poarta.transform.position.x + Random.Range(-1.5f, 1.5f), poarta.transform.position.y, poarta.transform.position.z) - transform.position;
                                                diff.Normalize();

                                                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                                transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                                ball.transform.parent = null;
                                                ball.GetComponent<Footballers_Ball>().player = null;
                                                Quaternion oldrot = ball.transform.rotation;
                                                ball.transform.rotation = transform.rotation;
                                                if (difficulty == "hard")
                                                    ball.GetComponent<Rigidbody2D>().velocity = -transform.up * weakShot;
                                                else if (activateChargeBar)
                                                    ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                                else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                                ball.transform.rotation = oldrot;
                                                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                                GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                                GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                                passes++;
                                                AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                                foreach (Footballers_Poarta poarta2 in FindObjectsOfType<Footballers_Poarta>())
                                                {
                                                    Vector2 dirFromAtoB = (poarta2.transform.position - transform.position).normalized;
                                                    float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                                    if (Vector2.Distance(transform.position, poarta2.transform.position) < 7.5f && dotProd > 0.25f)
                                                        FindObjectOfType<Footballers_Crowd>().Play();
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (Footballers_Poarta poarta in FindObjectsOfType<Footballers_Poarta>())
                                    {
                                        if (poarta.team != team)
                                        {
                                            Vector3 diff = new Vector3(poarta.transform.position.x + Random.Range(-1.5f, 1.5f), poarta.transform.position.y, poarta.transform.position.z) - transform.position;
                                            diff.Normalize();

                                            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                            transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                            ball.transform.parent = null;
                                            ball.GetComponent<Footballers_Ball>().player = null;
                                            Quaternion oldrot = ball.transform.rotation;
                                            ball.transform.rotation = transform.rotation;
                                            if (difficulty == "hard")
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * weakShot;
                                            else if (activateChargeBar)
                                                ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot * Mathf.Max(chargeTime, 0.2f) * dribbleMultiplier;
                                            else ball.GetComponent<Rigidbody2D>().velocity = -transform.up * powerfulShot;
                                            ball.transform.rotation = oldrot;
                                            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), ball.GetComponent<CircleCollider2D>(), false); ball = null;
                                            GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                                            GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                                            passes++;
                                            AudioSource.PlayClipAtPoint(pass, Camera.main.transform.position);
                                            foreach (Footballers_Poarta poarta2 in FindObjectsOfType<Footballers_Poarta>())
                                            {
                                                Vector2 dirFromAtoB = (poarta2.transform.position - transform.position).normalized;
                                                float dotProd = Vector2.Dot(dirFromAtoB, -transform.up);
                                                if (Vector2.Distance(transform.position, poarta2.transform.position) < 7.5f && dotProd > 0.25f)
                                                    FindObjectOfType<Footballers_Crowd>().Play();
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (botPlayStyle == "goalkeeper")
                        {
                            if (waypoint != null)
                            {
                                Vector3 newPos = Vector2.MoveTowards(transform.position, waypoint.transform.position, Time.deltaTime * speed);
                                Vector3 diff = newPos - transform.position;
                                diff.Normalize();

                                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                transform.position = newPos;
                                if (waypoint != null)
                                    GetComponent<Animator>().Play("runAnim");
                                else GetComponent<Animator>().Play("idleAnim");
                                if (Vector2.Distance(transform.position, waypoint.transform.position) < 0.25f)
                                {
                                    waypoint = null;
                                    xTransform = transform.position.x;
                                }
                                Debug.Log("ye");
                            }
                            else
                            {
                                Vector2 newPos2 = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, FindObjectOfType<Footballers_Ball>().transform.position.y), Time.deltaTime * speed);

                                if (newPos2.y > 0.8f)
                                    newPos2.y = 0.8f;
                                if (newPos2.y < -1.3f)
                                    newPos2.y = -1.3f;
                                if (Mathf.Abs(transform.position.y - newPos2.y) < Time.deltaTime) GetComponent<Animator>().Play("idleAnim");
                                else
                                {
                                    if (transform.position.y > newPos2.y)
                                    {
                                        transform.eulerAngles = new Vector3(0, 0, -0);
                                        GetComponent<Animator>().Play("runAnim");
                                    }
                                    else if (transform.position.y < newPos2.y)
                                    {
                                        transform.eulerAngles = new Vector3(0, 0, 180);
                                        GetComponent<Animator>().Play("runAnim");
                                    }
                                }
                                transform.position = new Vector2(xTransform, newPos2.y);
                            }
                        }
                        else
                        {
                            if ((ballFind.GetComponent<Footballers_BallFind>().ball == null || Vector2.Distance(transform.position, waypoint.transform.position) > 4) && waypoint != null)
                            {
                                ballFind.GetComponent<Footballers_BallFind>().ball = null;
                                Vector3 newPos = Vector2.MoveTowards(transform.position, waypoint.transform.position, Time.deltaTime * speed);
                                Vector3 diff = newPos - transform.position;
                                diff.Normalize();

                                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                transform.position = newPos;
                                if (difficulty == "easy" && Vector2.Distance(transform.position, waypoint.transform.position) < 0.25f)
                                    GetComponent<Animator>().Play("idleAnim");
                                else
                                {
                                    if (waypoint != null)
                                        GetComponent<Animator>().Play("runAnim");
                                    else GetComponent<Animator>().Play("idleAnim");
                                }
                                if (Vector2.Distance(transform.position, waypoint.transform.position) < 0.25f && difficulty != "easy")
                                {
                                    if (botPlayStyle != "goalkeeper")
                                        waypoint = waypoints[Random.Range(1, 1000000) % waypoints.Count];
                                    else waypoint = null;
                                }
                                else
                                {
                                    if (difficulty == "easy")
                                        GetComponent<Animator>().Play("idle");
                                }
                            }
                            else
                            {

                                Vector3 newPos = Vector2.MoveTowards(transform.position, ballFind.GetComponent<Footballers_BallFind>().ball.transform.position, Time.deltaTime * speed);
                                Vector3 diff = newPos - transform.position;
                                diff.Normalize();

                                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                                transform.position = newPos;
                                GetComponent<Animator>().Play("runAnim");

                            }
                        }
                    }
                }
            }
            else
            {
                GetComponent<AIDestinationSetter>().target = null;
                GetComponent<AIPath>().enabled = false;
                GetComponent<Animator>().Play("idleAnim");
            }
        }
    }
    float xTransform;
}
