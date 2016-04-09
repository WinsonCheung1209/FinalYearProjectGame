using UnityEngine;
using System.Collections;

public class Tut1 : Quest {

    public GameObject portal;

    public Tut1() {
        name = "Tutorial 1";
        goal = 1;
        progress = 0;
        description = "Press WASD on your keyboard to move and move your mouse to look around. Click on your left-mouse button to attack enemies or interact with NPCs. In this world you will need to fight monsters and complete various quests to become stronger.\nSee that Monster over there? If you get too close it will start attacking you. Talk to me again after you have defeated it.";
        task = "Kill 1 Monguer then talk to pixie";
        rewardGold = 5;
        rewardExp = 5;
        completeText = "Well done! Killing an enemy will earn you exp and gold. The stronger the enemies, the more exp and gold you get. Here are your rewards for completing this quest: " + rewardGold + " gold, " + rewardExp + " exp.";
        next = new DeliverMessage();
        id = 1;
    }

    public void reward() {
        base.reward();
    }

    public override void updateProgress() {

    }
}
