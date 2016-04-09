using UnityEngine;
using System.Collections;

public class SavePoint : NPC {

    public override void interact() {
        GameEngine.ge.changePanel(5);
        SaveGUI m = GameEngine.ge.dialogPanel.GetComponent<SaveGUI>();
        if (m == null) {
            GameEngine.ge.dialogPanel.AddComponent<SaveGUI>();
        }
    }
}
