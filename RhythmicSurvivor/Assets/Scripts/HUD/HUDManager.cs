using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    //ingame variables
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject clockImage;
    [SerializeField] private TextMeshProUGUI hitCounterText;
    [SerializeField] private GameObject x1Combo;
    [SerializeField] private GameObject x2Combo;
    [SerializeField] private GameObject x3Combo;
    private float time;
    private Vector3 startScale;
    private float scaleMultiplier = 2f;

    //levelup
    [SerializeField] private GameObject levelUpPanel;

    private MyGameManager gameManager;
    private AudioManager audioManager;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        startScale = clockImage.transform.localScale;

        gameManager = FindObjectOfType<MyGameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGamePause())
        {
            updateTimer();
            updateClockScale();
            updateMultiplier();
        }
    }

    //ingame functions
    private void updateTimer()
    {
        time += Time.deltaTime;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void updateClockScale()
    {
        if (audioManager.isHitBeat())
        {
            clockImage.transform.localScale = startScale * scaleMultiplier;
        }
        else
        {
            clockImage.transform.localScale = Vector3.Lerp(clockImage.transform.localScale, startScale, Time.deltaTime * scaleMultiplier);
        }
    }

    private void updateMultiplier()
    {
        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }

        int hitCount = player.getHitCount();
        hitCounterText.text = hitCount.ToString();

        switch (hitCount / 5)
        {
            case 0:
                break;
            case 1:
                x1Combo.SetActive(true);
                x2Combo.SetActive(false);
                x3Combo.SetActive(false);
                break;
            case 2:
                x1Combo.SetActive(false);
                x2Combo.SetActive(true);
                x3Combo.SetActive(false);
                break;
            case 3:
                x1Combo.SetActive(false);
                x2Combo.SetActive(false);
                x3Combo.SetActive(true);
                break;
            default:
                x1Combo.SetActive(false);
                x2Combo.SetActive(false);
                x3Combo.SetActive(true);
                break;
        }
    }

    //levelup
    public void showLevelUpPanel()
    {
        levelUpPanel.SetActive(true);
    }

    public void upgradeDamage()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

        player.upgradeDamage();
        levelUpPanel.SetActive(false);
        gameManager.setGamePause(false);
        gameManager.restartSpawningEnemies();
    }

    public void upgradeHealth()
    {
        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }

        player.upgradeHealth();
        levelUpPanel.SetActive(false);
        gameManager.setGamePause(false);
        gameManager.restartSpawningEnemies();
    }
}
