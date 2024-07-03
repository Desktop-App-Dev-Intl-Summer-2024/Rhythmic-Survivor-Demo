using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float rangeAttack;
    private float steps;

    private bool isWalking;

    AudioManager audioManager;
    MyGameManager gameManager;
    Player player;

    enum enemyType { skeleton, slime, shell, golem };

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<AudioManager>();
        gameManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<MyGameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rangeAttack = 1f;
        steps = 1f;
        isWalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGamePause())
        {
            if(Vector3.Distance(transform.position, player.transform.position) > rangeAttack && audioManager.isHitBeat())
            {
                Vector3 dir = Vector3.Normalize(transform.position - player.transform.position);
                transform.position -= new Vector3(dir.x * audioManager.getBPM() * steps, 0f, dir.z * audioManager.getBPM() * steps);
                //Vector3 step = transform.position - new Vector3(dir.x * steps, 0f, dir.z * steps);
                //transform.position = Vector3.Lerp(transform.position, step, audioManager.getBPM() * steps);
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }
        }
    }

    public void setEnemyType(int type)
    {
        switch (type)
        {
            case (int)enemyType.skeleton:
                rangeAttack = 1f;
                steps = 2f;
                break;
            case (int)enemyType.shell:
                rangeAttack = 0.5f;
                steps = 1.5f;
                break;
            case (int)enemyType.slime:
                rangeAttack = 0.5f;
                steps = 1.5f;
                break;
            case (int)enemyType.golem:
                rangeAttack = 5f;
                steps = 0.5f;
                break;
            default:
                break;
        }
    }
}
