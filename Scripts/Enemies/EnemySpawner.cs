using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject monguer;
    public GameObject bigCrush;
    public GameObject monster;
    public float spawnTimer;
    public int spawned = 0;
    public int minLevel = 2;
    public int maxLevel = 5;
    int level = 0;

    // Use this for initialization
    void Start() {
        spawnTimer = Random.value * 4;
    }

    // Update is called once per frame
    void Update() {
        level = GameEngine.ge.towerLevel;
        //print(spawnTimer);
        if ((level > 0 && GameEngine.ge.monsterSpawn > 0) || level == 0) {
            if (spawned < 1) {
                spawnTimer -= Time.deltaTime;
                
                if (spawnTimer <= 0) {
                    randomiseDir();
                    chooseMonster();
                    GameObject enemyObj = (GameObject) Instantiate(monster, this.transform.position, this.transform.rotation);
                    Enemy enemy = enemyObj.GetComponent<Enemy>();
                    level = GameEngine.ge.towerLevel;
                    if (level > 0) {
                        enemy.updateStats(level);
                    } else {
                        if (Application.loadedLevelName.Equals("StartLevel")) {
                            minLevel = 1;
                            maxLevel = 1;
                        } else {
                            int eLevels = GameEngine.ge.enemyLevels;
                            minLevel = eLevels;
                            maxLevel = eLevels + 5;
                        }
                        enemy.updateStats(Random.Range(minLevel, maxLevel));
                    }
                    enemy.setSpawner(this);
                    spawned++;
                    GameEngine.ge.monsterSpawn--;
                    spawnTimer = Random.value * 10 + 2;
                }
            }
        }
    }

    private void chooseMonster() {
        float ran = Random.value;
        if (ran > 0.8f) {
            monster = bigCrush;
        } else {
            monster = monguer;
        }
    }
    private void randomiseDir() {
        Vector3 objRotation = this.transform.rotation.eulerAngles;
        objRotation.y += Random.Range(0, 359);
        this.transform.eulerAngles = objRotation;
    }
}
