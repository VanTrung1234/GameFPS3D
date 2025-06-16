using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreUI;
    string newGameScene = "SampleScene";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int highScore = SaveLoadManager.instance.LoadScore();
        highScoreUI.text = $"Top Wave Survied : {highScore}";
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
 
        SceneManager.LoadScene(newGameScene);
    }
    public void InventoryGame()
    {
        SceneManager.LoadScene("Inventory");
    }
    public void Exti()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
    Application.Quit();
#endif
    }
}
