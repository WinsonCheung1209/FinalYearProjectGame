using UnityEngine;
using System.Collections;

public class ClassStone : Item {

    public ClassStone() {
        name = "Class Stone";
        value = 3000;
        quantity = 1;
        description = "Take it to the class instructor to change back to Normal class";
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        usable = false;
    }

    public override bool use() {
        return false;
    }

    public override void initTexture() {
        base.initTexture();
        texture.GetComponent<GUITexture>().texture = Resources.Load("ClassStone") as Texture2D;
    }

    public override Item clone() {
        ClassStone itemClone = new ClassStone();
        itemClone.quantity = this.quantity;
        return itemClone;
    }
}
