using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour {

    GUITexture texture;
    public new GameObject camera;
    public GameObject camera2;
    public GameObject player;
    GameEngine game;
    Player p;


	// Use this for initialization
	void Start () {
        texture = this.GetComponent<GUITexture>();
        game = camera.GetComponent<GameEngine>();
        p = player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerPrefs.GetInt("SaveFile") == 1) {
            texture.enabled = true;
            if (Input.GetMouseButtonDown(0) && texture.HitTest(Input.mousePosition)) {
                Destroy(camera2);
                camera.SetActive(true);
                player.SetActive(true);
                game.Start();
                p.Start();
                game.loadGame();
                Application.LoadLevel("Village");
                //GameEngine.ge.loadGame();
                
            }
        } else {
            texture.enabled = false;
        }
	}
}
