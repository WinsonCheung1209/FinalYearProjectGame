using UnityEngine;
using System.Collections;

public class ItemGUI : MonoBehaviour {
    Player playerScript;
    string itemName;
    string itemInfo;
    int itemQuantity;

    // Use this for initialization
    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() {
        int ithItemSelected = getSelectedItem();
        if (ithItemSelected != -1) {
            Item itemSelected = playerScript.bag.getItem(ithItemSelected);
            if (itemSelected != null) {
                itemName = itemSelected.name;
                itemInfo = itemSelected.description;
                itemQuantity = itemSelected.quantity;
            } else {
                itemName = "";
                itemInfo = "";
                itemQuantity = 0;
            }
        } else {
            itemName = "";
            itemInfo = "";
            itemQuantity = 0;
        }
        if (Input.GetMouseButtonDown(0)) {
            if (ithItemSelected != -1) {
                Item itemSelected = playerScript.bag.getItem(ithItemSelected);
                if (playerScript.getHp() > 0 && itemSelected != null && itemSelected.usable) {
                    if (playerScript.bag.getItem(ithItemSelected).use()) {
                        playerScript.bag.removeItem(ithItemSelected);
                    }
                }
            }
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonDown(1)) {
            if (ithItemSelected != -1) {
                Item itemSelected = playerScript.bag.getItem(ithItemSelected);
                if (playerScript.getHp() > 0 && itemSelected != null && itemSelected.value > 0) {
                    while (itemSelected.quantity > 0) {
                        playerScript.bag.removeItem(ithItemSelected);
                    }
                }
            }
        } else if (Input.GetMouseButtonDown(1)) {
            if (ithItemSelected != -1) {
                Item itemSelected = playerScript.bag.getItem(ithItemSelected);
                if (playerScript.getHp() > 0 && itemSelected != null && itemSelected.value > 0) {
                    playerScript.bag.removeItem(ithItemSelected);
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
        GUIStyle itemStyle = new GUIStyle();
        itemStyle.fontSize = 18;
        itemStyle.fontStyle = FontStyle.Bold;
        itemStyle.normal.textColor = new Color(0.9f, 0.9f, 0.9f);
        itemStyle.wordWrap = true;

        GUI.Label(new Rect(106, Screen.height - 203, 220, 50), itemName, itemStyle);
        GUI.Label(new Rect(36, Screen.height - 155, 220, 50), itemInfo, itemStyle);
        itemStyle.fontSize = 28;
        itemStyle.alignment = TextAnchor.UpperRight;
        GUI.Label(new Rect(106, Screen.height - 90, 150, 50), "" + itemQuantity, itemStyle);
        GUI.Label(new Rect(106, Screen.height - 60, 150, 50), "" + playerScript.gold, itemStyle);
    }
}
