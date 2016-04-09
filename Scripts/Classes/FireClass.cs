using UnityEngine;
using System.Collections;

public class FireClass : ClassType {

    Player playerScript;

    // Use this for initialization
    public override void Start() {
        if (playerScript == null) {
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            sk1 = new FireSkill1();
            sk2 = new FireSkill2();
            playerScript.attMultiplier = 1.5f;
        }
    }

    // Update is called once per frame
    void Update() {
        sk1.cdTimer -= Time.deltaTime;
        sk2.cdTimer -= Time.deltaTime;
        if (playerScript.baseHp > 0 && Input.GetKeyDown(KeyCode.Alpha1)) {
            sk1.use();
        }
        if (playerScript.baseHp > 0 && Input.GetKeyDown(KeyCode.Alpha2)) {
            sk2.use();
        }
    }

    void OnDestroy() {
        playerScript.attMultiplier = 1;
    }
}
