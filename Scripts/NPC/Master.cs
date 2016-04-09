using UnityEngine;
using System.Collections;

public class Master : NPC {

    public override void interact() {
        GameEngine.ge.changePanel(5);
        TrainingGUI m = GameEngine.ge.dialogPanel.GetComponent<TrainingGUI>();
        if (m == null) {
            GameEngine.ge.dialogPanel.AddComponent<TrainingGUI>();
        }
    }
}
