using UnityEngine;
using System.Collections;

public class ElimMonguer : Quest {

    public ElimMonguer() {
        name = "Eliminating threats";
        goal = 10;
        progress = 0;
        description = "That was expected of you. Before I will acknowledge you, come back alive after killing " + goal + " Monguers. You can find them after leaving this village via the portal at (55, 5).";
        task = "Kill " + goal + " Monguers and talk to the class instructor.";
        rewardGold = 50;
        rewardExp = 20;
        rewardItemList = new Item[1];
        HpPotion hpPotion = new HpPotion();
        hpPotion.quantity = 3;
        rewardItemList[0] = hpPotion;
        completeText = "Well done!";
        next = new BringAxes();
        id = 7;
    }

    public override void updateProgress() {

    }
}
