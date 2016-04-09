using UnityEngine;
using System.Collections;

public class WaterSkill1 : Skill {

    Player playerScript;
    public bool skillActive = false;
    public GameObject wg;

    public WaterSkill1() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        name = "Water gun";
        description = "Create a water gun that damage and slow enemies.";
        level = 1;
        damageRate = 1f;
        coolDown = 0;
        mpCost = 5;
        cdTimer = 0;
    }

    public override void use() {
        //MonoBehaviour.print(cdTimer);
        if (playerScript.mp >= mpCost / 5) {
            skillActive = true;
            wg.GetComponent<ParticleEmitter>().emit = true;
            WaterGun wgScript = wg.GetComponent<WaterGun>();
            wgScript.damageRate = damageRate;
            if (!wgScript.audioSource.isPlaying) {
                wgScript.audioSource.Play();
            }
        }
    }

    public override string toString(int level) {
        return "Damage: " + (((float) level) / 10 + 1f) + ", " + "mp cost: " + (level + 4) + " per sec";
    }

    public override void updateStat() {
        damageRate = ((float) level) / 10 + 1f;
        mpCost = level + 4;
    }
}
