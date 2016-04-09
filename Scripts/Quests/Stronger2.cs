using UnityEngine;
using System.Collections;

public class Stronger2 : Quest {

    public Stronger2() {
        name = "Getting stronger 2";
        goal = 5;
        progress = 0;
        description = "So, you are the newbie that everyone was so excited about? The way I see it, you wouldn't last 10 seconds once leaving this village. If you want to learn from me, prove me wrong by leveling up to level 5.";
        task = "Talk to the class instructor at (39, 45) after leveling up to level 5.";
        rewardGold = 10;
        rewardExp = 10;
        completeText = "What? You are level 5 already? That was quick.";
        next = new ElimMonguer();
        id = 6;
    }

    public void reward() {
        base.reward();
    }

    public override void updateProgress() {
        if (playerScript.charLevel > goal) {
            progress = goal;
        } else {
            progress = playerScript.charLevel;
        }
    }
}
