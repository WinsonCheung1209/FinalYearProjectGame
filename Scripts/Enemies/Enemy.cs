using UnityEngine;
using System.Collections;

public abstract class Enemy : Character {

    protected Transform playerTransform;
    protected Player playerScript;
    public bool alerted;
    protected NavMeshAgent navAgent;
    protected Animator animator;
    protected float rotateSpeed;
    //protected float lookRange;
    protected float actionTimer;
    protected float dropExp;
    protected int dropGold;
    protected EnemySpawner spawn;
    protected Item[] dropList;
    public new string name;
    public float originMoveSpeed;
    public float resetSpeedTimer;
    GameObject alertPar;
    AlertParticles alertParScript;

    // Use this for initialization
    public new void Start() {
        base.Start();
        alertPar = this.transform.FindChild("AlertRange").gameObject;
        alertParScript = alertPar.GetComponent<AlertParticles>();
        alertParScript.enemy = this;
    }

    // Update is called once per frame
    public void Update() {
        if (playerScript.baseHp > 0) {
            if (alerted) {
                if (alertPar != null) {
                    Destroy(alertPar.gameObject);
                }
            }
            if (baseHp > 0) {
                recovering();
            }
            resetSpeedTimer -= Time.deltaTime;
            if (resetSpeedTimer <= 0) {
                moveSpeed = originMoveSpeed;
            }
            navAgent.speed = moveSpeed;
        }
    }

    public float getHp() {
        return this.baseHp;
    }

    public void setSpawner(EnemySpawner s) {
        spawn = s;
    }

    protected float distanceFromPlayer() {
        return Vector3.Distance(transformObject.position, playerTransform.position);
    }

    protected void facePlayer() {
        Vector3 oldAngle = transformObject.eulerAngles;
        transformObject.LookAt(playerTransform);
        float targetAngle = transformObject.eulerAngles.y;

        float finalFacingAngle = Mathf.MoveTowardsAngle(oldAngle.y, targetAngle, rotateSpeed * Time.deltaTime);
        transformObject.eulerAngles = new Vector3(0, finalFacingAngle, 0);
    }

    public virtual void damaged(float damage) {
        if (baseHp > 0) {
            alerted = true;
            if (baseHp > maxHp / 2 && baseHp - damage < maxHp / 2) {
                animator.SetBool("damage", true);
            }
            baseHp -= damage;
            GameEngine.ge.hpText = "Level " + charLevel + " " + name;// + ", hp: " + Mathf.RoundToInt(baseHp);
            GameEngine.ge.enemyHp = baseHp;
            GameEngine.ge.enemyMaxHp = maxHp;
            if (baseHp <= 0) {
                animator.SetBool("death", true);
                GameEngine.ge.hpText = "";
                GameEngine.ge.monsterLeft--;
                playerScript.score += dropGold;
            }
        }
    }

    private void recovering() {
        baseHp += recoverHp * Time.deltaTime;
        if (baseHp > maxHp) {
            baseHp = maxHp;
        } else {
            GameEngine.ge.hpText = "Level " + charLevel + " " + name;
            GameEngine.ge.enemyHp = baseHp;
            GameEngine.ge.enemyMaxHp = maxHp;
        }
    }

    public abstract void updateStats(int level);
}
