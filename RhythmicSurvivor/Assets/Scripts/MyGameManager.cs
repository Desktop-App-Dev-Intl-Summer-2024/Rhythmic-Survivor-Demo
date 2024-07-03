using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    private int spawnSeconds;

    private bool gamePause;
    AudioManager audioManager;

    enum enemyType { skeleton, slime, shell, golem };

    // Start is called before the first frame update
    void Start()
    {
        gamePause = false;
        spawnSeconds = 10;
        spawnCharacter1();
        StartCoroutine(spawningEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        if (!gamePause)
        {

        }
    }

    //spawn Player
    public void spawnCharacter1()
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
        Instantiate(Resources.Load("PlayerCharacter1") as GameObject, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    //spawn Enemy
    private void spawnEnemy()
    {
        int random = Random.Range(0, 4);
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("EnemySpawn1");
        GameObject enemy = null;
        switch (random)
        {
            case 0:
                enemy = Instantiate(Resources.Load("EnemyGolem") as GameObject, spawnPoint.transform.position, spawnPoint.transform.rotation);
                enemy.GetComponent<Enemy>().setEnemyType((int)enemyType.golem);
                break;
            case 1:
                enemy = Instantiate(Resources.Load("EnemySlime") as GameObject, spawnPoint.transform.position, spawnPoint.transform.rotation);
                enemy.GetComponent<Enemy>().setEnemyType((int)enemyType.slime);
                break;
            case 2:
                enemy = Instantiate(Resources.Load("EnemySkeleton") as GameObject, spawnPoint.transform.position, spawnPoint.transform.rotation);
                enemy.GetComponent<Enemy>().setEnemyType((int)enemyType.skeleton);
                break;
            case 3:
                enemy = Instantiate(Resources.Load("EnemyShell") as GameObject, spawnPoint.transform.position, spawnPoint.transform.rotation);
                enemy.GetComponent<Enemy>().setEnemyType((int)enemyType.shell);
                break;
            default:
                break;
        }
    }

    //getters
    public bool isGamePause()
    {
        return gamePause;
    }

    //courotines
    IEnumerator spawningEnemies()
    {
        while (!gamePause)
        {
            spawnEnemy();
            yield return new WaitForSeconds(spawnSeconds);
        }
    }
}
