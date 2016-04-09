using UnityEngine;
using System.Collections;

public class No : MonoBehaviour {

    GUITexture texture;
    public GameObject confirm;
    public GameObject start;
    public GameObject load;

    // Use this for initialization
    void Start() {
        texture = this.GetComponent<GUITexture>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0) && texture.HitTest(Input.mousePosition)) {
            start.SetActive(true);
            load.SetActive(true);
            confirm.SetActive(false);
        }
    }
}
