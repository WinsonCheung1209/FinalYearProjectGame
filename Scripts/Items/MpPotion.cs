using UnityEngine;
using System.Collections;

public class MpPotion : Item {

    public MpPotion() {
        name = "Mana Potion";
        value = 250;
        quantity = 1;
        description = "Restore 50 MP";
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        usable = true;
    }

    public override bool use() {
        if (playerScript.mp < playerScript.maxMp) {
            playerScript.minusMp(-50);
            return true;
        } else {
            GameEngine.ge.setText(GameEngine.ge.getText() + "Mp already full!\n");
            return false;
        }
        
        
    }

    public override void initTexture() {
        base.initTexture();
        texture.GetComponent<GUITexture>().texture = Resources.Load("MpPotion") as Texture2D;
    }

    public override Item clone() {
        MpPotion itemClone = new MpPotion();
        itemClone.quantity = this.quantity;
        return itemClone;
    }
}
