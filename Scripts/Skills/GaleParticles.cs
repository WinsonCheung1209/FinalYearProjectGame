using UnityEngine;
using System.Collections;

public class GaleParticles : MonoBehaviour {

    public float damageRate;

    // Use this for initialization
    void Start() {
    }

    void OnParticleCollision(GameObject other) {
        if (other.tag.Equals("Enemy")) {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.damaged(damageRate);
            }
        }
    }

}
