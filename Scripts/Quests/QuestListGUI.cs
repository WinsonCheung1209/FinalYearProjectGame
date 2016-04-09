using UnityEngine;
using System.Collections;

public class QuestListGUI : MonoBehaviour {

    Player playerScript;
    public GameObject questPanel;
    QuestGUI questPanelScript;

    // Use this for initialization
    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        questPanelScript = questPanel.GetComponent<QuestGUI>();
    }

    // Update is called once per frame
    void Update() {

    }

    void OnGUI() {
        Rect mainTextRect = new Rect(35, Screen.height - 370, 230, 40);
        if (playerScript.activeQuests[0] != null) {
            if (GUI.Button(mainTextRect, playerScript.activeQuests[0].name)) {
                questPanel.SetActive(true);
                questPanelScript.questNo = 0;
                GameEngine.ge.changePanel(4);
            }
        }
        for (int i = 1; i < playerScript.activeQuests.Length; i++) {
            if (playerScript.activeQuests[i] != null) {
                Rect sideTextRect = new Rect(35, Screen.height - 280 + 50 * (i - 1), 230, 40);
                if (GUI.Button(sideTextRect, playerScript.activeQuests[i].name)) {
                    questPanelScript.questNo = i;
                    GameEngine.ge.changePanel(4);
                }
            }
        }
    }
}
