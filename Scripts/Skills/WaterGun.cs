using UnityEngine;
using System.Collections;

public class WaterGun : MonoBehaviour {

    public float damageRate;
    protected Transform transformObj;
    public AudioSource audioSource;

    // Use this for initialization
    void Start() {
        audioSource = this.GetComponent<AudioSource>();
        transformObj = this.transform;
        
    }

    void OnParticleCollision(GameObject other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.damaged(damageRate);
            enemy.resetSpeedTimer = 5;
            enemy.moveSpeed = enemy.originMoveSpeed / 2;
        }
    }

}
