using UnityEngine;
using System.Collections;

public class NPCName : MonoBehaviour {

    Player playerScript;

	// Use this for initialization
	void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(playerScript.transformObject.position, this.transform.position) < 30) {
            Color original = this.transform.GetComponent<TextMesh>().color;
            original.a = 1;
            this.transform.GetComponent<TextMesh>().color = original;
        } else {
            Color transparent = this.transform.GetComponent<TextMesh>().color;
            transparent.a = 0;
            this.transform.GetComponent<TextMesh>().color = transparent;
        }
	}
}
