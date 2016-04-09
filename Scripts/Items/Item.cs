using UnityEngine;
using System.Collections;

public abstract class Item {

    public string name;
    public int value;
    public int quantity;
    public string description;
    protected Player playerScript;
    public GameObject texture;
    public bool usable;

    public abstract bool use();

    public virtual void initTexture() {
        texture = new GameObject();
        texture.transform.position = new Vector3(0, 0, 100);
        texture.transform.localScale = new Vector3(0, 0, 1);
        texture.AddComponent<GUITexture>();
        texture.layer = LayerMask.NameToLayer("HideFromMiniMap");
    }

    public abstract Item clone();

}
