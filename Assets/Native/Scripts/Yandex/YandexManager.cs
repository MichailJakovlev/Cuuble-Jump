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
    public static extern void ShowReward();

    [SerializeField] TextMeshProUGUI _languageText;
    [SerializeField] private GameState _gameState;
    [SerializeField] private CharacterSkinManager _characterSkinManager;
   // [SerializeField] private ThemeSkinManager _themeSkinManager;

    public string _currentLanguage;
    bool _isRewarded = false;

    public void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            ShowFullscreenAd();
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

  //  private void Awake()
   // {
       // if (_instance == null)
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
   // }

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

    public void ShowRewardAd(int number)
    {
        _gameState.StopGame();
        ShowReward();
        GetAdAward(number);
    }

    public void Rewarded()
    {
        _gameState.StartGame();
        _characterSkinManager.UnlockSkinAd();
        _isRewarded = true;
    }

    public void GetAdAward(int number)
    {
        if(_isRewarded)
        {
            switch(number)
            {
                case 0:
                    // Character
                    break;
                case 1:
                  //  _themeSkinManager.UnlockSkinAd();
                    break;
                case 2:
                    // Add double coins method;
                    break;
                case 3:
                    //Add continue game method;
                    break;
            }
        }
        _isRewarded = false;
    }
}
