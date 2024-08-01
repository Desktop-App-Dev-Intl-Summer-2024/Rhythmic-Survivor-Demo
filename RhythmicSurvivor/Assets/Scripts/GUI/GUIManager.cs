using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Text;

public class GUIManager : MonoBehaviour
{
    //login screen
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject signUpPanel;

    //saves screen
    [SerializeField] private GameObject saveSlotsPanel;
    [SerializeField] private GameObject deleteButton;
    [SerializeField] private GameObject loadButton;
    [SerializeField] private Sprite character1Face;
    [SerializeField] private Sprite character2Face;
    private int slotSelected = -1;

    //character selection screen
    [SerializeField] private GameObject characterSelectionPanel;

    //main menu
    [SerializeField] private GameObject mainMenuPanel;

    //ingame hud
    [SerializeField] private GameObject inGameHUD;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject clockImage;
    [SerializeField] private TextMeshProUGUI hitCounterText;
    [SerializeField] private GameObject x1Combo;
    [SerializeField] private GameObject x2Combo;
    [SerializeField] private GameObject x3Combo;
    [SerializeField] private GameObject pausePanel;
    private float time;
    private Vector3 startScale;
    private float scaleMultiplier = 2f;

    //levelup screen
    [SerializeField] private GameObject levelUpPanel;

    //victory screen
    [SerializeField] private GameObject victoryPanel;

    //defeat screen
    [SerializeField] private GameObject defeatPanel;

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

    //login functions
    public void openSignUpPanelButton()
    {
        signUpPanel.SetActive(true);
    }

    public void signUpButton()
    {
        string nickName = signUpPanel.transform.GetChild(1).GetComponent<TMP_InputField>().text;
        string email = signUpPanel.transform.GetChild(2).GetComponent<TMP_InputField>().text;
        string password = signUpPanel.transform.GetChild(3).GetComponent<TMP_InputField>().text;

        PlayerData playerData = new PlayerData();
        playerData.nickName = nickName;
        playerData.email = email;
        playerData.password = password;

        StartCoroutine(signUpPlayer(playerData));
    }

    public void returnLoginPanel()
    {
        signUpPanel.SetActive(false);
    }

    public void loginButton()
    {
        string email = loginPanel.transform.GetChild(0).GetChild(1).GetComponent<TMP_InputField>().text;
        string password = loginPanel.transform.GetChild(0).GetChild(2).GetComponent<TMP_InputField>().text;
        StartCoroutine(getAllGamesPlayerData(email, password));
    }

    //saves functions
    private void updateSaveSlotsData()
    {
        foreach(PlayerData data in gameManager.gamesData)
        {
            switch (data.character)
            {
                case 0:
                    saveSlotsPanel.transform.GetChild(0).GetChild(data.saveSlot).GetChild(0).GetComponent<TMP_Text>().text = "Empty";
                    saveSlotsPanel.transform.GetChild(0).GetChild(data.saveSlot).GetChild(1).gameObject.SetActive(false);
                    break;
                case 1:
                    saveSlotsPanel.transform.GetChild(0).GetChild(data.saveSlot).GetChild(0).GetComponent<TMP_Text>().text = "Saved Game " + data.saveSlot;
                    saveSlotsPanel.transform.GetChild(0).GetChild(data.saveSlot).GetChild(1).GetComponent<Image>().sprite = character1Face;
                    saveSlotsPanel.transform.GetChild(0).GetChild(data.saveSlot).GetChild(1).gameObject.SetActive(true);
                    break;
                case 2:
                    saveSlotsPanel.transform.GetChild(0).GetChild(data.saveSlot).GetChild(0).GetComponent<TMP_Text>().text = "Saved Game " + data.saveSlot;
                    saveSlotsPanel.transform.GetChild(0).GetChild(data.saveSlot).GetChild(1).GetComponent<Image>().sprite = character2Face;
                    saveSlotsPanel.transform.GetChild(0).GetChild(data.saveSlot).GetChild(1).gameObject.SetActive(true);
                    break;
            }
        }
    }

    public void loadSlot1()
    {
        slotSelected = 0;
        if (gameManager.gamesData[slotSelected].character > 0)
        {
            deleteButton.SetActive(true);
            loadButton.SetActive(true);
        }
        else
        {
            saveSlotsPanel.SetActive(false);
            characterSelectionPanel.SetActive(true);
        }
    }

