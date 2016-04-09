using UnityEngine;
using System.Collections;

public class WithdrawGUI : MonoBehaviour {

    Player playerScript;
    GUIStyle depositStyle = new GUIStyle();
    string itemName, item2Name;
    int itemQuantity, item2Quantity;
    string goldToWD;
    int xOffset = 245;

    // Use this for initialization
    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        goldToWD = "";
    }

    void Update() {
        int ithItemSelected = getSelectedItem();
        if (ithItemSelected != -1) {
            Item itemSelected = GameEngine.ge.bank.getItem(ithItemSelected);
            if (itemSelected != null) {

                int playerBagIndex = playerScript.bag.indexOf(itemSelected);
                if (playerBagIndex != -1) {
                    item2Name = itemSelected.name;
                    item2Quantity = playerScript.bag.getItem(playerBagIndex).quantity;
                } else {
                    item2Name = "";
                    item2Quantity = 0;
                }

                itemName = itemSelected.name;
                itemQuantity = itemSelected.quantity;
            } else {
                itemName = "";
                itemQuantity = 0;
                item2Name = "";
                item2Quantity = 0;
            }
        } else {
            itemName = "";
            itemQuantity = 0;
            item2Name = "";
            item2Quantity = 0;
        }
        if (Input.GetMouseButtonDown(0)) {
            if (ithItemSelected != -1) {
                Item itemSelected = GameEngine.ge.bank.getItem(ithItemSelected);
                if (playerScript.getHp() > 0 && itemSelected != null) {
                    GameEngine.ge.bank.removeItem(itemSelected);
                    playerScript.bag.insertItem(itemSelected, false, true);
                }
            }
        }
    }

    private int getSelectedItem() {
        Vector2 mousePos = Input.mousePosition;
        int ithItemSelected = -1;
        if (mousePos.x > 30 + xOffset && mousePos.x < 270 + xOffset && mousePos.y > 208 && mousePos.y < 400) {
            int column = (int) ((mousePos.x - (30 + xOffset)) / 48);
            int row = (int) (4 - (mousePos.y - 208) / 48);
            ithItemSelected = row * 5 + column;
        }
        return ithItemSelected;
    }

    void OnGUI() {
        depositStyle.alignment = TextAnchor.UpperLeft;
        depositStyle.fontSize = 22;
        depositStyle.fontStyle = FontStyle.Bold;
        depositStyle.normal.textColor = new Color(0.9f, 0.9f, 0.9f);
        depositStyle.wordWrap = true;

        GUI.Label(new Rect(106 + xOffset, Screen.height - 203, 220, 50), itemName, depositStyle);
        GUI.Label(new Rect(106, Screen.height - 203, 220, 50), item2Name, depositStyle);
        depositStyle.alignment = TextAnchor.UpperRight;
        if (itemQuantity > 0) {
            GUI.Label(new Rect(36 + xOffset, Screen.height - 175, 220, 50), "" + itemQuantity, depositStyle);
        }
        if (item2Quantity > 0) {
            GUI.Label(new Rect(36, Screen.height - 175, 220, 50), "" + item2Quantity, depositStyle);
        }
        GUI.Label(new Rect(36 + xOffset, Screen.height - 145, 220, 50), "" + GameEngine.ge.bankGold, depositStyle);
        goldToWD = GUI.TextField(new Rect(121 + xOffset + 20, Screen.height - 112, 70, 25), goldToWD);
        if (GUI.Button(new Rect(200 + xOffset + 20, Screen.height - 112, 40, 25), "OK")) {
            try {
                int gold = int.Parse(goldToWD);
                if (gold > 0 && GameEngine.ge.bankGold >= gold) {
                    GameEngine.ge.bankGold -= gold;
                    playerScript.gainGold(gold, true);
                }
                goldToWD = "";
            } catch (System.Exception) {
                goldToWD = "";
            }
        }
    }
}
