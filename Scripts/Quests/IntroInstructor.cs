using UnityEngine;
using System.Collections;

public class IntroInstructor : Quest {

    public IntroInstructor() {
        name = "Getting stronger 1";
        goal = 1;
        progress = 1;
        description = "You need to become stronger if you want to survive in this world. Go see the class instructor at (39, 45) and he will teach you many essential skills.";
        task = "Talk to the class instructor at (39, 45).";
        rewardGold = 10;
        rewardExp = 10;
        //completeText = "Thank you! Everyone in this village share the same storage, feel free to use it. Don't worry, no one steals from this village. Here are some rewards for your effort.";
        next = new Stronger2();
        id = 5;
    }

    public void reward() {
        base.reward();
    }

    public override void updateProgress() {

    }
}