    public void loadSlot2()
    {
        slotSelected = 1;
        if (gameManager.gamesData[slotSelected].character > 0)
        {
            deleteButton.SetActive(true);
            loadButton.SetActive(true);
        }
        else
        {
            saveSlotsPanel.SetActive(false);
            characterSelectionPanel.SetActive(true);
        }
    }

    public void loadSlot3()
    {
        slotSelected = 2;
        if (gameManager.gamesData[slotSelected].character > 0)
        {
            deleteButton.SetActive(true);
            loadButton.SetActive(true);
        }
        else
        {
            saveSlotsPanel.SetActive(false);
            characterSelectionPanel.SetActive(true);
        }
    }

    public void deleteSelectedSlot()
    {
        PlayerData playerData = gameManager.gamesData[slotSelected];
        StartCoroutine(deleteGameData(playerData));
    }

    public void loadSelectedSlot()
    {
        gameManager.spawnPlayer(slotSelected);
        saveSlotsPanel.SetActive(false);
        deleteButton.SetActive(false);
        loadButton.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    //character selection functions
    public void selectCharacter1()
    {
        gameManager.characterSelected(slotSelected, 1);
        characterSelectionPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void selectCharacter2()
    {
        gameManager.characterSelected(slotSelected, 2);
        characterSelectionPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void returnSaveSlotsButton()
    {
        saveSlotsPanel.SetActive(true);
        characterSelectionPanel.SetActive(false);
    }

    //main menu functions
    public void playButton()
    {
        mainMenuPanel.SetActive(false);
        inGameHUD.SetActive(true);

        if(gameManager.currentGame.damageLevel == 0 && gameManager.currentGame.healthLevel == 0)
        {
            StartCoroutine(instructionsScreen());
        }
        else
        {
            audioManager.startGame();
            gameManager.startGame();
            time = 0;
        }
    }

    public void quitButton()
    {
        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }
        gameManager.currentGame.damageLevel = player.getDamageLevel();
        gameManager.currentGame.healthLevel = player.getHealthLevel();
        StartCoroutine(saveGameDataAndQuitGame(gameManager.currentGame));        
    }

    //ingame functions
    private void updateTimer()
    {
        time += Time.deltaTime;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if(time >= 300 && !gameManager.isFirstBossSpawn())
        {
            gameManager.spawnFinalBoss();
        }
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
                x1Combo.SetActive(false);
                x2Combo.SetActive(false);
                x3Combo.SetActive(false);
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

    public void pauseGame()
    {
        gameManager.setGamePause(true);
        audioManager.pauseMusic();
        pausePanel.SetActive(true);
    }

    public void resumeGame()
    {
        pausePanel.SetActive(false);
        gameManager.setGamePause(false);
        gameManager.restartSpawningEnemies();
        audioManager.resumeMusic();
    }

    public void restartButton()
    {
        pausePanel.SetActive(false);
        time = 0;
        audioManager.startGame();
        gameManager.restartGame();
    }

    public void mainMenuButton()
    {
        mainMenuPanel.SetActive(true);
        pausePanel.SetActive(false);
        inGameHUD.SetActive(false);

        gameManager.returnMainMenu();
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

    //endgame
    public void wonGame()
    {
        gameManager.setGamePause(true);
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        gameManager.currentGame.damageLevel = player.getDamageLevel();
        gameManager.currentGame.healthLevel = player.getHealthLevel();
        audioManager.stopMusic();
        StartCoroutine(saveGameDataFinishedGame(gameManager.currentGame, victoryPanel));
    }
    public void lostGame()
    {
        gameManager.setGamePause(true);
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        gameManager.currentGame.damageLevel = player.getDamageLevel();
        gameManager.currentGame.healthLevel = player.getHealthLevel();
        audioManager.stopMusic();
        StartCoroutine(saveGameDataFinishedGame(gameManager.currentGame, defeatPanel));
    }

    //victory panel
    public void victoryPlayAgain()
    {
        player.revivePlayer();
        gameManager.returnMainMenu();
        victoryPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        time = 0;
    }

    //defeat panel
    public void defeatPlayAgain()
    {
        player.revivePlayer();
        gameManager.returnMainMenu();
        defeatPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        time = 0;
    }

    //instructions courutines
    IEnumerator instructionsScreen()
    {
        gameManager.setGamePause(false);
        inGameHUD.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        inGameHUD.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "2";
        yield return new WaitForSeconds(1);
        inGameHUD.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "1";
        yield return new WaitForSeconds(1);
        inGameHUD.transform.GetChild(0).gameObject.SetActive(false);
        audioManager.startGame();
        gameManager.startGame();
        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }
        player.resetCounter();
        time = 0;
    }

    //sql courutines
    IEnumerator getAllGamesPlayerData(string email, string password)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://localhost:7266/api/PlayerData/GetAllGamesPlayerData/" + email))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.Log("error conexion");
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.Log("data error");
                    break;
                case UnityWebRequest.Result.Success:
                    ServerResponse response = JsonConvert.DeserializeObject<ServerResponse>(webRequest.downloadHandler.text);

                    if (response.statusCode == 200)
                    {
                        if (response.gamesData[0].password == password)
                        {
                            gameManager.gamesData = response.gamesData;
                            loginPanel.SetActive(false);
                            saveSlotsPanel.SetActive(true);
                            updateSaveSlotsData();
                        }
                        else
                        {
                            loginPanel.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Wrong Password!";
                        }
                    }
                    else
                    {
                        loginPanel.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = response.statusMessage;
                    }
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.Log("error protocolo");
                    break;
            }
        }
    }

