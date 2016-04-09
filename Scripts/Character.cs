using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public Transform transformObject;
    public float moveSpeed = 10;
    public float gravity = 20;
    public int charLevel;
    public float maxHp;
    public float baseHp;
    public float maxMp;
    public float mp;
    public float baseAttack;
    public float attackRange;
    public float recoverHp;

    // Use this for initialization
    public void Start() {
        transformObject = this.transform;
    }




}
