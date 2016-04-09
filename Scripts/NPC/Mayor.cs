using UnityEngine;
using System.Collections;

public class Mayor : NPC {

    public override void interact() {
        GameEngine.ge.changePanel(5);
        MayorGUI m = GameEngine.ge.dialogPanel.GetComponent<MayorGUI>();
        if (m == null) {
            GameEngine.ge.dialogPanel.AddComponent<MayorGUI>();
        }
    }
}
