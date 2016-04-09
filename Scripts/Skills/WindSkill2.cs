using UnityEngine;
using System.Collections;

public class WindSkill2 : Skill {

    Player playerScript;
    bool sk2Active = false;
    public GameObject gale;

    public WindSkill2() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        name = "Gale";
        description = "Create a gale around you that damage enemies within it";
        level = 1;
        damageRate = 2.2f;
        mpCost = 8;
    }

    public override void use() {
        sk2Active = !sk2Active;
        if (sk2Active) {
            if (playerScript.mp >= mpCost / 5) {
                gale = (GameObject) MonoBehaviour.Instantiate(Resources.Load("Gale"), playerScript.transformObject.position, playerScript.transformObject.rotation);
                GaleParticles galeScript = gale.GetComponent<GaleParticles>();
                MonoBehaviour.DontDestroyOnLoad(gale);
                galeScript.damageRate = damageRate;
            }
        } else if (gale != null) {
            MonoBehaviour.Destroy(gale);
        }
    }

    public override string toString(int level) {
        return "Damage: " + (((float) level) / 5 + 2f) + ", mp cost: " + (int) (((float) level) * 6 / 5 + 6.8f) + " per sec";
    }

    public override void updateStat() {
        damageRate = ((float) level) / 5 + 2f;
        mpCost = (int) (((float) level) * 6 / 5 + 6.8f);
    }
}
