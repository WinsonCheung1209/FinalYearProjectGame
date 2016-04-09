using UnityEngine;
using System.Collections;

public class IntroMerchant : Quest {

    public IntroMerchant() {
        name = "Mayor's request 1";
        goal = 2;
        progress = 0;
        description = "I was battling the monsters outside the village but was badly injured by them. Please can you bring me 2 health potions? You can buy them from the merchant at (17, 46).";
        task = "Bring " + goal + " health potions to the mayor.";
        rewardGold = 500;
        rewardExp = 5;
        completeText = "Thank you! You must be the brave adventurer that the pixie told me about. I am the mayor of this village. You can find many people in this village that can help you in your adventure.";
        next = new IntroStorage();
        id = 3;
    }

    public void reward() {
        base.reward();
    }

    public override void updateProgress() {
        Item item = playerScript.bag.getItem(new HpPotion());
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
