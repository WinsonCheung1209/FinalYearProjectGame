﻿using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

    public Vector3 velocity;
    public float damageRate;
    protected Transform transformObj;
    Player playerScript;

    // Use this for initialization
    void Start() {
        transformObj = this.transform;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() {
        if (velocity != null) {
            transformObj.Translate(velocity * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Enemy")) {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.damaged(Mathf.RoundToInt(Random.Range(playerScript.calMinAttack(), playerScript.calMaxAttack()) * damageRate));
            Destroy(this.gameObject);
        }
        if (other.tag.Equals("Wall")) {
            Destroy(this.gameObject);
        }
    }

}
