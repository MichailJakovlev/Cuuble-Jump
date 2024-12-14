using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YandexManager : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern void SendGameReady();

    [DllImport("__Internal")]
    public static extern void SendGameStart();

    [DllImport("__Internal")]
    public static extern void SendGameStop();

    [DllImport("__Internal")]
    public static extern void CallRateGame();

    [DllImport("__Internal")]
    public static extern void ShowAd();

    [DllImport("__Internal")]
    public static extern void ShowReward(int num);

    [DllImport("__Internal")]
    public static extern void GetPlayerAuthData();

    [DllImport("__Internal")]
    public static extern void AuthingPlayer();

    [SerializeField] private GameState _gameState;
    [SerializeField] private PlayerRevival _playerRevival;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private Authorization _authorization;
    [SerializeField] private Leaderboard _leaderboard;
    [SerializeField] private CharacterSkinDB _skinDb;
    private CharacterSkinManager _characterSkinManager;
    private ThemeSkinManager _themeSkinManager;

    private bool _isGameReady = false;

    public void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            ShowFullscreenAd();

            GetPlayerAuthData();

            _characterSkinManager = GameObject.Find("Current Skin").GetComponent<CharacterSkinManager>();
            _themeSkinManager = GameObject.Find("Current Theme").GetComponent<ThemeSkinManager>();

            if (_isGameReady == false)
            {
                ReadyGame();
                _isGameReady = true;
            }
        }
    }

    // Ready Game API managment
    public void ReadyGame()
    {
        SendGameReady();
    }

    public void StartGame()
    {
        SendGameStart();
    }

    public void StopGame()
    {
        SendGameStop();
    }

    // Rate game managment
    public void RateGame()
    {
        _gameState.StopGame();
        _gameState._isNotShowingAd = false;
        CallRateGame();
    }

    public void GetRatedAward()
    {
        _gameState._isNotShowingAd = true;
        _gameState.StartGame();
        _characterSkinManager.UnlockSkinReview();
    }

    // Ad managment
    public void ShowFullscreenAd()
    {
        _gameState.StopGame();
        _gameState._isNotShowingAd = false;
        ShowAd();
    }

    public void AdClosed()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex == 0 ? 1 : 1);
            _gameState._isNotShowingAd = true;
            _gameState.StartGame();
        }
        else
        {
            _gameState._isNotShowingAd = true;
            _gameState.StartGame();
        }
    }

    public void ShowRewardAd(int num)
    {
        _gameState.StopGame();
        _gameState._isNotShowingAd = false;
        ShowReward(num);
    }

    public void Auth()
    {
        _authorization.AuthorizationClick();
        _leaderboard._isAuthtorization = true;
        _leaderboard.SetPlayerScore(PlayerPrefs.GetInt("Record"));
        _leaderboard.AuthGetScore();
    }

    public void ReviewToPrice()
    {
        _gameState.StartGame();
        _skinDb.skins[9].isReview = false;
    }

    public void Rewarded(int num)
    {
        switch (num)
        {
            case 1:
                _characterSkinManager.UnlockSkinAd();
                break;
            case 2:
                _themeSkinManager.UnlockSkinAd();
                break;
            case 3:
                _gameOverScreen.DoubleCoins();
                break;
            case 4:
                _playerRevival.Revive();
                break;
        }
    }
}
