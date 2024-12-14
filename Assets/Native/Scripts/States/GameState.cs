using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public YandexManager _yandexManager;
    public AudioState _audioState;
    [HideInInspector] public bool _isNotShowingAd = true;

    // Game States managment
    public void StopGame()
    {
        _yandexManager.StopGame();
        _audioState.StopSounds();
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        _isNotShowingAd = true;
        _yandexManager.StartGame();
        _audioState.StartSounds();
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
        if (_isNotShowingAd)
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
}
