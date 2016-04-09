using UnityEngine;
using System.Collections;

public class FireExplosion : MonoBehaviour {

    public float damageRate;
    protected Transform transformObj;
    Player playerScript;
    float timeToLive;

    // Use this for initialization
    void Start() {
        transformObj = this.transform;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        timeToLive = 4;
    }

    // Update is called once per frame
    void Update() {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0) {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Enemy")) {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.damaged(Mathf.RoundToInt(Random.Range(playerScript.calMinAttack(), playerScript.calMaxAttack()) * damageRate));
        }
    }

}
