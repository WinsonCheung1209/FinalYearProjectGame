using UnityEngine;
using System.Collections;

public class ToVillage : MonoBehaviour {

    Player playerScript;

    // Use this for initialization
    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            //if (other.GetComponent<Player>().activeQuests[0] != null && other.GetComponent<Player>().activeQuests[0].id > 1) {
            playerScript.transform.position = new Vector3(8, 0, 7);
            Vector3 camPosition = playerScript.transformObject.position;
            camPosition.y += playerScript.camHeight;
            playerScript.camTransform.position = camPosition;
            GameEngine.ge.changePanel(0);
            Application.LoadLevel("Village");
            //} else {
            //    GameEngine.ge.setText(GameEngine.ge.getText() + "This area is not opened yet. Come back after you have completed the necessary quests to unlock it.\n");
            //}
        }
    }
}
