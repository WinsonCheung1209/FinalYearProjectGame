using UnityEngine;
using System.Collections;

public class TrainingGUI : DialogGUI {

    // Use this for initialization
    new void Start() {
        base.Start();
        name = "Master Kai";
        dialog = "Do you want to get stronger? If so, train in the training tower in High score mode. In High score mode you can submit your highscore to compete with other people. You won't lose any gold in High score mode when you die.";
        buttons = new string[] {"", "", "Maybe later", "Let's go!"};
    }

    // Update is called once per frame
    void Update() {
        if (buttonPressed[2]) {
            GameEngine.ge.changePanel(0);
            Destroy(this);
        }
        if (buttonPressed[3]) {
            GameEngine.ge.towerLevel = 1;
            GameEngine.ge.monsterSpawn = 1;
            GameEngine.ge.monsterLeft = GameEngine.ge.monsterSpawn;
            playerScript.score = 1000;
            playerScript.transform.position = new Vector3(3, 0, 3);
            Vector3 camPosition = playerScript.transformObject.position;
            camPosition.y += playerScript.camHeight;
            playerScript.camTransform.position = camPosition;
            GameEngine.ge.changePanel(0);
            Application.LoadLevel("TrainingTower");
        }
    }

    new void OnGUI() {
        base.OnGUI();
    }

}
