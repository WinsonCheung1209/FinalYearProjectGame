using UnityEngine;
using System.Collections;

public class TradeGUI : MonoBehaviour {

    Player playerScript;
    string itemName;
    string itemInfo;
    int itemQuantity;
    int itemValue;
    int gold;
    bool isBuying;

	// Use this for initialization
	void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameEngine.ge.showPanel == 6) {
            isBuying = true;
        } else if (GameEngine.ge.showPanel == 7) {
            isBuying = false;
        }
        int ithItemSelected = getSelectedItem();
        if (ithItemSelected != -1) {
            Item itemSelected;
            if (isBuying) {
                itemSelected = GameEngine.ge.merchant.getItem(ithItemSelected);
            } else {
                itemSelected = playerScript.bag.getItem(ithItemSelected);
            }
            if (itemSelected != null) {
                itemName = itemSelected.name;
                itemInfo = itemSelected.description;
                int itemIndex = playerScript.bag.indexOf(itemSelected);
                if (itemIndex == -1) {
                    itemQuantity = 0;
                } else {
                    itemQuantity = playerScript.bag.getItem(itemIndex).quantity;
                }
                if (isBuying) {
                    itemValue = itemSelected.value;
                } else {
                    itemValue = itemSelected.value / 3;
                }
            } else {
                itemName = "";
                itemInfo = "";
                itemQuantity = 0;
                itemValue = 0;
            }
        } else {
            itemName = "";
            itemInfo = "";
            itemQuantity = 0;
            itemValue = 0;
        }      
        if (Input.GetMouseButtonDown(0)) {
            if (ithItemSelected != -1) {
                if (isBuying) {
                    Item itemSelected = GameEngine.ge.merchant.getItem(ithItemSelected);
                    if (playerScript.getHp() > 0 && itemSelected != null && playerScript.gold >= itemSelected.value) {
                        playerScript.audio3.PlayOneShot(playerScript.coinsClip);
                        playerScript.bag.insertItem(itemSelected, false, true);
                        playerScript.gold -= itemSelected.value;
                    }
                } else {
                    Item itemSelected = playerScript.bag.getItem(ithItemSelected);
                    if (playerScript.getHp() > 0 && itemSelected != null && itemSelected.value > 0) {
                        playerScript.audio3.PlayOneShot(playerScript.coinsClip);
                        playerScript.bag.removeItem(itemSelected);
                        playerScript.gold += itemValue;
                    }
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
        itemStyle.fontSize = 22;
        itemStyle.alignment = TextAnchor.UpperRight;
        GUI.Label(new Rect(106, Screen.height - 90, 150, 50), "" + itemQuantity, itemStyle);
        GUI.Label(new Rect(-10, Screen.height - 60, 150, 50), "" + itemValue, itemStyle);
        GUI.Label(new Rect(116, Screen.height - 60, 150, 50), "" + playerScript.gold, itemStyle);

        itemStyle.fontSize = 25;
        itemStyle.alignment = TextAnchor.MiddleCenter;
        if (isBuying) {
            GUI.Label(new Rect(41, Screen.height - 440, 220, 30), "Buy Items", itemStyle);
        } else {
            GUI.Label(new Rect(41, Screen.height - 440, 220, 30), "Sell Items", itemStyle);
        }
    }
}
