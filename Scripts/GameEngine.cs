using UnityEngine;
using System.Collections;
//using UnityEditor;

public class GameEngine : MonoBehaviour {

    Player playerScript;
    public static GameEngine ge;
    public int showPanel = 0; //  -2 = help, -1 = menu, 0 = nothing, 1 = bag, 2 = character, 3 = questsList, 4 = quest, 5 = dialog, 6 = buy, 7 = sell, 8 = skill, 9 = bank
    public GameObject itemPanel;
    public GameObject characterPanel;
    public GameObject questListPanel;
    public GameObject questPanel;
    public GameObject dialogPanel;
    public GameObject tradePanel;
    public GameObject skillPanel;
    public GameObject depositPanel;
    public GameObject withdrawPanel;
    private ArrayList panels = new ArrayList();
    string displayInfo;
    float deleteTimer = 5;
    public string hpText;
    Hashtable itemList;
    Hashtable questList;
    public Bag bank;
    public int bankGold;
    public Bag merchant = new Bag();
    public float enemyHp;
    public float enemyMaxHp;
    public AudioSource audioSource;
    public AudioClip selectionClip;
    public int towerLevel = 0;
    public int monsterSpawn = 0;
    public int monsterLeft = 0;
    bool enteringCode;
    string code = "";
    string codeEntered = "";
    public int enemyLevels = 1;
    public GameObject help;

    // Use this for initialization
    public void Start() {
        ge = this;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (bank == null) {
            displayInfo = "";

            // make all the panels persistent so that it won't get destroyed when loading a new scene
            panels.Add(itemPanel);
            panels.Add(characterPanel);
            panels.Add(questListPanel);
            panels.Add(questPanel);
            panels.Add(tradePanel);
            panels.Add(dialogPanel);
            panels.Add(skillPanel);
            panels.Add(depositPanel);
            panels.Add(withdrawPanel);
            panels.Add(help);
            foreach (GameObject p in panels) {
                p.SetActive(false);
                DontDestroyOnLoad(p);
            }
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Panels"));

            // create a hashtable to store all the items in the game, this will be responsible for inserting the correct items in the player's bag when loading the game
            itemList = new Hashtable();
            itemList.Add("Health Potion", new HpPotion());
            itemList.Add("Mana Potion", new MpPotion());
            itemList.Add("Monguer's Axe", new MonguerAxe());
            itemList.Add("Mayor's Shield", new MayorShield());
            itemList.Add("Teleport Stone", new VillageStone());
            itemList.Add("Trident Stone", new TridentStone());
            itemList.Add("Level Stone", new LevelUpStone());
            itemList.Add("Class Stone", new ClassStone());

            // similar to the itemList hashtable, used to get the correct quest when loading the game
            questList = new Hashtable();
            questList.Add("Delivering message", new DeliverMessage());
            questList.Add("Eliminating threats", new ElimMonguer());
            questList.Add("Mayor's request 1", new IntroMerchant());
            questList.Add("Mayor's request 2", new IntroStorage());
            questList.Add("Getting stronger 1", new IntroInstructor());
            questList.Add("Getting stronger 2", new Stronger2());
            questList.Add("Instructor's final task", new BringAxes());

            // initialise the storage
            bank = new Bag();
            bank.isBank = true;
            bank.insertItem(new MayorShield(), false, false);
            bankGold = 0;

            //initialise the merchant
            merchant.insertItem(new HpPotion(), true, false);
            merchant.insertItem(new MpPotion(), true, false);
            merchant.insertItem(new VillageStone(), true, false);
            merchant.insertItem(new TridentStone(), true, false);
            merchant.insertItem(new LevelUpStone(), true, false);

            audioSource = this.GetComponent<AudioSource>();

            // used for debugging
            enteringCode = false;
        }
    }

