using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelWin;
    public static int CurrentLevelIndex = 0;
    public static int noOfPassingRings;
    public GameObject menuFimdejogo;
    public GameObject menuVocêGanhou;
    public TextMeshProUGUI CurrentLevelText;
    public TextMeshProUGUI nextLevelText;
    public Slider ProgressBar;
    public Button restartButton;
    public Button menuButton;
    public Button nextLevelButton;
    private AudioManager audioManager;
    private bool nextLevelTriggered = false;

    private void Awake()
    {
        CurrentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
    }

    private void Start()
    {
        Time.timeScale = 1f;
        noOfPassingRings = 0;
        gameOver = false;
        levelWin = false;
        nextLevelTriggered = false;
        audioManager = FindObjectOfType<AudioManager>();
        restartButton.onClick.AddListener(RestartGame);
        menuButton.onClick.AddListener(GoToMenu);
        nextLevelButton.onClick.AddListener(NextLevel);
        AtualizaUI();
    }

    private void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            menuFimdejogo.SetActive(true);
            if (audioManager != null)
            {
                audioManager.StopBackgroundMusic();
            }
        }
        int totalRings = FindObjectOfType<HelixManager>().noOfRings;
        int progress = (totalRings > 0) ? (noOfPassingRings * 100 / totalRings) : 0;
        ProgressBar.value = progress;
        if (levelWin)
        {
            menuVocêGanhou.SetActive(true);
        }
    }

    private void AtualizaUI()
    {
        CurrentLevelText.text = CurrentLevelIndex.ToString();
        nextLevelText.text = (CurrentLevelIndex + 1).ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        CurrentLevelIndex = 1;
        PlayerPrefs.SetInt("CurrentLevelIndex", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        if (!nextLevelTriggered)
        {
            nextLevelTriggered = true;
            CurrentLevelIndex++;
            PlayerPrefs.SetInt("CurrentLevelIndex", CurrentLevelIndex);
            PlayerPrefs.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
