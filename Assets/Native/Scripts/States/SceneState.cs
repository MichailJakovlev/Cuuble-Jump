using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneState : MonoBehaviour
{
    public GameState _gameState;

    public void ToGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex == 0 ? 1 : 1);
    }

    public void ToMenuScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