    // Update is called once per frame
    void Update() {
        // setup the shortcut keys for panels
        if (Input.GetKeyDown(KeyCode.I)) { // item panel
            if (showPanel == 1) {
                changePanel(0);
            } else {
                changePanel(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.C)) { // character panel
            if (showPanel == 2) {
                changePanel(0);
            } else {
                changePanel(2);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q)) { // quest list panel
            if (showPanel == 3) {
                changePanel(0);
            } else {
                changePanel(3);
            }
        }
        if (Input.GetKeyDown(KeyCode.K)) { // skill panel
            if (showPanel == 8) {
                changePanel(0);
            } else {
                changePanel(8);
            }
        }
        // setup the escape menu
        if (showPanel == 0 && Input.GetKeyDown(KeyCode.Escape)) {
            changePanel(-1);
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            changePanel(0);
        }

        // entering cheat code for debugging
        if (!enteringCode) {
            if (codeEntered.Length > 0) {
                if (codeEntered.Equals("/clear")) {
                    setText("");
                } else {
                    string[] tokens = codeEntered.Split(new char[] { ' ' }, System.StringSplitOptions.None);
                    if (tokens.Length > 1) {
                        if (tokens[0].Equals("syscom")) {
                            if (tokens[1].Equals("fly")) {
                                playerScript.gravity = 0;
                                playerScript.flymode = true;
                                playerScript.moveSpeed += 10;
                                if (playerScript.moveSpeed > 25) {
                                    playerScript.moveSpeed = 25;
                                }
                            } else if (tokens[1].Equals("normal")) {
                                playerScript.gravity = 20;
                                playerScript.flymode = false;
                                if (playerScript.moveSpeed >= 20) {
                                    playerScript.moveSpeed -= 10;
                                }
                            } else if (tokens[1].Equals("add")) {
                                if (tokens[2].Equals("gold")) {
                                    playerScript.gold += int.Parse(tokens[3]);
                                }
                            }
                        }
                    }
                }
                codeEntered = "";
            }
        }
        // display the appropriate panel
        help.SetActive(showPanel == -2);
        itemPanel.SetActive(showPanel == 1);
        characterPanel.SetActive(showPanel == 2);
        questListPanel.SetActive(showPanel == 3);
        questPanel.SetActive(showPanel == 4);
        dialogPanel.SetActive(showPanel == 5);
        if (!dialogPanel.activeInHierarchy) {
            Destroy(dialogPanel.GetComponent<DialogGUI>());
        }
        tradePanel.SetActive(showPanel == 6 || showPanel == 7);
        skillPanel.SetActive(showPanel == 8);
        depositPanel.SetActive(showPanel == 9);
        withdrawPanel.SetActive(showPanel == 9);

        // display the item's texture if the relevant panel is being displayed
        for (int i = 0; i < playerScript.bag.size; i++) { // item panel, selling panel or storage panel is being displayed
            Item item = playerScript.bag.getItem(i);
            item.texture.SetActive(showPanel == 1 || showPanel == 7 || showPanel == 9);
        }
        for (int i = 0; i < GameEngine.ge.merchant.size; i++) { // buying panel is displayed
            Item item = GameEngine.ge.merchant.getItem(i);
            item.texture.SetActive(showPanel == 6);
        }
        for (int i = 0; i < bank.size; i++) { // storage panel is displayed
            Item item = bank.getItem(i);
            item.texture.SetActive(showPanel == 9);
        }

        // lock and hide the cursor if no panels is being displayed
        if (showPanel != 0 || playerScript.getHp() <= 0 || enteringCode) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        // if there are some texts in the console, gradually delete them
        if (displayInfo.Length > 0) {
            deleteText();
        }
    }

    // graduately delete text in console
    private void deleteText() {
        deleteTimer -= Time.deltaTime;
        if (deleteTimer <= 0) {
            string[] tokens = displayInfo.Split(new string[] { "\n" }, 2, System.StringSplitOptions.None);
            string[] displayTextTokens = displayInfo.Split(new string[] { "\n" }, System.StringSplitOptions.None);
            if (displayTextTokens.Length > 8) {
                deleteTimer = 2;
            } else {
                deleteTimer = 5;
            }
            displayInfo = tokens[1];
        }
    }

    // change the panel being displayed
    public void changePanel(int i) {
        if (i != showPanel) {
            audioSource.PlayOneShot(selectionClip, 0.5f);
        }
        showPanel = i;
    }

    void OnGUI() {
        displayBars(); // display the HP, MP, and experience bar at the top left

        // setup the escape menu
        if (showPanel == -1) {
            GUI.Label(new Rect(35, Screen.height - 415, 230, 50), "M E N U");
            if (GUI.Button(new Rect(35, Screen.height - 365, 230, 30), "(C)haracter stats")) {
                changePanel(2);
            }
            if (GUI.Button(new Rect(35, Screen.height - 335, 230, 30), "(I)nventory")) {
                changePanel(1);
            }
            if (GUI.Button(new Rect(35, Screen.height - 305, 230, 30), "(Q)uests")) {
                changePanel(3);
            }
            if (GUI.Button(new Rect(35, Screen.height - 275, 230, 30), "S(K)ills")) {
                changePanel(8);
            }
            if (GUI.Button(new Rect(35, Screen.height - 245, 230, 30), "Help")) {
                changePanel(-2);
            }
            if (GUI.Button(new Rect(35, Screen.height - 215, 230, 30), "Credits")) {
                setText(" Game created by Winson Cheung\n 3D models and sound by various people from the internet.\n Please take a minute to fill out a survey after playing this game:\n  www.surveymonkey.com/s/6G8DR69");
            }
            if (GUI.Button(new Rect(10, Screen.height - 185, 280, 30), "Quit, any unsaved progress will be lost.")) {
                Application.Quit();
            }
        }

        // setup the console area
        Rect rect = new Rect(Screen.width / 2 - 100, Screen.height - 200, 250, 190);
        GUI.Box(rect, "");
        GUI.Label(rect, displayInfo);

        // setup for entering cheat codes/debugging
        Event e = Event.current;
        if (e.type == EventType.keyDown && e.keyCode == KeyCode.Return) {
            codeEntered = code;
            code = "";
            enteringCode = !enteringCode;
        }
        if (enteringCode) {
            GUI.SetNextControlName("cheat");
            code = GUI.TextField(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 15, 500, 30), code);
            GUI.FocusControl("cheat");
        }
        // setup the monster's HP bar
        if (hpText.Length > 0) {
            displayEnemyBars();
        }

        // add a close button when displaying panels
        if (showPanel != 0 && showPanel != -1 && showPanel != -2) {
            if (GUI.Button(new Rect(240, Screen.height - 440, 30, 30), "X")) {
                changePanel(0);
            }
        } else if (showPanel == -2) {
            if (GUI.Button(new Rect(340, Screen.height - 510, 40, 40), "X")) {
                changePanel(0);
            }
        }
    }

