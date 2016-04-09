using UnityEngine;
using System.Collections;

public class PosText : MonoBehaviour {

    Player playerScript;

    // Use this for initialization
    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() {
        string updatedText = "";
        if (playerScript != null) {
            if (Application.loadedLevelName.Equals("Level")) {
                int eLevels = GameEngine.ge.enemyLevels;
                updatedText = "Level " + eLevels + " - " + (eLevels + 4) + " ground";
            }
            updatedText += "(" + (int) playerScript.transform.position.x + ", " + (int) playerScript.transform.position.z + ")";
        } else {
            updatedText = "";
        }
        this.GetComponent<GUIText>().text = updatedText;
    }
}
