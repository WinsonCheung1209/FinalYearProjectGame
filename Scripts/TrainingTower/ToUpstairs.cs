using UnityEngine;
using System.Collections;

public class ToUpstairs : MonoBehaviour {

    Player playerScript;

    // Use this for initialization
    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            playerScript.transform.position = new Vector3(3, 0, 3);
            playerScript.score += GameEngine.ge.towerLevel * 10;
            Vector3 camPosition = playerScript.transformObject.position;
            camPosition.y += playerScript.camHeight;
            playerScript.camTransform.position = camPosition;
            GameEngine.ge.changePanel(0);
            //Application.LoadLevel(3);
            GameEngine.ge.towerLevel++;
            GameEngine.ge.monsterSpawn = GameEngine.ge.towerLevel;
            GameEngine.ge.monsterLeft = GameEngine.ge.monsterSpawn;
        }
    }
}
