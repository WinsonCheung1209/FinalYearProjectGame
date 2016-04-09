using UnityEngine;
using System.Collections;

public class SaveGUI : DialogGUI {

    // Use this for initialization
    new void Start() {
        name = "Save";
        dialog = "Do you want to save your progress?";
        buttons = new string[] { "Yes", "No", "", "" };
    }

    // Update is called once per frame
    void Update() {
        if (buttonPressed[0]) {
            GameEngine.ge.saveGame();
            GameEngine.ge.changePanel(0);
            Destroy(this);
        } else if (buttonPressed[1]) {
            GameEngine.ge.changePanel(0);
            Destroy(this);
        }
    }
}
