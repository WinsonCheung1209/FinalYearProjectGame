using UnityEngine;
using System.Collections;

public class VillageStone : Item {

    public VillageStone() {
        name = "Teleport Stone";
        value = 1000;
        quantity = 1;
        description = "Teleports you back to the Village";
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        usable = true;
    }

    public override bool use() {
        if (Application.loadedLevelName.Equals("TrainingTower")) {
            GameEngine.ge.setText(GameEngine.ge.getText() + "You can't use this here\n");
            return false;
        } else {
            playerScript.transform.position = new Vector3(8, 0, 7);
            Vector3 camPosition = playerScript.transformObject.position;
            camPosition.y += playerScript.camHeight;
            playerScript.camTransform.position = camPosition;
            GameEngine.ge.changePanel(0);
            Application.LoadLevel("Village");
            return true;
        }
    }

    public override void initTexture() {
        base.initTexture();
        texture.GetComponent<GUITexture>().texture = Resources.Load("VillageStone") as Texture2D;
    }

    public override Item clone() {
        VillageStone itemClone = new VillageStone();
        itemClone.quantity = this.quantity;
        return itemClone;
    }
}
