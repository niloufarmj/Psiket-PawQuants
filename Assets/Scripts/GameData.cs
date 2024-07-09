using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/GameData")]
public class GameData : ScriptableObject
{
    public bool soundOn;
    public int catSkinIndex;
    public int lastUnlockedLevel;
    public int currentLevel;
    public int[] levelStars = new int[15];
    public string[] levelHelps = new string[15];
    public int leftCatCurrentSkin, rightCatCurrentSkin;
}
