using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (GameEngine.ge.monsterLeft <= 0) {
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<BoxCollider>().enabled = false;
        } else {
            this.GetComponent<MeshRenderer>().enabled = true;
            this.GetComponent<BoxCollider>().enabled = true;
        }
	}
}
