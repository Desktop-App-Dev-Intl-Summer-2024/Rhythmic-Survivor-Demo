using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Enemy enemyInfo;
    private AudioManager audioManager;
    private Player player;

    private Vector3 dir;

    enum enemyType { skeleton, slime, shell, golem };

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        player = FindAnyObjectByType<Player>();
        dir = Vector3.Normalize(player.transform.position - transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyInfo.getEnemyType() == (int)enemyType.golem && audioManager.isHitBeat())
        {
            float time = audioManager.getBPM();
            float y = dir.y + (1.5f * Mathf.Sin(30) * time) - (0.5f * -9.8f * Mathf.Pow(time, 2));
            transform.position += new Vector3(dir.x * audioManager.getBPM(), -y, dir.z * audioManager.getBPM());

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
