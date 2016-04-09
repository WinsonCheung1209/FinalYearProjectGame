using UnityEngine;
using System.Collections;

public class SkillGUI : MonoBehaviour {

    Player playerScript;
    GUIStyle skillStyle = new GUIStyle();

    // Use this for initialization
    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update() {
        //if (Input.GetMouseButton(0)) {
        //    MonoBehaviour.print(Input.mousePosition);
        //}
    }

    void OnGUI() {
        ClassType ct = null;
        skillStyle.normal.textColor = new Color(1f, 1f, 1f);
        switch (playerScript.classType) {
            case 1: {
                    ct = playerScript.GetComponent<FireClass>();
                    skillStyle.normal.textColor = new Color(0.9f, 0.3f, 0.3f);
                }
                break;
            case 2: {
                    ct = playerScript.GetComponent<WaterClass>();
                    skillStyle.normal.textColor = new Color(0.3f, 0.3f, 0.9f);
                }
                break;
            case 3: {
                    ct = playerScript.GetComponent<WindClass>();
                    skillStyle.normal.textColor = new Color(0.3f, 0.9f, 0.3f);
                }
                break;
        }
        if (ct != null) {
            skillStyle.alignment = TextAnchor.UpperLeft;
            skillStyle.fontSize = 25;
            skillStyle.fontStyle = FontStyle.Bold;
            skillStyle.wordWrap = true;
            GUI.Label(new Rect(35, Screen.height - 397, 235, 30), ct.sk1.name + " Lv: " + ct.sk1.level, skillStyle);
            GUI.Label(new Rect(35, Screen.height - 229, 235, 30), ct.sk2.name + " Lv: " + ct.sk2.level, skillStyle);
            skillStyle.fontSize = 18;
            skillStyle.normal.textColor = new Color(0.9f, 0.9f, 0.9f);
            GUI.Label(new Rect(35, Screen.height - 362, 235, 50), ct.sk1.toString(ct.sk1.level), skillStyle);
            GUI.Label(new Rect(35, Screen.height - 194, 235, 50), ct.sk2.toString(ct.sk2.level), skillStyle);
            if (playerScript.classType == 3 && ct.sk1.level >= 71) {
                GUI.Label(new Rect(35, Screen.height - 278, 235, 50), "Skill Maxed!", skillStyle);
            } else {
                GUI.Label(new Rect(35, Screen.height - 278, 235, 50), ct.sk1.toString(ct.sk1.level + 1), skillStyle);
            }
            GUI.Label(new Rect(35, Screen.height - 110, 235, 50), ct.sk2.toString(ct.sk2.level + 1), skillStyle);
            skillStyle.fontSize = 25;
            skillStyle.alignment = TextAnchor.UpperRight;
            GUI.Label(new Rect(30, Screen.height - 60, 235, 30), "" + playerScript.skillPoints, skillStyle);
            if (playerScript.skillPoints > 0) {
                if (playerScript.classType != 3 || (playerScript.classType == 3 && ct.sk1.level <= 70)) {
                    if (GUI.Button(new Rect(240, Screen.height - 397, 28, 28), "+")) {
                        ct.sk1.level++;
                        ct.sk1.updateStat();
                        playerScript.skillPoints--;
                    }
                }
                if (GUI.Button(new Rect(240, Screen.height - 229, 28, 28), "+")) {
                    ct.sk2.level++;
                    ct.sk2.updateStat();
                    playerScript.skillPoints--;
                }
            }
        } else {
            skillStyle.alignment = TextAnchor.UpperLeft;
            skillStyle.fontSize = 25;
            skillStyle.fontStyle = FontStyle.Bold;
            skillStyle.wordWrap = true;
            GUI.Label(new Rect(35, Screen.height - 397, 235, 30), "No skills available", skillStyle);
        }
    }
}
