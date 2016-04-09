using UnityEngine;
using System.Collections;

public class MayorGUI : DialogGUI {

    // Use this for initialization
    new void Start() {
        base.Start();
        name = "Mayor";
        dialog = "";
        buttons = new string[] { "", "", "", "" };
        Quest curQuest = playerScript.activeQuests[0];
        if (curQuest == null) {
            defaultConvo();
        } else if (curQuest.name == "Delivering message") {
            curQuest.reward();
            reqConvo();
            curQuest = playerScript.activeQuests[0];
            Item item = playerScript.bag.getItem(new HpPotion());
            if (item != null) {
                if (item.quantity > curQuest.goal) {
                    curQuest.progress = curQuest.goal;
                } else {
                    curQuest.progress = item.quantity;
                }
            }
        } else if (curQuest.name == "Mayor's request 1") {
            if (curQuest.progress < curQuest.goal) {
                reqConvo();
            } else {
                dialog = curQuest.completeText;
                buttons[0] = "";
                buttons[1] = "";
                buttons[2] = "";
                buttons[3] = "Next";
                curQuest.reward();
                curQuest = playerScript.activeQuests[0];
                curQuest.updateProgress();
                for (int i = 0; i < 2; i++) {
                    playerScript.bag.removeItem(new HpPotion());
                }
            }
        } else if (curQuest.name == "Mayor's request 2") {
            if (curQuest.progress < curQuest.goal) {
                req2Convo();
            } else {
                dialog = curQuest.completeText;
                buttons[0] = "";
                buttons[1] = "";
                buttons[2] = "";
                buttons[3] = "OK";
                curQuest.reward();
                playerScript.bag.removeItem(new MayorShield());
            }
        } else if (curQuest.name == "Getting stronger 1") {
            dialog = curQuest.description;
            buttons[0] = "";
            buttons[1] = "";
            buttons[2] = "";
            buttons[3] = "OK";
        } else {
            defaultConvo();
        }
    }

    void defaultConvo() {
        dialog = "Hi, I am the mayor of this village. If you need any help, press Escape then select 'help' from the menu.";
        buttons[0] = "";
        buttons[1] = "";
        buttons[2] = "";
        buttons[3] = "OK";
    }

    void reqConvo() {
        dialog = "Can anyone help me?";
        buttons[0] = "What's wrong?";
        buttons[1] = "";
        buttons[2] = "";
        buttons[3] = "";
    }

    void req2Convo() {
        IntroStorage iS = new IntroStorage();
        dialog = iS.description;
        buttons[0] = "";
        buttons[1] = "";
        buttons[2] = "";
        buttons[3] = "OK";
    }


    // Update is called once per frame
    void Update() {
        Quest curQuest = playerScript.activeQuests[0];
        if (buttonPressed[0]) {
            if (buttons[0] == "What's wrong?") {
                //curQuest = playerScript.activeQuests[0];
                //string[] descs = curQuest.description.Split(new string[] { "\n" }, 2, System.StringSplitOptions.None);
                dialog = curQuest.description;
                buttons = new string[] { "", "", "", "Sure" };
            }
        }
        if (buttonPressed[3]) {
            if (buttons[3] == "OK" || buttons[3] == "Sure") {
                GameEngine.ge.changePanel(0);
                Destroy(this);
            } else if (buttons[3] == "Next") {
                curQuest = playerScript.activeQuests[0];
                dialog = curQuest.description;
                buttons[0] = "";
                buttons[1] = "";
                buttons[2] = "";
                buttons[3] = "OK";
            }
        }
    }

    new void OnGUI() {
        base.OnGUI();
    }
}
