using UnityEngine;
using System.Collections;

public class DeliverMessage : Quest {

    public DeliverMessage() {
        name = "Delivering message";
        goal = 1;
        progress = 1;
        description = "OK, I think you are ready to face the dangers in this world. Talk to the Mayor of the village through the portal and he will guide your next steps. Good luck adventurer!";
        task = "Go through the portal and talk to the Mayor at (41, 19)";
        rewardGold = 10;
        rewardExp = 10;
        next = new IntroMerchant();
        id = 2;
    }

    public void reward() {
        base.reward();
    }

    public override void updateProgress() {

    }

}
