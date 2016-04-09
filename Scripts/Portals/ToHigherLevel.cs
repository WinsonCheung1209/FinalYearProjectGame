using UnityEngine;
using System.Collections;

public class ToHigherLevel : MonoBehaviour {

    Player playerScript;
    public GameObject toVillage;
    public GameObject toLower;

	// Use this for initialization
	void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            
            GameEngine.ge.changePanel(0);
            GameEngine.ge.enemyLevels += 5;
            toVillage.SetActive(false);
            toLower.SetActive(true);
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

            playerScript.transform.position = new Vector3(20, 2, 20);
            Vector3 camPosition = playerScript.transformObject.position;
            camPosition.y += playerScript.camHeight;
            playerScript.camTransform.position = camPosition;
        }
    }
}
