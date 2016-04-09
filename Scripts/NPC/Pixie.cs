using UnityEngine;
using System.Collections;

public class Pixie : NPC {

    public override void interact() {
        GameEngine.ge.changePanel(5);
        PixieGUI m = GameEngine.ge.dialogPanel.GetComponent<PixieGUI>();
        if (m == null) {
            GameEngine.ge.dialogPanel.AddComponent<PixieGUI>();
        }
    }
}
