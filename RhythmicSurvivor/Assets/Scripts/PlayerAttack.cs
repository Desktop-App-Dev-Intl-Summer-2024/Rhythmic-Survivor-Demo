using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = FindObjectOfType<Player>();
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy && player.getIsAttacking())
        {
            enemy.getHit(player);
        }
    }
}
