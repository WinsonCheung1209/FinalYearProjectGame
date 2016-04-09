using UnityEngine;
using System.Collections;

public class Yes : MonoBehaviour {

    GUITexture texture;

	// Use this for initialization
	void Start () {
        texture = this.GetComponent<GUITexture>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && texture.HitTest(Input.mousePosition)) {
            Application.LoadLevel("StartLevel");
        }
	}
}
