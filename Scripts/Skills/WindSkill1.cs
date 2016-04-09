using UnityEngine;
using System.Collections;

public class WindSkill1 : Skill {

    Player playerScript;
    public float velocity;

    public WindSkill1() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        name = "Teleport";
        description = "Teleport you forward in the direction you are facing.";
        level = 1;
        velocity = 1;
        coolDown = 0.3f;
        mpCost = 12;
        cdTimer = 0;
    }

    public override void use() {
        //MonoBehaviour.print(cdTimer);
        if (playerScript.mp >= mpCost && cdTimer <= 0) {
            cdTimer = coolDown;
            playerScript.minusMp(mpCost);
            playerScript.charControl.Move(playerScript.transform.TransformDirection(Vector3.forward * velocity));
        }
    }

    public override string toString(int level) {
        return "Teleport distance: " + (level * 0.2f + 0.8f) + ", mp cost: " + ((int) (level * 1.5f + 10.5f));
    }

    public override void updateStat() {
        velocity = level * 0.2f + 0.8f;
        mpCost = (int) (level * 1.5f + 10.5f);
    }
}
