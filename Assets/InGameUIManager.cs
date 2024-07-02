using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    GameManager gameManager;

    public GameObject pauseMenu, winMenu, loseMenu;

    public Image pausePlayIcon;

    public Sprite pause, play;

    public GameObject pausePlayBtn;


    private void Update()
    {
        if (gameManager == null)
            gameManager = GameManager.instance;
    }

    public void PausePlayClicked()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            pausePlayIcon.sprite = pause;
            gameManager.ResumeGame();
        } else
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

    public void HomeClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void NextClicked()
    {
        gameManager.NextLevel();
    }

    public void ShowLoseMenu()
    {
        loseMenu.SetActive(true);
    }

    public void ShowWinMenu()
    {
        winMenu.SetActive(true);
    }
}
