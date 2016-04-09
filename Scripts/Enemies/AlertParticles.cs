using UnityEngine;
using System.Collections;

public class AlertParticles : MonoBehaviour {

    public Enemy enemy;


    // Use this for initialization
    void Start() {
    }

    void OnParticleCollision(GameObject other) {
        if (other.tag.Equals("Player")) {
            if (enemy != null) {
                enemy.alerted = true;
            }
        }
    }

}
