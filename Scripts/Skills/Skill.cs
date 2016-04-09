using UnityEngine;
using System.Collections;

public abstract class Skill {

    public string name;
    protected string description;
    public int level;
    protected float damageRate;
    protected float coolDown;
    public int mpCost;
    public float cdTimer;

    public abstract void use();

    public abstract string toString(int level);

    public abstract void updateStat();
}
