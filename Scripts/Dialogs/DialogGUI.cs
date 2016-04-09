using UnityEngine;
using System.Collections;

public class DialogGUI : MonoBehaviour {

    public Player playerScript;
    public new string name;
    public string dialog;
    public string[] buttons;
    public bool[] buttonPressed = new bool[4] { false, false, false, false };

    public void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void OnGUI() {
        GUIStyle dialogStyle = new GUIStyle();
        dialogStyle.fontSize = 20;
        dialogStyle.fontStyle = FontStyle.Bold;
        dialogStyle.normal.textColor = new Color(0.9f, 0.9f, 0.9f);
        dialogStyle.wordWrap = true;
        dialogStyle.alignment = TextAnchor.MiddleCenter;
        GUI.Label(new Rect(41, Screen.height - 440, 220, 30), name, dialogStyle);
        dialogStyle.alignment = TextAnchor.UpperLeft;
        dialogStyle.fontSize = 16;
        GUI.Label(new Rect(41, Screen.height - 395, 220, 230), dialog, dialogStyle);

        if (buttons != null) {
            for (int i = 0; i < buttons.Length; i++) {
                if (buttons[i].Length > 0) {
                    if (i < 2) {
                        buttonPressed[i] = GUI.Button(new Rect(i * 115 + 41, Screen.height - 137, 105, 43), buttons[i]);
                    } else {
                        buttonPressed[i] = GUI.Button(new Rect((i - 2) * 115 + 41, Screen.height - 84, 105, 43), buttons[i]);
                    }
                }
            }
        }
    }

}
