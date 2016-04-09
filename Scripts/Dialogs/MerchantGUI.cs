using UnityEngine;
using System.Collections;

public class MerchantGUI : DialogGUI {

    // Use this for initialization
    new void Start() {
        base.Start();
        name = "Merchant";
        dialog = "Hi Adventurer! I buy and sell items with people like you.";
        buttons = new string[] {"Buy", "Sell", "No thanks"};
    }

    // Update is called once per frame
    void Update() {
        if (buttonPressed[0]) {
            GameEngine.ge.changePanel(6);
            GameEngine.ge.merchant.sortItemTextures();
            Destroy(this);
        }
        if (buttonPressed[1]) {
            GameEngine.ge.changePanel(7);
            playerScript.bag.sortItemTextures();
            Destroy(this);
        }
        if (buttonPressed[2]) {
            GameEngine.ge.changePanel(0);
            Destroy(this);
        }
    }

    new void OnGUI() {
        base.OnGUI();
    }

}
