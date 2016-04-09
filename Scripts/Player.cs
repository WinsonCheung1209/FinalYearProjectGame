using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;

public class Player : Character {

    public CharacterController charControl;
    protected const float jumpSpeed = 40;
    protected float curHeight = 0;
    protected const float rotateSpeed = 2;
    public Transform camTransform;
    public float camHeight = 2f;
    protected Vector3 camRotation;
    protected bool jumpLocked = false;
    public float exp = 0;
    public float maxExp = 10;
    public int gold;

    public LayerMask layers;
    public Bag bag;
    public float recoverMp;
    public Quest[] activeQuests;
    public int attributePoints = 0;
    public int luck = 0;
    public int classType = 0;
    public int skillPoints;
    public float attMultiplier = 1;
    public float hpMultiplier = 1;
    public float agiMultiplier = 1;
    public AudioClip walkingClip;
    public AudioClip gruntClip;
    public AudioClip coinsClip;
    public AudioSource audio0;
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;
    public bool flymode;
    public int score;

    public Vector2 scrollPane = new Vector2(0, 0);
    public Rect highScoreGrid = new Rect(20, 20, 450, 500);
    public string[] highScoreEntries;
    protected string highScoreName = "Player";
    bool submitted = false;
    float onMovingPlatf = 0;
    private MySqlConnection conn;


    // Use this for initialization
    public void Start() {
        base.Start();
        if (bag == null) {
            // initialise the player's bag
            bag = new Bag(true);
            // initialise the player's stats
            charLevel = 1;
            DontDestroyOnLoad(this.gameObject);
            charControl = this.GetComponent<CharacterController>();
            moveSpeed = 10;
            maxHp = 100;
            maxMp = 100;
            baseHp = maxHp;
            mp = maxMp;
            baseAttack = 4;
            attackRange = 5;
            recoverHp = 0.75f;
            recoverMp = 1f;
            activeQuests = new Quest[6];
            skillPoints = 0;
            // setup the sound effects
            AudioSource[] audios = this.GetComponents<AudioSource>();
            audio0 = audios[0];
            audio1 = audios[1];
            audio2 = audios[2];
            audio3 = audios[3];

            // setup the camera
            camTransform = Camera.main.transform;
            Vector3 camPosition = transformObject.position;
            camPosition.y += camHeight;
            camTransform.position = camPosition;
            camTransform.rotation = transformObject.rotation;
            camRotation = camTransform.eulerAngles;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            // initialise variables
            flymode = false;
            highScoreEntries = new string[20];
            onMovingPlatf = 0;
            //connectToSQL();
        }
    }

