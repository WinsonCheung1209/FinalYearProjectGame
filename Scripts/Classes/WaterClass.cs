using UnityEngine;
using System.Collections;

public class WaterClass : ClassType {

    Player playerScript;
    WaterSkill1 wsk1;

    // Use this for initialization
    public override void Start() {
        if (playerScript == null) {
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            sk1 = new WaterSkill1();
            wsk1 = (WaterSkill1) sk1;
            sk2 = new WaterSkill2();
            playerScript.hpMultiplier = 1.5f;
            wsk1.wg = (GameObject) MonoBehaviour.Instantiate(Resources.Load("Water Gun"), playerScript.transformObject.position + (playerScript.transformObject.TransformDirection(Vector3.forward) * 2) + new Vector3(0, 2, 0), Quaternion.Euler(0, 0, 0));
            DontDestroyOnLoad(wsk1.wg);
            wsk1.wg.GetComponent<ParticleEmitter>().emit = false;
        }

    }

    // Update is called once per frame
    void Update() {
        sk2.cdTimer -= Time.deltaTime;
        if (playerScript.baseHp > 0 && Input.GetKeyDown(KeyCode.Alpha1)) {
            sk1.use();
        }

        if (Input.GetKeyUp(KeyCode.Alpha1)) {
            wsk1.skillActive = false;
        }
        if (playerScript.baseHp > 0 && Input.GetKeyDown(KeyCode.Alpha2)) {
            sk2.use();
        }
        if (wsk1.skillActive) {
            playerScript.minusMp(wsk1.mpCost * Time.deltaTime);
            if (playerScript.mp <= 0) {
                wsk1.skillActive = false;
                if (wsk1.wg != null) {
                    wsk1.skillActive = false;
                }
            }
        }
        if (!wsk1.skillActive) {
            if (wsk1.wg != null) {
                WaterGun wgScript = wsk1.wg.GetComponent<WaterGun>();
                if (wgScript.audioSource.isPlaying) {
                    wgScript.audioSource.Stop();
                }
                wsk1.wg.GetComponent<ParticleEmitter>().emit = false;
            }

        }
        if (wsk1.wg != null) {
            Vector3 rotVector = playerScript.transform.eulerAngles;
            rotVector.x += 90;
            wsk1.wg.transform.position = playerScript.transform.position + (playerScript.transformObject.TransformDirection(Vector3.forward) * 0.5f) + new Vector3(0, 1.8f, 0);
            wsk1.wg.transform.eulerAngles = rotVector;
            wsk1.wg.transform.Find("particle effect").GetComponent<ParticleEmitter>().emit = wsk1.skillActive;
        }
    }

    void OnDestroy() {
        Destroy(wsk1.wg);
        playerScript.hpMultiplier = 1;
    }
}
