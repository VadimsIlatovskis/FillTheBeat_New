using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRestartButton : MonoBehaviour
{
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
