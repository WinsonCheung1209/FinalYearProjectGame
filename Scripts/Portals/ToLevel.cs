using UnityEngine;
using System.Collections;

public class ToLevel : MonoBehaviour {

    Player playerScript;

	// Use this for initialization
	void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            playerScript.transform.position = new Vector3(8, 2, 7);
            Vector3 camPosition = playerScript.transformObject.position;
            camPosition.y += playerScript.camHeight;
            playerScript.camTransform.position = camPosition;
            GameEngine.ge.changePanel(0);
            Application.LoadLevel("Level");
        }
    }
}
