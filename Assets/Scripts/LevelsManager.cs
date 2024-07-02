
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
    public GameData data;

    public LevelBtn[] levelBtns;

    public Sprite enabledBtn, disabledBtn, fullStar, emptyStar;

    private void Start()
    {
        for (int i = 0; i < levelBtns.Length; i++)
        {
            int level = i + 1; // Use a local variable to capture the current level
            levelBtns[i].btn.onClick.AddListener(() =>
                {
                    data.currentLevel = level;
                    SceneManager.LoadScene("level " + level);
                }
            );
        }

        for (int i = 0; i < data.lastUnlockedLevel; i++)
        {
            levelBtns[i].btn.interactable = true;
            levelBtns[i].img.sprite = enabledBtn;

            for (int j = 0; j < data.levelStars[i]; j++)
            {
                levelBtns[i].stars[j].gameObject.SetActive(true);
                levelBtns[i].stars[j].sprite = fullStar;
            }

            for (int j = data.levelStars[i]; j < 3; j++)
            {
                levelBtns[i].stars[j].gameObject.SetActive(true);
                levelBtns[i].stars[j].sprite = emptyStar;
            }
        }

        

        for (int i = data.lastUnlockedLevel; i < levelBtns.Length; i++)
        {
            levelBtns[i].btn.interactable = false;
            levelBtns[i].img.sprite = disabledBtn;
        }
    }

    public void Return()
    {
        SceneManager.LoadScene(0);
    }
}

[System.Serializable]
public struct LevelBtn
{
    public Button btn;
    public Image img;
    public Image[] stars;
}


