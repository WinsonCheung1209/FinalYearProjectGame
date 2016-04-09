using UnityEngine;
using System.Collections;

public class FireSkill1 : Skill {

    Player playerScript;

    public FireSkill1() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        name = "Fireball";
        description = "Create a fireball that burns enemies.";
        level = 1;
        damageRate = 1.1f;
        coolDown = 2;
        mpCost = 14;
        cdTimer = 0;
    }

    public override void use() {
        //MonoBehaviour.print(cdTimer);
        if (playerScript.mp >= mpCost && cdTimer <= 0) {
            cdTimer = coolDown;
            GameObject fb = (GameObject) MonoBehaviour.Instantiate(Resources.Load("FireBall"), playerScript.transformObject.position + (playerScript.transformObject.TransformDirection(Vector3.forward) * 2), playerScript.transformObject.rotation);
            Fireball fbScript = fb.GetComponent<Fireball>();
            playerScript.minusMp(mpCost);
            fbScript.velocity = Vector3.forward * 10;
            fbScript.damageRate = damageRate;
        }
    }

    public override string toString(int level) {
        return (((float) level) / 20 + 1.05f) * 100 + "% damage, 2 sec cool down, mp cost: " + (level * 2 + 12);
    }

    public override void updateStat() {
        damageRate = ((float) level) / 20 + 1.05f;
        mpCost = level * 2 + 12;
    }
}
