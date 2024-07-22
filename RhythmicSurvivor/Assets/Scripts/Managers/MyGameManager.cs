using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    private int spawnSeconds = 10;
    private bool gamePause = true;
    private AudioManager audioManager;

    enum enemyType { skeleton, slime, shell, golem };

    // Start is called before the first frame update
    void Start()
    {
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
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("EnemySpawn");
        GameObject enemy = null;
        switch (random)
        {
            case (int)enemyType.golem:
                enemy = Instantiate(Resources.Load("EnemyGolem") as GameObject, spawnPoint.transform.position, spawnPoint.transform.rotation);
                enemy.GetComponent<Enemy>().setEnemyType((int)enemyType.golem);
                break;
            case (int)enemyType.slime:
                enemy = Instantiate(Resources.Load("EnemySlime") as GameObject, spawnPoint.transform.position, spawnPoint.transform.rotation);
                enemy.GetComponent<Enemy>().setEnemyType((int)enemyType.slime);
                break;
            case (int)enemyType.skeleton:
                enemy = Instantiate(Resources.Load("EnemySkeleton") as GameObject, spawnPoint.transform.position, spawnPoint.transform.rotation);
                enemy.GetComponent<Enemy>().setEnemyType((int)enemyType.skeleton);
                break;
            case (int)enemyType.shell:
                enemy = Instantiate(Resources.Load("EnemyShell") as GameObject, spawnPoint.transform.position, spawnPoint.transform.rotation);
                enemy.GetComponent<Enemy>().setEnemyType((int)enemyType.shell);
                break;
            default:
                break;
        }
    }

    public void restartSpawningEnemies()
    {
        StartCoroutine(spawningEnemies());
    }

    //setters
    public void setGamePause(bool status)
    {
        gamePause = status;
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
