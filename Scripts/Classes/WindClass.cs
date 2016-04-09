using UnityEngine;
using System.Collections;

public class WindClass : ClassType {

    Player playerScript;
    bool sk2Active;
    WindSkill2 wsk2;

    // Use this for initialization
    public override void Start() {
        if (playerScript == null) {
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            sk1 = new WindSkill1();
            sk2 = new WindSkill2();
            wsk2 = (WindSkill2) sk2;
            playerScript.agiMultiplier = 1.5f;
            sk2Active = false;
        }
    }

    // Update is called once per frame
    void Update() {
        sk1.cdTimer -= Time.deltaTime;
        sk2.cdTimer -= Time.deltaTime;
        if (playerScript.baseHp > 0 && Input.GetKeyDown(KeyCode.Alpha1)) {
            if (GameEngine.ge.showPanel == 0) {
                sk1.use();
            }
        }
        if (playerScript.baseHp > 0 && Input.GetKeyDown(KeyCode.Alpha2)) {
            sk2.use();
            sk2Active = !sk2Active;
        }
        if (sk2Active) {
            playerScript.minusMp(sk2.mpCost * Time.deltaTime);
            if (wsk2.gale != null) {
                wsk2.gale.transform.position = playerScript.transform.position + (playerScript.transformObject.TransformDirection(Vector3.forward) * 0.5f);
            }
        }
    }

    void OnDestroy() {
        playerScript.agiMultiplier = 1;
    }
}
