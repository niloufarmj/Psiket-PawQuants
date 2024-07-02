using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite soundOn, soundOff;
    public Image soundIcon;
    public GameObject skinPanel;
    public GameData data;

    private void Start()
    {
        InitSound();
    }

    public void PlayClicked()
    {
        // load levels scene
        SceneManager.LoadScene(1);
    }

    public void SoundClicked()
    {
        data.soundOn = !data.soundOn;
        InitSound();
    }

    public void CatSkinClicked()
    {
        skinPanel.SetActive(true);
    }

    public void InitSound()
    {
        if (data.soundOn)
            soundIcon.sprite = soundOn;
        else
            soundIcon.sprite = soundOff;
    }
}
