using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager instance;
    private string hidhScoreKey = "BestWaveSaveVaule";
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }
    public void SaveScore(int score)
    {
        PlayerPrefs.SetInt(hidhScoreKey, score);
    }
    public int LoadScore()
    {
        if (PlayerPrefs.HasKey(hidhScoreKey))
        {
            return PlayerPrefs.GetInt(hidhScoreKey);
        }
        else return 0;
    }
}
