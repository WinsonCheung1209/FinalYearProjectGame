using UnityEngine;
using System.Collections;

public class Monguer : Enemy {

    public AudioSource audioSource;

    // Use this for initialization
    public new void Start() {
        base.Start();
        originMoveSpeed = 5;
        moveSpeed = originMoveSpeed;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = playerTransform.GetComponent<Player>();
        alerted = false;
        navAgent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        rotateSpeed = 1200;
        //lookRange = 15;
        actionTimer = 0.5f;
        attackRange = 7;
        dropList = new Item[7];
        name = "Monguer";

        dropList[0] = new MonguerAxe();
        dropList[1] = new HpPotion();
        dropList[2] = new MpPotion();
        dropList[3] = new VillageStone();
        dropList[4] = new TridentStone();
        dropList[5] = new LevelUpStone();
        dropList[6] = new ClassStone();
        audioSource = this.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    public new void Update() {
        if (playerScript.baseHp > 0) {
            base.Update();
            if (alerted) {

                AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

                if (info.fullPathHash == Animator.StringToHash("Base Layer.idle") && !animator.IsInTransition(0)) {
                    animator.SetBool("idle", false);
                    actionTimer -= Time.deltaTime;
                    if (actionTimer <= 0) {
                        if (distanceFromPlayer() <= attackRange) {
                            animator.SetBool("attack", true);
                        } else {
                            actionTimer = 0.5f;
                            navAgent.SetDestination(playerTransform.position);
                            animator.SetBool("walk", true);
                        }
                    }
                }
                if (info.fullPathHash == Animator.StringToHash("Base Layer.walk") && !animator.IsInTransition(0)) {
                    animator.SetBool("walk", false);
                    actionTimer -= Time.deltaTime;
                    if (actionTimer < 0) {

                        navAgent.SetDestination(playerTransform.position);
                        actionTimer = 0.5f;
                    }
                    navAgent.Move(transformObject.TransformDirection((new Vector3(0, 0, moveSpeed * Time.deltaTime))));
                    if (distanceFromPlayer() <= attackRange) {
                        navAgent.ResetPath();
                        animator.SetBool("attack", true);
                    }
                }
                if (info.fullPathHash == Animator.StringToHash("Base Layer.attack") && !animator.IsInTransition(0)) {
                    facePlayer();
                    animator.SetBool("attack", false);
                    if (info.normalizedTime >= 0.4f) {
                        playerScript.damaged(baseAttack + Random.value * baseAttack / 10);
                        animator.SetBool("idle", true);
                        actionTimer = 0.5f;
                        //print(info.normalizedTime);
                    }
                }
                if (info.fullPathHash == Animator.StringToHash("Base Layer.damage") && !animator.IsInTransition(0)) {
                    animator.SetBool("damage", false);
                    moveSpeed = 0;
                    if (info.normalizedTime >= 1) {
                        animator.SetBool("idle", true);
                        actionTimer = 0.5f;
                        moveSpeed = originMoveSpeed;
                    }

                }
                if (info.fullPathHash == Animator.StringToHash("Base Layer.death") && !animator.IsInTransition(0)) {
                    //animator.SetBool("death", false);
                    if (!audioSource.isPlaying) {
                        audioSource.Play();
                        //print(info.normalizedTime);
                    }
                    //
                    if (info.normalizedTime >= 1) {
                        if (spawn != null) {
                            spawn.spawned--;
                        }
                        playerScript.gainExp(dropExp, false);
                        playerScript.gainGold(Mathf.RoundToInt(Random.Range(dropGold, dropGold * 2) * (playerScript.luck + 100.0f) / 100.0f), true);
                        float drop = Random.value;
                        if (drop < 0.8) {
                            float dropItem = Random.value;
                            if (dropItem <= 0.02) {
                                playerScript.bag.insertItem(dropList[3], false, true);
                            } else if (dropItem <= 0.04) {
                                playerScript.bag.insertItem(dropList[4], false, true);
                            } else if (dropItem <= 0.05) {
                                playerScript.bag.insertItem(dropList[5], false, true);
                            } else if (dropItem <= 0.06) {
                                playerScript.bag.insertItem(dropList[6], false, true);
                            } else if (dropItem <= 0.75) {
                                playerScript.bag.insertItem(dropList[0], false, true);
                            } else if (dropItem <= 0.9) {
                                playerScript.bag.insertItem(dropList[1], false, true);
                            } else {
                                playerScript.bag.insertItem(dropList[2], false, true);
                            }
                        }
                        for (int i = 0; i < playerScript.activeQuests.Length; i++) {
                            Quest q = playerScript.activeQuests[i];
                            if (q != null && (q.name == "Tutorial 1" || q.name == "Eliminating threats")) {
                                if (q.progress < q.goal) {
                                    q.progress++;
                                }
                            }
                        }
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }

    public override void damaged(float damage) {
        base.damaged(damage);
        GameObject text = (GameObject) Instantiate(Resources.Load("DamagedText"), transformObject.position + new Vector3(-0.5f, 3, 0), playerScript.transformObject.rotation);
        text.GetComponent<TextMesh>().text = "" + damage;
    }

    public override void updateStats(int level) {
        charLevel = level;
        maxHp = level * level + 5 * level;
        baseHp = maxHp;
        baseAttack = level * level / 2 + level;
        dropExp = level * 2 - 1;
        dropGold = level * 2 - 1;
        recoverHp = level / 10 + level;
    }
}
