using UnityEngine;
using System.Collections;

public class Merchant : NPC {

    public override void interact() {
        GameEngine.ge.changePanel(5);
        MerchantGUI m = GameEngine.ge.dialogPanel.GetComponent<MerchantGUI>();
        if (m == null) {
            GameEngine.ge.dialogPanel.AddComponent<MerchantGUI>();
        }
    }
}
