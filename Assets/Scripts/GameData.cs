using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/GameData")]
public class GameData : ScriptableObject
{
    public bool soundOn;
    public int catSkinIndex;
    public int lastUnlockedLevel;
    public int currentLevel;
    public int[] levelStars = new int[15];
}
