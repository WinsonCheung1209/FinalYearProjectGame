using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

    GUITexture texture;
    public GameObject confirm;
    public GameObject start;
    public GameObject load;

	// Use this for initialization
	void Start () {
        //PlayerPrefs.SetInt("pBankSize", 1);
        //PlayerPrefs.SetString("pBankName0", "Level Stone");
        //PlayerPrefs.SetInt("pBankQuant0", 100);
        //PlayerPrefs.SetInt("pBankGold", 100000);
        texture = this.GetComponent<GUITexture>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && texture.HitTest(Input.mousePosition)) {
            confirm.SetActive(true);
            load.SetActive(false);
            start.SetActive(false);
        }
	}
}
