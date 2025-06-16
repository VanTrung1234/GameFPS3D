using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExitGame : MonoBehaviour
{
    private static ExitGame instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Giữ lại giữa các scene
        }
        else
        {
            Destroy(gameObject); // Xoá bản dư nếu có
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); // Thoát ngay lập tức
        }
    }
}
