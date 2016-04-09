using UnityEngine;
using System.Collections;

public class CharacterGUI : MonoBehaviour {

    Player playerScript;

    // Use this for initialization
    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnGUI() {
        GUIStyle charStyle = new GUIStyle();
        charStyle.fontSize = 18;
        charStyle.fontStyle = FontStyle.Bold;
        charStyle.normal.textColor = new Color(0.9f, 0.9f, 0.9f);
        charStyle.wordWrap = true;
        charStyle.alignment = TextAnchor.UpperRight;
        string classtype = "";
        if (playerScript.classType == 0) {
            classtype = "Normal";
        } else if (playerScript.classType == 1) {
            classtype = "Fire";
            charStyle.normal.textColor = new Color(0.9f, 0.3f, 0.3f);
        } else if (playerScript.classType == 2) {
            classtype = "Water";
            charStyle.normal.textColor = new Color(0.3f, 0.3f, 0.9f);
        } else if (playerScript.classType == 3) {
            classtype = "Wind";
            charStyle.normal.textColor = new Color(0.3f, 0.9f, 0.3f);
        }
        GUI.Label(new Rect(155, Screen.height - 397, 100, 25), classtype, charStyle);
        charStyle.normal.textColor = new Color(0.9f, 0.9f, 0.9f);
        GUI.Label(new Rect(155, Screen.height - 372, 100, 25), "" + playerScript.charLevel, charStyle);
        GUI.Label(new Rect(125, Screen.height - 346, 100, 25), Mathf.Round(playerScript.getHp()) + "/" + Mathf.Round(playerScript.getMaxHp()), charStyle);
        GUI.Label(new Rect(125, Screen.height - 321, 100, 25), Mathf.Round(playerScript.getMp()) + "/" + Mathf.Round(playerScript.getMaxMp()), charStyle);
        GUI.Label(new Rect(125, Screen.height - 296, 100, 25), "" + playerScript.calMinAttack() + "-" + playerScript.calMaxAttack(), charStyle);
        GUI.Label(new Rect(125, Screen.height - 271, 100, 25), "" + playerScript.luck, charStyle);
        GUI.Label(new Rect(125, Screen.height - 246, 100, 25), "" + playerScript.getSpeed(), charStyle);
        GUI.Label(new Rect(125, Screen.height - 221, 100, 25), playerScript.recoverHp.ToString("F1"), charStyle);
        GUI.Label(new Rect(125, Screen.height - 196, 100, 25), playerScript.recoverMp.ToString("F1"), charStyle);
        charStyle.fontSize = 22;
        GUI.Label(new Rect(163, Screen.height - 58, 100, 25), "" + playerScript.attributePoints, charStyle);

        if (playerScript.attributePoints > 0) {
            if (GUI.Button(new Rect(230, Screen.height - 346, 40, 23), "+10")) {
                playerScript.baseHp += 10;
                playerScript.maxHp += 10;
                playerScript.attributePoints--;
            }
            if (GUI.Button(new Rect(230, Screen.height - 321, 40, 23), "+10")) {
                playerScript.mp += 10;
                playerScript.maxMp += 10;
                playerScript.attributePoints--;
            }
            if (GUI.Button(new Rect(230, Screen.height - 296, 40, 23), "+4")) {
                playerScript.baseAttack += 4;
                playerScript.attributePoints--;
            }
            if (GUI.Button(new Rect(230, Screen.height - 271, 40, 23), "+2")) {
                playerScript.luck += 2;
                playerScript.attributePoints--;
            }
            if (playerScript.moveSpeed < 20 && GUI.Button(new Rect(230, Screen.height - 246, 40, 23), "+1")) {
                playerScript.moveSpeed += 1;
                playerScript.attributePoints--;
            }
            if (GUI.Button(new Rect(230, Screen.height - 221, 40, 23), "+0.2")) {
                playerScript.recoverHp += 0.2f;
                playerScript.attributePoints--;
            }
            if (GUI.Button(new Rect(230, Screen.height - 196, 40, 23), "+0.4")) {
                playerScript.recoverMp += 0.4f;
                playerScript.attributePoints--;
            }
        }
    }
}
