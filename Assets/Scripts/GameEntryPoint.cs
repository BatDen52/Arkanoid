using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        TimeManager.Run();
    }

    public void GameStop()
    {
        TimeManager.Pause();
        gameOverUI.SetActive(true);
    }
}
