using UnityEngine;
using System.Collections;

public class WaterSkill2 : Skill {

    Player playerScript;

    public WaterSkill2() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        name = "Ice Frost";
        description = "Create an ice frost that may freeze enemies.";
        level = 1;
        damageRate = 1.1f;
        coolDown = 1;
        mpCost = 14;
        cdTimer = 0;
    }

    public override void use() {
        //MonoBehaviour.print(cdTimer);
        if (playerScript.mp >= mpCost && cdTimer <= 0) {
            cdTimer = coolDown;
            GameObject icef = (GameObject) MonoBehaviour.Instantiate(Resources.Load("IceFrost"), playerScript.transformObject.position + (playerScript.transformObject.TransformDirection(Vector3.forward) * 2), playerScript.transformObject.rotation);
            IceFrost ifScript = icef.GetComponent<IceFrost>();
            playerScript.minusMp(mpCost);
            ifScript.velocity = Vector3.forward * 10;
            ifScript.damageRate = damageRate;
        }
    }

    public override string toString(int level) {
        return (((float) level) / 20 + 1.05f) * 100 + "% damage, 1 sec cool down, mp cost: " + (level * 2 + 12);
    }

    public override void updateStat() {
        damageRate = ((float) level) / 20 + 1.05f;
        mpCost = level * 2 + 12;
    }
}