    // setup the monster's HP bar
    private void displayEnemyBars() {
        GUIStyle whiteStyle = new GUIStyle();
        Texture2D whiteTexture = new Texture2D(1, 1);
        whiteTexture.SetPixel(1, 1, new Color(255, 255, 255, 0.9f));
        whiteTexture.Apply();
        whiteStyle.normal.background = whiteTexture;
        GUIStyle transparentStyle = new GUIStyle();
        Texture2D transTexture = new Texture2D(1, 1);
        transTexture.SetPixel(1, 1, new Color(0, 0, 0, 0));
        transTexture.Apply();
        transparentStyle.normal.background = transTexture;
        transparentStyle.alignment = TextAnchor.UpperCenter;
        transparentStyle.normal.textColor = Color.green;
        transparentStyle.fontStyle = FontStyle.Bold;
        transparentStyle.fontSize = 32;

        GUI.backgroundColor = new Color(1, 0, 0, 0.2f);
        GUI.Box(new Rect(Screen.width / 2 - 200, 0, 400, 40), "", whiteStyle);
        GUI.backgroundColor = new Color(1, 0, 0);
        GUI.Box(new Rect(Screen.width / 2 - 202, 2, enemyHp / enemyMaxHp * 396, 36), "", whiteStyle);
        GUI.Box(new Rect(Screen.width / 2 - 200, 0, 400, 40), hpText, transparentStyle);
    }

    // setup the player's HP, MP and experience bar.
    private void displayBars() {
        GUIStyle whiteStyle = new GUIStyle();
        Texture2D whiteTexture = new Texture2D(1, 1);
        whiteTexture.SetPixel(1, 1, new Color(255, 255, 255, 0.9f));
        whiteTexture.Apply();
        whiteStyle.normal.background = whiteTexture;
        GUIStyle transparentStyle = new GUIStyle();
        Texture2D transTexture = new Texture2D(1, 1);
        transTexture.SetPixel(1, 1, new Color(0, 0, 0, 0));
        transTexture.Apply();
        transparentStyle.normal.background = transTexture;
        transparentStyle.alignment = TextAnchor.MiddleCenter;
        transparentStyle.normal.textColor = Color.green;
        transparentStyle.fontStyle = FontStyle.Bold;
        transparentStyle.fontSize = 18;
        GUIStyle greenStyle = new GUIStyle();
        greenStyle.normal.textColor = new Color(0, 1, 0.47f);
        greenStyle.fontStyle = FontStyle.Bold;
        greenStyle.fontSize = 21;

        GUI.Label(new Rect(10, 5, 150, 25), "Level: " + playerScript.charLevel, greenStyle);

        GUI.backgroundColor = new Color(1, 0, 0, 0.2f);
        GUI.Box(new Rect(8, 33, 254, 24), "", whiteStyle);
        GUI.backgroundColor = Color.red;
        GUI.Box(new Rect(10, 35, playerScript.getHp() / playerScript.getMaxHp() * 250, 20), "", whiteStyle);
        GUI.Box(new Rect(8, 33, 254, 24), "Health: " + Mathf.Round(playerScript.getHp()) + "/" + Mathf.Round(playerScript.getMaxHp()), transparentStyle);

        GUI.backgroundColor = Color.grey;
        GUI.Box(new Rect(8, 58, 254, 24), "", whiteStyle);
        GUI.backgroundColor = Color.blue;
        GUI.Box(new Rect(10, 60, playerScript.getMp() / playerScript.getMaxMp() * 250, 20), "", whiteStyle);
        GUI.Box(new Rect(8, 58, 254, 24), "MP: " + Mathf.Round(playerScript.getMp()) + "/" + Mathf.Round(playerScript.getMaxMp()), transparentStyle);

        GUI.backgroundColor = Color.yellow;
        GUI.Box(new Rect(8, 83, 254, 14), "", whiteStyle);
        GUI.backgroundColor = Color.white;
        GUI.Box(new Rect(10, 85, playerScript.getExp() / playerScript.getMaxExp() * 250, 10), "", whiteStyle);
        GUI.Box(new Rect(8, 83, 254, 14), "Exp: " + Mathf.Floor(playerScript.getExp()) + "/" + Mathf.Round(playerScript.getMaxExp()), transparentStyle);
    }

