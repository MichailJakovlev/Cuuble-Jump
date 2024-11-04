using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private ScoreSaver _scoreSaver;

    [SerializeField]
    private ScoreCounter _scoreCounter;

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private TextMeshProUGUI _recordText;

    [SerializeField]
    private TextMeshProUGUI _newRecordText;

    [SerializeField]
    private GameObject _defeatScreen;

    [SerializeField]
    private GameObject _newRecord;

    [SerializeField]
    private AudioState _audioState;

    void Start()
    {
        _scoreSaver.Save();

        if (PlayerPrefs.GetInt("Score", 0) > PlayerPrefs.GetInt("Record", 0))
        {
            _audioState.PlayNewRecordSound();
            _newRecord.SetActive(true);
            _defeatScreen.SetActive(false);
            _newRecordText.text = _scoreCounter.score.ToString();
            PlayerPrefs.SetInt("Record", _scoreCounter.score);
            PlayerPrefs.Save();
        }
        else
        {
            _newRecord.SetActive(false);
            _defeatScreen.SetActive(true);
            _scoreText.text = _scoreCounter.score.ToString();
            _recordText.text = PlayerPrefs.GetInt("Record", 0).ToString();
        }
    }
}
