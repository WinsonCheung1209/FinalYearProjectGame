using UnityEngine;
using System.Collections;

public class HpPotion : Item {

    public HpPotion() {
        name = "Health Potion";
        value = 200;
        quantity = 1;
        description = "Restore 50 HP";
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        usable = true;
    }

    public override bool use() {
        if (playerScript.getHp() < playerScript.getMaxHp()) {
            playerScript.damaged(-50);
            return true;
        } else {
            GameEngine.ge.setText(GameEngine.ge.getText() + "Hp already full!\n");
            return false;
        }
    }

    public override void initTexture() {
        base.initTexture();
        texture.GetComponent<GUITexture>().texture = Resources.Load("HpPotion") as Texture2D;
    }

    public override Item clone() {
        HpPotion itemClone = new HpPotion();
        itemClone.quantity = this.quantity;
        return itemClone;
    }
}