    // save the game
    public void saveGame() {
        PlayerPrefs.SetInt("SaveFile", 1);
        PlayerPrefs.SetFloat("pMoveSpeed", playerScript.moveSpeed);
        PlayerPrefs.SetFloat("pMaxHp", playerScript.maxHp);
        PlayerPrefs.SetFloat("pHp", playerScript.baseHp);
        PlayerPrefs.SetFloat("pMaxMp", playerScript.maxMp);
        PlayerPrefs.SetFloat("pMp", playerScript.mp);
        PlayerPrefs.SetFloat("pAttack", playerScript.baseAttack);
        PlayerPrefs.SetFloat("pRecoverHp", playerScript.recoverHp);
        PlayerPrefs.SetFloat("pRecoverMp", playerScript.recoverMp);
        PlayerPrefs.SetFloat("pExp", playerScript.exp);
        PlayerPrefs.SetFloat("pMaxExp", playerScript.maxExp);
        PlayerPrefs.SetInt("pGold", playerScript.gold);
        PlayerPrefs.SetInt("pCharLevel", playerScript.charLevel);
        PlayerPrefs.SetInt("pAttributePoints", playerScript.attributePoints);
        PlayerPrefs.SetInt("pLuck", playerScript.luck);

        PlayerPrefs.SetInt("pClass", playerScript.classType);
        PlayerPrefs.SetInt("pSkillPoints", playerScript.skillPoints);
        PlayerPrefs.SetFloat("pAttMultiplier", playerScript.attMultiplier);
        PlayerPrefs.SetFloat("pHpMultiplier", playerScript.hpMultiplier);
        PlayerPrefs.SetFloat("pAgiMultiplier", playerScript.agiMultiplier);
        PlayerPrefs.SetInt("pScore", playerScript.score);
        PlayerPrefs.SetInt("pClass", playerScript.classType);
        if (playerScript.classType != 0) {
            PlayerPrefs.SetInt("pSkill1Level", playerScript.gameObject.GetComponent<ClassType>().sk1.level);
            PlayerPrefs.SetInt("pSkill2Level", playerScript.gameObject.GetComponent<ClassType>().sk2.level);
        }

        PlayerPrefs.SetInt("pBSize", playerScript.bag.size);
        for (int i = 0; i < playerScript.bag.size; i++) {
            PlayerPrefs.SetString("pBName" + i, playerScript.bag.getItem(i).name);
            PlayerPrefs.SetInt("pBQuant" + i, playerScript.bag.getItem(i).quantity);
        }
        PlayerPrefs.SetInt("pBankSize", bank.size);
        for (int i = 0; i < bank.size; i++) {
            PlayerPrefs.SetString("pBankName" + i, bank.getItem(i).name);
            PlayerPrefs.SetInt("pBankQuant" + i, bank.getItem(i).quantity);
        }
        PlayerPrefs.SetInt("pBankGold", bankGold);

        int numOfQuests = 0;
        for (int i = 0; i < playerScript.activeQuests.Length; i++) {
            Quest q = playerScript.activeQuests[i];
            if (q != null) {
                PlayerPrefs.SetString("pQName" + i, q.name);
                PlayerPrefs.SetInt("pQProgress" + i, q.progress);
                numOfQuests++;
            }
        }
        PlayerPrefs.SetInt("pQSize", numOfQuests);
        setText(getText() + "Game Saved Successfully!\n");
    }

