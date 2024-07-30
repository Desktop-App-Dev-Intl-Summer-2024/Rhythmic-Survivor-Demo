using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossAttack : MonoBehaviour
{
    [SerializeField] private int attackType;
    private FinalBoss boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponentInParent<FinalBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if(player && !player.getIsRecovering() && !player.getIsDead() && boss.getIsAttackingWithBite())
        {
            player.playerGetHit(boss, attackType);
        }
        else if(player && !player.getIsRecovering() && !player.getIsDead() && boss.getIsAttackingWithClaws())
        {
            player.playerGetHit(boss, attackType);
        }
    }
}
