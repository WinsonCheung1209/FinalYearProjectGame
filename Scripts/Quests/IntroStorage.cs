using UnityEngine;
using System.Collections;

public class IntroStorage : Quest {

    public IntroStorage() {
        name = "Mayor's request 2";
        goal = 1;
        progress = 0;
        description = "I know why I was injured by the monsters earlier: I was missing a shield!\nCan you bring me back my shield from the storage at (27, 8)?";
        task = "Retrieve the mayor's shield from the storage at (27, 8) and bring it back to the mayor.";
        rewardGold = 10;
        rewardExp = 10;
        completeText = "Thank you! Everyone in this village share the same storage, feel free to use it. Don't worry, no one steals from this village. Here are some rewards for your effort.";
        next = new IntroInstructor();
        id = 4;
    }

    public void reward() {
        base.reward();
    }

    public override void updateProgress() {
        Item item = playerScript.bag.getItem(new MayorShield());
        if (item != null) {
            if (item.quantity > goal) {
                progress = goal;
            } else {
                progress = item.quantity;
            }
        } else {
            progress = 0;
        }
        MonoBehaviour.print("Progress: " + progress);
    }
}
