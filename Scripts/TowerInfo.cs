using UnityEngine;
using System.Collections;

public class TowerInfo : MonoBehaviour {

    Player playerScript;

	// Use this for initialization
	void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<GUIText>().text = "Floor : " + GameEngine.ge.towerLevel + "\nMonster Remaining : " + GameEngine.ge.monsterLeft + "\nScore : " + playerScript.score;
	}
}
