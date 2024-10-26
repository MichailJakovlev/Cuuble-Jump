using UnityEngine;

public class Lose : MonoBehaviour
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] public GameObject _popupScreen;

    public void GameOver()
    {
        //_gameState.StopGame();
        _gameOverMenu.SetActive(true);
        _popupScreen.SetActive(true);
    }
}
