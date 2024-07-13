using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite soundOn, soundOff;
    public Image soundIcon;
    public GameObject skinPanel;
    public GameData data;
    public Sprite[] skins;
    public Image[] cats;
    public int[] currentSkin = new int[2];

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
        currentSkin[0] = data.leftCatCurrentSkin;
        currentSkin[1] = data.rightCatCurrentSkin;
        cats[0].sprite = skins[currentSkin[0]];
        cats[1].sprite = skins[currentSkin[1]];
        skinPanel.SetActive(true);
    }

    public void CloseSkinPanelClicked()
    {
        data.leftCatCurrentSkin = currentSkin[0];
        data.rightCatCurrentSkin = currentSkin[1];
        skinPanel.SetActive(false);
    }

    public void InitSound()
    {
        if (data.soundOn)
        {
            if (PersistentAudioPlayer.instance)
                PersistentAudioPlayer.instance.GetComponent<AudioSource>().Play(0);
            soundIcon.sprite = soundOn;
        }
            
        else
        {
            PersistentAudioPlayer.instance.GetComponent<AudioSource>().Stop();
            soundIcon.sprite = soundOff;
        }
            
    }

    public void ChangeCatSkinLeft(int direction)
    {
        ChangeCatSkin(0, direction);
    }

    public void ChangeCatSkinRight(int direction)
    {
        ChangeCatSkin(1, direction);
    }

    public void ChangeCatSkin(int cat, int direction)
    {
        currentSkin[cat] = (currentSkin[cat] + direction + skins.Length) % skins.Length;
        cats[cat].sprite = skins[currentSkin[cat]];
    }
}
