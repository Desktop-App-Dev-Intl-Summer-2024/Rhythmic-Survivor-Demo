using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int currentMaxHP;
    private int baseMaxHp = 100;
    private int currentHP;
    private int currentDamage;
    private int baseDamage = 10;
    private int hitCount = 0;
    private int damageLevel = 0;
    private int healthLevel = 0;
    private int actualExp = 0;
    private int requireExp = 100;
    private bool isAttacking = false;
    private bool isRecovering = false;
    private bool isDead = false;

    private Animator animator;
    private CinemachineVirtualCamera camera;
    private HealthBarManager healthBarManager;

    private AudioManager audioManager;
    private MyGameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        currentMaxHP = baseMaxHp;
        currentHP = currentMaxHP;
        currentDamage = baseDamage;

        animator = GetComponent<Animator>();
        camera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        camera.Follow = transform;
        healthBarManager = GetComponentInChildren<HealthBarManager>();

        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<MyGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGamePause() && !isDead)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                checkFollowBeat();
                animator.Play("JumpNormal");
                Vector3 dir = new Vector3(-1f, 0f, 0f);
                Quaternion look = Quaternion.AngleAxis(270, transform.up);
                transform.rotation = look;
                transform.position += dir;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                checkFollowBeat();
                animator.Play("JumpNormal");
                Vector3 dir = new Vector3(1f, 0f, 0f);
                Quaternion look = Quaternion.AngleAxis(90, transform.up);
                transform.rotation = look;
                transform.position += dir;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                checkFollowBeat();
                animator.Play("JumpNormal");
                Vector3 dir = new Vector3(0f, 0f, 1f);
                Quaternion look = Quaternion.AngleAxis(0, transform.up);
                transform.rotation = look;
                transform.position += dir;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                checkFollowBeat();
                animator.Play("JumpNormal");
                Vector3 dir = new Vector3(0f, 0f, -1f);
                Quaternion look = Quaternion.AngleAxis(180, transform.up);
                transform.rotation = look;
                transform.position += dir;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                checkFollowBeat();
                animator.Play("Attack01");
                StopCoroutine(attackTime());
                StartCoroutine(attackTime());
            }
        }
    }

    //multiplier
    private void checkFollowBeat()
    {
        if (audioManager.isHitBeat())
        {
            hitCount++;
        }
    }

    //combat
    public void playerGetHit(Enemy enemyInfo)
    {
        StartCoroutine(getHit());
        currentHP -= enemyInfo.getDamage();
        healthBarManager.updateHealthBar(currentHP, currentMaxHP);

        if(currentHP <= 0)
        {
            isDead = true;
            animator.Play("Die01");
        }
    }

    public void addEnemyExp(int exp)
    {
        int hitMultiplier = hitCount / 5;
        if (hitMultiplier > 3)
        {
            hitMultiplier = 3;
        }

        actualExp += exp * hitMultiplier;
        if(actualExp >= requireExp)
        {
            StartCoroutine(levelUp());
        }
    }

    //upgrade functions
    public void upgradeDamage()
    {
        damageLevel++;
        currentDamage = baseDamage + damageLevel;
        Debug.Log("damage " + currentDamage);
    }

    public void upgradeHealth()
    {
        healthLevel++;
        currentMaxHP = baseMaxHp + healthLevel;
        Debug.Log("health " + currentMaxHP);
    }

    //getters
    public int getDamage()
    {
        return currentDamage;
    }

    public int getHitCount()
    {
        return hitCount;
    }

    public bool getIsAttacking()
    {
        return isAttacking;
    }

    public bool getIsRecovering()
    {
        return isRecovering;
    }

    public bool getIsDead()
    {
        return isDead;
    }

    //courutines
    IEnumerator attackTime()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }

    IEnumerator getHit()
    {
        animator.Play("GetHit");
        isRecovering = true;
        while (isRecovering)
        {
            transform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(2f);
            transform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
            isRecovering = false;
        }
    }

    IEnumerator levelUp()
    {
        gameManager.setGamePause(true);
        animator.Play("LevelUp");
        actualExp = actualExp % requireExp;
        yield return new WaitForSeconds(2f);
        FindObjectOfType<HUDManager>().showLevelUpPanel();
    }
}
