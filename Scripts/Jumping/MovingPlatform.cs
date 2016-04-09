using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    bool moveRight;
    public float velocity;
    float timer;

    // Use this for initialization
    void Start() {
        if (Random.value > 0.5) {
            moveRight = true;
            velocity = Random.value * 10 + 5;
        } else {
            moveRight = false;
            velocity = - (Random.value * 10 + 5);
        }
        timer = 2;
        
    }

    // Update is called once per frame
    void Update() {
        if (!moveRight && this.transform.position.x < 10) {
            velocity = 0;
            timer -= Time.deltaTime;
            if (timer < 0) {
                moveRight = true;
                velocity = Random.value * 10 + 5;
                timer = 2;
            }
        } else if (moveRight && this.transform.position.x > 90) {
            velocity = 0;
            timer -= Time.deltaTime;
            if (timer < 0) {
                moveRight = false;
                velocity = - (Random.value * 10 + 5);
                timer = 2;
            }
        }
        this.transform.position += new Vector3(velocity * Time.deltaTime, 0, 0);
    }
}
