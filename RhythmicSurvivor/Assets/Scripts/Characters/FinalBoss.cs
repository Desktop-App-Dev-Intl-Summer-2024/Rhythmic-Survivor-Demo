using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    private int currentHP;
    private int maxHP = 10000;
    private int biteDamage = 200;
    private int clawDamage = 500;
    private float clawAttackRange = 6;
    private float biteAttackRange = 9;
    private bool clawAttackOnCooldown = false;
    private bool biteAttackOnCooldown = false;
    private float clawAttackCooldown = 3;
    private float biteAttackCooldown = 1;
    private bool isAttackingWithBite = false;
    private bool isAttackingWithClaws = false;
    private bool isDead = false;

    private Player player;
    private AudioManager audioManager;
    private MyGameManager gameManager;
    private Animator animator;
    private HealthBarManager healthBarManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        
        player = FindObjectOfType<Player>();
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<MyGameManager>();
        animator = GetComponent<Animator>();
        healthBarManager = GetComponentInChildren<HealthBarManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGamePause() && !isDead && !player.getIsDead())
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance > biteAttackRange && audioManager.isHitBeat())
            {
                animator.Play("Walk");
                Vector3 dir = Vector3.Normalize(player.transform.position - transform.position);
                transform.position += new Vector3(dir.x * audioManager.getBPM(), 0f, dir.z * audioManager.getBPM());
                transform.rotation = Quaternion.LookRotation(dir);
            }
            else if(distance < biteAttackRange && distance > clawAttackRange && audioManager.isHitBeat() && !biteAttackOnCooldown)
            {
                isAttackingWithBite = true;  
                StartCoroutine(attackBite());
            }
            else if(distance < clawAttackRange && audioManager.isHitBeat() && !clawAttackOnCooldown)
            {
                StartCoroutine(attackClaws());
            }
        }
    }

    //combat
    public void getHit(Player player)
    {
        currentHP -= player.getDamage();
        healthBarManager.updateHealthBar(currentHP, maxHP);
        
        if(currentHP <= 0)
        {
            isDead = true;
            //player xp??
            animator.Play("Die");
            FindObjectOfType<GUIManager>().wonGame();
        }
    }

    //getters
    public int getClawDamage()
    {
        return clawDamage;
    }
    public int getBiteDamage()
    {
        return biteDamage;
    }
    public bool getIsAttackingWithBite()
    {
        return isAttackingWithBite;
    }
    public bool getIsAttackingWithClaws()
    {
        return isAttackingWithClaws;
    }

    //courutines
    IEnumerator attackClaws()
    {
        clawAttackOnCooldown = true;
        biteAttackOnCooldown = true;
        animator.Play("ClawAttack");
        Vector3 dir = Vector3.Normalize(player.transform.position - transform.position);
        transform.rotation = Quaternion.LookRotation(dir);
        yield return new WaitForSeconds(0.5f);
        isAttackingWithClaws = true;
        yield return new WaitForSeconds(clawAttackCooldown);
        clawAttackOnCooldown = false;
        biteAttackOnCooldown = false;
        isAttackingWithClaws = false;
    }

    IEnumerator attackBite()
    {
        clawAttackOnCooldown = true;
        biteAttackOnCooldown = true;
        animator.Play("BasicAttack");
        Vector3 dir = Vector3.Normalize(player.transform.position - transform.position);
        transform.rotation = Quaternion.LookRotation(dir);
        yield return new WaitForSeconds(biteAttackCooldown);
        clawAttackOnCooldown = false;
        biteAttackOnCooldown = false;
        isAttackingWithBite = false;
    }
}