    // Update is called once per frame
    void Update() {
        if (baseHp > 0) {
            // if the player is still alive...

            // constantly decrease the player's HP if they fall below the ground, this is used when the player fell off a jumping puzzle
            if (transformObject.position.y < -0.5) {
                baseHp -= Time.deltaTime * maxHp / 3;
            }
            // graduately recover HP and MP
            recovering();

            // allow movement and look around if no panel is being displayed
            if (GameEngine.ge.showPanel == 0) {
                float rotateH = Input.GetAxis("Mouse X");
                float rotateV = Input.GetAxis("Mouse Y");

                if (camRotation.x - rotateV >= -90 && camRotation.x - rotateV <= 90) {
                    camRotation.x -= rotateV * rotateSpeed;
                }
                camRotation.y += rotateH * rotateSpeed;
                camTransform.eulerAngles = camRotation;

                Vector3 camYRotation = camRotation;
                camYRotation.x = 0;
                camYRotation.z = 0;
                transformObject.eulerAngles = camYRotation;

                float x = 0;
                float y = 0;
                float z = 0;

                y -= gravity * Time.deltaTime;
                curHeight -= gravity * Time.deltaTime;
                if (curHeight <= 0) {
                    curHeight = 0;
                }
                if (Input.GetKey(KeyCode.W)) {
                    z = moveSpeed * Time.deltaTime;
                } else if (Input.GetKey(KeyCode.S)) {
                    z = -moveSpeed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.A)) {
                    x = -moveSpeed * Time.deltaTime;
                } else if (Input.GetKey(KeyCode.D)) {
                    x = moveSpeed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
                    playWalkingSound();
                }

                if (!jumpLocked && Input.GetKey(KeyCode.Space)) {
                    if (curHeight >= 3) {
                        jumpLocked = true;
                    } else {
                        y += jumpSpeed * Time.deltaTime;
                        if (!flymode)
                            curHeight += jumpSpeed * Time.deltaTime;
                    }
                }
                if (Input.GetKeyUp(KeyCode.Space)) {
                    jumpLocked = true;
                }
                if (curHeight == 0 && !Input.GetKey(KeyCode.Space)) {
                    jumpLocked = false;
                }
                charControl.Move(new Vector3(onMovingPlatf, 0, 0) + transformObject.TransformDirection(new Vector3(x, y, z)));
                // right click to check monster's HP
                if (Input.GetMouseButton(1)) {
                    checkTargetHp();
                }
            } else { // a panel is being displayed, so show the cursor and stop the walking sound
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                audio0.Stop();
            }
            Vector3 camPosition = transformObject.position;
            camPosition.y += camHeight;
            camTransform.position = camPosition;
        } else { // player dies, so show the cursor and stop the walking sound
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            audio0.Stop();
        }
        // if not moving or is currently jumping in the air, stop the walking sound
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) {
            audio0.Stop();
        }
        if (curHeight > 0) {
            audio0.Stop();
        }
    }

    private void playWalkingSound() {
        if (curHeight <= 0 && !audio0.isPlaying) {
            audio0.loop = true;
            audio0.clip = walkingClip;
            audio0.Play();
        }
    }

    private void checkTargetHp() {
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(camTransform.position, camTransform.TransformDirection(Vector3.forward), out hitInfo, 20, layers);
        string text = "";
        if (hit) {
            if (hitInfo.transform.tag.Equals("Enemy")) {
                Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                text = "Level " + enemy.charLevel + " " + enemy.name;
                GameEngine.ge.enemyHp = enemy.baseHp;
                GameEngine.ge.enemyMaxHp = enemy.maxHp;
            }
            GameEngine.ge.hpText = text;

        }
    }

    public float getHp() {
        return baseHp * hpMultiplier;
    }

    public float getMaxHp() {
        return maxHp * hpMultiplier;
    }

    public float getMp() {
        return mp;
    }

    public float getMaxMp() {
        return maxMp;
    }

    public float getExp() {
        return exp;
    }

    public float getMaxExp() {
        return maxExp;
    }

    public float getSpeed() {
        return moveSpeed * agiMultiplier;
    }

    // add to the player's experience, once it reached max, it will be reset to 0 with a higher max experience, which means it will take longer to level up
    public void gainExp(float e, bool display) {
        exp += e;
        if (display)
            GameEngine.ge.setText(GameEngine.ge.getText() + "Obtained " + e + " exp\n");
        if (exp >= maxExp) {
            while (exp >= maxExp) {
                maxHp += 2 * charLevel + 51;
                baseHp = maxHp;
                maxMp += 2 * charLevel + 29;
                mp = maxMp;
                baseAttack += 0.2f * charLevel + 3.1f;

                charLevel++;
                exp -= maxExp;
                maxExp *= 1.3f;
                maxExp = Mathf.Round(maxExp);
                attributePoints += 5;
                skillPoints += 3;
            }
            // load the level up text
            Instantiate(Resources.Load("LevelUpText"), new Vector3(1, 1, 15), Quaternion.Euler(new Vector3(0, 0, 0)));
            if (activeQuests[0] != null && activeQuests[0].name == "Getting stronger 2") {
                if (charLevel > activeQuests[0].goal) {
                    activeQuests[0].progress = activeQuests[0].goal;
                } else {
                    activeQuests[0].progress = charLevel;
                }
            }
        }
    }

    // add to player's gold
    public void gainGold(int g, bool display) {
        gold += g;
        if (display)
            GameEngine.ge.setText(GameEngine.ge.getText() + "Obtained " + g + " gold\n");
        if (gold < 0) {
            gold = 0;
        }
    }

    // damage the player
    public void damaged(float damage) {
        if (baseHp > 0) {
            baseHp -= damage / hpMultiplier;
            if (damage > 0) {
                audio2.PlayOneShot(gruntClip);
                decreaseScore(((int) damage) / 2);
            }
            if (baseHp <= 0) {
                audio1.Play();
                baseHp = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                if (GameEngine.ge.towerLevel > 0) {
                    downloadScores();
                }
            } else if (baseHp > maxHp) {
                baseHp = maxHp;
            }
        }
    }

    // decrease player's MP
    public void minusMp(float m) {
        mp -= m;
        if (mp < 0) {
            mp = 0;
        } else if (mp > maxMp) {
            mp = maxMp;
        }
    }

    // recover HP and MP
    private void recovering() {
        baseHp += recoverHp / hpMultiplier * Time.deltaTime;
        mp += recoverMp * Time.deltaTime;
        if (baseHp > maxHp) {
            baseHp = maxHp;
        }
        if (mp > maxMp) {
            mp = maxMp;
        }
    }

    // set the class of the player
    public void setClass(int classT) {
        ClassType currentClass = this.gameObject.GetComponent<ClassType>();
        if (currentClass != null) {
            Destroy(currentClass);
            currentClass = null;
        }
        if (classT == 1) {
            currentClass = this.gameObject.GetComponent<ClassType>();
            if (currentClass == null) {
                this.gameObject.AddComponent<FireClass>();
            }
        } else if (classT == 2) {
            currentClass = this.gameObject.GetComponent<ClassType>();
            if (currentClass == null) {
                this.gameObject.AddComponent<WaterClass>();
            }
        } else if (classT == 3) {
            currentClass = this.gameObject.GetComponent<ClassType>();
            if (currentClass == null) {
                this.gameObject.AddComponent<WindClass>();
            }
        }
        classType = classT;
    }

    // calculate the minimum damage the player can deal. 
    public int calMinAttack() {
        return Mathf.RoundToInt(baseAttack * attMultiplier - baseAttack * attMultiplier * 0.25f);
    }

    // calculate the maximum damage the player can deal. actual damage will be a random value between the two
    public int calMaxAttack() {
        return Mathf.RoundToInt(baseAttack * attMultiplier + baseAttack * attMultiplier * 0.25f);
    }

    // decrease score when in high score mode
    public void decreaseScore(int s) {
        score -= s;
        if (score < 0) {
            score = 0;
        }
    }

    //public void connectToSQL() {
    //    conn = new MySqlConnection();
    //    conn.ConnectionString = "Server=sql4.freesqldatabase.com;Database=sql483312;Uid=sql483312;Pwd=uS8*kW3*;";
    //    conn.Open();

    //}

    public void uploadScore(string name, int score) {
        if (conn != null) {
            //string query = "SELECT * FROM hiscores ORDER BY score Desc";
            string query = "INSERT INTO hiscores VALUES ('" + name + "', " + score + ");";
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader dataReader = command.ExecuteReader();
            dataReader.Close();
            
            downloadScores();
        }
    }

    public void downloadScores() {
        if (conn != null) {
            string query = "SELECT * FROM hiscores ORDER BY score Desc LIMIT 20";
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader dataReader = command.ExecuteReader();

            //Read the data and store them in the list
            int i = 0;
            while (dataReader.Read()) {
                highScoreEntries[i] = "Rank " + (i + 1) + " - " + dataReader["name"] + ": " + dataReader["score"];
                i++;
            }

            //close Data Reader
            dataReader.Close();

        }
    }

    // upload score method, taken from the book at reference [7] of the dissertation, modified slightly to point to the right url etc.
    //IEnumerator UploadScore(string name, string score) {
    //    PostStream poststream = new PostStream();

    //    poststream.BeginWrite(true);
    //    poststream.Write("name", name);
    //    poststream.Write("score", score);
    //    poststream.EndWrite();

    //    WWW www = new WWW("users.sussex.ac.uk/~wcwc20/Project/UploadScore.php", poststream.BYTES, poststream.headers);

    //    yield return www;

    //    if (www.error != null) {
    //        Debug.LogError(www.error);
    //    }
    //    StartCoroutine(DownloadScores(highScoreName, "" + score));
    //}

    //// download score method, taken from the book at reference [7] of the dissertation, modified slightly to point to the right url etc.
    //IEnumerator DownloadScores(string name, string score) {

    //    WWW www = new WWW("users.sussex.ac.uk/~wcwc20/Project/DownloadScores.php");

    //    yield return www;

    //    if (www.error != null) {
    //        Debug.LogError(www.error);
    //    } else {
    //        int count = 0;

    //        PostStream poststream = new PostStream();

    //        poststream.BeginRead(www, true);
    //        poststream.ReadInt(ref count);

    //        if (count > 0)
    //            highScoreEntries = new string[count];
    //        for (int i = 0; i < count; i++) {
    //            string tname = "";
    //            string tscore = "";
    //            poststream.ReadString(ref tname);
    //            poststream.ReadString(ref tscore);

    //            highScoreEntries[i] = "Rank " + (i + 1) + " - " + tname + ": " + tscore;
    //        }
    //        bool ok = poststream.EndRead();
    //        if (!ok) {
    //            Debug.LogError("MD5 error");
    //        }
    //    }
    //}

    void OnDrawGizmos() {
        Gizmos.DrawIcon(this.transform.position, "Player.png");
    }

    void OnGUI() {
        if (baseHp <= 0) {
            // show GUI when the player has died
            GameEngine.ge.hpText = "";
            if (GameEngine.ge.towerLevel > 0) { // when the player is in high score mode

                GUIStyle textStyle = new GUIStyle();
                textStyle.alignment = TextAnchor.UpperCenter;
                textStyle.fontSize = 45;
                textStyle.normal.textColor = new Color(1, 1, 1);

                GUI.Label(new Rect(500, 20, 300, 30), "Score: " + score, textStyle);
                if (!submitted) {
                    textStyle.fontSize = 17;
                    GUI.Label(new Rect(350, 70, 150, 30), "Enter Your Name:", textStyle);
                    highScoreName = GUI.TextField(new Rect(500, 70, 300, 30), highScoreName);
                }
                textStyle.fontSize = 40;
                GUI.Label(new Rect(550, 100, 200, 50), "Ranking", textStyle);

                if (!submitted && GUI.Button(new Rect(800, 70, 80, 30), "Submit")) {
                    if (highScoreName.Trim().Length > 0) {
                        uploadScore(highScoreName, score);
                        highScoreName = "";
                        submitted = true;
                    }
                }

                if (submitted && GUI.Button(new Rect(800, 70, 80, 30), "Go Back")) {
                    baseHp = maxHp;
                    transformObject.position = new Vector3(8, 2, 7);
                    Vector3 camPosition = transformObject.position;
                    camPosition.y += camHeight;
                    camTransform.position = camPosition;
                    GameEngine.ge.changePanel(0);
                    Application.LoadLevel("Village");
                    submitted = false;
                    GameEngine.ge.towerLevel = 0;
                }

                scrollPane = GUI.BeginScrollView(new Rect(400, 140, 480, 220), scrollPane, new Rect(10, 10, 540, 1000));
                highScoreGrid.height = highScoreEntries.Length * 30;
                GUI.SelectionGrid(highScoreGrid, 0, highScoreEntries, 1);
                GUI.EndScrollView();

            } else if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 30, 200, 60), "Revive")) { // when not in high score mode
                gold /= 2;
                baseHp = getMaxHp() / 10;
                mp = maxMp / 10;
                transform.position = new Vector3(8, 2, 7);
                Vector3 camPosition = transformObject.position;
                camPosition.y += camHeight;
                camTransform.position = camPosition;
                GameEngine.ge.enemyLevels = 1;
                Application.LoadLevel("Village");
            }
        }
    }

    // when the player is on the moving platform, move the player accordingly
    void OnTriggerStay(Collider other) {
        if (other.tag.Equals("movingPlatform")) {
            print("player: " + transformObject.position.y + ", " + other.transform.position.y);
            if (transformObject.position.y > other.transform.position.y -.5) {
                MovingPlatform mp = other.transform.GetComponent<MovingPlatform>();
                charControl.Move(new Vector3(mp.velocity * Time.deltaTime, 0, 0));
                Vector3 camPosition = transformObject.position;
                camPosition.y += camHeight;
                camTransform.position = camPosition;
            }
        }
    }

}
