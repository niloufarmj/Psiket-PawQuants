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
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (leftWin && rightWin)
            Win();
    }

    public void PlayerWin(bool isLeft)
    {
        if (isLeft) leftWin = true;
        else rightWin = true;
    }

    public void Win()
    {
        if (data.currentLevel == data.lastUnlockedLevel)
        {
            if (data.lastUnlockedLevel < 15)
                data.lastUnlockedLevel++;
            data.levelStars[data.currentLevel - 1] = 3;
        }

        SceneManager.LoadScene("Levels");

        Debug.Log("Ha Ha You Win! :>");
    }

    public void Lose()
    {
        Debug.Log("Sorry U Lost! :<");
    }

    public void StopWalking()
    {
        first.animator.SetBool("Walking", false);
        second.animator.SetBool("Walking", false);
        isGamePaused = true;
    }

}
