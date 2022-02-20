using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int score;
    public int score2;
    public int scoreSpecialItem;
    public int scoreSpecialItem2;
    public int id;
    public int speak;
    public bool isPaused;
    public bool gameOverActive;
    public Text scoreText;
    public Text timer;
    public string language;

    private int SpawnNumber;
    private int Gender;
    private int clothes;

    private int characterId;

    private int totalScore;
    private int totalTime;
 
    static public float time = 0;

    [Header("Panels")]
    public GameObject gameOver;
    public GameObject pausePanel;
    public GameObject PausePanelAsk;


    public static GameController instance;
    public static GameController Instance { get { return instance; } }

    void Start()
    {
        instance = this;
        time = 0;

        //Carrega cena atual da fase.
        Scene scene = SceneManager.GetActiveScene();
        // Validações para cenas iniciais. 
        if (scene.name == "lvl_1" || scene.name == "lvl_11" || scene.name == "lvl_21" || scene.name == "HowToPlay" )
        {
            //Passa PlayerPrefs para variáveis para serem atribuídos novamente.
            id = PlayerPrefs.GetInt("UserId");
            language = PlayerPrefs.GetString("language");
            speak = PlayerPrefs.GetInt("FalarDnv");
            SpawnNumber = PlayerPrefs.GetInt("SelectedSkin");
            Gender = PlayerPrefs.GetInt("Gender");
            characterId = PlayerPrefs.GetInt("CharacterId");
            clothes = PlayerPrefs.GetInt("Clothes");

            //Deleta todos os PlayerPrefs
            PlayerPrefs.DeleteAll();

            PlayerPrefs.SetInt("FalarDnv", speak);
            PlayerPrefs.SetInt("UserId", id);
            PlayerPrefs.SetString("language", language);
            PlayerPrefs.SetInt("SelectedSkin", SpawnNumber);
            PlayerPrefs.SetInt("Gender", Gender);
            PlayerPrefs.SetInt("CharacterId", characterId);
            PlayerPrefs.SetInt("Clothes", clothes);
        }

        Debug.Log(PlayerPrefs.GetInt("scoreSpecialItem"));
        score2 = PlayerPrefs.GetInt("score");
        isPaused = false;
        totalScore = PlayerPrefs.GetInt("score");
        scoreText.text = totalScore.ToString();
        time = PlayerPrefs.GetFloat("time");
        scoreSpecialItem = PlayerPrefs.GetInt("scoreSpecialItem");
        scoreSpecialItem2 = PlayerPrefs.GetInt("scoreSpecialItem");
        PlayerPrefs.SetString("Paused", "False");
    }


    private void Update()
    {
        time += Time.deltaTime;
        UpdateTimer((int)Mathf.Round(time));

        // Caso aperte "P", chama o método de pause do jogo 
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScreen();
        }
    }

    public void ReturnToMap()
    {
        PausePanelAsk.SetActive(true);
    }

    public void YesPausePanel()
    {
        SceneManager.LoadScene("Map");
    }

    public void NoPausePanel()
    {
        PausePanelAsk.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        pausePanel.SetActive(false);
        PlayerPrefs.SetString("Paused", "False");
    }

    void PauseScreen()
    {
        if (!isPaused && gameOverActive != true)
        {
            isPaused = true;
            pausePanel.SetActive(true);
            PlayerPrefs.SetString("Paused", "True");
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
            pausePanel.SetActive(false);
            PlayerPrefs.SetString("Paused", "False");
        }
    }

    // Atualiza e Mostra quantidade de CommomCollectibles
    public void UpdateScoreText()
    {
        score++;
        totalScore++;
        scoreText.text = totalScore.ToString();
        PlayerPrefs.SetInt("score", totalScore);
    }

    // Atualiza quantidade de SpecialCollectibles
    public void UpdateScoreSpecialItem()
    {
        scoreSpecialItem++;
        PlayerPrefs.SetInt("scoreSpecialItem", scoreSpecialItem);
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
        gameOverActive = true;
        Time.timeScale = 0;
        PlayerPrefs.SetInt("score", score2);
        PlayerPrefs.SetInt("scoreSpecialItem", scoreSpecialItem2);
    }

    // Atualiza o tempo passado no jogo
    public void UpdateTimer(int segundos)
    {
        totalTime += segundos;
        PlayerPrefs.SetFloat("time", time);
        int tempoInt = (int)Mathf.Round(time);
        timer.text = tempoInt.ToString();

    }


    //Da restart na fase
    public void RestartGame()
    {        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

}