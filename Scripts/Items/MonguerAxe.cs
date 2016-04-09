using UnityEngine;
using System.Collections;

public class MonguerAxe : Item {

    public MonguerAxe() {
        name = "Monguer's Axe";
        value = 10;
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
        texture.GetComponent<GUITexture>().texture = Resources.Load("MonguerAxe") as Texture2D;
    }

    public override Item clone() {
        MonguerAxe itemClone = new MonguerAxe();
        itemClone.quantity = this.quantity;
        return itemClone;
    }
}
