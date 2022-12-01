using UnityEngine;

[CreateAssetMenu (fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public int levelIndex;
    public string levelName;
    public string levelDescription;
    public Sprite levelImage;
    public TextAsset inkJsonFile;
    public GameObject playerHand;
    public GameObject enemyHand;
}
