using UnityEngine;
using System.Collections;

public class MayorShield : Item {

    public MayorShield() {
        name = "Mayor's Shield";
        value = 0;
        quantity = 1;
        description = "Quest Item";
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        usable = false;
    }

    public override bool use() {
        return false;
    }

    public override void initTexture() {
        base.initTexture();
        texture.GetComponent<GUITexture>().texture = Resources.Load("MayorShield") as Texture2D;
    }

    public override Item clone() {
        MayorShield itemClone = new MayorShield();
        itemClone.quantity = this.quantity;
        return itemClone;
    }
}
