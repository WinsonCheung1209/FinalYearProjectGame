using UnityEngine;
using System.Collections;

public class FireSkill2 : Skill {

    Player playerScript;

    public FireSkill2() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        name = "Explosion";
        description = "Create an explosion that burns enemies close to you.";
        level = 1;
        damageRate = 0.7f;
        coolDown = 3;
        mpCost = 21;
        cdTimer = 0;
    }

    public override void use() {
        //MonoBehaviour.print(cdTimer);
        if (playerScript.mp >= mpCost && cdTimer <= 0) {
            cdTimer = coolDown;
            GameObject fe = (GameObject) MonoBehaviour.Instantiate(Resources.Load("FireExplosion"), playerScript.transformObject.position + Vector3.forward, playerScript.transformObject.rotation);
            FireExplosion feScript = fe.GetComponent<FireExplosion>();
            playerScript.minusMp(mpCost);
            feScript.damageRate = damageRate;
        }
    }

    public override string toString(int level) {
        return (((float) level) / 20 + 0.65f) * 100 + "% damage, 3 sec cool down, mp cost: " + (level * 3 + 18);
    }

    public override void updateStat() {
        damageRate = ((float) level) / 20 + 0.65f;
        mpCost = level * 3 + 18;
    }
}