    IEnumerator signUpPlayer(PlayerData playerData)
    {
        string content = JsonConvert.SerializeObject(playerData);
        var contentBytes = Encoding.UTF8.GetBytes(content);
        
        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm("https://localhost:7266/api/PlayerData/SignUpPlayer", "POST"))
        {
            webRequest.uploadHandler = new UploadHandlerRaw(contentBytes);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Accept", " text/plain");
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.Log("error conexion" + webRequest.error);
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.Log("data error " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    ServerResponse response = JsonConvert.DeserializeObject<ServerResponse>(webRequest.downloadHandler.text);
                    signUpPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = response.statusMessage.ToString();
                    yield return new WaitForSeconds(1);
                    signUpPanel.SetActive(false);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.Log("error protocolo " + webRequest.error);
                    break;
            }
        }
    }

    IEnumerator deleteGameData(PlayerData playerData)
    {
        string content = JsonConvert.SerializeObject(playerData);
        var contentBytes = Encoding.UTF8.GetBytes(content);

        using (UnityWebRequest webRequest = UnityWebRequest.Delete("https://localhost:7266/api/PlayerData/DeleteGameData"))
        {
            webRequest.uploadHandler = new UploadHandlerRaw(contentBytes);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Accept", " text/plain");
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.Log("error conexion" + webRequest.error);
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.Log("data error " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    ServerResponse response = JsonConvert.DeserializeObject<ServerResponse>(webRequest.downloadHandler.text);
                    gameManager.gamesData[slotSelected] = response.dataChanged;
                    updateSaveSlotsData();
                    slotSelected = -1;
                    deleteButton.gameObject.SetActive(false);
                    loadButton.gameObject.SetActive(false);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.Log("error protocolo " + webRequest.error);
                    break;
            }
        }
    }

    IEnumerator saveGameDataAndQuitGame(PlayerData playerData)
    {
        string content = JsonConvert.SerializeObject(playerData);
        var contentBytes = Encoding.UTF8.GetBytes(content);

        using (UnityWebRequest webRequest = UnityWebRequest.Put("https://localhost:7266/api/PlayerData/SaveGameData", "PUT"))
        {
            webRequest.uploadHandler = new UploadHandlerRaw(contentBytes);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Accept", " text/plain");
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.Log("error conexion" + webRequest.error);
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.Log("data error " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Application.Quit();
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.Log("error protocolo " + webRequest.error);
                    break;
            }
        }
    }

    IEnumerator saveGameDataFinishedGame(PlayerData playerData, GameObject panel)
    {
        string content = JsonConvert.SerializeObject(playerData);
        var contentBytes = Encoding.UTF8.GetBytes(content);

        using (UnityWebRequest webRequest = UnityWebRequest.Put("https://localhost:7266/api/PlayerData/SaveGameData", "PUT"))
        {
            webRequest.uploadHandler = new UploadHandlerRaw(contentBytes);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Accept", " text/plain");
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.Log("error conexion" + webRequest.error);
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.Log("data error " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    inGameHUD.SetActive(false);
                    panel.SetActive(true);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.Log("error protocolo " + webRequest.error);
                    break;
            }
        }
    }
}

public class PlayerData
{
    public int id = 0;
    public string nickName;
    public string email;
    public string password;
    public int saveSlot = 0;
    public int character = 0;
    public int damageLevel = 0;
    public int healthLevel = 0;
}
