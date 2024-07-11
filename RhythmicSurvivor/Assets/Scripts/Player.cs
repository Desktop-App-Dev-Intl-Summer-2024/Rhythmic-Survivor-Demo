using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int maxHP;
    private int currentHP;
    private int damage;
    private bool isAttacking = false;
    private bool isRecovering = false;
    private bool isDead = false;

    private Animator animator;
    private CinemachineVirtualCamera camera;
    // Start is called before the first frame update
    void Start()
    {
        maxHP = 100;
        currentHP = maxHP;
        damage = 10;

        animator = GetComponent<Animator>();
        camera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        camera.Follow = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.Play("JumpNormal");
            Vector3 dir = new Vector3(-1f, 0f, 0f);
            Quaternion look = Quaternion.AngleAxis(270, transform.up);
            transform.rotation = look;
            transform.position += dir;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            animator.Play("JumpNormal");
            Vector3 dir = new Vector3(1f, 0f, 0f);
            Quaternion look = Quaternion.AngleAxis(90, transform.up);
            transform.rotation = look;
            transform.position += dir;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            animator.Play("JumpNormal");
            Vector3 dir = new Vector3(0f, 0f, 1f);
            Quaternion look = Quaternion.AngleAxis(0, transform.up);
            transform.rotation = look;
            transform.position += dir;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            animator.Play("JumpNormal");
            Vector3 dir = new Vector3(0f, 0f, -1f);
            Quaternion look = Quaternion.AngleAxis(180, transform.up);
            transform.rotation = look;
            transform.position += dir;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.Play("Attack01");
            StopCoroutine(attackTime());
            StartCoroutine(attackTime());
        }
    }

    public void playerGetHit(Enemy enemyInfo)
    {
        StartCoroutine(getHit());
        currentHP -= enemyInfo.getDamage();

        if(currentHP <= 0)
        {
            isDead = true;
            animator.Play("Die01");
        }
    }

    public int getDamage()
    {
        return damage;
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
}
