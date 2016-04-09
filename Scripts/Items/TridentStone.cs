using UnityEngine;
using System.Collections;

public class TridentStone : Item {

    public TridentStone() {
        name = "Trident Stone";
        value = 2000;
        quantity = 1;
        description = "Teleports you to a place with higher level monsters";
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        usable = true;
    }

    public override bool use() {
        if (Application.loadedLevelName.Equals("Level")) {
            if (playerScript.charLevel <= GameEngine.ge.enemyLevels) {
                GameEngine.ge.setText(GameEngine.ge.getText() + "Your level is too low to use this item\n");
                return false;
            } else {
                playerScript.transform.position = new Vector3(95, 2, 95);
                Vector3 camPosition = playerScript.transformObject.position;
                camPosition.y += playerScript.camHeight;
                playerScript.camTransform.position = camPosition;
                return true;
            }
        } else if (Application.loadedLevelName.Equals("TrainingTower")) {
            GameEngine.ge.monsterLeft = 0;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject go in enemies) {
                MonoBehaviour.Destroy(go);
            }
            GameObject[] spawners = GameObject.FindGameObjectsWithTag("spawner");
            foreach (GameObject spawner in spawners) {
                EnemySpawner es = spawner.GetComponent<EnemySpawner>();
                es.spawned = 0;
                es.spawnTimer = Random.value * 4;
            }
            playerScript.transform.position = new Vector3(9, 3, 27);
            Vector3 camPosition = playerScript.transformObject.position;
            camPosition.y += playerScript.camHeight;
            playerScript.camTransform.position = camPosition;

            return true;
        } else {
            GameEngine.ge.setText(GameEngine.ge.getText() + "You can't use this here\n");
            return false;
        }

    }

    public override void initTexture() {
        base.initTexture();
        texture.GetComponent<GUITexture>().texture = Resources.Load("TridentStone") as Texture2D;
    }

    public override Item clone() {
        TridentStone itemClone = new TridentStone();
        itemClone.quantity = this.quantity;
        return itemClone;
    }
}
