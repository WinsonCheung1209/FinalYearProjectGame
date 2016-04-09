using UnityEngine;
using System.Collections;

public class QuestGUI : MonoBehaviour {

    public int questNo = -1;
    Player playerScript;
    Quest q;

	// Use this for initialization
	void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI() {
        if (questNo != -1) {

            GUIStyle questStyle = new GUIStyle();
            questStyle.fontSize = 17;
            questStyle.fontStyle = FontStyle.Bold;
            questStyle.normal.textColor = new Color(0.85f, 0.85f, 0.85f);
            questStyle.wordWrap = true;
            q = playerScript.activeQuests[questNo];

            GUI.Box(new Rect(35, Screen.height - 365, 230, 110), q.task, questStyle);
            GUI.Box(new Rect(35, Screen.height - 255, 230, 30), "Progress: " + q.progress + "/" + q.goal, questStyle);
            GUI.Box(new Rect(35, Screen.height - 195, 230, 50), "Exp: " + q.rewardExp + "\nGold: " + q.rewardGold, questStyle);
            string itemRewards = "";
            if (q.rewardItemList != null) {
                foreach (Item i in q.rewardItemList) {
                    itemRewards += i.name + " x" + i.quantity + "\n";
                }
            }
            GUI.Box(new Rect(35, Screen.height - 155, 230, 100), itemRewards, questStyle);
            questStyle.alignment = TextAnchor.UpperCenter;
            

            GUI.Box(new Rect(35, Screen.height - 395, 230, 30), q.name, questStyle);
            if (GUI.Button(new Rect(40, Screen.height - 80, 80, 50), "OK")) {
                GameEngine.ge.changePanel(0);
            }
            if (questNo != 0 && GUI.Button(new Rect(130, Screen.height - 80, 130, 50), "Give Up")) {
                playerScript.activeQuests[questNo].progress = 0;
                playerScript.activeQuests[questNo] = null;
                GameEngine.ge.changePanel(0);
            }
        }
    }

}
