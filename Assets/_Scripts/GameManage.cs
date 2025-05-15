using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManage : MonoBehaviour
{
   public virtual void gameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
