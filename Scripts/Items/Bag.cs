using UnityEngine;
using System.Collections;

public class Bag {

    Item[] items;
    public int size;
    public bool isBank = false;
    Player playerScript;

    public Bag() {
        items = new Item[20];
        size = 0;
        //playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public Bag(bool player) {
        items = new Item[20];
        size = 0;
        for (int i = 0; i < 5; i++) {
            insertItem(new HpPotion(), true, false);
            insertItem(new MpPotion(), true, false);
        }
        //items[0].quantity = 5;

        //items[1].quantity = 5;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public Item[] getItems() {
        return items;
    }

    public void insertItem(Item item, bool accumQuant, bool display) {
        bool itemAdded = false;
        if (contains(item)) { // already contained this item
            int index = indexOf(item);
            if (accumQuant) {
                items[index].quantity += item.quantity;
            } else {
                items[index].quantity++;
            }
            itemAdded = true;
        } else if (size < items.Length) { // adding new item
            //MonoBehaviour.Destroy(item.texture);
            items[size] = item.clone();
            if (accumQuant) {
                items[size].quantity += item.quantity - 1;
            } else {
                items[size].quantity = 1;
            }
            items[size].initTexture();
            //MonoBehaviour.print("Item: " + item.name + " , is Bank: " + isBank + ", is player: " + playerScript);
            MonoBehaviour.DontDestroyOnLoad(items[size].texture);
            size++;
            sortItemTextures();
            itemAdded = true;
        }
        if (itemAdded) {
            if (display) {
                string displayText;
                if (accumQuant) {
                    displayText = "Obtained " + item.quantity + " " + item.name + "s";
                } else {
                    displayText = "Obtained 1 " + item.name;
                }
                GameEngine.ge.setText(GameEngine.ge.getText() + displayText + "\n");
            }
            updateProgress();
        } else {
            GameEngine.ge.setText(GameEngine.ge.getText() + "Your bag is full!\n");
        }
    }

    public void updateProgress() {
        if (playerScript != null) {
            Quest curQuest = playerScript.activeQuests[0];
            if (curQuest != null) {
                curQuest.updateProgress();
            }
        }
    }

    public Item removeItem(int ithItem) {
        Item item = items[ithItem];
        int quantity = --items[ithItem].quantity;
        updateProgress();
        if (quantity <= 0) {
            MonoBehaviour.Destroy(items[ithItem].texture);
            for (int i = ithItem; i < size - 1; i++) {
                items[i] = items[i + 1];
            }
            items[size - 1] = null;
            size--;
            sortItemTextures();
        }
        return item;
    }

    public void removeItem(Item item) {
        int index = indexOf(item);
        if (index != -1) {
            removeItem(index);
        }
    }

    public Item getItem(int ithItem) {
        return items[ithItem];
    }

    public Item getItem(Item item) {
        if (contains(item)) {
            return items[indexOf(item)];
        } else {
            return null;
        }
    }

    public int indexOf(Item item) {
        int i = 0;
        int found = -1;
        while (found == -1 && i < size) {
            if (items[i].name.Equals(item.name)) {
                found = i;
            }
            i++;
        }
        return found;
    }

    public bool contains(Item item) {
        return indexOf(item) != -1;
    }

    public void sortItemTextures() {
        for (int i = 0; i < size; i++) {
            int row = i / 5;
            //items[i].texture.SetActive(true);
            int xPos = (i % 5) * 48 + 31;
            if (isBank) {
                xPos += 245;
            }
            items[i].texture.GetComponent<GUITexture>().pixelInset = new Rect(xPos, (3 - row) * 48 + 208, 47, 47);
        }
    }
}