    // load the game
    public void loadGame() {
        playerScript.moveSpeed = PlayerPrefs.GetFloat("pMoveSpeed");
        playerScript.maxHp = PlayerPrefs.GetFloat("pMaxHp");
        playerScript.baseHp = PlayerPrefs.GetFloat("pHp");
        playerScript.maxMp = PlayerPrefs.GetFloat("pMaxMp");
        playerScript.mp = PlayerPrefs.GetFloat("pMp");
        playerScript.baseAttack = PlayerPrefs.GetFloat("pAttack");
        playerScript.recoverHp = PlayerPrefs.GetFloat("pRecoverHp");
        playerScript.recoverMp = PlayerPrefs.GetFloat("pRecoverMp");

        playerScript.exp = PlayerPrefs.GetFloat("pExp");
        playerScript.maxExp = PlayerPrefs.GetFloat("pMaxExp");
        playerScript.gold = PlayerPrefs.GetInt("pGold");
        playerScript.charLevel = PlayerPrefs.GetInt("pCharLevel");
        playerScript.attributePoints = PlayerPrefs.GetInt("pAttributePoints");
        playerScript.luck = PlayerPrefs.GetInt("pLuck");

        playerScript.classType = PlayerPrefs.GetInt("pClass", playerScript.classType);
        playerScript.skillPoints = PlayerPrefs.GetInt("pSkillPoints", playerScript.skillPoints);
        playerScript.attMultiplier = PlayerPrefs.GetFloat("pAttMultiplier", playerScript.attMultiplier);
        playerScript.hpMultiplier = PlayerPrefs.GetFloat("pHpMultiplier", playerScript.hpMultiplier);
        playerScript.agiMultiplier = PlayerPrefs.GetFloat("pAgiMultiplier", playerScript.agiMultiplier);
        playerScript.score = PlayerPrefs.GetInt("pScore", playerScript.score);
        playerScript.classType = PlayerPrefs.GetInt("pClass", playerScript.classType);

        playerScript.setClass(playerScript.classType);
        if (playerScript.classType != 0) {
            ClassType currentClass = playerScript.gameObject.GetComponent<ClassType>();
            currentClass.Start();
            currentClass.sk1.level = PlayerPrefs.GetInt("pSkill1Level");
            currentClass.sk1.updateStat();
            currentClass.sk2.level = PlayerPrefs.GetInt("pSkill2Level");
            currentClass.sk2.updateStat();
        }
        int bagSize = playerScript.bag.size;
        for (int i = 0; i < bagSize; i++) {
            playerScript.bag.getItem(0).quantity = 1;
            playerScript.bag.removeItem(0);
        }
        bagSize = PlayerPrefs.GetInt("pBSize");
        for (int i = 0; i < bagSize; i++) {
            string itemName = PlayerPrefs.GetString("pBName" + i);
            Item itemI = (Item) itemList[itemName];
            playerScript.bag.insertItem(itemI, true, false);
            playerScript.bag.getItem(i).quantity = PlayerPrefs.GetInt("pBQuant" + i);
        }

        int bankSize = bank.size;
        for (int i = 0; i < bankSize; i++) {
            bank.getItem(0).quantity = 1;
            bank.removeItem(0);
        }

        bankSize = PlayerPrefs.GetInt("pBankSize");
        for (int i = 0; i < bankSize; i++) {
            string itemName = PlayerPrefs.GetString("pBankName" + i);
            Item itemI = (Item) itemList[itemName];
            bank.insertItem(itemI, true, false);
            bank.getItem(i).quantity = PlayerPrefs.GetInt("pBankQuant" + i);
        }
        bankGold = PlayerPrefs.GetInt("pBankGold");

        for (int i = 0; i < playerScript.activeQuests.Length; i++) {
            playerScript.activeQuests[i] = null;
        }

        int questSize = PlayerPrefs.GetInt("pQSize");
        for (int i = 0; i < questSize; i++) {
            string questName = PlayerPrefs.GetString("pQName" + i);
            Quest q = (Quest) questList[questName];
            playerScript.activeQuests[i] = q;
            q.progress = PlayerPrefs.GetInt("pQProgress" + i);
        }
        setText("Game Loaded Successfully!\n");
    }

    // set the console's text
    public void setText(string x) {
        displayInfo = x;
    }

    // get the console's text
    public string getText() {
        return displayInfo;
    }
}

