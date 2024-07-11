using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int maxHP;
    [SerializeField]
    private int currentHP;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float rangeAttack;
    [SerializeField]
    private float steps;
    private bool attackInCooldown = false;
    private bool isAttacking = false;
    private bool isDead = false;
    [SerializeField]
    private float attackTimeCooldown;
    private int type;

    private AudioManager audioManager;
    private MyGameManager gameManager;
    private Player player;
    private Animator animator;

    enum enemyType { skeleton, slime, shell, golem };

    private void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        gameManager = FindAnyObjectByType<MyGameManager>();
        player = FindAnyObjectByType<Player>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGamePause() && !isDead)
        {
            if(Vector3.Distance(transform.position, player.transform.position) > rangeAttack && audioManager.isHitBeat())
            {
                animator.Play("Walk");
                Vector3 dir = Vector3.Normalize(player.transform.position - transform.position);
                transform.position += new Vector3(dir.x * audioManager.getBPM() * steps, 0f, dir.z * audioManager.getBPM() * steps);
                transform.rotation = Quaternion.LookRotation(dir);
            }
            else if(Vector3.Distance(transform.position, player.transform.position) <  rangeAttack && audioManager.isHitBeat() && !attackInCooldown)
            {
                isAttacking = true;
                animator.Play("Attack");
                StartCoroutine(attackTime());

                if (type == (int)enemyType.golem)
                {
                    StartCoroutine(spawnRock());
                }
            }
        }
    }

    public void getHit(Player player)
    {
        animator.Play("GetHit");
        currentHP -= player.getDamage();

        if(currentHP <= 0)
        {
            StartCoroutine(death());
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

    public int getEnemyType()
    {
        return type;
    }

    public void setEnemyType(int enemytype)
    {
        switch (enemytype)
        {
            case (int)enemyType.skeleton:
                maxHP = 10;
                currentHP = maxHP;
                damage = 10;
                rangeAttack = 2f;
                steps = 2f;
                attackTimeCooldown = 1f;
                type = enemytype;
                GetComponentInChildren<EnemyAttack>().setEnemyInfo(this);
                break;
            case (int)enemyType.shell:
                maxHP = 20;
                currentHP = maxHP;
                damage = 15;
                rangeAttack = 2f;
                steps = 1.5f;
                attackTimeCooldown = 1f;
                type = enemytype;
                GetComponentInChildren<EnemyAttack>().setEnemyInfo(this);
                break;
            case (int)enemyType.slime:
                maxHP = 15;
                currentHP = maxHP;
                damage = 5;
                rangeAttack = 2f;
                steps = 1.5f;
                attackTimeCooldown = 1f;
                type = enemytype;
                GetComponentInChildren<EnemyAttack>().setEnemyInfo(this);
                break;
            case (int)enemyType.golem:
                maxHP = 30;
                currentHP = maxHP;
                damage = 30;
                rangeAttack = 15f;
                steps = 0.5f;
                attackTimeCooldown = 7f;
                type = enemytype;
                break;
            default:
                maxHP = 0;
                currentHP = maxHP;
                damage = 0;
                rangeAttack = 0f;
                steps = 0f;
                attackTimeCooldown = 0f;
                type = -1;
                break;
        }
    }

    IEnumerator spawnRock()
    {
        yield return new WaitForSeconds(1.7f);
        GameObject golemHand = GameObject.FindGameObjectWithTag("GolemAttackHand");
        GameObject rock = Instantiate(Resources.Load("GolemAttack") as GameObject, golemHand.transform.position, golemHand.transform.rotation);
        rock.GetComponent<EnemyAttack>().setEnemyInfo(this);
    }

    IEnumerator attackTime()
    {
        attackInCooldown = true;
        yield return new WaitForSeconds(attackTimeCooldown);
        attackInCooldown = false;
        isAttacking = false;
    }

    IEnumerator death()
    {
        isDead = true;
        animator.Play("Die");
        GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
