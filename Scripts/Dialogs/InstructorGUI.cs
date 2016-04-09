using UnityEngine;
using System.Collections;

public class InstructorGUI : DialogGUI {

    // Use this for initialization
    new void Start() {
        base.Start();
        name = "Class instructor";
        dialog = "";
        buttons = new string[] { "", "", "", "" };

        Quest curQuest = playerScript.activeQuests[0];
        if (curQuest == null) {
            defaultConvo();
        } else if (curQuest.name == "Getting stronger 1") {
            curQuest.reward();
            curQuest = playerScript.activeQuests[0];
            dialog = curQuest.description;
            buttons[0] = "";
            buttons[1] = "";
            buttons[2] = "";
            buttons[3] = "OK";

        } else if (curQuest.name == "Getting stronger 2" || curQuest.name == "Eliminating threats" || curQuest.name == "Instructor's final task") {
            curQuest.updateProgress();
            if (curQuest.progress < curQuest.goal) {
                dialog = curQuest.description;
                buttons[0] = "";
                buttons[1] = "";
                buttons[2] = "";
                buttons[3] = "OK";
            } else {
                dialog = curQuest.completeText;
                buttons[0] = "";
                buttons[1] = "";
                buttons[2] = "";
                buttons[3] = "OK";
                curQuest.reward();
            }
        } else {
            defaultConvo();
        }
    }

    void defaultConvo() {
        dialog = "Choosing a class can greatly increases your ability. Once you are at least level 5 you can talk to me to choose a class. After choosing a class you will need a class stone to change it, so choose wisely. Each elemental class has 2 skills. Press 1 and 2 to use them.";
        buttons = new string[] { "", "", "Never mind", "Next" };
    }

    //void convo1() {
    //    dialog = "";
    //    buttons[0] = "";
    //    buttons[1] = "";
    //    buttons[2] = "";
    //    buttons[3] = "OK";
    //}

    // Update is called once per frame
    void Update() {
        if (buttonPressed[0]) {
            if (buttons[0].Equals("Fire")) {
                if (playerScript.charLevel < 5) {
                    lowLevelError();
                } else if (playerScript.classType == 1) {
                    chosenClassError();
                } else if (playerScript.classType != 0) {
                    classNotNormalError();
                } else {
                    playerScript.setClass(1);
                    classChanged(1);
                }
            }
        }
        if (buttonPressed[1]) {
            if (buttons[1].Equals("Water")) {
                if (playerScript.charLevel < 5) {
                    lowLevelError();
                } else if (playerScript.classType == 2) {
                    chosenClassError();
                } else if (playerScript.classType != 0) {
                    classNotNormalError();
                } else {
                    playerScript.setClass(2);
                    classChanged(2);
                }
            }
        }
        if (buttonPressed[2]) {
            if (buttons[2].Equals("Never mind")) {
                closeDialog();
            } else if (buttons[2].Equals("Wind")) {
                if (playerScript.charLevel < 5) {
                    lowLevelError();
                } else if (playerScript.classType == 3) {
                    chosenClassError();
                } else if (playerScript.classType != 0) {
                    classNotNormalError();
                } else {
                    playerScript.setClass(3); // class 3 not implemented yet.
                    classChanged(3);
                }
            }
        }
        if (buttonPressed[3]) {
            if (buttons[3].Equals("Next")) {
                dialog = "Fire class focuses on attack and it's the fiercest of all.\nWater class focuses on HP and is usually the last to go down.\nWind Class is the swiftest and can avoid enemies before getting hit. Which class do you want to choose?";
                buttons[0] = "Fire";
                buttons[1] = "Water";
                buttons[2] = "Wind";
                buttons[3] = "Normal";
            } else if (buttons[3].Equals("OK")) {
                closeDialog();
            } else if (buttons[3].Equals("Normal")) {
                if (playerScript.classType == 0) {
                    chosenClassError();
                } else if (playerScript.bag.contains(new ClassStone())) {
                    playerScript.setClass(0);
                    classChanged(0);
                    playerScript.bag.removeItem(new ClassStone());
                } else {
                    dialog = "You need a Class Stone to change your class back to Normal. Class stones are not sold by the Merchant but are dropped by monsters at a very low rate, good luck!";
                    buttons = new string[] { "", "", "", "OK" };
                }
            }
        }
    }

    new void OnGUI() {
        base.OnGUI();
    }

    private void lowLevelError() {
        dialog = "Your level is too low. Go defeat some more enemies first.";
        buttons[0] = "";
        buttons[1] = "";
        buttons[2] = "";
        buttons[3] = "OK";
    }

    private void classNotNormalError() {
        dialog = "You can only choose one class at a time. Change back to normal class first.";
        buttons[0] = "";
        buttons[1] = "";
        buttons[2] = "";
        buttons[3] = "OK";
    }

    private void chosenClassError() {
        dialog = "You have already chosen this class.";
        buttons[0] = "";
        buttons[1] = "";
        buttons[2] = "";
        buttons[3] = "OK";
    }

    private void classChanged(int c) {
        if (c == 0) {
            dialog = "Congratulations! You are now a Normal class fighter. Normal class is what you have started with, and you have to change back to Normal class before switching to any other classes.";
        } else if (c == 1) {
            dialog = "Congratulations! You are now a Fire class fighter. Fire class deals extra damage to enemies.";
        } else if (c == 2) {
            dialog = "Congratulations! You are now a Water class fighter. Water class receives bonus HP.";
        } else if (c == 3) {
            dialog = "Congratulations! You are now a Wind class fighter. Wind class moves quicker than any other classes.";
        }

        buttons[0] = "";
        buttons[1] = "";
        buttons[2] = "";
        buttons[3] = "OK";
        playerScript.skillPoints = (playerScript.charLevel - 1) * 3;
    }

    private void closeDialog() {
        InstructorGUI i = GameEngine.ge.dialogPanel.GetComponent<InstructorGUI>();
        if (i != null) {
            Destroy(i);
        }
        GameEngine.ge.changePanel(0);
    }
}
