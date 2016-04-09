using UnityEngine;
using System.Collections;

public class DepositGUI : MonoBehaviour {

    Player playerScript;
    GUIStyle depositStyle = new GUIStyle();
    string itemName, item2Name;
    int itemQuantity, item2Quantity;
    string goldToDep;

    // Use this for initialization
    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        goldToDep = "";
    }

    void Update() {
        //if (Input.GetMouseButton(0)) {
        //    MonoBehaviour.print(Input.mousePosition);
        //}
        int ithItemSelected = getSelectedItem();
        if (ithItemSelected != -1) {
            Item itemSelected = playerScript.bag.getItem(ithItemSelected);
            if (itemSelected != null) {

                int BankIndex = GameEngine.ge.bank.indexOf(itemSelected);
                if (BankIndex != -1) {
                    item2Name = itemSelected.name;
                    item2Quantity = GameEngine.ge.bank.getItem(BankIndex).quantity;
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
                Item itemSelected = playerScript.bag.getItem(ithItemSelected);
                if (playerScript.getHp() > 0 && itemSelected != null) {
                    GameEngine.ge.bank.insertItem(itemSelected, false, false);
                    playerScript.bag.removeItem(itemSelected);
                }
            }
        }
    }

    private int getSelectedItem() {
        Vector2 mousePos = Input.mousePosition;
        int ithItemSelected = -1;
        if (mousePos.x > 30 && mousePos.x < 270 && mousePos.y > 208 && mousePos.y < 400) {
            int column = (int) ((mousePos.x - 30) / 48);
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

            GUI.Label(new Rect(106, Screen.height - 203, 220, 50), itemName, depositStyle);
            GUI.Label(new Rect(106 + 245, Screen.height - 203, 220, 50), item2Name, depositStyle);
            depositStyle.alignment = TextAnchor.UpperRight;
            if (itemQuantity > 0) {
                GUI.Label(new Rect(36, Screen.height - 175, 220, 50), "" + itemQuantity, depositStyle);
            }
            if (item2Quantity > 0) {
                GUI.Label(new Rect(36 + 245, Screen.height - 175, 220, 50), "" + item2Quantity, depositStyle);
            }
            GUI.Label(new Rect(36, Screen.height - 145, 220, 50), "" + playerScript.gold, depositStyle);
            goldToDep = GUI.TextField(new Rect(121, Screen.height - 112, 70, 25), goldToDep);
            if (GUI.Button(new Rect(200, Screen.height - 112, 60, 25), "OK")) {
                try {
                    int gold = int.Parse(goldToDep);
                    if (gold > 0 && playerScript.gold >= gold) {
                        playerScript.gainGold(-gold, false);
                        gold = (int) (gold * 0.95f);
                        GameEngine.ge.bankGold += gold;
                    }
                    goldToDep = "";
                } catch (System.Exception) {
                    goldToDep = "";
                }
            }
    }
}
