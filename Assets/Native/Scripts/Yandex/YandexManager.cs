using System.Runtime.InteropServices;
using TMPro;
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
    public static extern string GetLang();

    [DllImport("__Internal")]
    public static extern void CallRateGame();

    [DllImport("__Internal")]
    public static extern void ShowAd();

    [DllImport("__Internal")]
    public static extern void ShowReward(int num);

    [SerializeField] TextMeshProUGUI _languageText;
    [SerializeField] private GameState _gameState;
    private CharacterSkinManager _characterSkinManager;
    private ThemeSkinManager _themeSkinManager;

    public string _currentLanguage;
    private bool _isGameReady = false;

    public void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            ShowFullscreenAd();

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

    // Languages managment
    public static YandexManager _instance;

    // private void Awake()
    // {
    //   if (_instance == null)
    // {
    //   _instance = this;
    // DontDestroyOnLoad(gameObject);

    // _currentLanguage = GetLang();
    // _languageText.text = _currentLanguage;
    // }
    // else
    // {
    //   Destroy(gameObject);
    // }
    //}

    // Rate game managment
    public void RateGame()
    {
        CallRateGame();
    }

    public void GetRatedAward()
    {
        _characterSkinManager.UnlockSkinReview();
    }

    // Ad managment
    public void ShowFullscreenAd()
    {
        _gameState.StopGame();
        ShowAd();
    }

    public void ShowRewardAd(int num)
    {
        _gameState.StopGame();
        ShowReward(num);
    }

    public void Rewarded(int num)
    {
        _gameState.StartGame();

        switch (num)
        {
            case 1:
                _characterSkinManager.UnlockSkinAd();
                break;
            case 2:
                _themeSkinManager.UnlockSkinAd();
                break;
            case 3:
                //Double coins method
                break;
            case 4:
                //Rebirth player method
                break;
        }
    }
}
