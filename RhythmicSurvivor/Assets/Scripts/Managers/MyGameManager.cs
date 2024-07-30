using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    private int spawnSeconds = 10;
    private bool gamePause = false;

    private bool firstBossSpawn = false;

    private AudioManager audioManager;

    public PlayerData currentGame { get; private set; }
    public List<PlayerData> gamesData { get; set; }

    enum enemyType { skeleton, slime, shell, golem };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //game functions
    public void startGame()
    {
        gamePause = false;
        StartCoroutine(spawningEnemies());
    }
    public void restartGame()
    {
        foreach(Enemy enemy in FindObjectsOfType<Enemy>())
        {
            Destroy(enemy.gameObject);
        }

        GameObject spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
        Player player = FindObjectOfType<Player>();
        player.gameObject.transform.position = spawnPoint.transform.position;
        player.setLevels(currentGame.damageLevel, currentGame.healthLevel);
        player.initializePlayer(currentGame.damageLevel, currentGame.healthLevel);

        gamePause = false;
        StartCoroutine(spawningEnemies());
    }
    public void returnMainMenu()
    {
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            Destroy(enemy.gameObject);
        }

        GameObject spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
        Player player = FindObjectOfType<Player>();
        player.gameObject.transform.position = spawnPoint.transform.position;
        player.setLevels(currentGame.damageLevel, currentGame.healthLevel);
        player.initializePlayer(currentGame.damageLevel, currentGame.healthLevel);
    }

    //spawn Player
    public void characterSelected(int slotSelected, int character)
    {
        gamesData[slotSelected].character = character;
        currentGame = gamesData[slotSelected];

        switch (character)
        {
            case 1:
                spawnCharacter1(slotSelected);
                break;
            case 2:
                spawnCharacter2(slotSelected);
                break;
        }
    }
    public void spawnPlayer(int slotSelected)
    {
        currentGame = gamesData[slotSelected];
        switch (currentGame.character)
        {
            case 1:
                spawnCharacter1(slotSelected);
                break;
            case 2:
                spawnCharacter2(slotSelected);
                break;
        }
    }
    public void spawnCharacter1(int slotSelected)
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
        GameObject player = Instantiate(Resources.Load("PlayerCharacter1") as GameObject, spawnPoint.transform.position, spawnPoint.transform.rotation);
        player.GetComponent<Player>().initializePlayer(currentGame.damageLevel, currentGame.healthLevel);
    }

    public void spawnCharacter2(int slotSelected)
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn");
        GameObject player = Instantiate(Resources.Load("PlayerCharacter2") as GameObject, spawnPoint.transform.position, spawnPoint.transform.rotation);
        player.GetComponent<Player>().initializePlayer(currentGame.damageLevel, currentGame.healthLevel);
    }

    //spawn Enemy
    private void spawnEnemy()
    {
        int typeRandom = Random.Range(0, 4);
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
        int indexSpawn = Random.Range(0, spawns.Length);
        GameObject enemy = null;
        switch (typeRandom)
        {
            case (int)enemyType.golem:
                enemy = Instantiate(Resources.Load("EnemyGolem") as GameObject, spawns[indexSpawn].transform.position, spawns[indexSpawn].transform.rotation);
                enemy.GetComponent<Enemy>().setEnemyType((int)enemyType.golem);
                break;
            case (int)enemyType.slime:
                enemy = Instantiate(Resources.Load("EnemySlime") as GameObject, spawns[indexSpawn].transform.position, spawns[indexSpawn].transform.rotation);
                enemy.GetComponent<Enemy>().setEnemyType((int)enemyType.slime);
                break;
            case (int)enemyType.skeleton:
                enemy = Instantiate(Resources.Load("EnemySkeleton") as GameObject, spawns[indexSpawn].transform.position, spawns[indexSpawn].transform.rotation);
                enemy.GetComponent<Enemy>().setEnemyType((int)enemyType.skeleton);
                break;
            case (int)enemyType.shell:
                enemy = Instantiate(Resources.Load("EnemyShell") as GameObject, spawns[indexSpawn].transform.position, spawns[indexSpawn].transform.rotation);
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
    public void setFirstBossSpawn(bool status)
    {
        firstBossSpawn = status;
    }

    //getters
    public bool isGamePause()
    {
        return gamePause;
    }
    public bool isFirstBossSpawn()
    {
        return firstBossSpawn;
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
