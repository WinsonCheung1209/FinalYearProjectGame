using UnityEngine;
using System.Collections;

public class BringAxes : Quest {

    public BringAxes() {
        name = "Instructor's final task";
        goal = 15;
        progress = 0;
        description = "My final task for you is to bring me back " + goal + " Monguer's Axes. In case you haven't noticed, Some Monguers drop them after they die.";
        task = "Bring back " + goal + " Monguer's Axes to the class instructor.";
        rewardGold = 100;
        rewardExp = 20;
        completeText = "I have to admit, you truly are a great adventurer! As promised, I shall teach you about the classes and skills in this world.";
        //next = new ();
        id = 8;
    }

    public void reward() {
        base.reward();
    }

    public override void updateProgress() {
        Item item = playerScript.bag.getItem(new MonguerAxe());
        if (item != null) {
            if (item.quantity > goal) {
                progress = goal;
            } else {
                progress = item.quantity;
            }
        } else {
            progress = 0;
        }
    }
}
