using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGamePaused, leftWin, rightWin;

    public GameData data;

    public PlayerController first, second;

    public InGameUIManager UIManager;

    public bool isMobile;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        if (data.levelStars[data.currentLevel - 1] == 0 && data.levelHelps[data.currentLevel - 1] != "")
        {
            UIManager.ShowHelp();
        }


        if (Application.isMobilePlatform)
        {
            isMobile = true;
            UIManager.ShowMobileInput();
        }
        else isMobile = false;

        UIManager.SetCanvas(isMobile);
    }

    
    public void PlayerWin(bool isLeft)
    {
        if (isLeft) leftWin = true;
        else rightWin = true;

        if (leftWin && rightWin)
            UIManager.ShowWinMenu();
    }

   

    public void NextLevel()
    {
        if (data.currentLevel == data.lastUnlockedLevel)
        {
            if (data.lastUnlockedLevel < 15)
                data.lastUnlockedLevel++;
            else
            {
                SceneManager.LoadScene("Levels");
                return; 
            }
            
        }
        data.currentLevel++;
        SceneManager.LoadScene("Level " + data.currentLevel);
    }

    public void Lose()
    {
        UIManager.ShowLoseMenu();
        Debug.Log("Sorry U Lost! :<");
    }

    public void PauseGame()
    {
        first.animator.SetBool("Walking", false);
        second.animator.SetBool("Walking", false);
        isGamePaused = true;
    }


    public void ResumeGame()
    {
        isGamePaused = false;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Level " + data.currentLevel);
    }

    public string GenerateCurrentStateString()
    {
        string leftState_leftSide = SingleStateGenerator(first.currentGate, 0);
        string rightState_leftSide = SingleStateGenerator(second.currentGate, 0);

        string leftState_rightSide = SingleStateGenerator(first.currentGate, 1);
        string rightState_rightSide = SingleStateGenerator(second.currentGate, 1);

        return "| " + leftState_leftSide + rightState_leftSide + " > + | " + leftState_rightSide + rightState_rightSide + " >" +
            ((leftState_leftSide == "0" && rightState_leftSide == "0") ? " (bell state)" : "");

    }

    private string SingleStateGenerator(Gate current, int side)
    {
        switch (current)
        {
            case Gate.None:
                return side.ToString();
            case Gate.X:
                return side == 1 ? "0" : "1";
            case Gate.Y: //(same as Gate.X)
                return side == 1 ? "0" : "1";
            case Gate.H:
                return side == 1 ? "-" : "+";
            case Gate.XH:
                return side == 1 ? "+" : "-";
            case Gate.YH: // same as Gate.XH
                return side == 1 ? "+" : "-";
        }
        return "";
    }

    

}
