using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    GameManager gameManager;

    public GameObject pauseMenu, winMenu, loseMenu, help, mobileInput;

    public Image pausePlayIcon;
    public Image[] starImages;

    public Sprite pause, play, fullStar, emptyStar;

    public GameObject pausePlayBtn;

    public TextMeshProUGUI minutes, seconds, currentState, helpMessage;

    public AudioSource catMeow;

    public Button helpBtn, pauseBtn;

    public CanvasScaler canvasScaler;
    private float timer = 0f;

    private void Start()
    {
        // Initialize the timer
        timer = 0f;
        gameManager = GameManager.instance;
    }

    private void Update()
    {
        if (gameManager == null)
            gameManager = GameManager.instance;

        // Update the timer if the game is not paused
        if (!gameManager.isGamePaused)
        {
            UpdateTimer();
        }

        currentState.text = gameManager.GenerateCurrentStateString();
    }

    public void SetCanvas()
    {
        canvasScaler.matchWidthOrHeight = gameManager.isMobile ? 0.3f : 0;
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutesValue = Mathf.FloorToInt(timer / 60);
        int secondsValue = Mathf.FloorToInt(timer % 60);

        minutes.text = minutesValue.ToString("00");
        seconds.text = secondsValue.ToString("00");
    }

    public void PausePlayClicked()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            pausePlayIcon.sprite = pause;
            gameManager.ResumeGame();
        }
        else
        {
            pauseMenu.SetActive(true);
            pausePlayIcon.sprite = play;
            gameManager.PauseGame();
        }
    }

    public void ResetClicked()
    {
        gameManager.ResetGame();
    }

    public void ShowMobileInput()
    {
        mobileInput.SetActive(true);
    }

    public void HomeClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowHelp()
    {
        help.SetActive(true);

        if (!gameManager)
            gameManager = GameManager.instance;

        gameManager.PauseGame();

        int currentLevelHelpIndex = gameManager.data.currentLevel - 1;

        while (gameManager.data.levelHelps[currentLevelHelpIndex] == "") currentLevelHelpIndex--;

        helpMessage.text = gameManager.data.levelHelps[currentLevelHelpIndex];
    }

    public void CloseHelp()
    {
        help.SetActive(false);
        gameManager.ResumeGame();
    }

    public void NextClicked()
    {
        gameManager.NextLevel();
    }

    public void ShowLoseMenu()
    {
        gameManager.PauseGame();
        pauseBtn.gameObject.SetActive(false);
        helpBtn.gameObject.SetActive(false);
        
        loseMenu.SetActive(true);
    }

    public void ShowWinMenu()
    {
        gameManager.PauseGame();
        pauseBtn.gameObject.SetActive(false);
        helpBtn.gameObject.SetActive(false);

        winMenu.SetActive(true);

        catMeow.Play();

        int stars = 1;

        if (int.Parse(seconds.text) < 15)
            stars = 3;
        else if (int.Parse(seconds.text) < 30)
            stars = 2;

        for (int i = 0; i < stars; i++)
        {
            starImages[i].sprite = fullStar;
        }
        for (int i = stars; i < 3; i++)
        {
            starImages[i].sprite = emptyStar;
        }

        gameManager.data.levelStars[gameManager.data.currentLevel - 1] = stars;
    }
}