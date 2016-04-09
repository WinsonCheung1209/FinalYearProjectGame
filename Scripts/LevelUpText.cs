using UnityEngine;
using System.Collections;

public class LevelUpText : MonoBehaviour {

    Player playerScript;
    float timeToLive;

	// Use this for initialization
	void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        timeToLive = 3;
        this.GetComponent<GUIText>().pixelOffset = new Vector2(-Screen.width / 2 - 50, -Screen.height / 2 + 50);
        this.GetComponent<GUIText>().text = "Level Up! Level " + playerScript.charLevel + "!";
	}
	
	// Update is called once per frame
	void Update () {
        timeToLive -= Time.deltaTime;
        
        //this.transform.Translate(new Vector3(0, 2, 0) * Time.deltaTime);
        Color currentColour = this.GetComponent<GUIText>().color;
        currentColour.a -= 0.3f * Time.deltaTime;
        this.GetComponent<GUIText>().color = currentColour;
        this.GetComponent<GUIText>().fontSize += 2;
        if (timeToLive <= 0) {
            Destroy(this.gameObject);
        }
	}
}
