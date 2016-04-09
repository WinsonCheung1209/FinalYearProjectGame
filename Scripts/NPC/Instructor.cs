using UnityEngine;
using System.Collections;

public class Instructor : NPC {

    public override void interact() {
        GameEngine.ge.changePanel(5);
        InstructorGUI m = GameEngine.ge.dialogPanel.GetComponent<InstructorGUI>();
        if (m == null) {
            GameEngine.ge.dialogPanel.AddComponent<InstructorGUI>();
        }
    }
}
