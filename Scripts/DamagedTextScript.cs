using UnityEngine;
using System.Collections;

public class DamagedTextScript : MonoBehaviour {

    float timeToLive;

	// Use this for initialization
	void Start () {
        timeToLive = 2;
	}
	
	// Update is called once per frame
	void Update () {
        timeToLive -= Time.deltaTime;
        this.transform.Translate(new Vector3(0, 2, 0) * Time.deltaTime);
        Color currentColour = this.GetComponent<TextMesh>().color;
        currentColour.a -= 0.5f * Time.deltaTime;
        this.GetComponent<TextMesh>().color = currentColour;
        if (timeToLive <= 0) {
            Destroy(this.gameObject);
        }
	}
}
