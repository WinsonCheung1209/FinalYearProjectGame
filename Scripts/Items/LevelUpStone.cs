using UnityEngine;
using System.Collections;

public class LevelUpStone : Item {

    public LevelUpStone() {
        name = "Level Stone";
        value = 5000;
        quantity = 1;
        description = "Level you up once";
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        usable = true;
    }

    public override bool use() {
        float e = playerScript.getMaxExp() - playerScript.getExp();
        playerScript.gainExp(e, true);
        return true;
    }

    public override void initTexture() {
        base.initTexture();
        texture.GetComponent<GUITexture>().texture = Resources.Load("LevelStone") as Texture2D;
    }

    public override Item clone() {
        LevelUpStone itemClone = new LevelUpStone();
        itemClone.quantity = this.quantity;
        return itemClone;
    }
}
