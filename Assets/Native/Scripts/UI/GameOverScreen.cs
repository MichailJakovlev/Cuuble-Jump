using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _recordText;
    [SerializeField] private TextMeshProUGUI _newRecordText;
    [SerializeField] private GameObject _defeatScreen;
    [SerializeField] private GameObject _newRecord;
    [SerializeField] private AudioState _audioState;
    [SerializeField] private Leaderboard _leaderboard;

    [SerializeField] private CoinCounter _coinCounter;
    [SerializeField] private TextMeshProUGUI _textCoins;
    [SerializeField] private GameObject _doubleCoinsButton;

    [SerializeField] private GameObject _continueButton;

    private bool _isFirstTry = true;

    public void SetFirstTryFalse() => _isFirstTry = false;

    public void Show()
    {
        _coinCounter = GameObject.Find("Canvas").GetComponent<CoinCounter>();
        _textCoins.text = $"+{_coinCounter.coins}";
        double percent = (double)_scoreCounter.score / PlayerPrefs.GetInt("Record", 0) * 100;


        if (_scoreCounter.score == 0)
        {
            _continueButton.SetActive(false);
            _doubleCoinsButton.SetActive(false);
        }
        else if (percent >= 75 && _isFirstTry == true)
        {
            _continueButton.SetActive(true);
            _doubleCoinsButton.SetActive(false);
        }
        else
        {
            _continueButton.SetActive(false);
            _doubleCoinsButton.SetActive(true);
            if (_coinCounter.coins <= 0)
            {
                _doubleCoinsButton.SetActive(false);
            }
        }

        if (_scoreCounter.score > PlayerPrefs.GetInt("Record", 0))
        {
            _audioState.PlayNewRecordSound();
            _newRecord.SetActive(true);
            _defeatScreen.SetActive(false);
            _newRecordText.text = $"{_scoreCounter.score}";
            PlayerPrefs.SetInt("Record", _scoreCounter.score);
            PlayerPrefs.Save();
            _leaderboard.SetPlayerScore(_scoreCounter.score);
        }
        else
        {
            _newRecord.SetActive(false);
            _defeatScreen.SetActive(true);
            _scoreText.text += $": {_scoreCounter.score}";
            _recordText.text += $": {PlayerPrefs.GetInt("Record", 0)}";
        }
    }

    public void DoubleCoins()
    {
        _coinCounter.PlayerPrefsCoinsSet(_coinCounter.PlayerPrefsCoinsGet() + _coinCounter.coins);
        _coinCounter.coins *= 2;
        _textCoins.text = $"+{_coinCounter.coins}";
        _doubleCoinsButton.SetActive(false);
    }
}
