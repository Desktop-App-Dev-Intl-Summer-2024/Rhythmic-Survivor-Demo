using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Enemy enemyInfo;
    private AudioManager audioManager;
    private MyGameManager gameManager;
    private Player player;

    private Vector3 dir;

    enum enemyType { skeleton, slime, shell, golem };

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        gameManager = FindAnyObjectByType<MyGameManager>();
        player = FindAnyObjectByType<Player>();

        dir = Vector3.Normalize(player.transform.position - transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyInfo.getEnemyType() == (int)enemyType.golem && audioManager.isHitBeat() && !gameManager.isGamePause())
        {
            transform.position += new Vector3(dir.x * audioManager.getBPM() * 2, dir.y * audioManager.getBPM(), dir.z * audioManager.getBPM() * 2);

            if(transform.position.y <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void setEnemyInfo(Enemy enemy)
    {
        enemyInfo = enemy;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player && !player.getIsRecovering() && !player.getIsDead() && enemyInfo.getIsAttacking())
        {
            player.playerGetHit(enemyInfo);
            if(enemyInfo.getEnemyType() == (int)enemyType.golem)
            {
                Destroy(gameObject);
            }
        }
    }
}
