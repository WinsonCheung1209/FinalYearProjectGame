using UnityEngine;
using System.Collections;

public class PixieGUI : DialogGUI {

    GameObject portal;

    // Use this for initialization
    new void Start() {
        base.Start();
        portal = GameObject.FindGameObjectWithTag("ToVillage");
        name = "Pixie";
        dialog = "";
        buttons = new string[] {"", "", "", ""};
        Quest curQuest = playerScript.activeQuests[0];
        if (curQuest == null) {
            dialog = "Hello adventurer! Shall I teach you the basics on how to survive in this world?";
            buttons = new string[] { "", "", "Yes please!", "I am fine" };
        } else if (curQuest.name == "Tutorial 1") {
            if (curQuest.progress < curQuest.goal) {
                string[] descs = curQuest.description.Split(new string[] { "\n" }, 2, System.StringSplitOptions.None);
                dialog = descs[0];
                buttons = new string[] { "", "", "", "Next" };
            } else {
                dialog = curQuest.completeText;
                buttons[0] = "";
                buttons[1] = "";
                buttons[2] = "";
                buttons[3] = "Thank you";
            }
        } else {
            DeliverMessage dm = new DeliverMessage();
            dialog = dm.description;
            buttons[0] = "";
            buttons[1] = "";
            buttons[2] = "";
            buttons[3] = "OK";
        }
    }

    // Update is called once per frame
    void Update() {
        Quest curQuest = playerScript.activeQuests[0];
        if (buttonPressed[2]) {
            if (buttons[2] == "Yes please!") {
                playerScript.activeQuests[0] = new Tut1();
                curQuest = playerScript.activeQuests[0];
                string[] descs = curQuest.description.Split(new string[] { "\n" }, 2, System.StringSplitOptions.None);
                dialog = descs[0];
                buttons = new string[] { "", "", "", "Next" };
            }
        }
        if (buttonPressed[3]) {
            if (buttons[3] == "I am fine") {
                DeliverMessage dm = new DeliverMessage();
                portal.transform.position = new Vector3(3, 0, 23);
                playerScript.activeQuests[0] = dm;
                dialog = dm.description;
                buttons[0] = "";
                buttons[1] = "";
                buttons[2] = "";
                buttons[3] = "OK";
                portal.SetActive(true);
            } else if (buttons[3] == "OK") {
                GameEngine.ge.changePanel(0);
                Destroy(this);
            } else if (buttons[3] == "Next") {
                string[] descs = curQuest.description.Split(new string[] { "\n" }, 2, System.StringSplitOptions.None);
                dialog = descs[1];
                buttons[0] = "";
                buttons[1] = "";
                buttons[2] = "";
                buttons[3] = "OK";
            } else if (buttons[3] == "Thank you") {
                curQuest.reward();
                portal.transform.position = new Vector3(3, 0, 23);
                GameEngine.ge.changePanel(0);
                Destroy(this);
            }
        }
    }

    new void OnGUI() {
        base.OnGUI();
    }

    //private void closeDialog() {
    //    PixieGUI m = GameEngine.ge.dialogPanel.GetComponent<PixieGUI>();
    //    if (m != null) {
    //        Destroy(m);
    //    }
        
    //}
}
