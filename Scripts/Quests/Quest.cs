using UnityEngine;
using System.Collections;

public abstract class Quest {

    protected Player playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    public string name;
    public int goal;
    public int progress;
    public string description;
    public string task;
    public int rewardGold;
    public int rewardExp;
    public Item[] rewardItemList;
    public string completeText;
    public Quest next;
    public int id;

    public void reward() {
        playerScript.gainExp(rewardExp, true);
        playerScript.gainGold(rewardGold, true);
        if (rewardItemList != null) {
            foreach (Item i in rewardItemList) {
                playerScript.bag.insertItem(i, true, true);
            }
        }
        if (next != null) {
            playerScript.activeQuests[0] = next;
            playerScript.activeQuests[0].updateProgress();
        } else {
            playerScript.activeQuests[0] = null;
        }
    }

    public abstract void updateProgress();
}
