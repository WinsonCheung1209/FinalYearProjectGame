using UnityEngine;
using System.Collections;

public class DialogTest : DialogGUI {

    // Use this for initialization
    new void Start() {
        buttons = new string[] { "button A", "button B", "button C", "button D" };
    }

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < buttons.Length; i++) {
            if (buttonPressed[i])
                print("button " + i + " pressed");

        }
    }
}
