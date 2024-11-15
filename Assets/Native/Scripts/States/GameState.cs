using UnityEngine;

public class GameState : MonoBehaviour
{
    public YandexManager _yandexManager;
    public AudioState _audioState;

    // Game States managment
    public void StopGame()
    {
        _yandexManager.StopGame();
        _audioState.StopMusic();
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        _yandexManager.StartGame();
        _audioState.StartMusic();
        Time.timeScale = 1;
    }

    // Game Focus managment
    void OnApplicationFocus(bool hasFocus)
    {
        GameFocus(!hasFocus);
    }

    void OnApplicationPause(bool isPaused)
    {
        GameFocus(isPaused);
    }

    private void GameFocus(bool focusState)
    {
        if (focusState)
        {
            StopGame();
        }
        else
        {
            StartGame();
        }
    }
}
