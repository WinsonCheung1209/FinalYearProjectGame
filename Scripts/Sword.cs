using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

    Animator animator;
    Transform playerTransform;
    Transform camTransform;
    Player playerScript;
    float attackTimer = 0;
    public LayerMask layers;
    public AudioClip swingClip;
    public AudioSource audioSource;

    // Use this for initialization
    void Start() {
        animator = this.GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = playerTransform.GetComponent<Player>();
        camTransform = Camera.main.transform;
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (playerScript.getHp() > 0) {
            attackTimer -= Time.deltaTime;
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (info.fullPathHash == Animator.StringToHash("Base Layer.attack") && !animator.IsInTransition(0)) {
                animator.SetBool("attack", false);
            }

            if (Input.GetMouseButtonDown(0) && attackTimer <= 0) {
                animator.SetBool("attack", true);
                audioSource.PlayOneShot(swingClip);
                attackTimer = 0.6f;
                RaycastHit hitInfo;
                bool hit = Physics.Raycast(camTransform.position, camTransform.TransformDirection(Vector3.forward), out hitInfo, playerScript.attackRange, layers);
                if (hit) {
                    if (hitInfo.transform.tag.Equals("Enemy")) {
                        Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                        //print(enemy.baseHp);
                        if (enemy.baseHp > 0) {
                            enemy.damaged(Random.Range(playerScript.calMinAttack(), playerScript.calMaxAttack()));
                        }
                    } else if (GameEngine.ge.showPanel == 0) {
                        if (hitInfo.transform.tag.Equals("NPC")) {
                            NPC npc = hitInfo.transform.GetComponent<NPC>();
                            if (npc != null) {
                                npc.interact();
                            }
                        } else if (hitInfo.transform.tag.Equals("Bank")) {
                            GameEngine.ge.changePanel(9);
                        } else if (hitInfo.transform.tag.Equals("chest")) {
                            HpPotion hpPotion = new HpPotion();
                            hpPotion.quantity = 5;
                            MpPotion mpPotion = new MpPotion();
                            mpPotion.quantity = 5;
                            
                            if (Random.value > 0.5) {
                                Item rareItem;
                                float rare = Random.value;
                                if (rare <= 0.15) {
                                    rareItem = new ClassStone();
                                } else if (rare <= 0.30) {
                                    rareItem = new LevelUpStone();
                                } else if (rare <= 0.65) {
                                    rareItem = new TridentStone();
                                } else {
                                    rareItem = new VillageStone();
                                }
                                playerScript.bag.insertItem(rareItem, true, true);
                            }

                            playerScript.bag.insertItem(hpPotion, true, true);
                            playerScript.bag.insertItem(mpPotion, true, true);
                            Destroy(hitInfo.transform.gameObject);
                        }
                    }
                }
            }
        }

    }
}
