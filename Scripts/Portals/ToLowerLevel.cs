using UnityEngine;
using System.Collections;

public class ToLowerLevel : MonoBehaviour {

    Player playerScript;
    public GameObject toVillage;

	// Use this for initialization
	void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {

            playerScript.transform.position = new Vector3(80, 2, 80);
            Vector3 camPosition = playerScript.transformObject.position;
            camPosition.y += playerScript.camHeight;
            playerScript.camTransform.position = camPosition;

            GameEngine.ge.changePanel(0);
            GameEngine.ge.enemyLevels -= 5;
            
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject go in enemies) {
                Destroy(go);
            }
            GameObject[] spawners = GameObject.FindGameObjectsWithTag("spawner");
            foreach (GameObject spawner in spawners) {
                EnemySpawner es = spawner.GetComponent<EnemySpawner>();
                es.spawned = 0;
                es.spawnTimer = Random.value * 4;
            }

            if (GameEngine.ge.enemyLevels <= 1) {
                toVillage.SetActive(true);
                this.gameObject.SetActive(false);
            } else {
                toVillage.SetActive(false);
            }

        }
    }
}
